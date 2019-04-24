using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;
using System;

public class RollingStock : MovableObject
{
    public float PositionInPath { get; set; }

    private RollingStock rollingStock;
    private Rigidbody rollingStockRB;
    [SerializeField]
    private TrackPathUnit thisRSTrack;
    public string Number { get; set; }   
    
    public bool brakes = true;    
    private Coupler activeCoupler;
    private Coupler passiveCoupler;
    private Coupler connectedToPassive; 
    [SerializeField]
    public float breakeForce;

     
    [SerializeField]
    private float rsPosition;

    public bool isDirectionChanged;    

    public float acceleration;
    public float movingSpeed;
    public float pathLength;
    TrackPath trackPath;

    public Bogey[] bogeys;
    public Bogey bogeyA;
    public Bogey bogeyB;
    Transform bogeyTransformA;
    Transform bogeyTransformB;
    Transform rsTransform;
    public RollingStock rightCarToConnect;
    public RollingStock leftCarToConnect;
    
    private float angle;
    private Vector3 dir;

    
    public float distanceToRightCar;
    public float distanceToLeftCar;

    public bool isConnectedLeft;
    public bool isConnectedRight;
    private float coupleDist = 88;
    
    public bool IsConnectedRight { get; set; }
    public int CompositionNumber { get; set; }


    public static int test;

    private void Awake()
    {
        EventManager.onPathChanged += UpdatePath;
        OwnTrack = thisRSTrack;
        OwnEngine = GetComponent<Engine>();
        rsTransform = GetComponent<Transform>();                
        OwnPosition = rsPosition;
        
        // set couplers to RS
        SetCouplers();
        // set bogeys to RS
        SetBogeys();
        OwnTrackCircuit = OwnTrack.TrackCircuit;

        IsConnectedRight = false;
        
    }

   
    private void Start()
    {
        UpdatePath();
        distanceToRightCar = 999999;
        distanceToLeftCar = 999999;
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody>();        
        ActiveCoupler = transform.GetChild(0).GetComponent<Coupler>();
        PassiveCoupler = transform.GetChild(1).GetComponent<Coupler>();
        Brakes = true;        

        acceleration = 0;
        IsMoving = true;
        
        CalcPositionInPath();
       
        
        
    }

    void Update()
    {        
        MoveByPath();
        CalcPositionInPath();           
        FindCloseCars(); 
        CheckConnectionToCar();        
        rsPosition = OwnPosition;  // for debug
    }

    public void UpdatePath()
    {
        TrackPath.Instance.GetTrackPath(this);
        bogeyA.OwnPath = OwnPath;
        bogeyB.OwnPath = OwnPath;
        temp++;
        print(temp);
    }

    public void CheckConnectionToCar()
    {
        if(rightCarToConnect)
            distanceToRightCar = rightCarToConnect.PositionInPath - PositionInPath;
        if(leftCarToConnect)
            distanceToLeftCar = PositionInPath - leftCarToConnect.PositionInPath;

        if(rightCarToConnect && !isConnectedRight  && distanceToRightCar < coupleDist && Mathf.Abs(OwnTransform.position.x - rightCarToConnect.OwnTransform.position.x) < coupleDist)
        {
            isConnectedRight = true;
            rightCarToConnect.isConnectedLeft = true;
            rightCarToConnect.leftCarToConnect = this;            
            rightCarToConnect.OwnEngine = OwnEngine;
            rightCarToConnect.bogeyA.OwnEngine = OwnEngine;
            rightCarToConnect.bogeyB.OwnEngine = OwnEngine;
            activeCoupler.Couple(rightCarToConnect.passiveCoupler);
            
        }
        if (leftCarToConnect && !isConnectedLeft &&  distanceToLeftCar < coupleDist && Mathf.Abs(OwnTransform.position.x - leftCarToConnect.OwnTransform.position.x) < coupleDist)
        {
            isConnectedLeft = true;
            leftCarToConnect.isConnectedRight = true;
            leftCarToConnect.rightCarToConnect = this;            
            leftCarToConnect.OwnEngine = OwnEngine;
            leftCarToConnect.bogeyA.OwnEngine = OwnEngine;
            leftCarToConnect.bogeyB.OwnEngine = OwnEngine;
            leftCarToConnect.activeCoupler.Couple(passiveCoupler);
        }      

        //  correction of couplers positions
        if (isConnectedLeft)
        {
            
            if (leftCarToConnect.OwnTrack == OwnTrack &&  
                leftCarToConnect.OwnPosition > 40 && OwnPosition < OwnTrack.trackMath.GetDistance() - 40  &&
                Mathf.Abs(distanceToLeftCar - coupleDist) > 1)
            {
                
                leftCarToConnect.OwnPosition = OwnPosition - coupleDist;
                leftCarToConnect.bogeyA.OwnPosition = bogeyA.OwnPosition - coupleDist;
                leftCarToConnect.bogeyB.OwnPosition = bogeyB.OwnPosition - coupleDist;
                print("Correction " + ++test);
            }
        }

    }

    public void FindCloseCars()
    {
        if(OwnPath != null )
        {
            float temp = 0;
            float left = 99999;
            float right = 99999;
            leftCarToConnect = null;
            rightCarToConnect = null;
            foreach (RollingStock item in CompositionManager.Instance.RollingStocks)
            {
                if (item.OwnPath != null && item.OwnPath.Contains(OwnTrack) && item != this)
                {
                    temp = PositionInPath - item.PositionInPath;
                    if (temp > 0 && temp < left)
                    {
                        left = temp;
                        leftCarToConnect = item;
                    }
                    else if (temp < 0 && Mathf.Abs(temp) < right)
                    {
                        right = temp;
                        rightCarToConnect = item;
                    }
                }
            }
            if (leftCarToConnect == null)
                left = 99999;
            if (rightCarToConnect == null)
                right = 99999;
        }
        
    }

    // forced changing direction
    public void ChangeDirection()
    {
        acceleration *= -1;
        if(!IsMoving)
        {
            IsMoving = true;
        }
       
    }
   
    

    public void CalcPositionInPath()
    {
        if (OwnPath != null)
        {
            PositionInPath = 0;
            foreach (TrackPathUnit item in OwnPath)
            {
                if (item == OwnTrack)
                {
                    PositionInPath += rsPosition;
                    break;
                }
                else
                {
                    PositionInPath += item.trackMath.GetDistance();
                }
            }
        }
    }

    private void SetBogeys()
    {
        bogeys = GetComponentsInChildren<Bogey>();
        if (bogeys[0].transform.position.x < bogeys[1].transform.position.x)
        {
            bogeyA = bogeys[0];
            bogeyB = bogeys[1];
        }
        else
        {
            bogeyA = bogeys[1];
            bogeyB = bogeys[0];
        }
        bogeyTransformA = bogeyA.GetComponent<Transform>();
        bogeyTransformB = bogeyB.GetComponent<Transform>();
    }

    private void SetCouplers()
    {

        OwnTransform = gameObject.GetComponent<Transform>();

        ActiveCoupler = transform.Find("ActiveCoupler").GetComponent<Coupler>();
        PassiveCoupler = transform.Find("PassiveCoupler").GetComponent<Coupler>();
    }

    public override void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance(OwnPosition, out tangent);
        dir = bogeyTransformB.position - bogeyTransformA.position;
        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        OwnTransform.rotation = Quaternion.Euler(0, angle, 0);
        OwnTransform.rotation *= Quaternion.Euler(0, -90, 0);

    }


    public Coupler ConnectedToPassive
    {
        get
        {
            return connectedToPassive;
        }

        set
        {
            connectedToPassive = value;
        }
    }

    public Coupler ActiveCoupler
    {
        get
        {
            return activeCoupler;
        }

        set
        {
            activeCoupler = value;
        }
    }

    public Coupler PassiveCoupler
    {
        get
        {
            return passiveCoupler;
        }

        set
        {
            passiveCoupler = value;
        }
    }

   

    public bool Brakes
    {
        get
        {
            return brakes;
        }

        set
        {
            brakes = value;
        }
    }

    
      

}