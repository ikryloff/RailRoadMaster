using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2_4;
    [SerializeField]
    private Switch sw6_8;
    [SerializeField]
    private Switch sw10;
    [SerializeField]
    private Switch sw12;
    [SerializeField]
    private Switch sw14;
    [SerializeField]
    private Switch sw16;
    [SerializeField]
    private Switch sw1_3;
    [SerializeField]
    private Switch sw5_17;
    [SerializeField]
    private Switch sw7_9;
    [SerializeField]
    private Switch sw11;
    [SerializeField]
    private Switch sw13;
    [SerializeField]
    private Switch sw15;

    [SerializeField]
    private TrafficLights n5;
    [SerializeField]
    private TrafficLights ch;
    [SerializeField]
    private TrafficLights m2;
    [SerializeField]
    private TrafficLights n3;
    [SerializeField]
    private TrafficLights nI;
    [SerializeField]
    private TrafficLights n4;
    [SerializeField]
    private TrafficLights n6;
    [SerializeField]
    private TrafficLights m3;
    ArrayList routes;

    private RouteObject route;

    const string DIR_TURN = "turn";
    const string DIR_STR = "straight";
    const string LIGHTS_FREE = "TrafficLight";
    const string LIGHTS_IN_ROUTE = "TrafficLightInRoute";
    private bool isRoute;
    private string _routeName;   



    void Start()
    {
        routes = new ArrayList();        
    }

    

    
    /// Make routes
    /// 

    /// Make routes manager
    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
        _routeName = startLight.Name + endLight.Name;

        TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
        route = gameObject.AddComponent<RouteObject>();
        route.TrafficLights = tl;
        routes.Add(route);
        route.RouteName = _routeName;
        RouteLightsManage(startLight, true);
        Debug.Log(_routeName);
        RouteManage(route, _routeName);     
           
    }  
    

    private void RouteManage(RouteObject ro,  string routeName)
    {

        // Routes NI 
        if (routeName == "NIM2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.StartLight.SetLightColor(3);
        }
        else if (routeName == "NICH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(1);
        }

        // Routes N3
        else if (routeName == "N3CH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(6);
        }
        else if (routeName == "N3M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.StartLight.SetLightColor(3);
        }

        // Routes N4
        else if (routeName == "N4CH")
        {
            ro.SwitchesStr = new Switch[] { sw10 , sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(6);
        }
        else if (routeName == "N4M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.StartLight.SetLightColor(3);
        }

        // Routes N5
        else if (routeName == "N5CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(6);
        }
        else if (routeName == "N5M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.StartLight.SetLightColor(3);
        }

        // Routes N6
        else if (routeName == "N6CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(6);
        }
        else if (routeName == "N6M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.StartLight.SetLightColor(3);
        }

        // Routes M3
        else if (routeName == "M3M2")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.StartLight.SetLightColor(3);
        }

        // Routes CH
        else if (routeName == "CHCH3")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(4);
        }       
       
        else if (routeName == "CHCHI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(4);
        }

        else if (routeName == "CHCH4")
        {
            ro.SwitchesStr = new Switch[] { sw10, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(4);
        }

        else if(routeName == "CHCH5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(4);
        }

        else if (routeName == "CHCH6")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(4);
        }

        // Routes M2

        else if (routeName == "M2N5")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.StartLight.SetLightColor(3);
            ro.EndtLight.SetLightColor(2);
        }
       
        





        ///////
        else DestroyRoute(ro);

        if (ro)
        {
            if (CheckRouteBySwitches(ro.SwitchesStr, DIR_STR) && CheckRouteBySwitches(ro.SwitchesTurn, DIR_TURN))
            {
                RouteDirection(ro.SwitchesStr, DIR_STR);
                RouteDirection(ro.SwitchesTurn, DIR_TURN);
            }
            else
            {
                Debug.Log("Danger route");
                RouteLightsManage(ro.StartLight, false);
                routes.Remove(ro);
                Destroy(ro);
            }            
        }
    }
    


    private void DestroyRoute(RouteObject ro)
    {
        RouteLightsManage(ro.StartLight, false);
        RouteSwitchesUnlock(ro.SwitchesStr);
        RouteSwitchesUnlock(ro.SwitchesTurn);
        routes.Remove(ro);
        Destroy(ro);
    }

    public void DestroyRouteByLight(TrafficLights hitLight)
    {
        if (String.IsNullOrEmpty(hitLight.LightInRoute))
        {
            hitLight.tag = LIGHTS_FREE;            
        }
        else
        {
            // Remove route object
            foreach (RouteObject routeObject in routes)
            {
                if (routeObject.RouteName == hitLight.LightInRoute)
                {
                    DestroyRoute(routeObject);
                    break;
                }
            }
        } 
    }

    private bool CheckRouteBySwitches(Switch[] switches, string dir)
    {
        if (switches != null)
        {
            foreach (Switch sw in switches)
            {
                if ((dir == DIR_STR && !sw.IsSwitchStraight && sw.SwitchLockCount > 0) || (dir == DIR_TURN && sw.IsSwitchStraight && sw.SwitchLockCount > 0))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void RouteSwitchesUnlock(Switch[] arr)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                sw.SwitchLockCount -= 1;
            }
        }
    }

    public void RouteLightsManage(TrafficLights light, bool isRoute)
    {

        
        if (isRoute)
        {
            light.tag = LIGHTS_IN_ROUTE;
            light.LightInRoute = route.RouteName;
        }
        else
        {
            light.SetLightColor(0);
            light.tag = LIGHTS_FREE;
            light.LightInRoute = "";
        }
        

    }

    public void RouteDirection(Switch[] arr, string dir)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                if (dir == DIR_STR)
                {
                    sw.directionStraight();                    
                }
                else
                {
                    sw.directionTurn();                   
                }
                sw.SwitchLockCount += 1;
            }
        }
    }
}

