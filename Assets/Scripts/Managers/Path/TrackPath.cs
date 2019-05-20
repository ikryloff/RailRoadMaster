using System.Collections;
using System.Collections.Generic;

public class TrackPath : Singleton<TrackPath>, IManageable
{

    public TrackPathUnit [] TrackList { get; private set; }

    public static int PathMade = 0;

    public float pathLength;
    private TrackPathUnit currentTrack;

    public void Init()
    {
        TrackList = FindObjectsOfType<TrackPathUnit> ();
        TrackPathUnitInit ();
        SetClosePaths ();
    }


    public void GetTrackPath( RollingStock car )
    {
        StartCoroutine (GetTrackPathCoroutine (car));
    }

    IEnumerator GetTrackPathCoroutine( RollingStock car )
    {
        // if we change swithces in indication mode
        if ( UIManager.Instance.IsIndicate )
        {
            UIManager.Instance.TurnIndicationOff ();
            UIManager.Instance.IsIndicate = true;
        }
            
        PathMade++;
        print ("Made path# " + PathMade);
        SetEachPathClosePaths ();
        TrackPathUnit currentTrack = car.OwnTrack;
        List<TrackPathUnit> pathPrevious = new List<TrackPathUnit> ();
        List<TrackPathUnit> pathOwn = new List<TrackPathUnit> ();
        TrackPathUnit tempLeft = currentTrack.LeftTrackPathUnit;
        TrackPathUnit tempRight = currentTrack.RightTrackPathUnit;
        pathPrevious = car.OwnPath;
        pathOwn.Add (currentTrack);

        while ( tempLeft != null || tempRight != null )
        {
            if ( tempLeft != null )
            {
                pathOwn.Insert (0, tempLeft);
                tempLeft = tempLeft.LeftTrackPathUnit;
            }
            if ( tempRight != null )
            {
                pathOwn.Add (tempRight);
                tempRight = tempRight.RightTrackPathUnit;
            }

            yield return null;
        }
        car.SetPathToRS (pathOwn);
        PathMade--;
        // if all paths of all compositions are made
        if ( PathMade == 0 )
        {
            EventManager.PathUpdated ();
            // if we turn swithces in indication mode
            if( UIManager.Instance.IsIndicate )
                UIManager.Instance.TurnIndicationOn ();         
        }
    }



    private void TrackPathUnitInit()
    {
        foreach ( TrackPathUnit item in TrackList )
        {
            item.Init ();
        }
    }


    public TrackPathUnit GetNextTrack( TrackPathUnit _current, List<TrackPathUnit> _path )
    {
        return _current.RightTrackPathUnit;
    }

    public TrackPathUnit GetPrevTrack( TrackPathUnit _current, List<TrackPathUnit> _path )
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
        foreach ( TrackPathUnit track in TrackList )
        {
            track.SetOwnClosePaths ();
        }
    }

    public void OnStart()
    {
        SetClosePaths ();
    }
}
