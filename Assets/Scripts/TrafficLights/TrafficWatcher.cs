using System.Collections.Generic;
using UnityEngine;

public class TrafficWatcher : MonoBehaviour
{

    private Engine engine;
    private Transform engineT;
    private RollingStock car;
    private TrafficLight leftTL;
    private TrafficLight rightTL;
    private List<TrafficLight> listTLs;
    private float tempDistLeft;
    private float tempDistRight;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        engineT = GetComponent<Transform> ();
        car = GetComponent<RollingStock> ();
        //make list of signals in path
        listTLs = new List<TrafficLight> ();
    }
    private void Start()
    {
        Invoke ("GetAllTLs", 2);
    }

    public void GetAllTLs()
    {
        //clear list 
        listTLs.Clear (); 
        //gettting list of signals in path
        foreach ( TrackPathUnit tpu in car.OwnPath )
        {
            //just for temp operations
            leftTL = tpu.TrackCircuit.TrackLights [0];
            rightTL = tpu.TrackCircuit.TrackLights [1];
            if ( leftTL != null )
                listTLs.Add (leftTL);
            if ( rightTL != null )
                listTLs.Add (rightTL);
        }
        //finding nearest
        tempDistLeft = 1000000;
        tempDistRight = -1000000;
        foreach ( TrafficLight tl in listTLs)
        {
            if( tl.SignalDirection < 0 && GetDistanceToSignal(tl) > 0 )
            {
                if(GetDistanceToSignal(tl) < tempDistLeft )
                {
                    tempDistLeft = GetDistanceToSignal (tl);
                    leftTL = tl;
                }

            }
            else if ( tl.SignalDirection > 0 && GetDistanceToSignal (tl) < 0 )
            {
                if ( GetDistanceToSignal (tl) > tempDistLeft )
                {
                    tempDistRight = GetDistanceToSignal (tl);
                    rightTL = tl;
                }
            }
        }
    }

    private float GetDistanceToSignal(TrafficLight tl )
    {
        return engineT.position.x - tl.GetPositionX;
    }

}
