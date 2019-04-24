using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPath : Singleton<TrackPath>, IManageable {    
    
    public TrackPathUnit[] TrackList { get; private set; }
    
    public float pathLength;

   
    public void Init()
    {
        TrackList = FindObjectsOfType<TrackPathUnit>();
        TrackPathUnitInit();
        SetClosePaths();
    }


    public void GetTrackPath( MovableObject movable)
    {
        
        CleanAndLockPreviousPath(movable.OwnPath);
        StartCoroutine(GetTrackPathCoroutine(movable));
        
        
    }

    private void CleanAndLockPreviousPath(List<TrackPathUnit> ownTrackPath)
    {
      
        if(ownTrackPath != null)
        {
            /*
            foreach (TrackPathUnit item in ownTrackPath)
            {
                item.TrackCircuit.isInUse = true;
                if (item.TrackCircuit.isSwitch)
                    item.TrackCircuit.switchTrack.isSwitchInUse = true;
            }
            */
        }
      
    }

    private void UnLockPreviousPath(List<TrackPathUnit> ownTrackPath)
    {
        /*
        if (ownTrackPath != null)
        {
            foreach (TrackPathUnit item in ownTrackPath)
            {
                item.TrackCircuit.isInUse = false;
                if (item.TrackCircuit.isSwitch)
                    item.TrackCircuit.switchTrack.isSwitchInUse = false;
            }
        }
        */
    }
       
    IEnumerator GetTrackPathCoroutine(MovableObject movable)
    {
        SetEachPathClosePaths();
        TrackPathUnit currentTrack = movable.OwnTrack;
        List<TrackPathUnit> pathPrevious = new List<TrackPathUnit>();
        pathPrevious = movable.OwnPath;
        List<TrackPathUnit> pathOwn = new List<TrackPathUnit>();
        TrackPathUnit tempLeft = currentTrack.LeftTrackPathUnit;
        TrackPathUnit tempRight = currentTrack.RightTrackPathUnit;
        pathOwn.Add(currentTrack);
        
        while (tempLeft != null || tempRight != null)
        {
            if(tempLeft != null)
            {
                pathOwn.Insert(0, tempLeft);
                tempLeft = tempLeft.LeftTrackPathUnit;
            }
            if (tempRight != null)
            {
                pathOwn.Add(tempRight);
                tempRight = tempRight.RightTrackPathUnit;
            }

            yield return new WaitForFixedUpdate();
        }
        movable.OwnPath = pathOwn;
        //movable.rollingStock.pathLength = GetPathLength(pathOwn);
        UnLockPreviousPath(pathPrevious);
    }


    private void TrackPathUnitInit()
    {
        foreach (TrackPathUnit item in TrackList)
        {
            item.Init();
        }
    }


    public TrackPathUnit GetNextTrack(TrackPathUnit _current, List<TrackPathUnit> _path)
    {
        return _current.RightTrackPathUnit;
    }

    public TrackPathUnit GetPrevTrack(TrackPathUnit _current, List<TrackPathUnit> _path)
    {
        return _current.LeftTrackPathUnit;        
    }

    public float GetPathLength(List<TrackPathUnit > paths)
    {
        float length = 0;               
        if (paths != null)
        {
            foreach (TrackPathUnit item in paths)
            {
                length += item.trackMath.GetDistance(); 
            }
        }
        return length;
    }       
    
    

    // For each TPU set left and right TPunits Lists
    public void SetClosePaths()
    {
        foreach (TrackPathUnit track in TrackList)
        {
            foreach (TrackPathUnit _track in TrackList)
            {
                if(track.LeftPoint == _track.RightPoint)
                {
                    track.LeftTrackPathUnits.Add(_track);
                }
                if (track.RightPoint == _track.LeftPoint)
                {
                    track.RightTrackPathUnits.Add(_track);
                }
            }
        }

    }
    // For each TPU set left and right TPunit wich is Active at the moment
    public void SetEachPathClosePaths()
    {
        foreach (TrackPathUnit track in TrackList)
        {
            track.SetOwnClosePaths();
        }
    }

    public void OnStart()
    {
        SetClosePaths();
    }
}
