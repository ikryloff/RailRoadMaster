using System;
using System.Collections;
using System.Collections.Generic;

public class TrackPath : Singleton<TrackPath>, IManageable
{
    
    int count;
    int countLeft;
    int countRight;
    public TrackPathUnit [] TrackList { get; private set; }

    public static int PathMade = 0;

    public float pathLength;
   
    TrackPathUnit tempLeft;
    TrackPathUnit tempRight;

    public void Init()
    {
        TrackList = FindObjectsOfType<TrackPathUnit> ();
        TrackPathUnitInit ();
        SetClosePaths ();
    }

    public void GetTrackPath( RollingStock car )
    {
        PathMade++;
        ClearPath (car);
        // if we change swithces in indication mode
        if ( IndicationManager.Instance.IsPathIndicate )
        {
            IndicationManager.Instance.TurnPathIndicationOff ();
            IndicationManager.Instance.IsPathIndicate = true;
        }

        SetEachPathClosePaths ();
        TrackPathUnit currentTrack = car.OwnTrack;
        tempLeft = currentTrack.LeftTrackPathUnit;
        tempRight = currentTrack.RightTrackPathUnit;

        count = Constants.PATHS_NUM / 2;
        countLeft = count;
        countRight = count;
        TrackPathUnit [] pathOwn = new TrackPathUnit [Constants.PATHS_NUM];
        pathOwn [count] =  currentTrack;

        while ( tempLeft != null || tempRight != null )
        {
            if ( tempLeft != null )
            {
                pathOwn[countLeft - 1]  =  tempLeft;
                countLeft--;
                tempLeft = tempLeft.LeftTrackPathUnit;
            }
            if ( tempRight != null )
            {
                pathOwn [countRight + 1] = tempRight;
                countRight++;
                tempRight = tempRight.RightTrackPathUnit;
            }            
        }
        car.SetPathToRS (pathOwn, countLeft, countRight);
        PathMade--;
        // if all paths of all compositions are made
        if ( PathMade == 0 )
        {
            print ("PathMade");           
            EventManager.PathUpdated ();
        }
    }

    private void ClearPath( RollingStock car )
    {
        if ( car.OwnPath != null )  
        {
            for ( int i = car.FirstTrackIndex; i <= car.LastTrackIndex; i++ )
            {
                car.OwnPath [i] = null;
            }
        }
    }


    private void TrackPathUnitInit()
    {
        foreach ( TrackPathUnit item in TrackList )
        {
            item.Init ();
        }
    }

    public TrackPathUnit GetTrackPathUnitByName(string trName)
    {
        for ( int i = 0; i < TrackList.Length; i++ )
        {
            if ( TrackList [i].name.Equals (trName) )
                return TrackList [i];
        }
        return null;
    }


    public TrackPathUnit GetNextTrack( TrackPathUnit _current, TrackPathUnit [] _path )
    {
        return _current.RightTrackPathUnit;
    }

    public TrackPathUnit GetPrevTrack( TrackPathUnit _current, TrackPathUnit [] _path )
    {
        return _current.LeftTrackPathUnit;
    }

    public float GetPathLength( List<TrackPathUnit> paths )
    {
        float length = 0;
        if ( paths != null )
        {
            foreach ( TrackPathUnit item in paths )
            {
                length += item.trackMath.GetDistance ();
            }
        }
        return length;
    }



    // For each TPU set left and right TPunits Lists
    public void SetClosePaths()
    {
        foreach ( TrackPathUnit track in TrackList )
        {
            foreach ( TrackPathUnit _track in TrackList )
            {
                if ( track.LeftPoint == _track.RightPoint )
                {
                    track.LeftTrackPathUnits.Add (_track);
                }
                if ( track.RightPoint == _track.LeftPoint )
                {
                    track.RightTrackPathUnits.Add (_track);
                }
            }
        }

    }
    // For each TPU set left and right TPunit wich is Active at the moment
    public void SetEachPathClosePaths()
    {
        for ( int i = 0; i < TrackList.Length; i++ )
        {
            TrackPathUnit track = TrackList [i];
            track.SetOwnClosePaths ();
        }
    }

    public void OnStart()
    {
        SetClosePaths ();
    }
}
