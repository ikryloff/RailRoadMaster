#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    private RouteObject route;
    private List <TrackCircuit> trackCircuits;
    public Engine engine;
    private TrackCircuit startTrack;
    private TrackCircuit occupiedTrack;
    private TrackCircuit lastRouteTrackForward;
    private TrackCircuit lastRouteTrackBackward;
    private Switch[] switches;
    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    IEnumerable<TrackCircuit> fullPath;
    public List<TrackCircuit> fullTCPath;
    private bool isRoute;
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




    private void Awake()
    {
       

    }
    void Start()
    {
        
    }

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
        //CheckRoutePresense();
        TestRoute ();
    }



    /// Make routes
    /// 

    /// Make routes manager
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






    public void RouteDirection(Switch[] arr, string direction)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                if (direction == Constants.DIR_STR)
                {
                    //sw.SetDirectionStraight();                    
                }
                else
                {
                    //sw.SetDirectionTurn();                   
                }                
            }
        }
    }

    

    public IEnumerable<TrackCircuit> FullPath
    {
        get
        {
            return fullPath;
        }

        set
        {
            fullPath = value;
        }
    }

   
    public TrackCircuit OccupiedTrack
    {
        get
        {
            return occupiedTrack;
        }

        set
        {
            occupiedTrack = value;
        }
    }

    public TrackCircuit LastRouteTrackForward
    {
        get
        {
            return lastRouteTrackForward;
        }

        set
        {
            lastRouteTrackForward = value;
        }
    }

    public TrackCircuit LastRouteTrackBackward
    {
        get
        {
            return lastRouteTrackBackward;
        }

        set
        {
            lastRouteTrackBackward = value;
        }
    }
}

