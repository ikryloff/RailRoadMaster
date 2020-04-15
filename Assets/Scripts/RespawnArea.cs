using UnityEngine;

public class RespawnArea : MonoBehaviour
{

    private TrackCircuit trackCircuit;
    private RollingStock [] cars;
    private CarsHolder carsHolder;
    private int side;
    // respawn position
    private int resp;
    

    private void Awake()
    {
        trackCircuit = GetComponent<TrackCircuit> ();
        carsHolder = FindObjectOfType<CarsHolder> ();
        EventManager.onMinutePassed += CheckCars;
        
    }

    private void Start()
    {
        // side -1 - Left, side 1- Right
        side = trackCircuit.paths [0].name == Constants.TRACK_I_LEFT ? -1 : 1;
        resp = side == -1 ? Constants.RESP_LEFT_PLAYER : Constants.RESP_RIGHT_PLAYER;
    }


    public void CheckCars()
    {
        if ( trackCircuit.HasCarPresence )
        {
            for ( int i = 0; i < trackCircuit.Bogeys.Count; i++ )
            {
                Bogey bogey = trackCircuit.Bogeys [i];
                if ( bogey.IsRightBogey )
                {
                    if( 
                        (side * bogey.RollingStock.OwnPosition > side * resp) 
                      )
                    {
                        carsHolder.PutOneCarOnBackTrack (bogey.RollingStock);
                    }
                }

            }
        }
    }

}
