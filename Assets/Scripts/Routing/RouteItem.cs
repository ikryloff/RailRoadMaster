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
    public int RouteButton { get; set; }
   


    public void InstantiateRoute(int routeButton)
    {
        TargetTrack = TrackCircuits.Last ();
        RouteButton = routeButton;
        SetAllSwitchesStraight ();
        SetAllSwitchesTurn ();
        AllTCInRouteOn ();
        TrafficLightOn ();
        StartCoroutine (CheckStartingRoute ());
    }

    IEnumerator CheckStartingRoute()
    {
        while (true)
        {
            if( TrackCircuits.Any (t => t.HasCarPresence) )
                break;
            yield return null;
        }
        if ( !IsShunting )
        {
            TrafficLightOff ();
            //for all train passing through
            RouteLights [0].IsClosed = false;
        }
        StartCoroutine (CheckPassingRoute());
    }

    IEnumerator CheckPassingRoute()
    {
        StopCoroutine (CheckStartingRoute ());
        while ( TrackCircuits.Any (t => t != TargetTrack && t.HasCarPresence) )
        {
            yield return new WaitForSeconds(0.5f);
        }
        Route.Instance.DestroyRoute (RouteNumber);
    }


    private void TrafficLightOn()
    {
        //light TL with parameters
        RouteLights [0].LightOn (this);
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
        }
    }

    private void AllTCInRouteOff()
    {
        foreach ( TrackCircuit tc in TrackCircuits )
        {
            tc.IsInRoute = false;
        }
    }

    public bool CheckTrainRoute()
    {
        return TrackCircuits.All (t => !t.IsInRoute && !t.HasCarPresence);
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
        TrafficLightOff ();
        AllTCInRouteOff ();
        if ( RouteButton != -1 )
            FindObjectOfType<RoutePanelManager> ().GetRouteButtonByNumber (RouteButton).SetRouteOff ();
    }

}
