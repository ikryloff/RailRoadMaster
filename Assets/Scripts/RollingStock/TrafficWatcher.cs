using System.Collections.Generic;
using UnityEngine;

public class TrafficWatcher : MonoBehaviour
{

    private Engine engine;
    private RSConnection rSConnection;
    private Transform engineT;
    private RollingStock car;
    private TrafficLight leftTL;
    private TrafficLight rightTL;
    private List<TrafficLight> listTLs;
    private float tempDistLeft;
    private float tempDistRight;
    private float distRight;
    private float distLeft;
    private TrackCircuit tempTrack;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        rSConnection = GetComponent<RSConnection> ();
        engineT = GetComponent<Transform> ();
        car = GetComponent<RollingStock> ();
        //make list of signals in path
        listTLs = new List<TrafficLight> ();
    }
    private void Start()
    {
        InvokeRepeating ("GetAllTLs", 2, 2);
    }

    private void Update()
    {
        WatchSignals ();
    }

    private void WatchSignals()
    {
        if ( car.Translation != 0 )
        {
            WatchLeftTL ();
            WatchRightTL ();
        }
    }

    private void WatchLeftTL()
    {
        if ( leftTL != null && !car.GetCoupledLeft () )
        {
            distLeft = GetDistanceToSignal (leftTL);
            if ( distLeft < 1500 && distLeft > 1100 )
            {
                if ( engine.InstructionsHandler < -5 )
                    engine.InstructionsHandler = -5;
            }

            else if ( distLeft < 1100 && distLeft > 800 )
            {
                if ( engine.InstructionsHandler < -4 )
                    engine.InstructionsHandler = -4;
            }

            else if ( distLeft <= 800 && distLeft > 300 )
            {
                if ( engine.InstructionsHandler < -3 )
                    engine.InstructionsHandler = -3;
            }
            else if ( distLeft <= 300 && distLeft > 150 )
            {
                if ( engine.InstructionsHandler < -2 )
                    engine.InstructionsHandler = -2;
            }
            else if ( distLeft <= 150 && distLeft > 60 )
            {
                if ( engine.InstructionsHandler < -1 )
                    engine.InstructionsHandler = -1;
            }
            else if ( distLeft <= 60 )
            {
                if ( engine.InstructionsHandler < 0 )
                    engine.HandlerZero ();
            }
        }
    }

    private void WatchRightTL()
    {
        if ( rightTL != null && !car.GetCoupledRight () )
        {
            distRight = GetDistanceToSignal (rightTL);
            if ( distRight < 1500 && distRight > 800 )
            {
                if ( engine.InstructionsHandler > 5 )
                    engine.InstructionsHandler = 5;
            }
            else if ( distRight < 1100 && distRight > 800 )
            {
                if ( engine.InstructionsHandler > 4 )
                    engine.InstructionsHandler = 4;
            }
            else if ( distRight <= 800 && distRight > 300 )
            {
                if ( engine.InstructionsHandler > 3 )
                    engine.InstructionsHandler = 3;
            }

            else if ( distRight <= 300 && distRight > 150 )
            {
                if ( engine.InstructionsHandler > 2 )
                    engine.InstructionsHandler = 2;
            }
            else if ( distRight <= 150 && distRight > 60 )
            {
                if ( engine.InstructionsHandler > 1 )
                    engine.InstructionsHandler = 1;
            }
            else if ( distRight <= 60 )
            {
                if ( engine.InstructionsHandler > 0 )
                    engine.HandlerZero ();
            }
        }
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
            if ( leftTL != null && !listTLs.Contains (leftTL) )
                listTLs.Add (leftTL);
            if ( rightTL != null && !listTLs.Contains (rightTL))
                listTLs.Add (rightTL);
        }
        //finding nearest
        tempDistLeft = 1000000;
        tempDistRight = 1000000;
        //clear tls 
        leftTL = null;
        rightTL = null;
        foreach ( TrafficLight tl in listTLs )
        {
            if ( tl.IsClosed )
            {
                if ( tl.SignalDirection < 0 && engineT.position.x > tl.GetPositionX )
                {
                    if ( GetDistanceToSignal (tl) < tempDistLeft )
                    {
                        tempDistLeft = GetDistanceToSignal (tl);
                        leftTL = tl;
                    }

                }
                else if ( tl.SignalDirection > 0 && engineT.position.x < tl.GetPositionX )
                {
                    if ( GetDistanceToSignal (tl) < tempDistRight )
                    {
                        tempDistRight = GetDistanceToSignal (tl);
                        rightTL = tl;
                    }
                }
            }            
          
        }
    }

    private float GetDistanceToSignal( TrafficLight tl )
    {
        return Mathf.Abs (engineT.position.x - tl.GetPositionX);
    }


}
