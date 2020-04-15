using System;
using System.Collections.Generic;
using UnityEngine;

public class CarWatcher : MonoBehaviour
{

    private Engine engine;
    private Transform engineT;
    private RSConnection rSConnection;
    private Composition [] ownListComps;
    private Composition [] listComps;
    private Composition tempComp;
    private RollingStock tempLeftRS;
    private RollingStock tempRightRS;
    private RollingStock engineRS;
    private RSComposition rsComposition;
    private RollingStock leftCar;
    private RollingStock rightCar;
    private float tempDistLeft;
    private float tempDistRight;
    private float distRight;
    private float distLeft;
    private int count;


    private void Awake()
    {
        engine = GetComponent<Engine> ();
        rSConnection = GetComponent<RSConnection> ();
        engineT = GetComponent<Transform> ();
        engineRS = GetComponent<RollingStock> ();
        rsComposition = GetComponent<RSComposition> ();
    }

    private void Start()
    {
        ownListComps = new Composition [CompositionManager.Instance.Compositions.Length];
        listComps = CompositionManager.Instance.Compositions;
        EventManager.onPathUpdated += GetOwnListRS;
        EventManager.onPlayerUsedThrottle += GetOwnListRS;
    }

    private void Update()
    {
        if(!rsComposition.CarComposition.IsOutside)
            WatchCars ();
    }

    private void WatchCars()
    {
        if ( engine.EngineStep != 0 )
        {
            if(!rSConnection.LeftCar)
                WatchLeftCar ();
            if(!rSConnection.RightCar )
                WatchRightCar ();
        }
    }

    public void GetOwnListRS()
    {
        if ( rsComposition.CarComposition.IsOutside )
            return;
        ClearOwnListRS ();        
        count = 0;
        for ( int i = 0; i < listComps.Length; i++ )
        {
            tempComp = listComps [i];
            if ( tempComp == null)
                continue;
            if ( !tempComp.IsActive )
                continue;
            if ( tempComp.IsOutside )
                continue;
            if ( tempComp == rsComposition.CarComposition )
                continue;
            for ( int j = engineRS.FirstTrackIndex; j <= engineRS.LastTrackIndex; j++ )
            {
                if (engineRS.OwnPath[j] == tempComp.MainCar.OwnTrack )
                {
                    ownListComps [count] = tempComp;
                    count++;
                }
            }
            
        }

        leftCar = null;
        rightCar = null;
        tempDistLeft = 100000;
        tempDistRight = 100000;

        tempLeftRS = null;
        tempRightRS = null;

        for ( int i = 0; i < ownListComps.Length; i++ )
        {
            tempComp = ownListComps [i];
            if ( tempComp == null )
                return;
            tempLeftRS = tempComp.RightCar;
            tempRightRS = tempComp.LeftCar;

            // look for left car
            if ( !engineRS.GetCoupledLeft() && engineT.position.x > tempLeftRS.GetPositionX() )
            {
                if ( GetDistanceToCar (tempLeftRS) < tempDistLeft )
                {
                    tempDistLeft = GetDistanceToCar (tempLeftRS);
                    leftCar = tempLeftRS;
                }

            }
            else if ( !engineRS.GetCoupledRight () && engineT.position.x < tempRightRS.GetPositionX () )
            {
                if ( GetDistanceToCar (tempRightRS) < tempDistRight )
                {
                    tempDistRight = GetDistanceToCar (tempRightRS);
                    rightCar = tempRightRS;
                }
            }
        }
    }

    private void ClearOwnListRS()
    {
        for ( int i = 0; i < ownListComps.Length; i++ )
        {
            if ( ownListComps [i] == null )
                return;
            else
                ownListComps [i] = null;
        }
    }

    private float GetDistanceToCar( RollingStock car )
    {
        return Mathf.Abs (engineT.position.x - car.GetPositionX());
    }

    private void WatchLeftCar()
    {
        if ( leftCar != null )
        {
            distLeft = GetDistanceToCar (leftCar);

            if ( distLeft < 1500 && distLeft > 1000 )
            {
                if ( engine.InstructionsHandler < -4 )
                    engine.InstructionsHandler = -4;
            }

            else if ( distLeft <= 1000 && distLeft > 500 )
            {
                if ( engine.InstructionsHandler < -4 )
                    engine.InstructionsHandler = -4;
            }
            else if ( distLeft <= 500 && distLeft > 300 )
            {
                if ( engine.InstructionsHandler < -3 )
                    engine.InstructionsHandler = -3;

            }
            else if ( distLeft <= 300 && distLeft > 120 )
            {
                if ( engine.InstructionsHandler < -2 )
                    engine.InstructionsHandler = -2;
            }
            else if ( distLeft <= 120  )
            {
                if ( engine.InstructionsHandler < -1 )
                    engine.InstructionsHandler = -1;
            }
        }
        else
            distLeft = 10000;

    }

    private void WatchRightCar()
    {
        if ( rightCar != null )
        {
            distRight = GetDistanceToCar (rightCar);
            if ( distRight < 1500 && distRight > 1000 )
            {
                if ( engine.InstructionsHandler > 4 )
                    engine.InstructionsHandler = 4;
            }
            else if ( distRight <= 1000 && distRight > 500 )
            {
                if ( engine.InstructionsHandler > 3 )
                    engine.InstructionsHandler = 3;
            }

            else if ( distRight <= 500 && distRight > 300 )
            {
                if ( engine.InstructionsHandler > 2 )
                    engine.InstructionsHandler = 2;
            }
            else if ( distRight <= 300 && distRight > 120 )
            {
                if ( engine.InstructionsHandler > 1 )
                    engine.InstructionsHandler = 1;
            }
            else if ( distRight <= 120)
            {
                if ( engine.InstructionsHandler > 1 )
                    engine.InstructionsHandler = 1;
            }
        }
        else
            distRight = 10000;
    }

}
