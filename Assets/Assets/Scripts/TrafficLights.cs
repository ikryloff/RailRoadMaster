using System.Collections;
using UnityEngine;

public class TrafficLights : MonoBehaviour {
    [SerializeField]
    Sprite red;
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
    private SpriteRenderer lightColor;
    private int intColor;
    
    const float flashTime = 1f;
    private string lightInRoute;
   

    private void Start()
    {
        lightColor = GetComponent<SpriteRenderer>();
        SetLightColor(IntColor);
    }    



    public string Name
    {
        get
        {
            return trafficLightName;
        }
    }

    public string LightInRoute
    {
        get
        {
            return lightInRoute;
        }

        set
        {
            lightInRoute = value;
        }
    }

    public int IntColor
    {
        get
        {
            return intColor;
        }

        set
        {
            intColor = value;
        }
    }

    private IEnumerator YellowFlashing()
    {
        
        while (IntColor == 5)
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
        lightColor.sprite = red;
    }
    private IEnumerator YellowTopFlashing()
    {
        const float flashTime = 1f;
        while (IntColor == 6)
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
        lightColor.sprite = red;
    }


    public void SetLightColor(int color)
    {
        IntColor = color;
        switch (color)
        {
            case 0:
                lightColor.sprite = red;
                break;
            case 1:
                lightColor.sprite = green;
                break;
            case 2:
                lightColor.sprite = blue;
                break;
            case 3:
                lightColor.sprite = white;                
                break;
            case 4:
                lightColor.sprite = yellow;
                break;
            case 5:
                StartCoroutine("YellowFlashing");            
                break;
            case 6:
                StartCoroutine("YellowTopFlashing");
                break;
            default:
                lightColor.sprite = red;
                break;
        }           
    }
}
