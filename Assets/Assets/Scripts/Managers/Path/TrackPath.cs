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
            foreach (TrackPathUnit item in ownTrackPath)
            {
                item.TrackCircuit.isInUse = true;
                if (item.TrackCircuit.isSwitch)
                    item.TrackCircuit.switchTrack.isSwitchInUse = true;
            }
        }
      
    }

    private void UnLockPreviousPath(List<TrackPathUnit> ownTrackPath)
    {
        if (ownTrackPath != null)
        {
            foreach (TrackPathUnit item in ownTrackPath)
            {
                item.TrackCircuit.isInUse = false;
                if (item.TrackCircuit.isSwitch)
                    item.TrackCircuit.switchTrack.isSwitchInUse = false;
            }
        }
    }

    IEnumerator GetTrackPathCoroutine(MovableObject movable )
    {
        TrackPathUnit _current = movable.OwnTrack;
        List<TrackPathUnit> pathPrevious = new List<TrackPathUnit>();
        pathPrevious = movable.OwnPath;
        List<TrackPathUnit> pathOwn = new List<TrackPathUnit>();
        pathOwn.Add(_current);
        int i = 0;
        while (i != pathOwn.Count)
        {
            bool counting = true;
            foreach (TrackPathUnit item in trackList)
            {
                
                if (!item.trackMath.isActiveAndEnabled)
                    continue;
                if (!pathOwn.Contains(item))
                {

                    if (item.trackMath.Curve.Points.First().PositionWorld == pathOwn[i].trackMath.Curve.Points.Last().PositionWorld)
                    {
                        pathOwn.Insert(pathOwn.IndexOf(pathOwn[i]) + 1, item);                        
                        i = 0;                        
                        counting = false;
                        break;
                    }
                    if (item.trackMath.Curve.Points.Last().PositionWorld == pathOwn[i].trackMath.Curve.Points.First().PositionWorld)
                    {
                        pathOwn.Insert(pathOwn.IndexOf(pathOwn[i]), item);                        
                        i = 0;                        
                        counting = false;
                        break;
                    }
                }
               
            }
            if (counting)
                i++;
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
}
