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

    void Start()
    {
        routes = new ArrayList();        
    }
     
    /// <summary>
    /// Make and Destroy routes
    /// </summary>

    private void Make12to5()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8 };
        RouteDirection(switchesStr, DIR_STR);
        Switch[] switchesTurn = new Switch[] { sw12, sw10 };
        RouteDirection(switchesTurn, DIR_TURN);
      
    }
    private void Destroy12to5()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw10, sw12};
        RouteDestroy(switches);        
    }

    private void Make3toI()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8 };
        RouteDirection(switchesStr, DIR_STR);
        Switch[] switchesTurn = new Switch[] { sw16 };
        RouteDirection(switchesTurn, DIR_TURN);
    }
    private void Destroy3toI()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw16 };
        RouteDestroy(switches);        
    }

    private void MakeItoICH()
    {
        Switch[] switchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
        RouteDirection(switchesStr, DIR_STR);        
    }
    private void DestroyItoICH()
    {
        Switch[] switches = new Switch[] { sw2_4, sw6_8, sw16 };
        RouteDestroy(switches);
    }






    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
        route = gameObject.AddComponent<RouteObj>();
        if (startLight.Name == n5.Name && endLight.Name == m2.Name)
        {
            TrafficLights[] tl = new TrafficLights[] {startLight, endLight};
            route.RouteName = "n5m2";         
            routes.Add(route);            
            startLight.SetLightColor(3);
            RouteLightsManage( tl, true);
            Make12to5();
            
        }

        if (startLight.Name == m2.Name && endLight.Name == n5.Name)
        {
            TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
            route.RouteName = "m2n5";
            routes.Add(route);
            startLight.SetLightColor(3);
            endLight.SetLightColor(2);
            RouteLightsManage(tl, true);
            Make12to5();           
        }

        if (startLight.Name == n3.Name && endLight.Name == ch.Name)
        {
            TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
            route.RouteName = "n3ch";
            routes.Add(route);
            startLight.SetLightColor(6);
            RouteLightsManage(tl, true);
            Make3toI();
        }

        if (startLight.Name == nI.Name && endLight.Name == ch.Name)
        {
            TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
            route.RouteName = "nIch";
            routes.Add(route);
            startLight.SetLightColor(1);
            RouteLightsManage(tl, true);
            MakeItoICH();
        }

    }


    /// <summary>
    /// Destroy Route function
    /// </summary>
    
    public void DestroyRoute(TrafficLights startLight)
    {
        // Remove route object
        foreach (RouteObj item in routes)
        {
            Debug.Log(item.RouteName);

            if (item.RouteName == startLight.LightInRoute)
            {
                routes.Remove(item);
                Destroy(item);
                break;
            }
        }


        if (startLight.LightInRoute == "n5m2")
        {
            TrafficLights[] tl = new TrafficLights[] { m2, n5};
            RouteLightsManage(tl, false);
            Destroy12to5();
        }

        if (startLight.LightInRoute == "m2n5")
        {
            TrafficLights[] tl = new TrafficLights[] { m2, n5 };
            RouteLightsManage(tl, false);
            Destroy12to5();
        }

        if (startLight.LightInRoute == "n3ch")
        {
            TrafficLights[] tl = new TrafficLights[] { n3, ch };
            RouteLightsManage(tl, false);
            Destroy3toI();
        }

        if (startLight.LightInRoute == "nIch")
        {
            TrafficLights[] tl = new TrafficLights[] { nI, ch };
            RouteLightsManage(tl, false);
            DestroyItoICH();
        }

        Debug.Log(startLight.LightInRoute);

       
    }
    void RouteDirection(Switch[] arr, string dir)
    {
        if (dir == DIR_STR)
        {
            foreach (Switch sw in arr)
            {
                sw.directionStraight();
                sw.SwitchLockCount += 1;
            }
        }
        else
        {
            foreach (Switch sw in arr)
            {
                sw.directionTurn();
                sw.SwitchLockCount += 1;
            }

        }
    }
    void RouteDestroy(Switch[] arr)
    {
        foreach (Switch sw in arr)
        {
            sw.SwitchLockCount -= 1;
        }
    }

    void RouteLightsManage (TrafficLights [] arr, bool isRoute)
    {
        if (isRoute)
        {
            foreach (TrafficLights tl in arr)
            {
                tl.tag = LIGHTS_IN_ROUTE;
                tl.LightInRoute = route.RouteName;                
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
}

class RouteObj : MonoBehaviour
{
    private string routeName;    

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