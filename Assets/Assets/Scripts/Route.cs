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
    private TrackCircuit tcI_CH;
    [SerializeField]
    private TrackCircuit tcI_2_8;
    [SerializeField]
    private TrackCircuit tc2;
    [SerializeField]
    private TrackCircuit tcI_8_16;    
    [SerializeField]
    private TrackCircuit tcsw_16;
    [SerializeField]
    private TrackCircuit tcsw_6_8;
    [SerializeField]
    private TrackCircuit tcsw_2_4;

    List<RouteObject> routes;
    private RouteObject route;
   
    private bool isRoute;
    private string _routeName;

    void Start()
    {
        routes = new List<RouteObject>();            
    }

    private void Update()
    {
        if(routes != null)
        {

            for (int i = 0; i < routes.Count; i++)
            {
                if (routes[i].TrackCircuits[0].IsCarPresence)
                {
                    routes[i].StartLight.SetLightColor(0);
                    routes[i].InUse = true;
                }
                if (routes[i].InUse)
                {
                    if (CheckRouteUsed(routes[i].TrackCircuits))
                        DestroyRoute(routes[i]);
                }
            }

        }
    }


    /// Make routes
    /// 

    /// Make routes manager
    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
        
        _routeName = startLight.Name + endLight.Name;

        TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
        route = gameObject.AddComponent<RouteObject>();
        Debug.Log("Make route");
        route.TrafficLights = tl;
        routes.Add(route);
        route.RouteName = _routeName;
        Debug.Log("Route name = "+ _routeName);
        RouteLightsManage(startLight, true);       
        RouteManage(route, _routeName);     
           
    }  
    

    private void RouteManage(RouteObject ro,  string routeName)
    {

        // Routes NI 
        if (routeName == "NIM2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "NICH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
        }
        
        // Routes N2
        else if (routeName == "N2CH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }
        else if (routeName == "N2M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
       
        // Routes N3
        else if (routeName == "N3CH")
        {
            ro.SwitchesStr = new Switch[] { sw10 , sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }
        else if (routeName == "N3M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }

        // Routes N4
        else if (routeName == "N4CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }
        else if (routeName == "N4M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        // Routes N5
        else if (routeName == "N5CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }
        else if (routeName == "N5M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        // Routes M3
        else if (routeName == "M3M2")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        // Routes CH
        else if (routeName == "CHCH2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            //order does matter
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcsw_2_4, tcI_2_8, tcI_8_16, tcsw_6_8, tcsw_16, tc2 };
            
            
        }   
        else if (routeName == "CHCHI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "CHCH3")
        {
            ro.SwitchesStr = new Switch[] { sw10, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if(routeName == "CHCH4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "CHCH5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }

        // Routes M2
        else if (routeName == "M2NI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M2N2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M2N3")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "M2N4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M2N5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }       
        else if (routeName == "M2M3")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }

        // Routes CHI 
        else if (routeName == "CHIM1")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CHIM5")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CHIN")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
        }

        // Routes CH2 
        else if(routeName == "CH2M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw15, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH2M5")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH2N")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }

        // Routes CH3
        else if (routeName == "CH3M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw11, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH3M5")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17};
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH3N")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9, sw5_17};
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }
        
        // Routes CH4
        else if (routeName == "CH4M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH4M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17};
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH4N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17 };
            ro.SwitchesTurn = new Switch[] { sw11, sw13, sw1_3};
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }

        // Routes CH5
        else if (routeName == "CH5M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH5M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "CH5N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
        }

        // Routes M4
        else if (routeName == "M4M5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }

        // Routes M5
        else if (routeName == "M5M4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
        }
        else if (routeName == "M5CH2")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M5CHI")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M5CH3")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M5CH4")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }
        else if (routeName == "M5CH5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.StartLight.SetLightColor(Constants.COLOR_WHITE);
            ro.EndLight.SetLightColor(Constants.COLOR_BLUE);
        }

        // Routes N
        else if (routeName == "NNI")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "NN2")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "NN3")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw11 };
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "NN4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11, sw13 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }
        else if (routeName == "NN5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
        }

        ///////
        else DestroyRoute(ro);

        if (ro)
        {
            if (CheckRootByPresence(ro.TrackCircuits))
            {
                
                if (CheckRouteBySwitches(ro.SwitchesStr, Constants.DIR_STR) && CheckRouteBySwitches(ro.SwitchesTurn, Constants.DIR_TURN))
                {
                    RouteDirection(ro.SwitchesStr, Constants.DIR_STR);
                    RouteDirection(ro.SwitchesTurn, Constants.DIR_TURN);
                }
                else
                    Debug.Log("Danger route");
            }
            else
            {
                Debug.Log("Track presence");
                RouteLightsManage(ro.StartLight, false);
                routes.Remove(ro);
                Destroy(ro);
            }
        }
    }
    


    private void DestroyRoute(RouteObject ro)
    {
        Debug.Log("Destroy");
        RouteLightsManage(ro.StartLight, false);
        RouteSwitchesUnlock(ro.SwitchesStr);
        RouteSwitchesUnlock(ro.SwitchesTurn);
        routes.Remove(ro);
        Destroy(ro);
    }

    public void DestroyRouteByLight(TrafficLights hitLight)
    {
        if (String.IsNullOrEmpty(hitLight.NameRouteOfLight))
        {
            hitLight.tag = Constants.LIGHTS_FREE;            
        }
        else
        {
            // Remove route object
            foreach (RouteObject routeObject in routes)
            {
                if (routeObject.RouteName == hitLight.NameRouteOfLight)
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
                if ((dir == Constants.DIR_STR && !sw.IsSwitchStraight && sw.SwitchLockCount > 0) || (dir == Constants.DIR_TURN && sw.IsSwitchStraight && sw.SwitchLockCount > 0))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckRootByPresence(TrackCircuit[] trackCircuits)
    {
        foreach (TrackCircuit tc in trackCircuits)
        {
            if (tc.IsCarPresence)
                return false;
        }
        return true;
    }

    private bool CheckRouteUsed(TrackCircuit[] trackCircuits)
    {
        for (int i = 0; i < trackCircuits.Length - 1; i++)
        {
            if (route.TrackCircuits[i].IsCarPresence == false && route.TrackCircuits[route.TrackCircuits.Length - 1].IsCarPresence == true)
                return true;
        }
        return false;
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
            light.tag = Constants.LIGHTS_IN_ROUTE;
            light.NameRouteOfLight = route.RouteName;
        }
        else
        {
            if (String.IsNullOrEmpty(route.EndLight.NameRouteOfLight))
                route.EndLight.SetLightColor(0);
            light.SetLightColor(0);
            light.tag = Constants.LIGHTS_FREE;
            light.NameRouteOfLight = "";
            
        }
        

    }

    public void RouteDirection(Switch[] arr, string direction)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                if (direction == Constants.DIR_STR)
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

