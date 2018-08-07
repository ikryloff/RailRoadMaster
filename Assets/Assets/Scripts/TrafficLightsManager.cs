using System;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightsManager : Singleton<TrafficLightsManager> {

    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    private TrafficLights startLight;
    private TrafficLights endLight;
    private TrafficLights tempLight = null;
    [SerializeField]
    private Route route;   
    [SerializeField]
    private Text lightText;
    [SerializeField]
    private RemoteControlScript rcs;
    private TrafficLights[] routeLights = new TrafficLights[2];

    public TrafficLights StartLight
    {
        get
        {
            return startLight;
        }

        set
        {
            startLight = value;
        }
    }

    public TrafficLights EndLight
    {
        get
        {
            return endLight;
        }

        set
        {
            endLight = value;
        }
    }

    public void SetRouteLights(TrafficLights light)
    {
        if (rcs.IsRemoteControllerOn)
        {
            if (light.tag == Constants.LIGHTS_FREE)
            {
                if (isStart)
                {
                    routeLights[0] = light;
                    SetLightsNames(routeLights[0].Name);
                    routeLights[0].tag = Constants.LIGHTS_IN_ROUTE;
                    isStart = false;
                }
                else
                {
                    routeLights[1] = light;
                    MakeRouteIfPossible(routeLights[0], routeLights[1]);
                    isStart = true;
                }
            }
            else if (light.tag == Constants.LIGHTS_IN_ROUTE && !isStart)
            {
                routeLights[0].tag = Constants.LIGHTS_FREE;
                isStart = true;
                lightText.text = "Wrong light in route";
            }
        }  
    }
   

    private void Start()
    {
        lightText.text = "None";
        
    }

    void Update () {
      
        if (!rcs.IsRemoteControllerOn)
        {
            /*
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
                // if you hit a free traffic light
                if (hit.collider != null)
                {
                    if (hit.collider.tag == Constants.LIGHTS_FREE)
                    {
                        // its your first light
                        if (isStart)
                        {
                            startLight = hit.collider.GetComponent<TrafficLights>();
                            startLight.tag = Constants.LIGHTS_IN_ROUTE;
                            SetLightsNames(startLight.Name);
                            isStart = false;
                        }
                        //its your second light
                        else
                        {
                            //detect the end of Route
                            endLight = hit.collider.GetComponent<TrafficLights>();
                            MakeRouteIfPossible(startLight, endLight);
                            isStart = true;
                        }
                    }
                    else if (hit.collider.tag == Constants.LIGHTS_IN_ROUTE && !isStart)
                    {
                        startLight.tag = Constants.LIGHTS_FREE;
                        isStart = true;
                        lightText.text = "Wrong light in route";
                    }
                }
            } 
            */

            if (Input.GetMouseButton(1))
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
                if (hit.collider != null && hit.collider.tag == Constants.LIGHTS_IN_ROUTE)
                {
                    startLight = hit.collider.GetComponent<TrafficLights>();
                    route.DestroyRouteByLight(startLight);
                    isStart = true;
                    lightText.text = "None";
                }

            }
        }
    }

    // Check possible Routes By Lights
    public bool IsPossibleLight (String[,] arr, TrafficLights first, TrafficLights second )
    {
        string start = first.Name;
        string end = second.Name;

        int n = arr.GetLength(0);
        int m = arr.GetLength(1);        
        for (int i = 0; i < n; i++)
        {
            if (start == arr[i, 0])
            {
                for (int j = 1; j < m; j++)
                {
                    if (end == arr[i, j])
                        return true;
                }                
            }
            
        }
        return false;
    }

    public void MakeRouteIfPossible(TrafficLights startLight, TrafficLights endLight)
    {
        if (IsPossibleLight(Constants.POSSIBLE_LIGHTS, startLight, endLight))
        {
            route.MakeRoute(startLight, endLight);
            SetLightsNames(startLight.Name, endLight.Name);
        }
        else
        {
            startLight.tag = Constants.LIGHTS_FREE;
            lightText.text = "Wrong light";
        }
    }

    void SetLightsNames(String start, String end = "")
    {
        lightText.text = start + " -> " + end;
    }
   
}
