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
    public bool isDirectionChanged;
    public int isMoving;

    public float acceleration;
    public float force;
    public List<TrackPathUnit> ownTrackPath;
    TrackPath trackPath;

    public BogeyPathScript[] bogeys;
    public BogeyPathScript bogeyA;
    public BogeyPathScript bogeyB;
    Transform bogeyTransformA;
    Transform bogeyTransformB;
    public RollingStock rightCarToConnect;
    
    private float angle;
    private Vector3 dir;

    public float distanceToNextCar;



    private void Awake()    {
        trackPath = FindObjectOfType<TrackPath>();
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
        distanceToNextCar = 999999;
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody>();        
        ActiveCoupler = transform.GetChild(0).GetComponent<Coupler>();
        PassiveCoupler = transform.GetChild(1).GetComponent<Coupler>();
        
        Brakes = true;

        // exp

        acceleration = 0;
        isMoving = 1;
        distance = 150f;
                
        
    }

    void Update()
    {

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
            GetDistanceToRightCar();
        CheckConnectionToCar();

    }

    public void GetDistanceToRightCar()
    {
        bool pathIsFree = false;
        float toCarDist = mathTemp.math.GetDistance() - mathTemp.rightBogey.distance;
        for (int i = ownTrackPath.IndexOf(mathTemp) + 1; i < ownTrackPath.Count; i++)
        {
            if (!ownTrackPath[i].hasObjects)
            {
                toCarDist += ownTrackPath[i].math.GetDistance();                
            }
            else if (ownTrackPath[i].hasObjects)
            {
                if(ownTrackPath[i].leftBogey != bogeyB)                    
                {
                    toCarDist += ownTrackPath[i].leftBogey.distance;
                    rightCarToConnect = ownTrackPath[i].leftBogey.rollingStock;
                    pathIsFree = false;
                    break;
                }                
            }
            pathIsFree = true;
        }
        if (mathTemp.bogeys.Count > 2 && mathTemp.bogeys.IndexOf(bogeyB) < mathTemp.bogeys.Count)
        {
            toCarDist = mathTemp.bogeys[mathTemp.bogeys.IndexOf(bogeyB) + 1].distance - bogeyB.distance;
            pathIsFree = false;
        }
            
        if (pathIsFree)
            distanceToNextCar = 999999;
        else
            distanceToNextCar = toCarDist;
    }

    public void CheckConnectionToCar()
    {
        if(distanceToNextCar < 37)
        {
            print("hello");
            rightCarToConnect.engine = engine;
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