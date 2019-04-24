using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RouteItem : MonoBehaviour
{
    public string Description;
    public string RouteName { get; set; }
    public Switch[] SwitchesToStraight { get; set; }
    public Switch[] SwitchesToTurn { get; set; }
    public TrackCircuit[] TrackCircuits { get; set; }
    public TrafficLight[] RouteLights { get; set; }
    public bool IsShunting { get; set; }
    public TrafficLight DependsOnSignal { get; set; }
    public bool isStraight { get; set; }


    public bool IsAnyTCStaysInRoute()
    {
      
        //if(SwitchesToStraight.All( u => !u.isLockedByRoute))

        foreach (TrackCircuit tc in TrackCircuits)
        {
            if (tc.isInRoute)
                return true;
        }
        return false;
    }

    public void InstantiateRoute()
    {
        if (!IsAnyTCStaysInRoute())
        {
            foreach (Switch sw in SwitchesToStraight)
            {
                sw.SetSwitchDirection(Switch.SwitchDir.Straight);
            }

            foreach (Switch sw in SwitchesToTurn)
            {
                sw.SetSwitchDirection(Switch.SwitchDir.Turn);
            }

            foreach (TrackCircuit tc in TrackCircuits)
            {
                tc.isInRoute = true;
            }
        }
        else
            Debug.Log("Is in Route");
    }

    public void DestroyRoute()
    {

    }

}
