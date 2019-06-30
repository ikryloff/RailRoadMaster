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
    private RouteValidation validation;

    public List<int> Routes = new List<int>();

       

    public void MakeRoute( int routeNum , int routeButton = -1)
    {
        RouteItem routeItem = RouteDictionary.Instance.RouteDict [routeNum];
        routeItem.InstantiateRoute (routeButton);
        print (routeButton);
        Routes.Add (routeNum);
        RouteDictionary.Instance.PanelRoutes [routeNum].Show (true);
        EventManager.PathChanged ();
    }

    public void DestroyRoute( int routeNum )
    {
        RouteItem routeItem = RouteDictionary.Instance.RouteDict [routeNum];
        routeItem.DestroyRoute ();
        Routes.Remove (routeNum);
        RouteDictionary.Instance.PanelRoutes [routeNum].Show (false);

    }

    public bool CheckRoute( int routeNum )
    {        
        if ( validation.InputRouteIsNotDangerouse (routeNum) )
            return true;
        else
            Debug.Log ("Danger Route");
        return false;
    }

    public bool Validate( int routeNum )
    {
        if ( validation.InputRouteNumIsValid (routeNum) )
            return true;
        else
            Debug.Log ("Wrong Num");
        return false;
    }

    private void Start()
    {
        validation = new RouteValidation ();
    }
}

