using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class EngineRun : MonoBehaviour
{
    public float stopPos;
    public int direction;
    public float distance;
    public Engine engine;
    public Rigidbody2D engineRB;   



    public void InitEngineRun(Engine _engine, int _direction, float _distanceInCars)
    {
        engine = _engine;
        direction = _direction;
        distance = _distanceInCars * 390f;
        stopPos = engine.transform.position.x + direction * distance;
    }


    private void Start()
    {
        engineRB = engine.GetComponent<Rigidbody2D>();
        if (direction == 1)
            engine.EngineInstructionsForward();
        else
            engine.EngineInstructionsBackwards();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(stopPos - engine.transform.position.x) <= 100 || !engine.IsEngineCanMove)
        {
            engine.EngineInstructionStop();
            Destroy(this);
        }


    }
}

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
    [SerializeField]
    private RollingStock nearestCar;
    [SerializeField]
    private float power;

    [SerializeField]
    private string startCompositionNumber;

    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    [SerializeField]
    private bool isEngineGoesAhead;
    [SerializeField]
    private bool isEngineMovesDistance;

    [SerializeField]
    private bool isEngineCanMove;
    private string report;
    [SerializeField]
    TrafficLights tlForward;
    [SerializeField]
    TrafficLights tlBackward;



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
                Power = 46626 * Mathf.Pow((float)Math.E, -0.062f * MSpeed);
                break;
            case 7:
                Power = 43674 * Mathf.Pow((float)Math.E, -0.066f * MSpeed);
                break;
            case 6:
                Power = 44827 * Mathf.Pow((float)Math.E, -0.079f * MSpeed);
                break;
            case 5:
                Power = 40474 * Mathf.Pow((float)Math.E, -0.092f * MSpeed);
                break;
            case 4:               
                Power = 33323 * Mathf.Pow((float)Math.E, -0.102f * MSpeed);
                break;
            case 3:               
                Power = 35744 * Mathf.Pow((float)Math.E, -0.156f * MSpeed);
                break;

            case 2:
                Power = 24623 * Mathf.Pow((float)Math.E, -0.272f * MSpeed);
                break;

            case 1:
                Power = 21808 * Mathf.Pow((float)Math.E, -0.794f * MSpeed);                
                break;
            default:
                Power = 0;                
                break;
        }
        AddForceToEngine(power);

    }

    void AddForceToEngine(float _power)
    {
        engine.AddRelativeForce(new Vector2(_power * Direction, 0), ForceMode2D.Force);
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
                    engine.AddRelativeForce(new Vector2(-3000, 0), ForceMode2D.Force);
                else if (engine.velocity.x < -3f)
                    engine.AddRelativeForce(new Vector2(3000, 0), ForceMode2D.Force);
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
        IsEngineCanMove = false;
    }
    public void EngineControllerToZero()
    {
        ControllerPosition = 0;

    }

    public void EngineInstructionsForward()
    {
        route.IsPathCheckingForward = true;
        Direction = Direction == -1 && Direction != 0 ? -1 : 1;
        if(Direction == 1 && instructionHandler == 0)
            route.MakePath(Direction);
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
        if(Direction == -1 && instructionHandler == 0)
            route.MakePath(Direction);
        GetAllExpectedCarsByDirection(Direction);
        ReleaseBrakes();
        instructionHandler--;
        if (instructionHandler == 0)
            Direction = 0;
        if (instructionHandler == -7)
            instructionHandler = -6;
        StartCompositionNumber = EngineRS.CompositionNumberString;

    }


    public void MakeEngineRun()
    {
        IsEngineCanMove = true;
        EngineRun run = gameObject.AddComponent<EngineRun>();
        run.InitEngineRun(this, 1, 1);
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

    public bool IsEngineGoesAheadByDirection(int _direction)
    {
        if ((_direction == 1 && !EngineRS.ActiveCoupler.JointCar) || (_direction == -1 && !EngineRS.PassiveCoupler.IsPassiveCoupleConnected))
        {
            IsEngineGoesAhead = true;
            return true;
        }
        else
        {
            IsEngineGoesAhead = false;
            return false;
        }
    }

    public void DriveByInstructions()
    {
        PrintHandler();
        if (IsEngineGoesAheadByDirection(Direction))
        {
            DriveAccordingToCarPresence();
            DriveAccordingToLights();
        }
        // Stop after each coupling
        if (EngineRS.CompositionNumberString != StartCompositionNumber)
        {
            EngineInstructionStop();
            instructionHandler = 0;
            IsCoupling = false;
            NearestCar = null;
            ExpectedСars.Clear();
        }

        if (instructionHandler == 0)
        {
            MaxSpeed = 0;
            EngineInstructionStop();
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
            if (MSpeed - MaxSpeed > 2 || MaxSpeed == 0)
                EngineControllerUseBrakes();
            else
                EngineControllerReleaseAll();
        }
        else
        {
            ReleaseBrakes();
            if (MSpeed < 3)
                ControllerPosition = 1 * Direction;
            if (MSpeed >= 3 && MSpeed < 10)
                ControllerPosition = 2 * Direction;
            if (MSpeed >= 10 && MSpeed < 15)
                ControllerPosition = 3 * Direction;
            if (MSpeed >= 15 && MSpeed < 25)
                ControllerPosition = 4 * Direction;
            if (MSpeed >= 25 && MSpeed < 40)
                ControllerPosition = 5 * Direction;
            if (MSpeed >= 40)
                ControllerPosition = 6 * Direction;
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
        if (route.LastRouteTrackForward)
            TlForward = route.LastRouteTrackForward.TrackLights[1];

        if (route.LastRouteTrackBackward)
            TlBackward = route.LastRouteTrackBackward.TrackLights[0];
        TrafficLights templight = null;
        if (Direction == 1)
            templight = TlForward;
        if (Direction == -1)
            templight = TlBackward;

        if(templight)
            GetDistanceToLight(templight);

        if (DistanceToLight <= 5000 && DistanceToLight > 1500)
        {
            //Debug.Log("Light is Closed!!" + " TL " + tl + "Distance " + Mathf.Abs(engine.transform.position.x - tl.transform.position.x));
            if (Mathf.Abs(instructionHandler) > 5)
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
            {
                EngineInstructionStop();
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

    public void DriveAccordingToCarPresence()
    {
        GetExpectedCar();
        if (NearestCar && !IsCoupling)
        {
            if (DistanceToCar < 300)
            {
                NearestCar = null;
            }

            else if (DistanceToCar <= 5000 && DistanceToCar > 1500)
            {
                if (Mathf.Abs(instructionHandler) > 5)
                    instructionHandler = 5 * Direction;
            }
            else if (DistanceToCar <= 3000 && DistanceToCar > 1500)
            {
                if (Mathf.Abs(instructionHandler) > 3)
                    instructionHandler = 3 * Direction;
            }
            else if (DistanceToCar <= 1500 && DistanceToCar > 600)
            {
                if (Mathf.Abs(instructionHandler) > 2)
                    instructionHandler = 2 * Direction;
            }
            else if (DistanceToCar <= 600 && DistanceToCar >= 300)
            {
                if (Mathf.Abs(instructionHandler) > 0)
                {
                    if (NearestCar.transform.position.x < transform.position.x && Direction == -1 || NearestCar.transform.position.x > transform.position.x && Direction == 1)
                    {
                        Debug.Log("Can't move, the car is ahead");
                        IsEngineCanMove = false;
                        EngineInstructionStop();
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

            if (rc.TrackCircuit == route.OccupiedTrack || rc.TrackCircuit == EngineRS.TrackCircuit)
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

    public void GetDistanceToExpetedCar()
    {
        RollingStock tempCarFoward = null;
        RollingStock tempCarBackward = null;
        

        float tempCarCoordForward = float.MaxValue;
        float tempCarCoordBackward = - float.MaxValue;

        float tempLightCoordForward = float.MaxValue;
        float tempLightCoordBackward = -float.MaxValue;

        route.MakePath(1);

        foreach (RollingStock rc in cars)
        {

            if (rc.TrackCircuit == route.OccupiedTrack || rc.TrackCircuit == EngineRS.TrackCircuit)
            {
                if (rc.transform.position.x < tempCarCoordForward && rc.transform.position.x > engine.transform.position.x)
                {
                    tempCarFoward = rc;
                    tempCarCoordForward = rc.transform.position.x;
                }

            }
        }

        route.MakePath(-1);

        foreach (RollingStock rc in cars)
        {

            if (rc.TrackCircuit == route.OccupiedTrack || rc.TrackCircuit == EngineRS.TrackCircuit)
            {
                if (rc.transform.position.x > tempCarCoordBackward && rc.transform.position.x < engine.transform.position.x)
                {
                    tempCarBackward = rc;
                    tempCarCoordBackward = rc.transform.position.x;
                }

            }
        }
        if(route.LastRouteTrackForward)
        {
            tempLightCoordForward = route.LastRouteTrackForward.TrackLights[1].transform.position.x;
            
        }            
        if (route.LastRouteTrackBackward)
            tempLightCoordBackward = route.LastRouteTrackBackward.TrackLights[0].transform.position.x;

        ShowDistanceToStop(tempCarCoordForward, tempLightCoordForward, tempCarCoordBackward, tempLightCoordBackward);
    }

    public void GetExpectedCar()
    {

        if (ExpectedСars != null)
        {
            NearestCar = null;
            foreach (RollingStock rc in ExpectedСars)
            {
                if (rc.CompositionNumberofRS != engineRS.CompositionNumberofRS)
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
    

    public void ShowDistanceToStop(float fCar, float fLight, float bCar, float bLight)
    {
        report = "";
        Debug.Log(fCar + "  " + fLight + "  " + bCar + "  " + bLight);
        if (IsEngineGoesAheadByDirection(1))
        {
            if (fCar <= fLight && fCar < 10000)
                report += " Distance to Car forward " + (int)((Mathf.Abs(fCar - engine.transform.position.x) - 300) / 350) ;
            else if (fCar > fLight && fLight < 10000)
                report += " Distance to signal forward " + (int)((Mathf.Abs(fLight - engine.transform.position.x) - 150) / 350);
            else
                report += " Forward can't see!";
        }        
        else
            report += " Forward can't say!";

        if (IsEngineGoesAheadByDirection(-1))
        {
            if (bCar >= bLight && bCar > -10000)
                report += " Distance to Car backward " + (int)((Mathf.Abs(bCar - engine.transform.position.x) - 300) / 350);
            else if (bCar < bLight && bLight > -10000)
                report += " Distance to signal backward " + (int)((Mathf.Abs(bLight - engine.transform.position.x) - 150) / 350);
            else
                report += " Backward can't see!" + bCar + " " + bLight;
        }           
        else
            report += " Backward can't say!!";

        Debug.Log(report);

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

    public bool IsEngineMovesDistance
    {
        get
        {
            return isEngineMovesDistance;
        }

        set
        {
            isEngineMovesDistance = value;
        }
    }

    public bool IsEngineCanMove
    {
        get
        {
            return isEngineCanMove;
        }

        set
        {
            isEngineCanMove = value;
        }
    }

    public float Power
    {
        get
        {
            return power;
        }

        set
        {
            power = value;
        }
    }

    public TrafficLights TlBackward
    {
        get
        {
            return tlBackward;
        }

        set
        {
            tlBackward = value;
        }
    }

    public TrafficLights TlForward
    {
        get
        {
            return tlForward;
        }

        set
        {
            tlForward = value;
        }
    }
}