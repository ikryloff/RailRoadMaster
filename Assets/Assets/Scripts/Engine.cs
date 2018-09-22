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
    float distanceToClosedLight;
    public float distanceToCarX;
    public float distanceToCarY;
    public float distanceToExpectedCarX;
    public float distanceToExpectedCarY;
    public float tempX;    
    private bool isCoupling;
    [SerializeField]
    private float smothPower = 0;
    [SerializeField]
    private RollingStock nearestCar;
    
    [SerializeField]
    private string startCompositionNumber;

    

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
        route = GameObject.Find("Route").GetComponent<Route>();
        cars = FindObjectsOfType<RollingStock>();
        ExpectedСars = new List<RollingStock>();
        
    }

    void Start()
    {
        
        engine = GetComponent<Rigidbody2D>();
        EngineRS = GetComponent<RollingStock>();               
        if (EngineRS.Number == "8888")
        {
            speedTxt.text = "Speed: ";
            throttleTxt.text = "Throttle: ";
            directionTxt.text = "Direction: ";
        }
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
                if(MSpeed > 110)
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
                if (MSpeed >= 10 )
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

    void FixedUpdate()
    {
        GetTrack();
        
        MSpeed = (int)(Time.deltaTime * engine.velocity.magnitude * 5);
        if(EngineRS.Number == "8888")
        {
            speedTxt.text = "Speed: " + MSpeed;
            throttleTxt.text = "Throttle: " + Mathf.Abs(ControllerPosition);
            directionTxt.text = "Direction: " + Direction;
        }
        if(IsDrivingByInstructionsIsOn)
            DriveByInstructions();
        MoveEngine();        
        if (Brakes)
        {
            if(MSpeed > 0)
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
        else if(!Brakes)
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
        IsDrivingByInstructionsIsOn = true;          
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
        IsDrivingByInstructionsIsOn = true;
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

    public bool IsEngineGoesAhead()
    {
        return (Direction == 1 && !EngineRS.ActiveCoupler.JointCar) || (Direction == -1 && !EngineRS.PassiveCoupler.IsPassiveCoupleConnected);
    }

    public void DriveByInstructions()
    {
        PrintHandler();
        if (IsEngineGoesAhead())
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
            if (MSpeed < 10)
                ControllerPosition = 1 * Direction;
            if (MSpeed >= 10 && MSpeed < 15)
                ControllerPosition = 2 * Direction;
            if (MSpeed >= 15 && MSpeed < 25)
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
    
    private void DriveAccordingToLights()
    {
        
        if(Track.TrackLights != null && Track.gameObject.tag == "Track")
        {
            for (int i = 0; i < Track.TrackLights.Length; i++)
            {
                TrafficLights tl = Track.TrackLights[i];
                if (Track.TrackLights[i] && Track.TrackLights[i].IsClosed)
                {
                    if((i == 0 && Direction == -1) || (i == 1 && Direction == 1))
                    {
                        distanceToClosedLight = Mathf.Abs(engine.transform.position.x - tl.transform.position.x);
                        if (distanceToClosedLight <= 5000 && distanceToClosedLight > 1500)
                        {
                            Debug.Log("Light is Closed!!" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if(Mathf.Abs(instructionHandler) > 5)
                                instructionHandler = 5 * Direction;
                        }
                        else if (distanceToClosedLight <= 1500 && distanceToClosedLight > 250)
                        {
                            Debug.Log("Light is Closed!!" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if (Mathf.Abs(instructionHandler) > 2)
                                instructionHandler = 2 * Direction;
                        }
                        else if (distanceToClosedLight <= 250)
                        {
                            Debug.Log("Light is Closed!! I'll stop the engine" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
                            if (Mathf.Abs(instructionHandler) > 0)
                                instructionHandler = 0;
                        }
                    }
                   
                }

            }
        }
       
    }

    public void CoupleToCar()
    {
        GetAllExpectedCarsByDirection(Direction);
        GetExpectedCar();
        IsCoupling = true;
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
            distanceToCarX = Mathf.Abs(NearestCar.transform.position.x - engine.position.x);
            distanceToCarY = Mathf.Abs(NearestCar.transform.position.y - engine.position.y);

            if (distanceToCarX <= 5000 && distanceToCarX > 1500)
            {
                if (Mathf.Abs(instructionHandler) > 5)
                    instructionHandler = 5 * Direction;
            }
            if (distanceToCarX <= 3000 && distanceToCarX > 1500)
            {
                if (distanceToCarY <= 400)
                    if (Mathf.Abs(instructionHandler) > 3)
                        instructionHandler = 3 * Direction;
            }
            if (distanceToCarX <= 1500 && distanceToCarX > 40)
            {
                if (distanceToCarY <= 250)
                    if (Mathf.Abs(instructionHandler) > 2)
                        instructionHandler = 2 * Direction;
            }
            if (distanceToCarX <= 500)
            {
                if (distanceToCarY < 100)
                    if (Mathf.Abs(instructionHandler) > 0)
                        instructionHandler = 0 * Direction;                
            }            
        }
        
    }


    public void GetAllExpectedCarsByDirection(int _direction)
    {
        ExpectedСars.Clear();        
        foreach (RollingStock rc in cars)
        {
            if(rc.TrackCircuit == route.OccupiedTrack)
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
            foreach (RollingStock rc in ExpectedСars)
            {
                if(rc.CompositionNumberofRS != engineRS.CompositionNumberofRS)
                {
                    distanceToExpectedCarX = Mathf.Abs(rc.transform.position.x - engine.position.x);
                    if (NearestCar == null || distanceToExpectedCarX < NearestCar.transform.position.x - engine.position.x)
                    {
                        NearestCar = rc;
                    }
                }          
            }
        }else
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
}