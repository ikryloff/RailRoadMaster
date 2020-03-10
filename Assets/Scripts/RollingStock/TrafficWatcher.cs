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
    private float offset;
    private TrackCircuit tempTrack;
    TrackPathUnit tpu;
    List<TrackPathUnit> listTPU;
    TrafficLight tl;

    Transform leftCarT;
    Transform rightCarT;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        rSConnection = GetComponent<RSConnection> ();
        engineT = GetComponent<Transform> ();
        car = GetComponent<RollingStock> ();
        //make list of signals in path
        listTLs = new List<TrafficLight> ();
        listTPU = new List<TrackPathUnit> ();
        EventManager.onPathUpdated += GetAllTLs;
        EventManager.onSignalChanged += GetAllTLs;
        EventManager.onTrainSignalChanged += GetAllTLs;
        EventManager.onPlayerUsedThrottle += GetAllTLs;
        EventManager.onPlayerUsedThrottle += GetLastCars;
    }


    public void OnUpdate()
    {
        WatchSignals ();
    }

    private void WatchSignals()
    {
        if ( engine.EngineStep != 0 )
        {
            WatchLeftTL ();
            WatchRightTL ();
        }
        else if ( engine.IsPlayer && engine.EngineStep == 0 )
        {
            if ( leftTL != null )
            {
                UseAttention (engine, leftTL, distLeft);
            }
            if ( rightTL != null )
            {
                UseAttention (engine, rightTL, distRight);
            }
        }

    }

    private void GetLastCars()
    {
        leftCarT = engine.EngineRS.RSComposition.CarComposition.LeftCar.RSTransform;
        rightCarT = engine.EngineRS.RSComposition.CarComposition.RightCar.RSTransform;
    }

    private void WatchLeftTL()
    {
        if ( leftTL != null )
        {
            if ( !car.GetCoupledLeft () )
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
                    {
                        engine.HandlerZero ();
                    }
                }
            }
            else
            {
                if ( engine.IsPlayer )
                    distLeft = GetDistanceLastCarToLeftSignal (leftTL);
            }

            //show attention
            UseAttention (engine, leftTL, distLeft);
        }
    }

    private void WatchRightTL()
    {
        if ( rightTL != null )
        {
            if ( !car.GetCoupledRight () )
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
            else
            {
                if ( engine.IsPlayer )
                    distRight = GetDistanceLastCarToRightSignal (rightTL);
            }
            //show attention
            UseAttention (engine, rightTL, distRight);
        }
    }

    public void UseAttention( Engine eng, TrafficLight tl, float dist )
    {

        if ( eng.IsPlayer )
        {
            if ( eng.EngineStep == 0 )
            {
                if ( tl.Attention && tl.IsAttention )
                    tl.SetActiveAttention (false);
                return;
            }
            if ( dist <= 300 )
            {
                if ( tl.Attention && !tl.IsAttention )
                    tl.SetActiveAttention (true);
            }
            else
            {
                if ( tl.Attention && tl.IsAttention )
                    tl.SetActiveAttention (false);
            }
        }
        else
        {
            if ( tl.Attention && tl.IsAttention )
                tl.SetActiveAttention (false);
        }

    }

    public void GetAllTLs()
    {
        //clear Attentions
        if(leftTL && !leftTL.IsClosed )
        {
            if ( leftTL.Attention && leftTL.IsAttention )
                leftTL.SetActiveAttention (false);
        }

        if ( rightTL && !rightTL.IsClosed )
        {
            if ( rightTL.Attention && rightTL.IsAttention )
                rightTL.SetActiveAttention (false);
        }


        //clear list 
        listTLs.Clear ();
        if ( car.OwnPath != null )
        {
            //geting list of signals in path
            for ( int i = 0; i < car.OwnPath.Count; i++ )
            {
                tpu = car.OwnPath [i];
                //just for temp operations
                leftTL = tpu.TrackCircuit.TrackLights [0];
                rightTL = tpu.TrackCircuit.TrackLights [1];
                if ( leftTL != null && !listTLs.Contains (leftTL) )
                    listTLs.Add (leftTL);
                if ( rightTL != null && !listTLs.Contains (rightTL) )
                    listTLs.Add (rightTL);
            }
        }
        //finding nearest
        tempDistLeft = 1000000;
        tempDistRight = 1000000;
        //clear tls 
        leftTL = null;
        rightTL = null;
        for ( int i = 0; i < listTLs.Count; i++ )
        {
            tl = listTLs [i];
            if ( tl.IsClosed )
            {
                //when train pass througth signal he can take red signal
                if ( tl.IsClosedByTrain )
                    offset = 70f;
                else
                    offset = 0f;

                if ( tl.SignalDirection < 0 && engineT.position.x - offset > tl.GetPositionX )
                {
                    if ( GetDistanceToSignal (tl) < tempDistLeft )
                    {
                        tempDistLeft = GetDistanceToSignal (tl);
                        leftTL = tl;
                    }

                }
                else if ( tl.SignalDirection > 0 && engineT.position.x + offset < tl.GetPositionX )
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

    private float GetDistanceLastCarToLeftSignal( TrafficLight tl )
    {
        return Mathf.Abs (leftCarT.position.x - tl.GetPositionX);
    }

    private float GetDistanceLastCarToRightSignal( TrafficLight tl )
    {
        return Mathf.Abs (rightCarT.position.x - tl.GetPositionX);
    }


}
