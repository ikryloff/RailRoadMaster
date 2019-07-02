using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Engine : MonoBehaviour
{
    public int InstructionsHandler { get; set; }
    public RollingStock EngineRS { get; private set; }
    public bool Brakes { get; private set; }

    public int Direction { get; private set; }
    public float MaxSpeed { get; private set; }

    public float ForPauseTempAcceleration { get; private set; }
    public int SpeedReal { get; private set; }
    public float Acceleration { get; private set; }
    const float accForce= 0.15f;
    public EngineInertia Inertia { get; private set; }


    private bool isPaused;

    private void Awake()
    {
        EventManager.onPause += PauseMovingOn;
        EventManager.offPause += PauseMovingOff;
        
        Inertia = GetComponent<EngineInertia> ();
        EngineRS = GetComponent<RollingStock> ();
        Brakes = true;
    }

    void Start()
    {
        Acceleration = 0;        
    }

    void Update()
    {
        if ( !isPaused )
        {
            CalcRealSpeed ();
            CalcMaxSpeed ();
            MoveEngine (Time.deltaTime);
            Inertia.AddFriction ();
        }
    }

   

    public void MoveEngine(float dt)
    {
        if ( Brakes && SpeedReal == 0 )
        {
            Acceleration = 0;          
        }
        
        if ( SpeedReal != 0 && Math.Abs (SpeedReal) == MaxSpeed  )
        {
            Brakes = false;
            Acceleration += 0;
        }
        else
        {
            if ( Math.Abs (SpeedReal) < MaxSpeed )
            {
                Brakes = false;
                Acceleration += (accForce - Inertia.InertiaValue * accForce) * Direction * dt;
            }                
            else if ( Math.Abs (SpeedReal) > MaxSpeed )
            {
                Brakes = true;
                Acceleration -= Inertia.GetBreakeForce () * GetOpositeDirection () * dt;
            }
                
        }
       

    }

    public int GetOpositeDirection()
    {
        return SpeedReal / Math.Abs (SpeedReal);
    }

    public void CalcMaxSpeed()
    {
        HandlerValidation ();
        Direction = InstructionsHandler > 0 ? 1 : -1;

        if ( InstructionsHandler == 0 )
        {
            MaxSpeed = 0;
            Direction = 0;
            Brakes = true;
        }
        else Brakes = false;

        int absHandler = Mathf.Abs (InstructionsHandler);
        if ( absHandler == 1 ) MaxSpeed = 3;
        if ( absHandler == 2 ) MaxSpeed = 5;
        if ( absHandler == 3 ) MaxSpeed = 10;
        if ( absHandler == 4 ) MaxSpeed = 15;
        if ( absHandler == 5 ) MaxSpeed = 25;
        if ( absHandler == 6 ) MaxSpeed = 45;
    }

    private void HandlerValidation()
    {
        if ( InstructionsHandler > 6 ) InstructionsHandler = 6;
        if ( InstructionsHandler < -6 ) InstructionsHandler = -6;
    }  

    public void PauseMovingOn()
    {
        ForPauseTempAcceleration = Acceleration;
        Acceleration = 0;
        isPaused = true;
    }

    public void PauseMovingOff()
    {
        Acceleration = ForPauseTempAcceleration;
        isPaused = false;
    }

    private void CalcRealSpeed()
    {
        SpeedReal = (int)Mathf.Ceil (EngineRS.Translation * 25);
    }

   

}