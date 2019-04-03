using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour {
    [SerializeField]
    public Material closed;
    [SerializeField]
    Material green;
    [SerializeField]
    Material blue;
    [SerializeField]
    Material white;
    [SerializeField]
    Material yellow;    
    [SerializeField]
    Material noLight;
    [SerializeField]
    private string trafficLightName;

    //Set objects in Editor
    public GameObject closedSignal;
    public GameObject blueSignal;
    public GameObject whiteSignal;
    public GameObject redSignal;
    public GameObject topYellowSignal;
    public GameObject bottomYellowSignal;
    public GameObject greenSignal;
    public Engine engine;
    public List <GameObject> lights;
    public BGCcMath mathTemp;

    public SpriteRenderer controlLight;
    [SerializeField]
    private Material lightColor;
    [SerializeField]
    private int intColor;
    PathMaker pathMaker;
    public int lightDirection;

    //Colors for remote control
    private Color32 redRC = new Color32(255, 10, 0, 255);
    private Color32 greenRC = new Color32(0, 240, 0, 255);
    private Color32 whiteRC = new Color32(230, 230, 230, 255);
    [SerializeField]
    private bool isClosed;
    public Animator anim;
    public float distance;
    const float flashTime = 1f;
    private string nameRouteOfLight;
    public float maxLength;
   
    
    private void Awake()
    {
        AddSignal(blueSignal);
        AddSignal(whiteSignal);
        AddSignal(redSignal);
        AddSignal(topYellowSignal);
        AddSignal(bottomYellowSignal);
        AddSignal(greenSignal);
        pathMaker = FindObjectOfType<PathMaker>();
        engine = FindObjectOfType<Engine>();

    }
    private void Start()
    {
        SetLightColor(intColor);
        
        Vector3 tangent;
        if (mathTemp)
        {
            transform.position = mathTemp.CalcByDistance(distance, out tangent);

            maxLength = mathTemp.GetDistance();
        }
        

    }

   
    public void AddSignal(GameObject signal)
    {
        if (signal)
        {
            lights.Add(signal);
        }
    }

   
    //Main function of setting color 
    public void SetLightColor(int color)
    {
        if (anim)
        {
            anim.enabled = false;
        }
        intColor = color;
        IsClosed = color == 0 || color == 2 ? true : false;
        if (lights != null)
        {
            foreach (GameObject item in lights)
            {
                item.transform.Find("Ray").gameObject.SetActive(false);
                item.GetComponent<MeshRenderer>().material = noLight;
            }
        }

        GameObject tempSignal = null;
        Material tempMaterial = null;
        GameObject tempSignalAdd = null;
        Material tempMaterialAdd = null;

        switch (color)
        {
            case 0:
                if (closed != null) // just for Ends
                {
                    tempSignal = closedSignal;
                    tempMaterial = closed;
                    controlLight.color = redRC;
                }
                break;
            //green
            case 1:
                tempSignal = greenSignal;
                tempMaterial = green;
                controlLight.color = greenRC;
                break;
            //blue
            case 2:
                tempSignal = blueSignal;
                tempMaterial = blue;
                controlLight.color = redRC;
                break;
            //white
            case 3:
                tempSignal = whiteSignal;
                tempMaterial = white;
                controlLight.color = whiteRC;
                break;
            //yellow
            case 4:
                tempSignal = topYellowSignal;
                tempMaterial = yellow;
                controlLight.color = greenRC;
                break;
            case 5:
                //blinking Yellow;
                tempSignal = topYellowSignal;
                tempMaterial = yellow;
                if (anim)
                {
                    anim.enabled = true;
                    anim.Play("BlinkYellow");
                }
                controlLight.color = greenRC;
                break;

            case 6:
                //TopYellow and bottomYellow;
                tempSignal = topYellowSignal;
                tempMaterial = yellow;
                tempSignalAdd = bottomYellowSignal;
                tempMaterialAdd = yellow;
                controlLight.color = greenRC;
                break;

            case 7:
                //blinking Yellow and bottomYellow;
                tempSignal = topYellowSignal;
                tempMaterial = yellow;
                if (anim)
                {
                    anim.enabled = true;
                    anim.Play("BlinkYellow");
                }
                tempSignalAdd = bottomYellowSignal;
                tempMaterialAdd = yellow;
                controlLight.color = greenRC;
                break;
            default:
                tempSignal = closedSignal;
                tempMaterial = closed;
                controlLight.color = redRC;
                break;
        }
        if (tempSignal != null && tempMaterial != null)
        {
            tempSignal.transform.Find("Ray").gameObject.SetActive(true);
            tempSignal.GetComponent<MeshRenderer>().material = tempMaterial;
        }
        if (tempSignalAdd != null && tempMaterialAdd != null)
        {
            tempSignalAdd.transform.Find("Ray").gameObject.SetActive(true);
            tempSignalAdd.GetComponent<MeshRenderer>().material = tempMaterialAdd;
        }
        
    }      

    public TrafficLights GetTrafficLightByName(string lightName)
    {
        if (lightName == Name)
                return this;        
        return null;
    }

    public string Name
    {
        get
        {
            return trafficLightName;
        }
    }

    public string NameRouteOfLight
    {
        get
        {
            return nameRouteOfLight;
        }

        set
        {
            nameRouteOfLight = value;
        }
    }

    public int GetLightColor
    {
        get
        {
            return intColor;
        }
    }

    public bool IsClosed
    {
        get
        {
            return isClosed;
        }

        set
        {
            isClosed = value;
        }
    }
}
