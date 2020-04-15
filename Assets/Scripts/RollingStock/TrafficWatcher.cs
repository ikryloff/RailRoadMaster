using System;
using System.Collections.Generic;
using UnityEngine;

public class TrafficWatcher : MonoBehaviour
{

    private Engine engine;
    private RSConnection rSConnection;
    private Transform engineT;
    private RollingStock car;
    private RSComposition rsComposition;
    private TrafficLight leftTL;
    private TrafficLight rightTL;
    private float tempDistLeft;
    private float tempDistRight;
    private float distRight;
    private float distLeft;
    private float offset;
    private TrackCircuit tempTrack;   
    TrafficLight tl;
    TrackPathUnit currentPath;

    Transform leftCarT;
    Transform rightCarT;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        rSConnection = GetComponent<RSConnection> ();
        rsComposition = GetComponent<RSComposition> ();
        engineT = GetComponent<Transform> ();
        car = GetComponent<RollingStock> ();        

    }
    private void Start()
    {
        EventManager.onSignalChanged += GetTrafficLights;
        EventManager.onTrainSignalChanged += GetTrafficLights;
        EventManager.onPlayerUsedThrottle += GetTrafficLights;        
    }


    public void OnUpdate()
    {
        if ( !rsComposition.CarComposition.IsOutside )
        {
            WatchSignals ();
        }
    }

    private void WatchSignals()
    {
        if ( engine.EngineStep != 0 )
        {
            WatchLeftTL ();
            WatchRightTL ();
        }
    }   

    private void WatchLeftTL()
    {
        if ( leftTL != null )
        {
            if ( !car.GetCoupledLeft () )
            {
                distLeft = GetDistanceToSignal (leftTL);

                if(distLeft <= 0 )
                {
                    GetTrafficLights ();
                    return;
                }

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
                else if ( distLeft <= 150 && distLeft > 5 )
                {
                    if ( engine.InstructionsHandler < -1 )
                        engine.InstructionsHandler = -1;
                }
                else if ( distLeft <= 5 && distLeft > 0)
                {
                    if ( engine.InstructionsHandler < 0 )
                    {
                        engine.HandlerZero ();
                    }
                }
            }         

        }
    }

    private void WatchRightTL()
    {
        if ( rightTL != null )
        {
            if ( !car.GetCoupledRight () )
            {
                distRight = GetDistanceToSignal (rightTL);

                if ( distRight <= 0 )
                {
                    GetTrafficLights ();
                    return;
                }

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
                else if ( distRight <= 150 && distRight > 5 )
                {
                    if ( engine.InstructionsHandler > 1 )
                        engine.InstructionsHandler = 1;
                }
                else if ( distRight <= 5 && distRight > 0)
                {
                    if ( engine.InstructionsHandler > 0 )
                        engine.HandlerZero ();
                }
            }          

        }
    }

   
    public void GetTrafficLights()
    {
        if ( rsComposition.CarComposition.IsOutside )
            return;
        GetLeftTrafficLight ();
        GetRightTrafficLight ();       
       
    }



    public void GetLeftTrafficLight()
    {
        leftTL = null;
        //get comp path
        TrackPathUnit [] paths = rsComposition.CarComposition.MainCar.OwnPath;
        currentPath = rsComposition.CarComposition.MainCar.OwnTrack;
        int start = rsComposition.CarComposition.MainCar.FirstTrackIndex;
        int stop = Array.IndexOf (paths, currentPath);
        for ( int i = stop; i >= start; i-- )
        {
            TrafficLight tl = paths [i].TrackCircuit.TrackLights [0];
            if ( tl && tl.IsClosed )
            {
                leftTL = tl;
                return;
            }
        }

    }

    public void GetRightTrafficLight()
    {

        rightTL = null;
        //get comp path
        TrackPathUnit [] paths = rsComposition.CarComposition.MainCar.OwnPath;
        currentPath = rsComposition.CarComposition.MainCar.OwnTrack;
        int stop = rsComposition.CarComposition.MainCar.LastTrackIndex;
        int start = Array.IndexOf (paths, currentPath);
        for ( int i = start; i <= stop; i++ )
        {
            TrafficLight tl = paths [i].TrackCircuit.TrackLights [1];
            if ( tl && tl.IsClosed )
            {
                rightTL = tl;
                return;
            }
        }
    }



    private float GetDistanceToSignal( TrafficLight tl )
    {
        if(tl.SignalDirection == -1)
            return engineT.position.x - tl.GetPositionX - Constants.RS_CAR_OFFSET;
        else
            return tl.GetPositionX - engineT.position.x - Constants.RS_CAR_OFFSET;
    }

}
