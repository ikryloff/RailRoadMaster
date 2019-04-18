using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPath : Singleton<TrackPath>, IManageable {    
    
    [SerializeField]    
    public TrackPathUnit[] trackList;
    public BGCcMath nextTrack;
    public BGCcMath changedTrack;
    public Engine engine;

    public float pathLength;

   

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




    public TrackPathUnit GetNextTrack(TrackPathUnit _current, List<TrackPathUnit> _path)
    {
        int num = _path.IndexOf(_current) + 1;
        if (num < _path.Count)
            return _path[num];
        else
            return null;
    }
    public TrackPathUnit GetPrevTrack(TrackPathUnit _current, List<TrackPathUnit> _path)
    {
        int num = _path.IndexOf(_current) - 1;
        if (num >= 0)
            return _path[num];
        else
            return null;
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
    
    
    public void Init()
    {
        trackList = FindObjectsOfType<TrackPathUnit>();
        engine = FindObjectOfType<Engine>();
        foreach (TrackPathUnit item in trackList)
        {
            item.Init();
        }
        SetClosePaths();
    }

    // For each TPU set left and right TPunits Lists
    public void SetClosePaths()
    {
        foreach (TrackPathUnit track in trackList)
        {
            foreach (TrackPathUnit _track in trackList)
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
        foreach (TrackPathUnit track in trackList)
        {
            track.SetOwnClosePaths();
        }
    }
}
