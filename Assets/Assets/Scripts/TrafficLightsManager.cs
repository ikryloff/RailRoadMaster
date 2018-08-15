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
    private bool cancelRouteIsOn;

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

    public bool CancelRouteIsOn
    {
        get
        {
            return cancelRouteIsOn;
        }

        set
        {
            cancelRouteIsOn = value;
            lightText.text = "Cancel..";

        }
    }

    public void SetRouteByLights(TrafficLights firstLight, TrafficLights secondLight)
    {
        SetLightsInRoute(firstLight);
        SetLightsInRoute(secondLight);
    }

    public void SetLightsInRoute(TrafficLights light)
    {
        //if (rcs.IsRemoteControllerOn)
        {
            // Canceling Route
            if (CancelRouteIsOn)
            {
                if (light != null && light.tag == Constants.LIGHTS_IN_ROUTE)
                {
                    route.DestroyRouteByLight(light);
                    isStart = true;
                    lightText.text = "None";
                }
                lightText.text = "Done";
                cancelRouteIsOn = false;
                
            }
            else
            {
                //Taking lights in route
                if (light.tag == Constants.LIGHTS_FREE)
                {
                    if (isStart)
                    {
                        startLight = light;
                        SetLightsNames(startLight.Name);
                        startLight.tag = Constants.LIGHTS_IN_ROUTE;
                        isStart = false;
                    }
                    else
                    {
                        endLight = light;
                        MakeRouteIfPossible(startLight, endLight);
                        isStart = true;
                    }
                }
                else if (light.tag == Constants.LIGHTS_IN_ROUTE && !isStart)
                {
                    startLight.tag = Constants.LIGHTS_FREE;
                    isStart = true;
                    lightText.text = "Wrong light in route";
                }
            } 
        }  
    }
       
    
    private void Start()
    {
        lightText.text = "None";
        
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
