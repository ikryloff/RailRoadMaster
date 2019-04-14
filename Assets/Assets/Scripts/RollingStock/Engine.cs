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
        distance = _distanceInCars * 39f;
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
        if (Mathf.Abs(stopPos - engine.transform.position.x) <= 10 || !engine.IsEngineCanMove)
        {
            engine.EngineInstructionStop();
            Destroy(this);
        }


    }
}

public class Engine : MonoBehaviour
{
    public static int instructionHandler;

    private Rigidbody engine;
    public RollingStock engineRS;
    
    [SerializeField]
    private List<RollingStock> expectedСars;
    private Route route;
    [SerializeField]
    private bool brakes = true;
    public float controllerPosition;
    [SerializeField]
    private Text speedTxt;
    [SerializeField]
    private Text throttleTxt;
    [SerializeField]
    private Text directionTxt;
    [SerializeField]
    private Text handlerTxt;
    public CompositionManager cm;    
    
    public int direction;
    private int directionInstructions;
    public int maxSpeed;
    private bool isDrivingByInstructionsIsOn;
    [SerializeField]
    private TrackCircuit track;
    private float distanceToLight;
    [SerializeField]
    private float distanceToCar;
    private float distanceToExpectedCar;
    [SerializeField]
    private bool isCoupling;    
    [SerializeField]
    private RollingStock nearestCar;
    public float power;
    [SerializeField]
    float breakeForce;

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
    public int movingDirection;
    public PathMaker pathmaker;
    int speed;
    public float acceleration;
    public TrackPath trackPath;
    SwitchManager switchManager;

    public List<TrackCircuit> tcList;

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
        route = GameObject.Find("Route").GetComponent<Route>();
        tcList = new List<TrackCircuit>();
        ExpectedСars = new List<RollingStock>();
        engineRS = GetComponent<RollingStock>();
        trackPath = FindObjectOfType<TrackPath>();
        switchManager = FindObjectOfType<SwitchManager>();
        // Cashing hand switches

        switch18 = pathmaker.GetSwitchByName("Switch_18");
        switch19 = pathmaker.GetSwitchByName("Switch_19");
        switch20 = pathmaker.GetSwitchByName("Switch_20");
        switch21 = pathmaker.GetSwitchByName("Switch_21");
        switch22 = pathmaker.GetSwitchByName("Switch_22");
        switch10 = pathmaker.GetSwitchByName("Switch_10");
        switch12 = pathmaker.GetSwitchByName("Switch_12");
        switch14 = pathmaker.GetSwitchByName("Switch_14");
        GetTrack();
    }

    void Start()
    {

        engine = GetComponent<Rigidbody>();

        // conductor mode
        IsDrivingByInstructionsIsOn = true;
        InformationUpdateFunction();
        InvokeRepeating("InformationUpdateFunction", 0.3f, 0.3f);
        acceleration = 0;
        
        

    }
    public void MoveEngine()
    {
        if (Math.Abs(speed) == maxSpeed)
            acceleration += 0;
        else
        {
            if (Math.Abs(speed) < maxSpeed)
                acceleration += 0.2f * direction * Time.deltaTime;
            else if (Math.Abs(speed) > maxSpeed)
            {
                acceleration -= 0.6f * speed/Math.Abs(speed) * Time.deltaTime;                
            }
        } 
        if(brakes && speed == 0)
        {
            acceleration = 0;
        }
        
    }

    

    void InformationUpdateFunction()
    {
        if (EngineRS.Number == "8888")
        {
            speedTxt.text = "Speed: " + speed;
            throttleTxt.text = "Throttle: " + Mathf.Abs(controllerPosition);
            directionTxt.text = "Direction: " + direction;
        }

    }
      
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    void Update()
    {

        //CheckMovingDirection();
        PrintHandler();
        //GetTrack();
        

        speed = (int)(engineRS.Translation * 15);
        
        
        if (IsDrivingByInstructionsIsOn)
            DriveByInstructions();
        MoveEngine();
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public void ReleaseBrakes()
    {
        brakes = false;
        /*if (cm.CompositionsList.Any())
        {
            foreach (RollingStock rs in cm.CompositionsList[EngineRS.CompositionNumberofRS])
            {
                rs.Brakes = false;
            }
        }
        */

    }
   
    public void EngineControllerReleaseAll()
    {
        ReleaseBrakes();
        controllerPosition = 0;
    }

    public void EngineControllerUseBrakes()
    {
        brakes = true;        
    }

    public void EngineInstructionStop()
    {
        brakes = true;
        direction = 0;
        controllerPosition = 0;
        instructionHandler = 0;
    }
    
   

    public void EngineInstructionsForward()
    {
        direction = direction == -1 && direction != 0 ? -1 : 1;
        if(direction == 1)
        {
            //pathmaker.GetFullPath(direction);
        }   
        //GetAllExpectedCarsByDirection(direction);
        ReleaseBrakes();
        if(instructionHandler != 0)
        {
            instructionHandler++;
            if (instructionHandler == 0)
            {
                EngineInstructionStop();

            }
        }
        else
        {
            if (speed >= -1)
            {
                instructionHandler++;
            }
            else
                instructionHandler = 0;
            engineRS.ChangeDirection();
        }        
            
        if (instructionHandler == 7)
            instructionHandler = 6;
        StartCompositionNumber = EngineRS.CompositionNumberString;
    }

    public void EngineInstructionsBackwards()
    {
        direction = direction == 1 && direction != 0 ? 1 : -1;
        if(direction == -1)
        {
            //pathmaker.GetFullPath(direction);
        }
            
        //GetAllExpectedCarsByDirection(direction);        
        ReleaseBrakes();        

        if (instructionHandler != 0)
        {
            instructionHandler--;
            if (instructionHandler == 0)
            {
                EngineInstructionStop();

            }
        }
        else
        {
            if (speed <= 1)
            {
                instructionHandler--;
            }
            else
                instructionHandler = 0;
            engineRS.ChangeDirection();
        }
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

    public bool IsEngineGoesAhead()
    {
        
        return false;
    }

    public bool IsEngineGoesAheadByDirection(int _direction)
    {
        return false;
    }

    public void DriveByInstructions()
    {
        if (IsEngineGoesAheadByDirection(direction))
        {
            //DriveAccordingToCarPresence();
            DriveAccordingToLights();
        }
        // Stop after each coupling
        if (EngineRS.CompositionNumberString != StartCompositionNumber)
        {
            EngineInstructionStop();            
            IsCoupling = false;
            NearestCar = null;
            ExpectedСars.Clear();
        }

        if (instructionHandler == 0)
        {
            maxSpeed = 0;
            EngineInstructionStop();
            
        }

        int absHandler = Mathf.Abs(instructionHandler);
       
        if (absHandler == 1)
        {
            maxSpeed = 3;
            controllerPosition = 1;
        }
            
        if (absHandler == 2)
        {
            maxSpeed = 5;
            controllerPosition = 2;
        }
            
        if (absHandler == 3)
        {
            maxSpeed = 10;
            controllerPosition = 3;
        }
            
        if (absHandler == 4)
        {
            maxSpeed = 15;
            controllerPosition = 4;
        }
            
        if (absHandler == 5)
        {
            maxSpeed = 25;
            controllerPosition = 5;
        }
            
        if (absHandler == 6)
        {
            maxSpeed = 40;
            controllerPosition = 6;
        }

        if (speed == maxSpeed)
        {
            EngineControllerReleaseAll();
        }

        if (speed > maxSpeed || maxSpeed == 0)
        {
            EngineControllerUseBrakes();   
        }
        else 
        {
            ReleaseBrakes();       
        }
        

    }

    private void PrintHandler()
    {
        if (direction > 0)
            handlerTxt.text = "  >>> " + Mathf.Abs(instructionHandler) + ">>> " + maxSpeed;
        else if (direction < 0)
            handlerTxt.text = maxSpeed + "  <<< " + Mathf.Abs(instructionHandler) + " <<<";
        else
            handlerTxt.text = "  <<< 0 >>>";
    }

    public void GetTrack()
    {
        Track = EngineRS.OwnTrackCircuit;
    }

    public void GetDistanceToLight(TrafficLights tLights)
    {
        DistanceToLight = Mathf.Abs(engine.transform.position.x - tLights.transform.position.x);
    }

    public void DriveAccordingToLights()
    {
        if (pathmaker.lastRouteTrackForward)
            TlForward = pathmaker.lastRouteTrackForward.TrackLights[1];

        if (pathmaker.lastRouteTrackBackward)
            TlBackward = pathmaker.lastRouteTrackBackward.TrackLights[0];
        TrafficLights templight = null;
        if (direction == 1)
            templight = TlForward;
        if (direction == -1)
            templight = TlBackward;

        if(templight)
            GetDistanceToLight(templight);

        if (DistanceToLight <= 300 && DistanceToLight > 150)
        {
            //Debug.Log("Light is Closed!!" + " TL "  + "Distance " );
            if (Mathf.Abs(instructionHandler) > 5)
                instructionHandler = 5 * direction;
        }
        else if (DistanceToLight <= 150 && DistanceToLight > 25)
        {
            //Debug.Log("Light is Closed!!" + " TL " + "Distance ");
            if (Mathf.Abs(instructionHandler) > 2)
                instructionHandler = 2 * direction;
        }
        else if (DistanceToLight <= 25 && DistanceToLight >= 6)
        {
            //Debug.Log("Light is Closed!! I'll stop the engine");
            if (Mathf.Abs(instructionHandler) > 0)
            {
                EngineInstructionStop();
            }
                
        }             

    }


    public void CoupleToCar()
    {
        IsCoupling = IsCoupling ? false : true;
        GetAllExpectedCarsByDirection(direction);
        GetExpectedCar();
        if (NearestCar && EngineRS.CompositionNumberofRS != nearestCar.CompositionNumberofRS)
        {
            instructionHandler = 1 * direction;
        }
    }

    public void DriveAccordingToCarPresence()
    {
        GetExpectedCar();
        if (NearestCar && !IsCoupling)
        {
            if (DistanceToCar < 30)
            {
                NearestCar = null;
            }

            else if (DistanceToCar <= 500 && DistanceToCar > 150)
            {
                if (Mathf.Abs(instructionHandler) > 5)
                    instructionHandler = 5 * direction;
            }
            else if (DistanceToCar <= 300 && DistanceToCar > 150)
            {
                if (Mathf.Abs(instructionHandler) > 3)
                    instructionHandler = 3 * direction;
            }
            else if (DistanceToCar <= 150 && DistanceToCar > 60)
            {
                if (Mathf.Abs(instructionHandler) > 2)
                    instructionHandler = 2 * direction;
            }
            else if (DistanceToCar <= 60 && DistanceToCar >= 30)
            {
                if (Mathf.Abs(instructionHandler) > 0)
                {
                    if (NearestCar.transform.position.x < transform.position.x && direction == -1 || NearestCar.transform.position.x > transform.position.x && direction == 1)
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

        
    }

    public void GetDistanceToExpetedCar()
    {
        RollingStock tempCarFoward = null;
        RollingStock tempCarBackward = null;
        

        float tempCarCoordForward = float.MaxValue;
        float tempCarCoordBackward = - float.MaxValue;

        float tempLightCoordForward = float.MaxValue;
        float tempLightCoordBackward = -float.MaxValue;

        pathmaker.GetFullPath(1);

       

        pathmaker.GetFullPath(-1);

        
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
            if (fCar <= fLight && fCar < 1000)
                report += " Distance to Car forward " + (int)((Mathf.Abs(fCar - engine.transform.position.x) - 30) / 35) ;
            else if (fCar > fLight && fLight < 1000)
                report += " Distance to signal forward " + (int)((Mathf.Abs(fLight - engine.transform.position.x) - 15) / 35);
            else
                report += " Forward can't see!";
        }        
        else
            report += " Forward can't say!";

        if (IsEngineGoesAheadByDirection(-1))
        {
            if (bCar >= bLight && bCar > -1000)
                report += " Distance to Car backward " + (int)((Mathf.Abs(bCar - engine.transform.position.x) - 30) / 35);
            else if (bCar < bLight && bLight > -1000)
                report += " Distance to signal backward " + (int)((Mathf.Abs(bLight - engine.transform.position.x) - 15) / 35);
            else
                report += " Backward can't see!" + bCar + " " + bLight;
        }           
        else
            report += " Backward can't say!!";

        Debug.Log(report);

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

    
    public void CheckMovingDirection()
    {
        movingDirection = engine.velocity.x > 0 ? 1 : -1;              
    }
}