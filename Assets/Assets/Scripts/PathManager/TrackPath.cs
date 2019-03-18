using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPath : Singleton<TrackPath> {    
    private BGCcMath[] trackList;
    [SerializeField]
    private BGCcMath[] orderedForwardTracklist;
    [SerializeField]
    private BGCcMath[] orderedBackwardTracklist;
    public List<BGCcMath> path;
    public BGCcMath nextTrack;
    public BGCcMath changedTrack;
    public int numInList;
    BGCcMath currentTrack;
    public int direction = 1;
    public bool isReady = false;

              

    public void GetTrackPath( BogeyPathScript bogey)
    {
        
        BGCcMath _mathTemp = bogey.mathTemp;
        StartCoroutine(GetTrackPathCoroutine(bogey, _mathTemp));
    }

    IEnumerator GetTrackPathCoroutine(BogeyPathScript _bogey, BGCcMath _current)
    {        
        List<BGCcMath> pathOwn = new List<BGCcMath>();
        pathOwn.Add(_current);
        int i = 0;
        while (i != pathOwn.Count)
        {
            bool counting = true;
            foreach (BGCcMath item in trackList)
            {
                
                if (!item.isActiveAndEnabled)
                    continue;
                if (!pathOwn.Contains(item))
                {

                    if (item.Curve.Points.First().PositionWorld == pathOwn[i].Curve.Points.Last().PositionWorld)
                    {
                        pathOwn.Insert(pathOwn.IndexOf(pathOwn[i]) + 1, item);
                        i = 0;
                        counting = false;
                        break;
                    }
                    if (item.Curve.Points.Last().PositionWorld == pathOwn[i].Curve.Points.First().PositionWorld)
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
        _bogey.ownTrackPath = pathOwn;
        _bogey.rollingStock.ownTrackPath = pathOwn;
    }


    public BGCcMath GetNextTrack(BGCcMath _current, List<BGCcMath> _path)
    {
        int num = _path.IndexOf(_current) + 1;
        if (num < _path.Count)
            return _path[num];
        else
            return null;
    }
    public BGCcMath GetPrevTrack(BGCcMath _current, List<BGCcMath> _path)
    {
        int num = _path.IndexOf(_current) - 1;
        if (num >= 0)
            return _path[num];
        else
            return null;
    }

    private void Awake()
    {
        trackList = FindObjectsOfType<BGCcMath>();
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
orderedForwardTracklist = trackList;
   for (int i = 0; i<orderedForwardTracklist.Length - 1; i++)
   {
       for (int j = i + 1; j<trackList.Length; j++)
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
   orderedBackwardTracklist = trackList;
   for (int i = 0; i<orderedBackwardTracklist.Length - 1; i++)
   {
       for (int j = i + 1; j<trackList.Length; j++)
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
