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

    private RouteObj route;

    const string DIR_TURN = "turn";
    const string DIR_STR = "straight";
    const string LIGHTS_FREE = "TrafficLight";
    const string LIGHTS_IN_ROUTE = "TrafficLightInRoute";
    private bool isRoute;


    ///List of impossible Routes for each route
    ///
    string [,] imPossibleRoutes = new string [,]
    {
        ///Departure to Ch from all tracks
        {"n3ch", "n4m2", "m2n4", "n5m2","m2n5","n6m2","m2n6","m3m2","m2m3","n4m2","m2n4"},
        {"nIch", "n4m2", "m2n4", "n5m2","m2n5","n6m2","m2n6","m3m2","m2m3","n4m2","m2n4"},
        {"n4ch", "", "", "","","","", "", "", "", ""},
        {"n5ch", "", "", "","","","","","","",""},
        {"n6ch", "", "", "","","","","","","",""},
       
        ///From all tracks behind M2
        {"n5m2", "n6Ch", "chch6", "n4ch","chch4","","","","","",""},//
        {"nIm2", "n3ch", "chch3", "n4ch","chch4","n5ch","chch5","n6ch","chch6","",""},
        

    };




    void Start()
    {
        routes = new ArrayList();        
    }

    

    
    /// Make routes
    /// 

    /// Make routes manager
    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
        TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
        route = gameObject.AddComponent<RouteObj>();
        routes.Add(route);
        route.RouteName = $"{startLight.Name}{endLight.Name}";
        /// From track 5 to track 2
        /// 
        if (startLight.Name == n5.Name && endLight.Name == m2.Name && IsPossibleRoute(imPossibleRoutes, routes, "n5m2")) // need to make a function of returning route
        {
                       
            startLight.SetLightColor(3);
            route.RouteLightsManage(tl, true);
            Make12to5();           
            
        }
        /// From track 2 to track 5
        /// 
        else if (startLight.Name == m2.Name && endLight.Name == n5.Name)
        {
            route.RouteName = "m2n5";
            routes.Add(route);
            startLight.SetLightColor(3);
            endLight.SetLightColor(2);
            route.RouteLightsManage(tl, true);
            Make12to5();
        }

        else if (startLight.Name == n3.Name && endLight.Name == ch.Name)
        {
            route.RouteName = "n3ch";
            routes.Add(route);
            startLight.SetLightColor(6);
            route.RouteLightsManage(tl, true);
            Make3toI();
        }
        else if (startLight.Name == nI.Name && endLight.Name == ch.Name)
        {
            route.RouteName = "nIch";
            routes.Add(route);
            startLight.SetLightColor(1);
            route.RouteLightsManage(tl, true);
            MakeItoICH();
        }
        else
        {
            //bad code
            startLight.tag = LIGHTS_FREE;
            endLight.tag = LIGHTS_FREE;

            Debug.Log("Can't create!");
        }

    }

    /// Make and destroy routes functions by tracks number
    /// 

    private void Make12to5()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8 };
        Switch[] switchesTurn = new Switch[] { sw12, sw10 };
        route.RouteDirection(switchesStr, DIR_STR);
        route.RouteDirection(switchesTurn, DIR_TURN);
    }    

    private void Destroy12to5()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw10, sw12};
        route.RouteSwitchesDestroy(switches);        
    }

    private void Make3toI()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8 };
        route.RouteDirection(switchesStr, DIR_STR);
        Switch[] switchesTurn = new Switch[] { sw16 };
        route.RouteDirection(switchesTurn, DIR_TURN);
    }
    private void Destroy3toI()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw16 };
        route.RouteSwitchesDestroy(switches);        
    }

    private void MakeItoICH()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
        route.RouteDirection(switchesStr, DIR_STR);        
    }
    private void DestroyItoICH()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw16 };
        route.RouteSwitchesDestroy(switches);
    }



    /// <summary>
    /// Destroy Route function
    /// </summary>
    
    public void DestroyRoute(TrafficLights anyLight)
    {
        if (String.IsNullOrEmpty(anyLight.LightInRoute))
        {
            anyLight.tag = LIGHTS_FREE;
        }
        else
        {   // Remove route object
            foreach (RouteObj item in routes)
            {
                if (item.RouteName == anyLight.LightInRoute)
                {
                    routes.Remove(item);
                    Destroy(item);
                    break;
                }
            }
        }
        

        if (anyLight.LightInRoute == "n5m2")
        {
            TrafficLights[] tl = new TrafficLights[] { m2, n5};
            route.RouteLightsManage(tl, false);
            Destroy12to5();
        }

        if (anyLight.LightInRoute == "m2n5")
        {
            TrafficLights[] tl = new TrafficLights[] { m2, n5 };
            route.RouteLightsManage(tl, false);
            Destroy12to5();
        }

        if (anyLight.LightInRoute == "n3ch")
        {
            TrafficLights[] tl = new TrafficLights[] { n3, ch };
            route.RouteLightsManage(tl, false);
            Destroy3toI();
        }

        if (anyLight.LightInRoute == "nIch")
        {
            TrafficLights[] tl = new TrafficLights[] { nI, ch };
            route.RouteLightsManage(tl, false);
            DestroyItoICH();
        }      
    }

    private bool CheckRouteByName(ArrayList arr, string name)
    {
        if(arr.Count > 0)
        {
            foreach (RouteObj route in arr)
            {
                if (route.RouteName == name)
                    return true;
            }
        }        
        return false;
    }

    private bool IsPossibleRoute(String [,] possRoutes, ArrayList routes, string route)
    {
        int n = possRoutes.GetLength(0);
        int m = possRoutes.GetLength(1);
        for (int i = 0; i < n; i++)
        {
            if (route == imPossibleRoutes[i, 0])
            {
                for (int j = 1; j < m; j++)
                {
                    if (CheckRouteByName(routes, imPossibleRoutes[i, j]))
                        return false;
                }
            }

        }
        return true;
    }



}

/// RouteObject class
/// Every time Route make and destroy functions work with new Route object and its functions 


class RouteObj : MonoBehaviour
{
    const string DIR_TURN = "turn";
    const string DIR_STR = "straight";
    const string LIGHTS_FREE = "TrafficLight";
    const string LIGHTS_IN_ROUTE = "TrafficLightInRoute";

    public string routeName;   

    public void RouteDirection(Switch[] arr, string dir)
    {
        if (dir == DIR_STR)
        {
            foreach (Switch sw in arr)
            {
                {
                    sw.directionStraight();
                    sw.SwitchLockCount += 1;
                }
            }
        }
        else
        {            
            foreach (Switch sw in arr)
            {
                {
                    sw.directionTurn();
                    sw.SwitchLockCount += 1;
                }
                
            }
        }        
    }

    public void RouteSwitchesDestroy(Switch[] arr)
    {
        foreach (Switch sw in arr)
        {
            sw.SwitchLockCount -= 1;
        }
    }

    public void RouteLightsManage(TrafficLights[] arr, bool isRoute)
    {
        if (isRoute)
        {
            foreach (TrafficLights tl in arr)
            {
                tl.tag = LIGHTS_IN_ROUTE;
                tl.LightInRoute = RouteName;
            }
        }
        else
        {
            foreach (TrafficLights tl in arr)
            {
                tl.SetLightColor(0);
                tl.tag = LIGHTS_FREE;
                tl.LightInRoute = "";
            }
        }
    }

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

}