using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody rollingStockRB;
    
    [SerializeField]
    private string number;    
    private int compositionNumberofRS;
    private string compositionNumberString;
    public bool brakes = true;    
    private Coupler activeCoupler;
    private Coupler passiveCoupler;
    private Coupler connectedToPassive; 
    [SerializeField]
    private TrackCircuit trackCircuit;
    public float breakeForce;
   
    public Engine engine;    
    SwitchManager switchManager;   
    public TrackPathUnit mathTemp;
    Transform rsTransform;

    public float distance;
    public float distanceInPath;
    public bool isDirectionChanged;
    public int isMoving;

    public float acceleration;
    public float force;
    public List<TrackPathUnit> ownTrackPath;
    public float pathLength;
    TrackPath trackPath;

    public BogeyPathScript[] bogeys;
    public BogeyPathScript bogeyA;
    public BogeyPathScript bogeyB;
    Transform bogeyTransformA;
    Transform bogeyTransformB;
    public RollingStock rightCarToConnect;
    public RollingStock leftCarToConnect;
    
    private float angle;
    private Vector3 dir;

    public float distanceToRightCar;
    public float distanceToLeftCar;

    public bool isConnectedLeft;
    public bool isConnectedRight;
    public CompositionManager compositionManager;
    
    

    public static int test;

    private void Awake()    {
        trackPath = FindObjectOfType<TrackPath>();
        compositionManager = FindObjectOfType<CompositionManager>();
        switchManager = FindObjectOfType<SwitchManager>();        
        rsTransform = gameObject.GetComponent<Transform>();

        // set bogeys to RS
        bogeys = GetComponentsInChildren<BogeyPathScript>();              
        if(bogeys[0].transform.position.x < bogeys[1].transform.position.x)
        {
            bogeyA = bogeys[0];
            bogeyB = bogeys[1];
        }
        else
        {
            bogeyA = bogeys[1];
            bogeyB = bogeys[0];
        }
        bogeyTransformA = bogeyA.transform;
        bogeyTransformB = bogeyB.transform;
    }



    private void Start()
    {
        distanceToRightCar = 999999;
        distanceToLeftCar = 999999;
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody>();        
        ActiveCoupler = transform.GetChild(0).GetComponent<Coupler>();
        PassiveCoupler = transform.GetChild(1).GetComponent<Coupler>();
        
        Brakes = true;

        // exp

        acceleration = 0;
        isMoving = 1;

        CalcDistanceInPath();
        FindCloseCars();
    }

    void Update()
    {
        CalcDistanceInPath();
        if (engine)
        {
            acceleration = engine.acceleration;
        }
        else
            acceleration = 0;
        force = Time.fixedUnscaledDeltaTime * acceleration * isMoving;
        
        
        distance += force;
        if (mathTemp)
        {
            Vector3 tangent;

            rsTransform.position = mathTemp.math.CalcPositionAndTangentByDistance(distance, out tangent);            
            UpdateRSRotation();

            if (force > 0 && mathTemp.math.GetDistance() - distance < 0.1)
            {
                mathTemp = trackPath.GetNextTrack(mathTemp, ownTrackPath);
                distance = 0;
            }
            if (force < 0 && distance < 0.1)
            {
                mathTemp = trackPath.GetPrevTrack(mathTemp, ownTrackPath);
                if (mathTemp)
                    distance = mathTemp.math.GetDistance();                
            }           
        }
        else
        {
            isMoving = 0;
        }

        if (force != 0)
        {
            FindCloseCars();
            //if(!isConnectedRight)
            //   GetDistanceToRightCar();
            //if(!isConnectedLeft)
            //  GetDistanceToLeftCar();
        }
        CheckConnectionToCar();    
        
    }

    public void GetDistanceToLeftCar()
    {
        bool pathIsFree = true;
        float toCarDist = 0;
        BogeyPathScript nextTrackBogey = null;
       
        if (!nextTrackBogey)
        {           
            toCarDist = bogeyA.distance;
            for (int i = bogeyA.ownTrackPath.IndexOf(bogeyA.mathTemp) - 1; i >= 0; i--)
            {
                if (!bogeyA.ownTrackPath[i].hasObjects)
                {
                    toCarDist += bogeyA.ownTrackPath[i].math.GetDistance();
                }
                else 
                {
                    if(bogeyA.ownTrackPath[i].rightBogey && bogeyA.ownTrackPath[i].rightBogey != bogeyB)
                    {
                        toCarDist += bogeyA.ownTrackPath[i].math.GetDistance() - bogeyA.ownTrackPath[i].rightBogey.distance;
                        leftCarToConnect = bogeyA.ownTrackPath[i].rightBogey.rollingStock;
                        pathIsFree = false;
                        break;
                    }                    
                }
                pathIsFree = true;
            }
            for (int i = bogeyA.mathTemp.bogeys.IndexOf(bogeyA) - 1; i >= 0; i--)
            {
                if (bogeyA.mathTemp.bogeys[i] && bogeyA.mathTemp.bogeys[i] != bogeyB)
                {
                    nextTrackBogey = bogeyA.mathTemp.bogeys[i];
                    toCarDist = bogeyA.distance - nextTrackBogey.distance;
                    leftCarToConnect = bogeyA.mathTemp.bogeys[i].rollingStock;
                    pathIsFree = false;
                    break;
                }
            }

        }        
                    
        if (pathIsFree)
            distanceToLeftCar = 999999;
        else
            distanceToLeftCar = toCarDist;
    }

    public void GetDistanceToRightCar()
    {
        bool pathIsFree = true;        
        float toCarDist = 0;
        BogeyPathScript nextTrackBogey = null;     
        if(!nextTrackBogey)
        {
            toCarDist = bogeyB.mathTemp.math.GetDistance() - bogeyB.distance;
            for (int i = bogeyB.ownTrackPath.IndexOf(bogeyB.mathTemp) + 1; i < bogeyB.ownTrackPath.Count; i++)
            {
                
                if (!bogeyB.ownTrackPath[i].hasObjects)
                {
                    toCarDist += bogeyB.ownTrackPath[i].math.GetDistance();
                }
                else 
                {
                    if(bogeyB.ownTrackPath[i].leftBogey && bogeyB.ownTrackPath[i].leftBogey != bogeyA)
                    {
                        toCarDist += bogeyB.ownTrackPath[i].leftBogey.distance;
                        rightCarToConnect = bogeyB.ownTrackPath[i].leftBogey.rollingStock;
                        pathIsFree = false;
                        break;
                    }                                   
                }
                pathIsFree = true;
            }
        }

        for (int i = bogeyB.mathTemp.bogeys.IndexOf(bogeyB) + 1; i < bogeyB.mathTemp.bogeys.Count; i++)
        {
            if (bogeyB.mathTemp.bogeys[i] && bogeyB.mathTemp.bogeys[i] != bogeyA)
            {
                nextTrackBogey = bogeyB.mathTemp.bogeys[i];
                toCarDist = nextTrackBogey.distance - bogeyB.distance;
                rightCarToConnect = bogeyB.mathTemp.bogeys[i].rollingStock;
                pathIsFree = false;
                break;
            }
        }
        if (pathIsFree)
            distanceToRightCar = 999999;
        else
            distanceToRightCar = toCarDist;
    }

    public void CheckConnectionToCar()
    {
        if(rightCarToConnect)
            distanceToRightCar = rightCarToConnect.distanceInPath - distanceInPath;
        if(leftCarToConnect)
            distanceToLeftCar = distanceInPath - leftCarToConnect.distanceInPath;

        if(!isConnectedRight  && distanceToRightCar < 90)
        {
            isConnectedRight = true;
            rightCarToConnect.isConnectedLeft = true;
            rightCarToConnect.leftCarToConnect = this;            
            rightCarToConnect.engine = engine;
            
        }
        if (!isConnectedLeft &&  distanceToLeftCar < 89)
        {
            isConnectedLeft = true;
            leftCarToConnect.isConnectedRight = true;
            leftCarToConnect.rightCarToConnect = this;            
            leftCarToConnect.engine = engine;            
        }

        if (isConnectedRight)
        {
            if (rightCarToConnect.leftCarToConnect == null || !rightCarToConnect.isConnectedLeft)
            {
                rightCarToConnect = null;
                isConnectedRight = false;
            } 
        }
        if (isConnectedLeft)
        {
            if (leftCarToConnect.rightCarToConnect == null || !leftCarToConnect.isConnectedRight)
            {
                leftCarToConnect = null;
                isConnectedLeft = false;
            }
        }

        //  correction of couplers positions
        if (isConnectedLeft )
        {
            if( leftCarToConnect.mathTemp == mathTemp &&  
                leftCarToConnect.distance > 40 && distance < mathTemp.math.GetDistance() - 40  &&
                Mathf.Abs(leftCarToConnect.distance - distance + 41 - bogeyA.offset + bogeyB.offset) > 0.5)
            {
                leftCarToConnect.distance = distance - 41 + bogeyA.offset - bogeyB.offset;
                leftCarToConnect.bogeyA.distance = bogeyA.distance - 41 + bogeyA.offset - bogeyB.offset;
                leftCarToConnect.bogeyB.distance = bogeyB.distance - 41 + bogeyA.offset - bogeyB.offset;
                print("Correction " + ++test);
            }
        }

    }

    public void FindCloseCars()
    {
        float temp = 0;
        float left = 99999;
        float right = 99999;
        leftCarToConnect = null;
        rightCarToConnect = null;
        foreach (RollingStock item in compositionManager.cars)
        {            
            if(item.ownTrackPath.Contains(mathTemp) && item != this)
            {                
                temp = distanceInPath - item.distanceInPath;
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
        
    }

    // forced changing direction
    public void ChangeDirection()
    {
        acceleration *= -1;
        if(isMoving == 0)
        {
            isMoving = 1;
        }
       
    }
    // rotation of rolling stock
    void UpdateRSRotation()
    {
        dir = bogeyTransformB.position - bogeyTransformA.position;
        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        rsTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void CalcDistanceInPath()
    {
        distanceInPath = 0;
        foreach (TrackPathUnit item in ownTrackPath)
        {
            if (item == mathTemp)
            {
                distanceInPath += distance;
                break;
            }
            else
                distanceInPath += item.math.GetDistance();
        }
    }

    public void MakeRSList()
    {
        
    }

    public string Number
    {
        get
        {
            return number;
        }

        set
        {
            number = value;
        }
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

    public int CompositionNumberofRS
    {
        get
        {
            return compositionNumberofRS;
        }

        set
        {
            compositionNumberofRS = value;
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

    public string CompositionNumberString
    {
        get
        {
            return compositionNumberString;
        }

        set
        {
            compositionNumberString = value;
        }
    }

    public TrackCircuit TrackCircuit
    {
        get
        {
            return trackCircuit;
        }

        set
        {
            trackCircuit = value;
        }
    }

}