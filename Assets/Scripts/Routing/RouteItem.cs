using System.Collections;
using System.Linq;
using UnityEngine;
public class RouteItem : MonoBehaviour
{
    public string Description;
    public string RouteName { get; set; }
    public Switch [] SwitchesToStraight { get; set; }
    public Switch [] SwitchesToTurn { get; set; }
    public TrackCircuit [] TrackCircuits { get; set; }
    public TrafficLight [] RouteLights { get; set; }
    public bool IsShunting { get; set; }
    public TrafficLight DependsOnSignal { get; set; }
    public bool IsStraight { get; set; }
    public int RouteNumber { get; set; }
    public TrackCircuit TargetTrack { get; set; }
    public int RouteDirection { get; set; }
    public RouteUnit Unit { get; set; }


    private void Start()
    {
        Unit = RouteDictionary.Instance.PanelRoutes [RouteNumber];
    }

    public void InstantiateRoute( )
    {
        TargetTrack = TrackCircuits.Last ();
        SetAllSwitchesStraight ();
        SetAllSwitchesTurn ();
        AllTCInRouteOn ();
        TrafficLightOn ();
        StartCoroutine (CheckStartingRoute ());
    }

    IEnumerator CheckStartingRoute()
    {
        while ( true )
        {
            if ( TrackCircuits.Any (t => t.HasCarPresence && t != TargetTrack) )
                break;
            yield return null;
        }
        if ( !IsShunting )
        {
            RouteLights [0].IsClosedByTrain = true;
            TrafficLightOff ();
            //for all trains passing through
            RouteLights [0].Trigger.enabled = false;
        }
        else
            //if shunting comp goes behind closed signal and go back
            RouteLights [1].Trigger.enabled = false;
        // enter route
        AllTCInUseOn();
        Unit.IsInUse = true;
        EventManager.PathChanged ();
        StartCoroutine (CheckPassingRoute ());
    }

    IEnumerator CheckPassingRoute()
    {
        StopCoroutine (CheckStartingRoute ());        
        while ( TrackCircuits.Any (t => t != TargetTrack && t.HasCarPresence) )
        {            
            yield return new WaitForSeconds (0.5f);
        }
        
        if ( !IsShunting )
        {
            RouteLights [0].Trigger.enabled = true;
            RouteLights [0].IsClosedByTrain = false;
        }
        else
            RouteLights [1].Trigger.enabled = true;
        Unit.IsInUse = false;
        Route.Instance.DestroyRoute (RouteNumber);
        

    }


    private void TrafficLightOn()
    {
        //light TLs with parameters
        RouteLights [0].LightOn (this);
        if ( RouteDirection == -1 )
        {
            if ( TargetTrack.TrackLights [0] != null )
                TargetTrack.TrackLights [0].Trigger.enabled = true;
        }
        else
        {
            if ( TargetTrack.TrackLights [1] != null )
                TargetTrack.TrackLights [1].Trigger.enabled = true;
        }
    }

    private void TrafficLightOff()
    {
        //light TL with parameters
        RouteLights [0].LightOff ();
    }

    private void AllTCInRouteOn()
    {
        foreach ( TrackCircuit tc in TrackCircuits )
        {
            tc.IsInRoute = true;
            GameEventManager.SendEvent ("StateTrack", tc);
        }
        
    }

    private void AllTCInUseOn()
    {
        foreach ( TrackCircuit tc in TrackCircuits )
        {
            tc.IsInUse = true;
            GameEventManager.SendEvent ("StateTrack", tc);
        }

    }

    private void AllTCInRouteOff()
    {
        foreach ( TrackCircuit tc in TrackCircuits )
        {
            tc.IsInRoute = false;
            tc.IsInUse = false;
            GameEventManager.SendEvent ("StateTrack", tc);
        }
    }

    public bool CheckTrainRoute()
    {
        if(TrackCircuits.All (t => !t.IsInRoute && !t.HasCarPresence) )
        {
            print ("Checked - Not Danger");
            return true;

        }
        else
        {
            print ("Checked - Danger");
            return false;
        }
    }

    public bool CheckShuntingRoute()
    {
        foreach ( Switch sw in SwitchesToStraight )
        {
            if ( sw.TrackCircuit.IsInRoute || sw.TrackCircuit.HasCarPresence )
                return false;
        }

        foreach ( Switch sw in SwitchesToTurn )
        {
            if ( sw.TrackCircuit.IsInRoute || sw.TrackCircuit.HasCarPresence )
                return false;
        }

        return true;
    }

    private void SetAllSwitchesTurn()
    {
        foreach ( Switch sw in SwitchesToTurn )
        {
            sw.SetSwitchDirection (Switch.SwitchDir.Turn);
        }
    }

    private void SetAllSwitchesStraight()
    {
        foreach ( Switch sw in SwitchesToStraight )
        {
            sw.SetSwitchDirection (Switch.SwitchDir.Straight);
        }
    }

    public void DestroyRoute()
    {
        print ("Destroyed");
        TrafficLightOff ();
        AllTCInRouteOff ();        
    }

}
