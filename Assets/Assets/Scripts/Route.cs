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
   

    void Start()
    {
        routes = new ArrayList();        
    }
       

    private void Make12to5()
    {
        sw12.directionTurn();        
        sw12.SwitchLock = true;
        sw10.directionTurn();
        sw10.SwitchLock = true;
        sw2_4.directionStraight();
        sw2_4.SwitchLock = true;
        sw6_8.directionStraight();
        sw6_8.SwitchLock = true;
    }
    private void Destroy12to5()
    {
        sw12.SwitchLock = false;
        sw10.SwitchLock = false;
        sw2_4.SwitchLock = false;
        sw6_8.SwitchLock = false;
    }

    private void Make3toI()
    {
        sw16.directionTurn();
        sw16.SwitchLock = true;       
        sw2_4.directionStraight();
        sw2_4.SwitchLock = true;
        sw6_8.directionStraight();
        sw6_8.SwitchLock = true;
    }
    private void Destroy3toI()
    {
        sw16.SwitchLock = false;        
        sw2_4.SwitchLock = false;
        sw6_8.SwitchLock = false;
    }




    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
        route = gameObject.AddComponent<RouteObj>();
        if (startLight.Name == n5.Name && endLight.Name == m2.Name)
        {
            route.RouteName = "n5m2";         
            routes.Add(route);
            
            startLight.SetLightColor(3);

            m2.tag = "TrafficLightInRoute";
            n5.tag = "TrafficLightInRoute";

            n5.LightInRoute = route.RouteName;
            m2.LightInRoute = route.RouteName;
            
            Make12to5();
            
        }

        if (startLight.Name == m2.Name && endLight.Name == n5.Name)
        {
            route.RouteName = "m2n5";
            routes.Add(route);

            startLight.SetLightColor(3);
            
            m2.tag = "TrafficLightInRoute";
            n5.tag = "TrafficLightInRoute";

            n5.LightInRoute = route.RouteName;
            m2.LightInRoute = route.RouteName;

            Make12to5();           
        }

        if (startLight.Name == n3.Name && endLight.Name == ch.Name)
        {
            Debug.Log("Hello!");
            route.RouteName = "n3ch";
            routes.Add(route);

            startLight.SetLightColor(6);            

            n3.tag = "TrafficLightInRoute";
            ch.tag = "TrafficLightInRoute";

            n3.LightInRoute = route.RouteName;
            ch.LightInRoute = route.RouteName;

            Make3toI();

        }

    }

    public void DestroyRoute(TrafficLights startLight)
    {
        Debug.Log(startLight.LightInRoute);
        if (startLight.LightInRoute == "n5m2")
        {
            m2.SetLightColor(0);
            n5.SetLightColor(0);

            m2.tag = "TrafficLight";
            n5.tag = "TrafficLight";

            m2.LightInRoute = "";
            n5.LightInRoute = "";

            Destroy12to5();
        }

        if (startLight.LightInRoute == "m2n5")
        {
            
            m2.SetLightColor(0);
            n5.SetLightColor(0);
            m2.tag = "TrafficLight";
            n5.tag = "TrafficLight";

            m2.LightInRoute = "";
            n5.LightInRoute = "";

            Destroy12to5();
        }

        if (startLight.LightInRoute == "n3ch")
        {
            n3.SetLightColor(0);
            Debug.Log(n3.IntColor);
            ch.SetLightColor(0);
            n3.tag = "TrafficLight";
            ch.tag = "TrafficLight";

            n3.LightInRoute = "";
            ch.LightInRoute = "";

            Destroy3toI();
        }


        foreach (RouteObj item in routes)
        {
            if (item.RouteName == startLight.LightInRoute)
            {
                routes.Remove(item);
                Destroy(item);
                break;
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