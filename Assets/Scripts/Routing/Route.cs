#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    
    private string routeName;
    [SerializeField]
    private Text lightText;
    [SerializeField]
    private Button cancelRouteButton;
    [SerializeField]
    private List<RouteItem> routeItems;
    SwitchManager switchManager;
    private TextBuilder textBuilder;
    private string messageText;

    public List<string> Routes = new List<string>();



    private void TestRoute()
    {
        if ( Input.GetKeyDown (KeyCode.Y) )
        {
            RouteItem routeItem = RouteDictionary.Instance.RouteDict ["N2CH"];
            routeItem.InstantiateRoute ();
            Routes.Add (routeName);
            EventManager.PathChanged ();
        }

        if ( Input.GetKeyDown (KeyCode.U) )
        {
            DestroyRoute ("N2CH");
        }

        if ( Input.GetKeyDown (KeyCode.T) )
        {
            RouteItem routeItem = RouteDictionary.Instance.RouteDict ["NN2"];
            routeItem.InstantiateRoute ();
            Routes.Add (routeName);
            EventManager.PathChanged ();
        }
    }

    private void Update()
    {
        TestRoute ();
    }



    public void MakeRoute(TrafficLight startLight, TrafficLight endLight)
    {

        routeName = startLight.Name + endLight.Name;
        
        RouteItem routeItem = RouteDictionary.Instance.RouteDict[routeName];
        routeItem.InstantiateRoute();
        Routes.Add (routeName);
        EventManager.PathChanged();     
    }

    public void DestroyRoute( string routeName )
    {
        RouteItem routeItem = RouteDictionary.Instance.RouteDict [routeName];
        routeItem.DestroyRoute ();
        Routes.Remove (routeName);

    }


}

