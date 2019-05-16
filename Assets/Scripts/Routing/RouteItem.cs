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


    public void InstantiateRoute()
    {
        bool temp = false;
        if ( IsShunting && CheckShuntingRoute () )
            temp = true;
        else if ( !IsShunting && CheckTrainRoute () )
            temp = true;
        if ( temp )
        {
            SetAllSwitchesStraight ();
            SetAllSwitchesTurn ();
            AllTCInRouteOn ();
            TrafficLightOn ();
        }
        else
            Debug.Log ("Wrong Route");
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
            tc.isInRoute = true;
        }
    }

    private void AllTCInRouteOff()
    {
        foreach ( TrackCircuit tc in TrackCircuits )
        {
            tc.isInRoute = false;
        }
    }

    private bool CheckTrainRoute()
    {
        return TrackCircuits.All (t => !t.isInRoute && !t.HasCarPresence);
    }

    private bool CheckShuntingRoute()
    {
        if ( TrackCircuits.All (t => !t.isInRoute) ) // if all trackCircuits not in route
        {
            foreach ( Switch sw in SwitchesToStraight )
            {
                if ( sw.IsLockedByRS && !sw.IsSwitchStraight )
                    return false;
            }

            foreach ( Switch sw in SwitchesToTurn )
            {
                if ( sw.IsLockedByRS && sw.IsSwitchStraight )
                    return false;
            }
        }
        else
            return false;

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
    }

}
