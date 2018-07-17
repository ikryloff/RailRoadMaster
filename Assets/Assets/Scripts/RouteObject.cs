using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// RouteObject class
/// Every time Route make and destroy functions work with new Route object and its functions 


class RouteObject : MonoBehaviour
{
    [SerializeField]
    private string routeName;
    private TrafficLights [] trafficLights;
    private Switch[] switchesStr;
    private Switch[] switchesTurn;
    private TrafficLights startLight;
    private TrafficLights endtLight;

        
    public string RouteName
    {
        get
        {
            return routeName;
        }

        set
        {
            routeName = value;
        }
    }

    public TrafficLights[] TrafficLights
    {
        get
        {
            return trafficLights;
        }

        set
        {
            trafficLights = value;
        }
    }

    public Switch[] SwitchesStr
    {
        get
        {
            return switchesStr;
        }

        set
        {
            switchesStr = value;
        }
    }

    public Switch[] SwitchesTurn
    {
        get
        {
            return switchesTurn;
        }

        set
        {
            switchesTurn = value;
        }
    }

    public TrafficLights EndtLight
    {
        get
        {
            return trafficLights[1];
        }

        set
        {
            endtLight = value;
        }
    }

    public TrafficLights StartLight
    {
        get
        {
            return trafficLights[0];
        }

        set
        {
            startLight = value;
        }
    }
}
