using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// RouteObject class
/// Every time Route make and destroy functions work with new Route object and its functions 


public class RouteObject : MonoBehaviour
{
    private string routeName;
    private TrafficLight [] trafficLights;
    private Switch[] switchesStr;
    private Switch[] switchesTurn;
    private TrackCircuit[] trackCircuits;
    public int routeDirection;

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

    public TrafficLight[] TrafficLights
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

    public TrafficLight EndLight
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

    public TrafficLight StartLight
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
