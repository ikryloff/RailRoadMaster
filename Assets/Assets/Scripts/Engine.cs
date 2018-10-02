using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public static int instructionHandler = 0;

    private Rigidbody2D engine;
    private RollingStock engineRS;
    [SerializeField]
    private RollingStock[] cars;
    [SerializeField]
    private List<RollingStock> expectedСars;
    private Route route;
    private bool brakes = true;
    private int controllerPosition = 0;
    [SerializeField]
    private Text speedTxt;
    [SerializeField]
    private Text throttleTxt;
    [SerializeField]
    private Text directionTxt;
    [SerializeField]
    private Text handlerTxt;
    public CompositionManager cm;
    public float mSpeed;
    private int direction;
    private int directionInstructions;
    private int absControllerPosition;
    public int maxSpeed;
    private bool isDrivingByInstructionsIsOn;
    [SerializeField]
    private TrackCircuit track;
    private float distanceToLight;
    private float distanceToCar;
    private float distanceToExpectedCar;
    [SerializeField]
    private bool isCoupling;
    private float smothPower = 0;
    [SerializeField]
    private RollingStock nearestCar;

    [SerializeField]
    private string startCompositionNumber;

    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    private bool isEngineGoesAhead;

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
        route = GameObject.Find("Route").GetComponent<Route>();
        cars = FindObjectsOfType<RollingStock>();
        ExpectedСars = new List<RollingStock>();

        // Cashing hand switches

        switch18 = route.GetSwitchByName("Switch_18");
        switch19 = route.GetSwitchByName("Switch_19");
        switch20 = route.GetSwitchByName("Switch_20");
        switch21 = route.GetSwitchByName("Switch_21");
        switch22 = route.GetSwitchByName("Switch_22");
        switch10 = route.GetSwitchByName("Switch_10");
        switch12 = route.GetSwitchByName("Switch_12");
        switch14 = route.GetSwitchByName("Switch_14");

    }

    void Start()
    {

        engine = GetComponent<Rigidbody2D>();
        EngineRS = GetComponent<RollingStock>();
        // conductor mode
        IsDrivingByInstructionsIsOn = true;

        InformationUpdateFunction();
        InvokeRepeating("InformationUpdateFunction", 0.5f, 0.5f);
        GetTrack();

    }
    void MoveEngine()
    {

        switch (AbsControllerPosition)
        {
            case 8:
                if (MSpeed < 10)
                    AddForceToEngine(20200);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(18500);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(14000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(11500);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(14000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(7800);
                if (MSpeed >= 35 && MSpeed < 40)
                    AddForceToEngine(6000);
                if (MSpeed >= 40 && MSpeed < 45)
                    AddForceToEngine(5000);
                if (MSpeed >= 45 && MSpeed < 50)
                    AddForceToEngine(4000);
                if (MSpeed >= 50 && MSpeed < 55)
                    AddForceToEngine(3500);
                if (MSpeed >= 55 && MSpeed < 60)
                    AddForceToEngine(2700);
                if (MSpeed >= 60 && MSpeed < 110)
                    AddForceToEngine(1500);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 7:
                if (MSpeed < 10)
                    AddForceToEngine(20500);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(16000);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(13000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(10000);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(8000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(6000);
                if (MSpeed >= 35 && MSpeed < 40)
                    AddForceToEngine(5000);
                if (MSpeed >= 40 && MSpeed < 45)
                    AddForceToEngine(4000);
                if (MSpeed >= 45 && MSpeed < 50)
                    AddForceToEngine(3000);
                if (MSpeed >= 50 && MSpeed < 55)
                    AddForceToEngine(2000);
                if (MSpeed >= 55 && MSpeed < 60)
                    AddForceToEngine(1500);
                if (MSpeed >= 60 && MSpeed < 110)
                    AddForceToEngine(1000);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 6:
                if (MSpeed < 10)
                    AddForceToEngine(20500);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(14000);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(11000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(8000);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(6000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(5000);
                if (MSpeed >= 35 && MSpeed < 40)
                    AddForceToEngine(3800);
                if (MSpeed >= 40 && MSpeed < 45)
                    AddForceToEngine(3000);
                if (MSpeed >= 45 && MSpeed < 50)
                    AddForceToEngine(2100);
                if (MSpeed >= 50 && MSpeed < 55)
                    AddForceToEngine(1800);
                if (MSpeed >= 55 && MSpeed < 60)
                    AddForceToEngine(1000);
                if (MSpeed >= 60 && MSpeed < 110)
                    AddForceToEngine(800);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 5:
                if (MSpeed < 5)
                    AddForceToEngine(20500);
                if (MSpeed >= 5 && MSpeed < 10)
                    AddForceToEngine(18000);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(11800);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(9000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(6000);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(4000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(3200);
                if (MSpeed >= 35 && MSpeed < 40)
                    AddForceToEngine(2800);
                if (MSpeed >= 40 && MSpeed < 45)
                    AddForceToEngine(2000);
                if (MSpeed >= 45 && MSpeed < 50)
                    AddForceToEngine(1600);
                if (MSpeed >= 50 && MSpeed < 55)
                    AddForceToEngine(1100);
                if (MSpeed >= 55 && MSpeed < 60)
                    AddForceToEngine(900);
                if (MSpeed >= 60 && MSpeed < 110)
                    AddForceToEngine(700);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 4:
                if (MSpeed < 5)
                    AddForceToEngine(20500);
                if (MSpeed >= 5 && MSpeed < 10)
                    AddForceToEngine(13000);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(9500);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(6000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(4000);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(3000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(2000);
                if (MSpeed >= 35 && MSpeed < 40)
                    AddForceToEngine(1700);
                if (MSpeed >= 40 && MSpeed < 45)
                    AddForceToEngine(1100);
                if (MSpeed >= 45 && MSpeed < 50)
                    AddForceToEngine(900);
                if (MSpeed >= 50 && MSpeed < 110)
                    AddForceToEngine(700);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 3:
                if (MSpeed < 5)
                    AddForceToEngine(20500);
                if (MSpeed >= 5 && MSpeed < 10)
                    AddForceToEngine(11000);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(3000);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(2000);
                if (MSpeed >= 20 && MSpeed < 25)
                    AddForceToEngine(1200);
                if (MSpeed >= 25 && MSpeed < 30)
                    AddForceToEngine(1000);
                if (MSpeed >= 30 && MSpeed < 35)
                    AddForceToEngine(900);
                if (MSpeed >= 35 && MSpeed < 110)
                    AddForceToEngine(700);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;

            case 2:
                if (MSpeed < 5)
                    AddForceToEngine(12000);
                if (MSpeed >= 5 && MSpeed < 10)
                    AddForceToEngine(3900);
                if (MSpeed >= 10 && MSpeed < 15)
                    AddForceToEngine(1500);
                if (MSpeed >= 15 && MSpeed < 20)
                    AddForceToEngine(1000);
                if (MSpeed >= 20 && MSpeed < 110)
                    AddForceToEngine(500);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 1:
                if (MSpeed < 1)
                    AddForceToEngine(11000);
                if (MSpeed >= 1 && MSpeed < 10)
                    AddForceToEngine(2000);
                if (MSpeed >= 10)
                    AddForceToEngine(500);
                if (MSpeed > 110)
                    AddForceToEngine(0);
                break;
            default:
                AddForceToEngine(0);
                break;

        }

    }

    void AddForceToEngine(int power)
    {
        smothPower = Mathf.Lerp(power, smothPower, 0.9f);
        engine.AddRelativeForce(new Vector2(smothPower * Direction, 0), ForceMode2D.Force);
    }

    void InformationUpdateFunction()
    {
        if (EngineRS.Number == "8888")
        {
            speedTxt.text = "Speed: " + MSpeed;
            throttleTxt.text = "Throttle: " + Mathf.Abs(ControllerPosition);
            directionTxt.text = "Direction: " + Direction;
        }

    }

    private void Update()
    {
        PrintHandler();
    }

    void FixedUpdate()
    {
        GetTrack();

        MSpeed = (int)(Time.deltaTime * engine.velocity.magnitude * 5);

        if (IsDrivingByInstructionsIsOn)
            DriveByInstructions();
        MoveEngine();
        if (Brakes)
        {
            if (MSpeed > 0)
            {
                if (engine.velocity.x > 3f)
                    engine.AddRelativeForce(new Vector2(-2000, 0), ForceMode2D.Force);
                else if (engine.velocity.x < -3f)
                    engine.AddRelativeForce(new Vector2(2000, 0), ForceMode2D.Force);
                else
                    engine.velocity = new Vector2(0, 0);
                if (cm.CompositionsList.Any())
                {
                    foreach (RollingStock rs in cm.CompositionsList[EngineRS.CompositionNumberofRS])
                    {
                        rs.Brakes = true;

                    }
                }
            }
        }
        else if (!Brakes)
        {
            if (engine.velocity.x > 0)
            {
                engine.AddRelativeForce(new Vector2(-30f, 0), ForceMode2D.Force);
            }
            else if (engine.velocity.x < 0)
                engine.AddRelativeForce(new Vector2(30, 0), ForceMode2D.Force);
        }

    }

    public void ReleaseBrakes()
    {
        Brakes = false;
        if (cm.CompositionsList.Any())
        {
            foreach (RollingStock rs in cm.CompositionsList[EngineRS.CompositionNumberofRS])
            {
                rs.Brakes = false;
            }
        }

    }

    public void EngineControllerForward()
    {

        if (ControllerPosition < 8)
        {
            ControllerPosition++;
        }
    }

    public void EngineControllerBackwards()
    {
        if (ControllerPosition > -8)
        {
            ControllerPosition--;

        }
    }

    public void EngineControllerReleaseAll()
    {
        ReleaseBrakes();
        ControllerPosition = 0;
    }
    public void EngineControllerUseBrakes()
    {
        Brakes = true;
        ControllerPosition = 0;
    }

    public void EngineInstructionStop()
    {
        Brakes = true;
        Direction = 0;
        ControllerPosition = 0;
        instructionHandler = 0;
    }
    public void EngineControllerToZero()
    {
        ControllerPosition = 0;

    }

    public void EngineInstructionsForward()
    {
        route.IsPathCheckingForward = true;

        Direction = Direction == -1 && Direction != 0 ? -1 : 1;
        route.MakePath();
        GetAllExpectedCarsByDirection(Direction);
        ReleaseBrakes();
        instructionHandler++;
        if (instructionHandler == 0)
            Direction = 0;
        if (instructionHandler == 7)
            instructionHandler = 6;

        StartCompositionNumber = EngineRS.CompositionNumberString;
    }

    public void EngineInstructionsBackwards()
    {
        route.IsPathCheckingForward = false;
        Direction = Direction == 1 && Direction != 0 ? 1 : -1;
        route.MakePath();
        GetAllExpectedCarsByDirection(Direction);
        ReleaseBrakes();
        instructionHandler--;
        if (instructionHandler == 0)
            Direction = 0;
        if (instructionHandler == -7)
            instructionHandler = -6;
        StartCompositionNumber = EngineRS.CompositionNumberString;

    }

    public bool IsEngineGoesAhead
    {
        get
        {
           return (Direction == 1 && !EngineRS.ActiveCoupler.JointCar) || (Direction == -1 && !EngineRS.PassiveCoupler.IsPassiveCoupleConnected);
        }
        set
        {
            isEngineGoesAhead = value;
        }
        
    }

    public bool IsEngineGoesAheadByDirection( int _direction)
    {
        return (_direction == 1 && !EngineRS.ActiveCoupler.JointCar) || (_direction == -1 && !EngineRS.PassiveCoupler.IsPassiveCoupleConnected);
    }

    public void DriveByInstructions()
    {
        PrintHandler();
        if (IsEngineGoesAhead)
        {            
            DriveAccordingToLights();
            DriveAccordingToCarPresence();
        }

        if (EngineRS.CompositionNumberString != StartCompositionNumber)
        {
            instructionHandler = 0;
            IsCoupling = false;
            NearestCar = null;
            ExpectedСars.Clear();
        }

        if (instructionHandler== 0)
        {
            MaxSpeed = 0;
            EngineControllerUseBrakes();
            if (MSpeed < 1)
                engine.velocity = new Vector2(0, 0);
        }            
        if (Mathf.Abs(instructionHandler) == 1)
            MaxSpeed = 3;
        if (Mathf.Abs(instructionHandler) == 2)
            MaxSpeed = 5;
        if (Mathf.Abs(instructionHandler) == 3)
            MaxSpeed = 10;
        if (Mathf.Abs(instructionHandler) == 4)
            MaxSpeed = 15;
        if (Mathf.Abs(instructionHandler) == 5)
            MaxSpeed = 25;
        if (Mathf.Abs(instructionHandler) == 6)
            MaxSpeed = 40;        

        if (MSpeed > MaxSpeed || MaxSpeed == 0)
        {
            EngineControllerUseBrakes();
        }
        else
        {
            ReleaseBrakes();
            if (MSpeed < 6)
                ControllerPosition = 1 * Direction;
            if (MSpeed >= 6 && MSpeed < 10)
                ControllerPosition = 2 * Direction;
            if (MSpeed >= 10 && MSpeed < 25)
                ControllerPosition = 4 * Direction;
            if (MSpeed >= 25)
                    ControllerPosition = 8 * Direction;
        }  
    }

    private void PrintHandler()
    {
        if (Direction > 0)
            handlerTxt.text = "  >>> " + Mathf.Abs(instructionHandler) + ">>> " + MaxSpeed;
        else if (Direction < 0)
            handlerTxt.text = MaxSpeed + "  <<< " + Mathf.Abs(instructionHandler) + " <<<";
        else
            handlerTxt.text = "  <<< 0 >>>";        
    }

    public void GetTrack()
    {
        Track = EngineRS.TrackCircuit;        
    }

    public void GetDistanceToLight(TrafficLights tLights)
    {
        DistanceToLight = Mathf.Abs(engine.transform.position.x - tLights.transform.position.x);
    }
    
    public void DriveAccordingToLights()
    {
        
        if(Track.TrackLights != null)
        {
            for (int i = 0; i < Track.TrackLights.Length; i++)
            {
                TrafficLights tl = Track.TrackLights[i];
                CheckPathEnds(tl);
                if (Track.TrackLights[i] && Track.TrackLights[i].IsClosed)
                {
                    if((i == 0 && Direction == -1) || (i == 1 && Direction == 1))
                    {
                        GetDistanceToLight(tl);
                        if (DistanceToLight <= 5000 && DistanceToLight > 1500)
                        {
                            //Debug.Log("Light is Closed!!" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if(Mathf.Abs(instructionHandler) > 5)
                                instructionHandler = 5 * Direction;
                        }
                        else if (DistanceToLight <= 1500 && DistanceToLight > 250)
                        {
                          // Debug.Log("Light is Closed!!" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if (Mathf.Abs(instructionHandler) > 2)
                                instructionHandler = 2 * Direction;
                        }
                        else if (DistanceToLight <= 250 && DistanceToLight >= 60)
                        {
                            //Debug.Log("Light is Closed!! I'll stop the engine" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if (Mathf.Abs(instructionHandler) > 0)
                                instructionHandler = 0;
                        }                        
                    }
                   
                }

            }
        }
       
    }

    public void CheckPathEnds(TrafficLights trackEnd)
    {
        if (trackEnd)
        {
            if (trackEnd.Name == "End12_13CH" && Direction == 1)
            {
                if (Track.TrackName == "Track_12")
                    trackEnd.IsClosed = switch21.IsSwitchStraight ? false : true;
                if (Track.TrackName == "Track_13")
                    trackEnd.IsClosed = switch21.IsSwitchStraight ? true : false;
            }
            if (trackEnd.Name == "End12_13N" && Direction == -1)
            {
                if (Track.TrackName == "Track_12")
                    trackEnd.IsClosed = switch19.IsSwitchStraight ? false : true;
                if (Track.TrackName == "Track_13")
                    trackEnd.IsClosed = switch19.IsSwitchStraight ? true : false;
            }

            if (trackEnd.Name == "End9" && Direction == -1)
            {
                if (Track.TrackName == "Track_9")
                    trackEnd.IsClosed = switch18.IsSwitchStraight ? true : false;
            }
            if (trackEnd.Name == "End10_11" && Direction == -1)
            {
                if (Track.TrackName == "Track_10")
                    trackEnd.IsClosed = switch18.IsSwitchStraight && switch20.IsSwitchStraight ? false : true;

                if (Track.TrackName == "Track_11")
                    trackEnd.IsClosed = switch18.IsSwitchStraight && !switch20.IsSwitchStraight ? false : true;
            }
            if (trackEnd.Name == "End14SW" && Direction == 1)
            {
                if (Track.TrackName == "Track_14")
                    trackEnd.IsClosed = switch22.IsSwitchStraight ? true : false;

                if (Track.TrackName == "TrackCircuitSw14" || track.name == "TrackCircuitSw12" || track.name == "TrackCircuitSw10")
                    trackEnd.IsClosed = !switch22.IsSwitchStraight && !switch10.IsSwitchStraight && switch12.IsSwitchStraight && switch14.IsSwitchStraight ? true : false;
            }
            if (trackEnd.Name == "M3" && Direction == -1)
            {
                if (Track.TrackName == "Track_10_14_18" || track.name == "TrackCircuitSw18" || track.name == "TrackCircuitSw20" || track.name == "TrackCircuitSw22")
                {                                
                    if(!switch22.IsSwitchStraight)
                        trackEnd.IsClosed = false;
                    else if(switch22.IsSwitchStraight && trackEnd.GetLightColor == Constants.COLOR_DEFAULT)
                        trackEnd.IsClosed = true;
                }                
            }
        }


    }

    public void CoupleToCar()
    {
        IsCoupling = IsCoupling ? false : true;
        GetAllExpectedCarsByDirection(Direction);
        GetExpectedCar();        
        if (NearestCar && EngineRS.CompositionNumberofRS != nearestCar.CompositionNumberofRS)
        {
            instructionHandler = 1 * Direction;
        }        
    }

    private void DriveAccordingToCarPresence()
    {        
        GetExpectedCar();
        if (NearestCar  && !IsCoupling)
        {
            if(DistanceToCar < 300)
            {
                NearestCar = null;
            }

            else if (DistanceToCar <= 5000 && DistanceToCar > 1500)
            {
                if (Mathf.Abs(instructionHandler) > 5)
                    instructionHandler = 5 * Direction;
            }
            else if(DistanceToCar <= 3000 && DistanceToCar > 1500)
            {
                if (Mathf.Abs(instructionHandler) > 3)
                    instructionHandler = 3 * Direction;
            }
            else if(DistanceToCar <= 1500 && DistanceToCar > 600)
            {
                if (Mathf.Abs(instructionHandler) > 2)
                    instructionHandler = 2 * Direction;
            }
            else if(DistanceToCar <= 600 && DistanceToCar >= 300)
            {
                if (Mathf.Abs(instructionHandler) > 0)
                {
                    if (NearestCar.transform.position.x < transform.position.x && Direction == -1 || NearestCar.transform.position.x > transform.position.x && Direction == 1)
                    {
                        Debug.Log("Can't move, the car is ahead");
                        instructionHandler = 0 * Direction;
                    }
                    
                }
                                  
            }            
        }
        
    }


    public void GetAllExpectedCarsByDirection(int _direction)
    {
        ExpectedСars.Clear();
        
        foreach (RollingStock rc in cars)
        {
            
            if (rc.TrackCircuit == route.OccupiedTrack)
            {                
                if (!ExpectedСars.Contains(rc))
                {
                    if (_direction == -1 && rc.transform.position.x < engine.position.x - 200)
                    {
                        ExpectedСars.Add(rc);
                    }
                    if (_direction == 1 && rc.transform.position.x > engine.position.x + 200)
                    {
                        
                        ExpectedСars.Add(rc);
                    }
                }
            }

        }        
    }

    public void GetExpectedCar()
    {
                
        if(ExpectedСars != null)
        {
            NearestCar = null;
            foreach (RollingStock rc in ExpectedСars)
            {
                if(rc.CompositionNumberofRS != engineRS.CompositionNumberofRS)
                {
                    distanceToExpectedCar = Mathf.Abs(rc.transform.position.x - engine.position.x);
                    if (NearestCar == null || distanceToExpectedCar < Mathf.Abs(NearestCar.transform.position.x - engine.position.x))
                    {
                        NearestCar = rc;
                    }
                }          
            }
        }
        else
            NearestCar = null;

    }



    public int ControllerPosition
    {
        get
        {
            return controllerPosition;
        }

        set
        {
            controllerPosition = value;
        }
    }

    public int AbsControllerPosition
    {
        get
        {
            return Mathf.Abs(ControllerPosition);
        }

        set
        {
            absControllerPosition = value;
        }
    }
    public int Direction
    {
        get
        {
            if (IsDrivingByInstructionsIsOn)
            {
                if (instructionHandler > 0)
                    return 1;
                else if (instructionHandler < 0)
                    return -1;
            }
            if (ControllerPosition != 0)
            {
                if (AbsControllerPosition / ControllerPosition > 0)
                    return 1;
                else if (AbsControllerPosition / ControllerPosition < 0)
                    return -1;
            }

            return direction;
        }

        set
        {
            direction = value;
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

    public float MSpeed
    {
        get
        {
            return mSpeed;
        }

        set
        {
            mSpeed = value;
        }
    }

    public int MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public bool IsDrivingByInstructionsIsOn
    {
        get
        {
            return isDrivingByInstructionsIsOn;
        }

        set
        {
            isDrivingByInstructionsIsOn = value;
        }
    }

    public TrackCircuit Track
    {
        get
        {
            return track;
        }

        set
        {
            track = value;
        }
    }

    public List<RollingStock> ExpectedСars
    {
        get
        {
            return expectedСars;
        }

        set
        {
            expectedСars = value;
        }
    }

    public RollingStock NearestCar
    {
        get
        {
            return nearestCar;
        }

        set
        {
            nearestCar = value;
        }
    }

    public string StartCompositionNumber
    {
        get
        {
            return startCompositionNumber;
        }

        set
        {
            startCompositionNumber = value;
        }
    }

    public RollingStock EngineRS
    {
        get
        {
            return engineRS;
        }

        set
        {
            engineRS = value;
        }
    }

    public bool IsCoupling
    {
        get
        {
            return isCoupling;
        }

        set
        {
            isCoupling = value;
        }
    }

    public float DistanceToCar
    {
        get
        {
            return Mathf.Abs(NearestCar.transform.position.x - engine.position.x);
        }

        set
        {
            distanceToCar = value;
        }
    }

    public float DistanceToLight
    {
        get
        {
            return distanceToLight;
        }

        set
        {
            distanceToLight = value;
        }
    }
}