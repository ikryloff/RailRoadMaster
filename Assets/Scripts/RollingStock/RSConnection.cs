using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSConnection : MonoBehaviour, IManageable {

    private RollingStock rollingStock;
    public RSConnection LeftCar { get; set; }
    public RSConnection RightCar { get; set; }
    public RollingStock rightCarToConnect;
    public RollingStock leftCarToConnect;
    private Coupler [] couplers;
    public Coupler CouplerRight { get; private set; }
    public Coupler CouplerLeft { get; private set; }
    public Transform CouplerLeftTransform { get; private set; }
    public Transform CouplerRightTransform { get; private set; }

    private float distanceToRightCar;
    private float distanceToLeftCar;

    public bool IsConnectedRight { get; set; }
    public bool IsConnectedLeft { get; set; }
    public int CompositionNumber { get; set; }

    public void Init()
    {
        EventManager.onPathUpdated += FindCloseCars;
        EventManager.onCompositionChanged += UpdateCarComposition;
        rollingStock = gameObject.GetComponent<RollingStock> ();
        // set couplers to RS
        SetCouplers ();
    }

    public void OnStart()
    {
        distanceToRightCar = 999999;
        distanceToLeftCar = 999999;
        FindCloseCars ();
    }

	void Update () {
        rollingStock.CalcPositionInPath ();
        CheckConnectionToCar ();
    }

    private void AddRSInComposition( RSConnection rs, Composition composition, int compositionID )
    {
        composition.RSConnections.Add (rs);
        rs.CompositionNumber = compositionID;
    }

    public void CheckConnectionToCar()
    {
        if ( LeftCar == null && leftCarToConnect )
        {
            distanceToLeftCar = rollingStock.PositionInPath - leftCarToConnect.PositionInPath;
        }
        else
            distanceToLeftCar = 99999;
        if ( RightCar == null && rightCarToConnect )
        {
            distanceToRightCar = rightCarToConnect.PositionInPath - rollingStock.PositionInPath;
            if ( distanceToRightCar < 98 )
            {
                SetEngineToConnectedCar (rightCarToConnect);
                IsConnectedRight = true;
                rightCarToConnect.rSConnection.IsConnectedLeft = true;
                RightCar = rightCarToConnect.rSConnection;
                rightCarToConnect.rSConnection.LeftCar = this;
                rightCarToConnect.rSConnection.leftCarToConnect = null;
                rightCarToConnect = null;
                UpdateCarComposition ();
            }
        }
        else
            distanceToRightCar = 99999;
    }

    public void FindCloseCars()
    {
       if(!IsConnectedLeft && !IsConnectedRight )
        {
            rollingStock.CalcPositionInPath ();
            if ( rollingStock.OwnPath != null )
            {
                float temp = 0;
                float left = 99999;
                float right = 99999;
                leftCarToConnect = null;
                rightCarToConnect = null;
                foreach ( RollingStock item in CompositionManager.Instance.RollingStocks )
                {
                    if ( item.OwnPath != null && item.OwnPath.Contains (rollingStock.OwnTrack) && item != this )
                    {
                        temp = rollingStock.PositionInPath - item.PositionInPath;
                        if ( temp > 0 && temp < left )
                        {
                            left = temp;
                            leftCarToConnect = item;
                        }
                        else if ( temp < 0 && Mathf.Abs (temp) < right )
                        {
                            right = temp;
                            rightCarToConnect = item;
                        }
                    }
                }
                if ( leftCarToConnect == null )
                    left = 99999;
                if ( rightCarToConnect == null )
                    right = 99999;
            }
        }
    }

    public void SetEngineToConnectedCar( RollingStock connectedRS )
    {
        if ( rollingStock.OwnEngine )
        {
            connectedRS.OwnEngine = rollingStock.OwnEngine;
            connectedRS.bogeyLeft.OwnEngine = rollingStock.OwnEngine;
            connectedRS.bogeyRight.OwnEngine = rollingStock.OwnEngine;
        }
        else
        {
            rollingStock.OwnEngine = connectedRS.OwnEngine;
            rollingStock.bogeyLeft.OwnEngine = connectedRS.OwnEngine;
            rollingStock.bogeyRight.OwnEngine = connectedRS.OwnEngine;
        }
    }

    // Get this car position in path   
    public void UpdateCarComposition()
    {
        // if car not connected from right
        if ( !IsConnectedRight )
        {
            // make new composition
            Composition composition = new Composition (CompositionManager.CompositionID);

            // add composition in Dict
            CompositionManager.CompositionsDict.Add (CompositionManager.CompositionID, composition);
            // add rs in composition 
            AddRSInComposition (this, composition, CompositionManager.CompositionID);
            // if there ani connected to left cars
            if ( LeftCar )
            {
                // temp car
                RSConnection conLeft = LeftCar;
                // add rs in composition
                AddRSInComposition (conLeft, composition, CompositionManager.CompositionID);

                while ( conLeft.LeftCar != null )
                {
                    conLeft = conLeft.LeftCar;
                    AddRSInComposition (conLeft, composition, CompositionManager.CompositionID);

                }
            }
            //increase composition ID
            CompositionManager.CompositionID++;

        }
    }

    private void SetCouplers()
    {
        couplers = GetComponentsInChildren<Coupler> ();
        CouplerLeft = couplers [0].transform.position.x < couplers [1].transform.position.x ? couplers [0] : couplers [1];
        CouplerRight = CouplerLeft == couplers [0] ? couplers [1] : couplers [0];
        CouplerLeftTransform = CouplerLeft.GetComponent<Transform> ();
        CouplerRightTransform = CouplerRight.GetComponent<Transform> ();
    }


}
