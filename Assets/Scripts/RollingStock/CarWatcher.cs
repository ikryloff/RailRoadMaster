using System;
using System.Collections.Generic;
using UnityEngine;

public class CarWatcher : MonoBehaviour
{

    private Engine engine;
    private Transform engineT;
    private RSConnection rSConnection;
    private RollingStock [] listRS;
    private RollingStock [] ownListRS;
    private RollingStock tempRS;
    private RollingStock engineRS;
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
        

    }

    private void Start()
    {
        listRS = CompositionManager.Instance.RollingStocks;
        ownListRS = new RollingStock [listRS.Length];
        InvokeRepeating ("GetOwnListRS", 2, 2);
    }

    private void Update()
    {
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
        ClearOwnListRS ();
        leftCar = null;
        rightCar = null;
        count = 0;
        for ( int i = 0; i < listRS.Length; i++ )
        {
            tempRS = listRS [i];
            if (tempRS.gameObject.activeSelf && engineRS.OwnPath.Contains(tempRS.OwnTrack) && !tempRS.Equals(engineRS))
            {               
                ownListRS[count] =  tempRS;
                count++;
            }
        }

        tempDistLeft = 100000;
        tempDistRight = 100000;

        tempRS = null;

        for ( int i = 0; i < ownListRS.Length; i++ )
        {
            tempRS = ownListRS [i];
            if ( tempRS == null )
                return;
            if ( !engineRS.GetCoupledLeft() && engineT.position.x > tempRS.GetPositionX() )
            {
                if ( GetDistanceToCar (tempRS) < tempDistLeft )
                {
                    tempDistLeft = GetDistanceToCar (tempRS);
                    leftCar = tempRS;
                }

            }
            else if ( !engineRS.GetCoupledRight () && engineT.position.x < tempRS.GetPositionX () )
            {
                if ( GetDistanceToCar (tempRS) < tempDistRight )
                {
                    tempDistRight = GetDistanceToCar (tempRS);
                    rightCar = tempRS;
                }
            }
        }
    }

    private void ClearOwnListRS()
    {
        for ( int i = 0; i < ownListRS.Length; i++ )
        {
            if ( ownListRS [i] == null )
                return;
            else
                ownListRS [i] = null;
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
            else if ( distRight <= 120 )
            {                
                if ( engine.InstructionsHandler > 1 )
                    engine.InstructionsHandler = 1;                
            }
        }        
    }

}
