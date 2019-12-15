using UnityEngine;

public class CarsHolder : MonoBehaviour
{
    private RollingStock [] cars;
    private TrackCircuit tempTC;


    void Start()
    {
        cars = FindObjectsOfType<RollingStock> ();
        print (GetCar (8701));
        SetCarsPosition (8701, "PathTr4", 150);
        SetCarsPosition (8888, "PathTr3", 150, 6135);
    }


    public RollingStock GetCar( int num )
    {
        for ( int i = 0; i < cars.Length; i++ )
        {
            if ( cars [i].Number == num )
                return cars [i];
        }
        return null;
    }

    public void SetCarsPosition( int rsNum, string trackName, float position, int rightCarNum = 0 )
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

        if ( rightCarNum != 0 )
        {
            RollingStock rightCar = GetCar (rightCarNum);

            //release prev TC
            tempTC = rightCar.OwnTrackCircuit;

            rightCar.OwnTrack = rs.OwnTrack;
            rightCar.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;

            ReleaseStartTracks (rightCar, tempTC);

            rs.RSConnection.RightCar = rightCar.RSConnection;
            rightCar.ResetBogeys ();
        }

        EventManager.CompositionChanged ();

    }

    private void ReleaseStartTracks(RollingStock _rs, TrackCircuit _tc )
    {
        _tc.RemoveCars (_rs);
        _tc.RemoveCars (_rs.BogeyLeft);
        _tc.RemoveCars (_rs.BogeyRight);
    }

    
}
