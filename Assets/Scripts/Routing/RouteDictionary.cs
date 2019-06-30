using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteDictionary : Singleton<RouteDictionary>, IManageable
{
    public TrackCircuit[] TrackCircuits { get; set; }
    public Switch[] Switches { get; set; }
    public SwitchManager SwitchManager;
    public TrackCircuitManager TrackCircuitManager;
    public TrafficLightsManager TrafficLightsManager;
    // Scriptable obj array, contains all possible routes
    [SerializeField]
    private RouteData[] routeFiles;
    public Dictionary<int, PanelRoute> PanelRoutes { get; set; }

    public Dictionary<int, RouteItem> RouteDict { get; set; }

    public RouteData [] RouteFiles
    {
        get
        {
            return routeFiles;
        }
      
    }

    public void Init()
    {
        SwitchManager = FindObjectOfType<SwitchManager>();
        TrackCircuitManager = FindObjectOfType<TrackCircuitManager>();
        TrafficLightsManager = FindObjectOfType<TrafficLightsManager>();
        MakeRouteDictionary();        
        InstantiatePanelRoutesDictionary ();

    }
    // Take data from Scriptable objects and put it in cache
    private void MakeRouteDictionary()
    {
        RouteDict = new Dictionary<int, RouteItem>();

        foreach (RouteData routeFile in routeFiles)
        {
            RouteItem newRoute = gameObject.AddComponent<RouteItem>();
            newRoute.RouteName = routeFile.RouteName;
            newRoute.Description = routeFile.RouteName;
            newRoute.SwitchesToStraight = ParseDataToSwitches(routeFile.SwitchesToStraight);
            newRoute.SwitchesToTurn = ParseDataToSwitches(routeFile.SwitchesToTurn);
            newRoute.TrackCircuits = ParseDataToTrackCircuits(routeFile.TrackCircuits);
            newRoute.RouteLights = ParseDataToTrafficLights(routeFile.RouteLights);
            newRoute.IsShunting = routeFile.IsShunting;
            newRoute.IsStraight = routeFile.IsStraight;
            newRoute.DependsOnSignal = GetTL(routeFile.DependsOnSignal);
            newRoute.RouteNumber = routeFile.RouteNumber;
            RouteDict.Add(newRoute.RouteNumber, newRoute);
            
        }      

    }
    // Get Switch by Name
    private Switch GetSw(string switchName)
    {
        return SwitchManager.SwitchDict[switchName];
    }
    // Get TC by Name
    private TrackCircuit GetTC(string tcName)
    {
        return TrackCircuitManager.TCDict[tcName];
    }
    // Get TL by Name
    private TrafficLight GetTL(string tlName)
    {
        return TrafficLightsManager.TLDict[tlName];
    }

    private Switch [] ParseDataToSwitches(string [] list)
    {
        Switch[] tempList = new Switch[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            tempList[i] = GetSw(list[i]);
        }
        return tempList;
    }

    private TrackCircuit[] ParseDataToTrackCircuits(string[] list)
    {
        TrackCircuit[] tempList = new TrackCircuit[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            tempList[i] = GetTC(list[i]);
        }
        return tempList;
    }

    private TrafficLight[] ParseDataToTrafficLights(string[] list)
    {
        TrafficLight[] tempList = new TrafficLight[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            tempList[i] = GetTL(list[i]);
        }
        return tempList;
    }

    private void InstantiatePanelRoutesDictionary()
    {
        PanelRoutes = new Dictionary<int, PanelRoute> ();
        PanelRoute [] pr = FindObjectsOfType<PanelRoute> ();
        foreach ( PanelRoute item in pr )
        {
            PanelRoutes.Add (item.Num, item);
        }


    }

    public void OnStart()
    {        
    }
}
