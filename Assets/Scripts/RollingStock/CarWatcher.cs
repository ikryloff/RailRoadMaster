using System.Collections.Generic;
using UnityEngine;

public class CarWatcher : MonoBehaviour
{

    private Engine engine;
    private Transform engineT;
    private RSConnection rSConnection;
    private RollingStock [] listRS;
    private List<RollingStock> ownListRS;
    private RollingStock engineRS;
    private RollingStock leftCar;
    private RollingStock rightCar;
    private float tempDistLeft;
    private float tempDistRight;
    private float distRight;
    private float distLeft;


    private void Awake()
    {
        engine = GetComponent<Engine> ();
        rSConnection = GetComponent<RSConnection> ();
        engineT = GetComponent<Transform> ();
        engineRS = GetComponent<RollingStock> ();
        ownListRS = new List<RollingStock> ();

    }

    private void Start()
    {
        listRS = CompositionManager.Instance.RollingStocks;
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
        ownListRS.Clear ();
        leftCar = null;
        rightCar = null;
        foreach ( RollingStock car in listRS )
        {
            if ( engineRS.OwnPath.Contains(car.OwnTrack) && !car.Equals(engineRS))
            {               
                ownListRS.Add (car);
            }
        }

        tempDistLeft = 1000000;
        tempDistRight = 1000000;

        foreach ( RollingStock car in ownListRS )
        {
            if ( !engineRS.GetCoupledLeft() && engineT.position.x > car.GetPositionX() )
            {
                if ( GetDistanceToCar (car) < tempDistLeft )
                {
                    tempDistLeft = GetDistanceToCar (car);
                    leftCar = car;
                }

            }
            else if ( !engineRS.GetCoupledRight () && engineT.position.x < car.GetPositionX () )
            {
                if ( GetDistanceToCar (car) < tempDistRight )
                {
                    tempDistRight = GetDistanceToCar (car);
                    rightCar = car;
                }
            }
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
