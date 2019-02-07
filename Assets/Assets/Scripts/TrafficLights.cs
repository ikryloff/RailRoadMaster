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
    Material yellowFlashing;
    [SerializeField]
    Material noLight;
    [SerializeField]
    private string trafficLightName;

    //Set objects in Editor
    public GameObject closedSignal;
    public GameObject blueSignal;
    public GameObject whiteSignal;
    public GameObject redSignal;
    public GameObject yellowSignal;
    public GameObject greenSignal;

    public List <GameObject> lights;
    
    public SpriteRenderer controlLight;
    [SerializeField]
    private Material lightColor;
    [SerializeField]
    private int intColor;

    //Colors for remote control
    private Color32 redRC = new Color32(255, 10, 0, 255);
    private Color32 greenRC = new Color32(0, 240, 0, 255);
    private Color32 whiteRC = new Color32(230, 230, 230, 255);
    [SerializeField]
    private bool isClosed;
   

    const float flashTime = 1f;
    private string nameRouteOfLight;
   
    
    private void Awake()
    {
        if (blueSignal)
        {
            blueSignal = transform.Find("BlueSignal").gameObject;
            lights.Add(blueSignal);
        }


        if (whiteSignal)
        {
            whiteSignal = transform.Find("WhiteSignal").gameObject;
            lights.Add(whiteSignal);
        }
            
        
        
        //SetLightColor(GetLightColor);
        
    }
    private void Start()
    {
        SetLightColor(GetLightColor);
    }

    /*
    private IEnumerator YellowFlashing()
    {
        
        while (GetLightColor == Constants.COLOR_YELLOW_FLASH)
        {            
            float temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
               // lightColor.sprite = yellowFlashing;
                yield return null;
            }
            temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                lightColor.sprite = null;
                yield return null;
            }

        }
        //lightColor.sprite = closedSprite;
    }
    private IEnumerator YellowTopFlashing()
    {
        const float flashTime = 1f;
        while (GetLightColor == Constants.COLOR_YELLOW_TOP_FLASH)
        {
            float temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                //lightColor.sprite = yellowFlashing;
                yield return null;
            }
            temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                //lightColor.sprite = yellow;
                yield return null;
            }
        }
        //lightColor.sprite = closedSprite;
    }
    */

    //Main function of setting color using coroutine for delay
    public void SetLightColor(int color)
    {
        StartCoroutine(TurnOnLightWithDelay(color));
    }    
    
    public IEnumerator TurnOnLightWithDelay(int color)
    {
        yield return new WaitForSeconds(1.1f);

        intColor = color;
        IsClosed = color == 0 || color == 2 ? true : false;
        if (lights != null)
        {
            foreach (GameObject item in lights)
            {
                item.transform.Find("Light").gameObject.SetActive(false);
                item.GetComponent<MeshRenderer>().material = noLight;
            }
        }

        GameObject tempSignal = null;
        Material tempMaterial = null;


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
            case 1:
                //lightColor.sprite = green;
                controlLight.color = greenRC;
                break;
            case 2:
                tempSignal = blueSignal;
                tempMaterial = blue;
                controlLight.color = redRC;
                break;
            case 3:
                tempSignal = whiteSignal;
                tempMaterial = white;
                controlLight.color = whiteRC;
                break;
            case 4:
                // lightColor.sprite = yellow;
                controlLight.color = greenRC;
                break;
            case 5:
                StartCoroutine("YellowFlashing");
                controlLight.color = greenRC;
                break;
            case 6:
                StartCoroutine("YellowTopFlashing");
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
            tempSignal.transform.Find("Light").gameObject.SetActive(true);
            tempSignal.GetComponent<MeshRenderer>().material = tempMaterial;
        }
        print(this.Name + "   " + color);
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
