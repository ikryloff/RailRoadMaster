﻿using BansheeGz.BGSpline.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPathUnit : MonoBehaviour
{

    // Class connecting BGCCMATH Asset with TrackCircuit 

    public BGCcMath trackMath;
    public TrackCircuit TrackCircuit { get; set; }
    public Vector3 LeftPoint { get; private set; }
    public Vector3 RightPoint { get; private set; }
    public List<TrackPathUnit> LeftTrackPathUnits { get; set; }
    public List<TrackPathUnit> RightTrackPathUnits { get; set; }
    public TrackPathUnit LeftTrackPathUnit { get; set; }
    public TrackPathUnit RightTrackPathUnit { get; set; }
    public float PathLenght { get; private set; }


    public void Init()
    {
        trackMath = GetComponent<BGCcMath> ();
        LeftPoint = trackMath.Curve.Points.First ().PositionWorld;
        RightPoint = trackMath.Curve.Points.Last ().PositionWorld;
        LeftTrackPathUnits = new List<TrackPathUnit> ();
        RightTrackPathUnits = new List<TrackPathUnit> ();
        PathLenght = trackMath.GetDistance ();
    }


    public void SetOwnClosePaths()
    {
        LeftTrackPathUnit = null;
        RightTrackPathUnit = null;
        foreach ( TrackPathUnit item in LeftTrackPathUnits )
        {
            if ( item.isActiveAndEnabled )
            {
                LeftTrackPathUnit = item;
            }

        }

        foreach ( TrackPathUnit item in RightTrackPathUnits )
        {
            if ( item.isActiveAndEnabled )
            {
                RightTrackPathUnit = item;
            }
        }
    }

}
