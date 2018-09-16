using System.Collections;
using UnityEngine;

public class TrafficLights : MonoBehaviour {
    [SerializeField]
    Sprite closed;
    [SerializeField]
    Sprite green;
    [SerializeField]
    Sprite blue;
    [SerializeField]
    Sprite white;
    [SerializeField]
    Sprite yellow;
    [SerializeField]
    Sprite yellowFlashing;
    [SerializeField]
    private string trafficLightName;
    [SerializeField]
    SpriteRenderer controlLight;

    private SpriteRenderer lightColor;
    private int intColor;
    private Color32 redRC = new Color32(255, 10, 0, 255);
    private Color32 greenRC = new Color32(0, 240, 0, 255);
    private Color32 whiteRC = new Color32(230, 230, 230, 255);
    [SerializeField]
    private bool isClosed;
   

    const float flashTime = 1f;
    private string nameRouteOfLight;
   

    private void Awake()
    {
        lightColor = GetComponent<SpriteRenderer>();
        SetLightColor(GetLightColor);         
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

    private IEnumerator YellowFlashing()
    {
        
        while (GetLightColor == Constants.COLOR_YELLOW_FLASH)
        {            
            float temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                lightColor.sprite = yellowFlashing;
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
        lightColor.sprite = closed;
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
                lightColor.sprite = yellowFlashing;
                yield return null;
            }
            temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                lightColor.sprite = yellow;
                yield return null;
            }
        }
        lightColor.sprite = closed;
    }


    public void SetLightColor(int color)
    {
        intColor = color;
        IsClosed = color == 0 || color == 2 ? true : false;
        switch (color)
        {
            case 0:
                if(closed != null) // just for Ends
                {
                    lightColor.sprite = closed;
                    controlLight.color = redRC;
                }                    
                break;
            case 1:
                lightColor.sprite = green;
                controlLight.color = greenRC;
                break;
            case 2:
                lightColor.sprite = blue;
                controlLight.color = redRC;
                break;
            case 3:
                lightColor.sprite = white;
                controlLight.color = whiteRC;
                break;
            case 4:
                lightColor.sprite = yellow;
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
                lightColor.sprite = closed;
                controlLight.color = redRC;
                break;
        }           
    }

    public TrafficLights GetTrafficLightByName(string lightName)
    {
        if (lightName == Name)
                return this;        
        return null;
    }
}
