﻿using System;
using UnityEngine;

public class CarsHolder : MonoBehaviour, IManageable
{
    public RollingStock [] Cars { get; set; }
    public Engine [] Engines { get; set; }
    public RSConnection [] Connections { get; set; }
    private TrackCircuit tempTC;

    public void Init()
    {
        Cars = FindObjectsOfType<RollingStock> ();
        Engines = FindObjectsOfType<Engine> ();
        Connections = FindObjectsOfType<RSConnection> ();
    }

    

    public void OnStart()
    {
        SetCarsPosition (8701, "PathTr10", 60);
        SetCarsPosition (8888, "PathTr10", 200);
        SetCarsPosition (2140, "PathTr9", 200);
        SetCarsPosition (6135, "PathTr3", 60, new int [] { 7522, 7508, 7143, 7445, 7267, 6548 });
        SetCarsPosition (114, "PathTrI_N", 200, new int [] { 115, 116 });
    }

    public void OnUpdate()
    {
        RunEngines ();
    }

    private void RunEngines()
    {
        for ( int i = 0; i < Engines.Length; i++ )
        {
            Engines [i].RunEngineAction ();
        }
    }

    private void GetTrackPathForAllRS()
    {
        foreach ( RollingStock item in Cars )
        {
            TrackPath.Instance.GetTrackPath (item);
        }
    }

    public RollingStock GetCar( int num )
    {
        for ( int i = 0; i < Cars.Length; i++ )
        {
            if ( Cars [i].Number == num )
                return Cars [i];
        }
        return null;
    }
    //for group
    private void SetCarsPosition( int rsNum, string trackName, float position, int [] rightCarsNum )
    {
        RollingStock rs = GetCar (rsNum);

        //release prev TC 
        tempTC = rs.OwnTrackCircuit;

        rs.OwnTrack = TrackPath.Instance.GetTrackPathUnitByName (trackName);
        rs.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;

        ReleaseStartTracks (rs, tempTC);

        //set bogeys the same trackpathunit        
        rs.OwnPosition = position;
        rs.ResetBogeys ();


        for ( int i = 0; i < rightCarsNum.Length; i++ )
        {
            if ( i > 0 )
            {
                rs = GetCar (rightCarsNum [i - 1]);
            }

            RollingStock rightCar = GetCar (rightCarsNum [i]);
            print (rs.name + " " + rightCar.name);
            //release prev TC
            tempTC = rightCar.OwnTrackCircuit;
            rightCar.OwnTrack = rs.OwnTrack;
            rightCar.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;
            ReleaseStartTracks (rightCar, tempTC);

            rs.RSConnection.MakeConnection (rightCar.RSConnection);
            rightCar.ResetBogeys ();
        }
        CompositionManager.Instance.UpdateCompositions ();
    }
    //for one car
    public void SetCarsPosition( int rsNum, string trackName, float position )
    {
        RollingStock rs = GetCar (rsNum);

        //release prev TC 
        tempTC = rs.OwnTrackCircuit;

        rs.OwnTrack = TrackPath.Instance.GetTrackPathUnitByName (trackName);
        rs.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;

        ReleaseStartTracks (rs, tempTC);

        //set bogeys the same trackpathunit        
        rs.OwnPosition = position;
        rs.ResetBogeys ();
        rs.RSComposition.CarComposition.Instantiate ();

        CompositionManager.Instance.UpdateCompositions ();
    }

    private void ReleaseStartTracks( RollingStock _rs, TrackCircuit _tc )
    {
        _tc.RemoveCars (_rs);
        _tc.RemoveCars (_rs.BogeyLeft);
        _tc.RemoveCars (_rs.BogeyRight);
    }


    public void UpdateConnections()
    {
        for ( int i = 0; i < Connections.Length; i++ )
        {
            Connections [i].OnUpdate ();
        }
    }

    public void UnablePassEngine(int rsNum )
    {
        Engine eng = GetCar (rsNum).Engine;
        eng.EngineRS.IsEngine = false;
        eng.TWatcher.enabled = false;
        eng.Inertia.enabled = false;
        eng.gameObject.tag = "RollingStock";
    }

    public void SetUnactiveRS(int rsNum )
    {
        GameObject rs = GetCar (rsNum).gameObject;
        rs.SetActive(false);
        CompositionManager.Instance.UpdateCompositions ();

    }

    public void SetActiveRS( int rsNum )
    {
        GameObject rs = GetCar (rsNum).gameObject;
        rs.SetActive (true);
        CompositionManager.Instance.UpdateCompositions ();

    }

}
