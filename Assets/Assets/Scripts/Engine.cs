using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public static int instructionHandler = 0;
    
    private Rigidbody2D engine;
    private RollingStock engineRS;
    private bool brakes = true;
    private int controllerPosition = 0;
    [SerializeField]
    private Text speedTxt;
    [SerializeField]
    private Text throttleTxt;
    [SerializeField]
    private Text directionTxt;
    public CompositionManager cm;
    public float mSpeed;
    public int direction;
    private int directionInstructions;
    private int absControllerPosition;
    public int maxSpeed;
    private bool isDrivingByInstructionsIsOn;
    [SerializeField]
    Button brakesButton;
    Image brakesBtnColor;

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
            if (ControllerPosition != 0)
            {
                if (AbsControllerPosition / ControllerPosition > 0)
                    direction = 1;
                else if (AbsControllerPosition / ControllerPosition < 0)
                    direction = -1;
            }
            else
                direction = 1;
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

    public int DirectionInstructions
    {
        get
        {
            return instructionHandler/ Mathf.Abs(instructionHandler);
        }

        set
        {
            directionInstructions = value;
        }
    }

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
        

    }

    void Start()
    {
        
        engine = GetComponent<Rigidbody2D>();
        engineRS = GetComponent<RollingStock>();
        if (brakesButton)
            brakesBtnColor = brakesButton.GetComponent<Image>();
        if (engineRS.Number == "8888")
        {
            speedTxt.text = "Speed: ";
            throttleTxt.text = "Throttle: ";
            directionTxt.text = "Direction: ";
        }
            
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
        engine.AddRelativeForce(new Vector2(power * Direction, 0), ForceMode2D.Force);       
    }

    void FixedUpdate()
    {
        
        MSpeed = (int)(Time.deltaTime * engine.velocity.magnitude * 5);
        if(engineRS.Number == "8888")
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
                    foreach (RollingStock rs in cm.CompositionsList[engineRS.CompositionNumberofRS])
                    {
                        rs.Brakes = true;
                        //Debug.Log(rs.Number + " use brakes");
                    }
                }
            }
            if(engineRS.Number == "8888")
                brakesBtnColor.color = Color.red;
        }
        else if(!Brakes)
        {            
            if (engine.velocity.x > 0)
            {
                engine.AddRelativeForce(new Vector2(-30f, 0), ForceMode2D.Force);
            }
            else if (engine.velocity.x < 0)
                engine.AddRelativeForce(new Vector2(30, 0), ForceMode2D.Force);
            if (engineRS.Number == "8888")
                brakesBtnColor.color = Color.white;
        }
    }

    public void ReleaseBrakes()
    {
        Brakes = false;
        if (cm.CompositionsList.Any())
        {
            foreach (RollingStock rs in cm.CompositionsList[engineRS.CompositionNumberofRS])
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
        ControllerPosition = 0;
        instructionHandler = 0;
    }
    public void EngineControllerToZero()
    {
        ControllerPosition = 0;
    }

    public void EngineInstructionsForward()
    {
        IsDrivingByInstructionsIsOn = true;
        ReleaseBrakes();
        instructionHandler++;        
        Debug.Log(instructionHandler);               
    }

    public void EngineInstructionsBackwards()
    {
        IsDrivingByInstructionsIsOn = true;
        ReleaseBrakes();        
        instructionHandler--;
        Debug.Log(instructionHandler);        
    }

    public void DriveByInstructions()
    {
        if (instructionHandler== 0)
        {
            MaxSpeed = 0;
            EngineControllerUseBrakes();
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
                ControllerPosition = 1 * DirectionInstructions;
            if (MSpeed >= 10 && MSpeed < 15)
                ControllerPosition = 2 * DirectionInstructions;
            if (MSpeed >= 15 && MSpeed < 25)
                ControllerPosition = 4 * DirectionInstructions;
            if (MSpeed >= 25)
                    ControllerPosition = 8 * DirectionInstructions;
        }  
    }
}