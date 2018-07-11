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
    private SpriteRenderer light;    
    public int color;
    TrafficLights tl;
    const float flashTime = 1f;
   

    private void Start()
    {
        light = GetComponent<SpriteRenderer>();
        tl = gameObject.GetComponent<TrafficLights>();        

    }

    private void Update()
    {        
        tl.SetLightColor(color);
    }
    public TrafficLights trafficLight
    {
        get
        {
            return trafficLight;
        }
    }

    public string Name
    {
        get
        {
            return trafficLightName;
        }
    }

    private IEnumerator YellowFlashing()
    {
        
        while (color == 5)
        {            
            float temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                light.sprite = yellowFlashing;
                yield return null;
            }
            temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                light.sprite = null;
                yield return null;
            }

        }
    }
    private IEnumerator YellowTopFlashing()
    {
        const float flashTime = 1f;
        while (color == 6)
        {
            float temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                light.sprite = yellowFlashing;
                yield return null;
            }
            temp = 0f;
            while (temp < flashTime)
            {
                temp += Time.deltaTime;
                light.sprite = yellow;
                yield return null;
            }
        }
    }


    public void SetLightColor(int color)
    {
        switch (color)
        {

            case 0:
                light.sprite = red;
                break;
            case 1:
                light.sprite = green;
                break;
            case 2:
                light.sprite = blue;
                break;
            case 3:
                light.sprite = white;
                break;
            case 4:
                light.sprite = yellow;
                break;
            case 5:
                StartCoroutine("YellowFlashing");            
                break;
            case 6:
                StartCoroutine("YellowTopFlashing");
                break;
            default:
                light.sprite = red;
                break;
        }           
    }
}
