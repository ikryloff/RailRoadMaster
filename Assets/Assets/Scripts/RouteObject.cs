using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// RouteObject class
/// Every time Route make and destroy functions work with new Route object and its functions 


public class RouteObject : MonoBehaviour
{
    private string routeName;
    private TrafficLights [] trafficLights;
    private Switch[] switchesStr;
    private Switch[] switchesTurn;
    private TrackCircuit[] trackCircuits;


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

    public TrafficLights EndLight
    {
        get
        {
            return trafficLights[1];
        }

        set
        {
            trafficLights[1] = value;
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
            trafficLights[0] = value;
        }
    }

    public TrackCircuit[] TrackCircuits
    {
        get
        {
            return trackCircuits;
        }

        set
        {
            trackCircuits = value;
        }
    }
    
}
