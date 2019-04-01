using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPath : Singleton<TrackPath> {    
    
    [SerializeField]    
    private TrackPathUnit[] trackList;
    public BGCcMath nextTrack;
    public BGCcMath changedTrack;

    
   
              

    public void GetTrackPath( BogeyPathScript bogey)
    {
        CleanAndLockPreviousPath(bogey.ownTrackPath);        
        StartCoroutine(GetTrackPathCoroutine(bogey));
    }

    private void CleanAndLockPreviousPath(List<TrackPathUnit> ownTrackPath)
    {
        foreach (TrackPathUnit item in ownTrackPath)
        {
            item.trackCircuit.isInPath = false;
            item.trackCircuit.isInUse = true;
            if (item.trackCircuit.isSwitch)
                item.trackCircuit.switchTrack.isSwitchInUse = true;
        }
    }

    private void UnLockPreviousPath(List<TrackPathUnit> ownTrackPath)
    {
        foreach (TrackPathUnit item in ownTrackPath)
        {
           item.trackCircuit.isInUse = false;
            if (item.trackCircuit.isSwitch)
             item.trackCircuit.switchTrack.isSwitchInUse = false;
        }
    }

    IEnumerator GetTrackPathCoroutine(BogeyPathScript _bogey )
    {
        TrackPathUnit _current = _bogey.mathTemp;
        List<TrackPathUnit> pathPrevious = new List<TrackPathUnit>();
        pathPrevious = _bogey.ownTrackPath;
        List<TrackPathUnit> pathOwn = new List<TrackPathUnit>();
        pathOwn.Add(_current);
        int i = 0;
        while (i != pathOwn.Count)
        {
            bool counting = true;
            foreach (TrackPathUnit item in trackList)
            {
                
                if (!item.math.isActiveAndEnabled)
                    continue;
                if (!pathOwn.Contains(item))
                {

                    if (item.math.Curve.Points.First().PositionWorld == pathOwn[i].math.Curve.Points.Last().PositionWorld)
                    {
                        pathOwn.Insert(pathOwn.IndexOf(pathOwn[i]) + 1, item);
                        pathOwn[i].trackCircuit.isInPath = true;
                        i = 0;
                        counting = false;
                        break;
                    }
                    if (item.math.Curve.Points.Last().PositionWorld == pathOwn[i].math.Curve.Points.First().PositionWorld)
                    {
                        pathOwn.Insert(pathOwn.IndexOf(pathOwn[i]), item);
                        pathOwn[i].trackCircuit.isInPath = true;
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
        _bogey.ownTrackPath = pathOwn;
        _bogey.otherBogey.ownTrackPath = pathOwn;
        _bogey.rollingStock.ownTrackPath = pathOwn;
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

    private void Awake()
    {
        trackList = FindObjectsOfType<TrackPathUnit>();
    }

    private void Start()
    {
      
    }


    private void Update()
    {       
        
    }

   


    /*
BGCcMath temp;
// ordered forward track list
orderedForwardTracklist = pathList;
   for (int i = 0; i<orderedForwardTracklist.Length - 1; i++)
   {
       for (int j = i + 1; j<pathList.Length; j++)
       {
           // if one Path point is more left than other path
           if(orderedForwardTracklist[i].Curve.Points.First().PositionWorld.x > orderedForwardTracklist[j].Curve.Points.First().PositionWorld.x)
           {
               temp = orderedForwardTracklist[i];
               orderedForwardTracklist[i] = orderedForwardTracklist[j];
               orderedForwardTracklist[j] = temp;
           }
       }
   }

   // ordered backward track list
   orderedBackwardTracklist = pathList;
   for (int i = 0; i<orderedBackwardTracklist.Length - 1; i++)
   {
       for (int j = i + 1; j<pathList.Length; j++)
       {
           // if one Path point is more left than other path
           if (orderedBackwardTracklist[i].Curve.Points.Last().PositionWorld.x > orderedBackwardTracklist[j].Curve.Points.Last().PositionWorld.x)
           {
               temp = orderedBackwardTracklist[i];
               orderedBackwardTracklist[i] = orderedBackwardTracklist[j];
               orderedBackwardTracklist[j] = temp;
           }
       }
   }

*/
}
