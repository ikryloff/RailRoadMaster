using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{

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
    private float mSpeed;
    private int direction;
    private int absControllerPosition;
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
                if (mSpeed < 10)
                    AddForceToEngine(20200);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(18500);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(14000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(11500);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(14000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(7800);
                if (mSpeed >= 35 && mSpeed < 40)
                    AddForceToEngine(6000);
                if (mSpeed >= 40 && mSpeed < 45)
                    AddForceToEngine(5000);
                if (mSpeed >= 45 && mSpeed < 50)
                    AddForceToEngine(4000);
                if (mSpeed >= 50 && mSpeed < 55)
                    AddForceToEngine(3500);
                if (mSpeed >= 55 && mSpeed < 60)
                    AddForceToEngine(2700);
                if (mSpeed >= 60 && mSpeed < 110)
                    AddForceToEngine(1500);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 7:
                if (mSpeed < 10)
                    AddForceToEngine(20500);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(16000);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(13000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(10000);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(8000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(6000);
                if (mSpeed >= 35 && mSpeed < 40)
                    AddForceToEngine(5000);
                if (mSpeed >= 40 && mSpeed < 45)
                    AddForceToEngine(4000);
                if (mSpeed >= 45 && mSpeed < 50)
                    AddForceToEngine(3000);
                if (mSpeed >= 50 && mSpeed < 55)
                    AddForceToEngine(2000);
                if (mSpeed >= 55 && mSpeed < 60)
                    AddForceToEngine(1500);
                if (mSpeed >= 60 && mSpeed < 110)
                    AddForceToEngine(1000);
                if(mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 6:
                if (mSpeed < 10)
                    AddForceToEngine(20500);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(14000);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(11000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(8000);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(6000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(5000);
                if (mSpeed >= 35 && mSpeed < 40)
                    AddForceToEngine(3800);
                if (mSpeed >= 40 && mSpeed < 45)
                    AddForceToEngine(3000);
                if (mSpeed >= 45 && mSpeed < 50)
                    AddForceToEngine(2100);
                if (mSpeed >= 50 && mSpeed < 55)
                    AddForceToEngine(1800);
                if (mSpeed >= 55 && mSpeed < 60)
                    AddForceToEngine(1000);
                if (mSpeed >= 60 && mSpeed < 110)
                    AddForceToEngine(800);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 5:
                if (mSpeed < 5)
                    AddForceToEngine(20500);
                if (mSpeed >= 5 && mSpeed < 10)
                    AddForceToEngine(18000);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(11800);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(9000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(6000);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(4000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(3200);
                if (mSpeed >= 35 && mSpeed < 40)
                    AddForceToEngine(2800);
                if (mSpeed >= 40 && mSpeed < 45)
                    AddForceToEngine(2000);
                if (mSpeed >= 45 && mSpeed < 50)
                    AddForceToEngine(1600);
                if (mSpeed >= 50 && mSpeed < 55)
                    AddForceToEngine(1100);
                if (mSpeed >= 55 && mSpeed < 60)
                    AddForceToEngine(900);
                if (mSpeed >= 60 && mSpeed < 110)
                    AddForceToEngine(700);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 4:
                if (mSpeed < 5)
                    AddForceToEngine(20500);
                if (mSpeed >= 5 && mSpeed < 10)
                    AddForceToEngine(13000);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(9500);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(6000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(4000);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(3000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(2000);
                if (mSpeed >= 35 && mSpeed < 40)
                    AddForceToEngine(1700);
                if (mSpeed >= 40 && mSpeed < 45)
                    AddForceToEngine(1100);
                if (mSpeed >= 45 && mSpeed < 50)
                    AddForceToEngine(900);
                if (mSpeed >= 50 && mSpeed < 110)
                    AddForceToEngine(700);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 3:
                if (mSpeed < 5)
                    AddForceToEngine(20500);
                if (mSpeed >= 5 && mSpeed < 10)
                    AddForceToEngine(11000);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(3000);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(2000);
                if (mSpeed >= 20 && mSpeed < 25)
                    AddForceToEngine(1200);
                if (mSpeed >= 25 && mSpeed < 30)
                    AddForceToEngine(1000);
                if (mSpeed >= 30 && mSpeed < 35)
                    AddForceToEngine(900);
                if (mSpeed >= 35 && mSpeed < 110)
                    AddForceToEngine(700);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;

            case 2:
                if (mSpeed < 5)
                    AddForceToEngine(12000);
                if (mSpeed >= 5 && mSpeed < 10)
                    AddForceToEngine(3900);
                if (mSpeed >= 10 && mSpeed < 15)
                    AddForceToEngine(1500);
                if (mSpeed >= 15 && mSpeed < 20)
                    AddForceToEngine(1000);
                if (mSpeed >= 20 && mSpeed < 110)
                    AddForceToEngine(500);
                if (mSpeed > 110)
                    AddForceToEngine(0);
                break;
            case 1:
                if (mSpeed < 1)
                    AddForceToEngine(11000);
                if (mSpeed >= 1 && mSpeed < 10)
                    AddForceToEngine(2000);
                if (mSpeed >= 10 )
                    AddForceToEngine(500);
                if (mSpeed > 110)
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
        
        mSpeed = (int)(Time.deltaTime * engine.velocity.magnitude * 5);
        if(engineRS.Number == "8888")
        {
            speedTxt.text = "Speed: " + mSpeed;
            throttleTxt.text = "Throttle: " + Mathf.Abs(ControllerPosition);
            directionTxt.text = "Direction: " + Direction;
        }
        MoveEngine();
        if (Brakes)
        {
            if(mSpeed > 0)
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

    public void EngineUseBrakes()
    {
        Brakes = Brakes != false ? false : true;        
    }
    public void EngineControllerToZero()
    {
        ControllerPosition = 0;
    }


}