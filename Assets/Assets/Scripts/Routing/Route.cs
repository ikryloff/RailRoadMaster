#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2, sw4, sw6, sw8, sw10, sw12, sw14, sw16, sw1_3, sw5, sw17, sw7_9, sw11, sw13, sw15, sw18, sw20, sw19, sw21, sw22;

    [SerializeField]
    private TrackCircuit 
        tcI_CH,       
        tcI_N,       
        tcI_CH_2,
        tcI_1_N,
        tcI_16_15,
        tc2,
        tc3,
        tc4,
        tc5,
        tc6,        
        tc7,        
        tc8,        
        tc9,        
        tc10,
        tc10_10,
        tc11,
        tc12,
        tc12_12,
        tc12A,
        tc13,
        tc14,
        tcsw1, tcsw3,
        tcsw5, tcsw17,
        tcsw_7_9top,
        tcsw_7_9bot,
        tcsw_2_4top,
        tcsw_2_4bot,
        tcsw_6_8top,
        tcsw_6_8bot,
        tcsw14,
        tcsw22,
        tcsw16,
        tcsw10,
        tcsw11,
        tcsw12,
        tcsw13,        
        tcsw15,
        tcsw18,    
        tcsw19,    
        tcsw20, 
        tcsw21  
        ;    

    List<RouteObject> routes;
    private RouteObject route;
    private TrackCircuit[] forwardPath;
    [SerializeField]
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
    RouteDictionary routeDictionary;
   

    private void Awake()
    {
        routeDictionary = FindObjectOfType<RouteDictionary>();
        textBuilder = FindObjectOfType<TextBuilder>();
        TrackCircuit[] tempArr = FindObjectsOfType<TrackCircuit>();
        trackCircuits = new List<TrackCircuit>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            if (tempArr[i].tag == "Track")
                trackCircuits.Add(tempArr[i]);
        }
     switchManager = FindObjectOfType<SwitchManager>();


    }
    void Start()
    {
        routes = new List<RouteObject>();
        routeItems = new List<RouteItem>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();

    }

    private void Update()
    {
        //CheckRoutePresense();
    }


    public TrackCircuit GetTrackCircuitByName( string _track)
    {
        foreach (var tc in trackCircuits)
        {
            if (tc.TrackName == _track)
                return tc;
        }
        return null;
    }
      

    public void CheckRoutePresense()
    {
        if (routes != null)
        {
            for (int i = 0; i < routes.Count; i++)
            {
                if (routes[i].TrackCircuits[0].hasCarPresence)
                {
                    if (!IsShunting(routes[i].TrafficLights))
                        routes[i].StartLight.SetLightColor(0);
                }
                if (CheckRouteUsed(routes[i]))
                {
                    if (IsShunting(routes[i].TrafficLights))
                        routes[i].StartLight.SetLightColor(0);
                    DestroyRoute(routes[i]);
                }
            }

        }
    }

    /// Make routes
    /// 

    /// Make routes manager
    public void MakeRoute(TrafficLight startLight, TrafficLight endLight)
    {
        startLight.lightDirection = 0;        
        routeName = startLight.Name + endLight.Name;
        RouteItem routeItem = routeDictionary.RouteDict[routeName];
        routeItem.InstantiateRoute();
        EventManager.PathChanged();
        /*
        if (CheckRouteForDuplicates(startLight.Name))
        {
            TrafficLight[] tl = new TrafficLight[] { startLight, endLight };
            route = gameObject.AddComponent<RouteObject>();
            
            route.TrafficLights = tl;
            routes.Add(route);
            route.RouteName = routeName;                       


            RouteLightsManage(tl, true);
            RouteManage(route, routeName);

            switchManager.UpdatePathAfterSwitch();

            cancelRouteButton.interactable = true;
        }
        else
        {
          
            print("Duplicate");
        }            

        // Find the direction of route and checking all path
        if(startLight.transform.position.x < endLight.transform.position.x)
        {
            startLight.lightDirection = 1;
            route.routeDirection = 1;
        }
        else if(startLight.transform.position.x > endLight.transform.position.x)
        {
            startLight.lightDirection = -1;
            route.routeDirection = -1;
        }  
        */

    }      

    private void RouteManage(RouteObject ro,  string routeName)
    {        

        if (ro)
        {                        
            if (CheckRootByPresence(ro.TrackCircuits, ro.TrafficLights))
            {                
                if (CheckRouteBySwitches(ro.SwitchesStr, Constants.DIR_STR) && CheckRouteBySwitches(ro.SwitchesTurn, Constants.DIR_TURN))
                {
                    // if the track of reception of train is NOT used in other route
                    if((!IsShunting(ro.TrafficLights) && ro.TrackCircuits.Last().UseMode != Constants.TC_WAIT) || IsShunting(ro.TrafficLights))
                    {
                       // print("Make route direction " + ro.RouteName);
                        //RouteDirection(ro.SwitchesStr, Constants.DIR_STR);
                       // RouteDirection(ro.SwitchesTurn, Constants.DIR_TURN);
                        RouteLock(ro.TrackCircuits);
                        
                        //print("Route Locked " + ro.RouteName);
                        //Message
                        messageText = string.Format("Route {0} is ready. You can go as will", ro.RouteName);
                        textBuilder.PrintMessage(messageText, "Yardmaster:");
                        //
                    }
                    else
                    {
                        //print("Counter route");
                        DestroyRoute(ro, false);
                    }
                        
                }
                else
                {
                    //Message
                    messageText = string.Format("Can't do that, danger cross route");
                    textBuilder.PrintMessage(messageText, "Yardmaster:");
                    ///
                    DestroyRoute(ro, false);
                }   
            }
            else
            {
                print("Track presence");
                DestroyRoute(ro, false);
            }            
        }
    }

    private void RouteLock(TrackCircuit[] trackCircuits)
    {
        foreach (TrackCircuit tc in trackCircuits)
        {
            //tc.UseMode = Constants.TC_WAIT;
            //tc.isInRoute = true;
        }
    }

    public void DestroyRouteByRouteName(string _routeName)
    {
        DestroyRoute(GetRouteByName(_routeName));
        
    }
    

    private void DestroyRoute(RouteObject ro, bool withUnlock = true)
    {
        //print("Destroy");        
        lightText.text = "None";
        RouteLightsManage(ro.TrafficLights, false);
        if (withUnlock)
        {
            RouteUnlock(ro.TrackCircuits);            
            foreach (TrackCircuit tc in ro.TrackCircuits)
            {
                tc.UseMode = Constants.TC_DEFAULT;
                
            }
            //print("Unlock");
        }        
        routes.Remove(ro);
        Destroy(ro);
        if (!Routes.Any())
            cancelRouteButton.interactable = false;
        
    }

    public void DestroyRouteByLight(TrafficLight hitLight)
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

    // return true if switches in route are free
    private bool CheckRouteBySwitches(Switch[] switches, string dir)
    {
        if (switches != null)
        {
            foreach (Switch sw in switches)
            {
                if (
                        sw.isSwitchInUse || 
                        (dir == Constants.DIR_STR && !sw.IsSwitchStraight && (sw.isLockedByRS || sw.isLockedByRoute)) || 
                        (dir == Constants.DIR_TURN && sw.IsSwitchStraight && (sw.isLockedByRS || sw.isLockedByRoute ))
                    )
                {
                    return false;
                }
            }
        }
        return true;
    }

    // return true if the name is unique
    private bool CheckRouteForDuplicates(string startLight)
    {
        if (routes != null)
        {
            foreach (RouteObject ro in routes)
            {
                if (startLight.Equals(ro.StartLight.Name))
                {
                    return false;
                }
            }
        }
        return true;
    }

    // return true if al is ok, if track circuits are free
    private bool CheckRootByPresence(TrackCircuit[] trackCircuits, TrafficLight [] trafficLights)
    {
        bool dangerPresence = true;
        TrackCircuit last = trackCircuits.Last();
     
        //check all except first and last track circuit
        for (int i = 0; i < trackCircuits.Length - 1; i++)
        {
            
           // if (trackCircuits[i].hasCarPresence)
            {
                
               // dangerPresence = false;
            }
                
        }
       // print("Not presence path ="  + dangerPresence);
       // print("is Shunting: "  + IsShunting(trafficLights));
        //if it is a train route, all tracks must be free
        if (!IsShunting(trafficLights) && last.hasCarPresence)
            dangerPresence = false;
        //print(" Not presence track =" + dangerPresence);
        return dangerPresence;
    }

    private bool CheckRouteUsed(RouteObject ro)
    {
        TrackCircuit[] trackCircuits = ro.TrackCircuits;
        TrackCircuit last = trackCircuits.Last();
        TrackCircuit control = trackCircuits[trackCircuits.Length - 2];
        bool wasRouteUsed = false;
        
        for (int i = 0; i < trackCircuits.Length - 1; i++)
        {
            
            if (trackCircuits[i].UseMode == Constants.TC_USED)
            {
                wasRouteUsed = true;
            }
            else
                wasRouteUsed = false;

        }       

        return wasRouteUsed;
        

    }

    public void RouteUnlock(TrackCircuit[] arr)
    {
       if(arr != null)
        {
            foreach (TrackCircuit tc in arr)
            {
                tc.isInRoute = false;
            }
        }
    }

    public void RouteLightsManage(TrafficLight[] lights, bool isRoute)
    {
        TrafficLight startLight = lights[0];
        TrafficLight endLight = lights[1];
        
        if (isRoute)
        {
            startLight.tag = Constants.LIGHTS_IN_ROUTE;
            startLight.NameRouteOfLight = route.RouteName;
            if (IsShunting(lights))
            {
                startLight.SetLightColor(Constants.COLOR_WHITE);
                endLight.SetLightColor(Constants.COLOR_DEFAULT);
            }
                
        }
        else
        {
            if (string.IsNullOrEmpty(route.EndLight.NameRouteOfLight))
                route.EndLight.SetLightColor(Constants.COLOR_DEFAULT);
            startLight.SetLightColor(Constants.COLOR_DEFAULT);          
            startLight.tag = Constants.LIGHTS_FREE;
            startLight.NameRouteOfLight = "";
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
                    //sw.SetDirectionStraight();                    
                }
                else
                {
                    //sw.SetDirectionTurn();                   
                }                
            }
        }
    }

    
    
    private bool IsShunting(TrafficLight[] trafficLights)
    {
        return trafficLights[0].Name != "CH" && trafficLights[0].Name != "N" && trafficLights[1].Name != "CH" && trafficLights[1].Name != "N";
    }

    public RouteObject GetRouteByName(string name)
    {
        foreach (RouteObject ro in Routes)
        {
            if (ro.RouteName == name)
                return ro;
        }
        return null;
    }

    internal List<RouteObject> Routes
    {
        get
        {
            return routes;
        }

        set
        {
            routes = value;
        }
    }

    public TrackCircuit[] ForwardPath
    {
        get
        {
            return forwardPath;
        }

        set
        {
            forwardPath = value;
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

