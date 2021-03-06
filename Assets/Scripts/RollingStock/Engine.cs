﻿using System;
using UnityEngine;


public class Engine : MonoBehaviour
{
    public int InstructionsHandler;
    
    public RollingStock EngineRS { get; private set; }
    public TrafficWatcher TWatcher { get; private set; }
    public EngineInertia Inertia { get; private set; }
    public EngineAI EngineAI { get; private set; }
    public bool Brakes;
    //it can move itself
    public bool IsActive;
    // it is rulled by player
    public bool IsPlayer;

    public int Direction { get; private set; }
    public float MaxSpeed { get; private set; }

    public float ForPauseTempAcceleration { get; private set; }
    public int SpeedReal { get; private set; }
    public float Acceleration { get; set; }
    //common var to set step of moving
    public float EngineStep { get; set; }
    private int koeff = 25;

    private int absHandler;

    const float accForce = 0.015f;


    private bool isPaused;
    private EngineLightning engineLightning;

    private void Awake()
    {
        EventManager.onPause += PauseMovingOn;
        EventManager.offPause += PauseMovingOff;

        EngineRS = GetComponent<RollingStock> ();
        EngineAI = GetComponent<EngineAI> ();
        Inertia = GetComponent<EngineInertia> ();
        engineLightning = GetComponent<EngineLightning> ();
        TWatcher = GetComponent<TrafficWatcher> ();
        Brakes = true;
        IsActive = false;
    }

    void Start()
    {
        Acceleration = 0;
        EngineStep = 0;
    }

    public void RunEngineAction()
    {
        if ( !isPaused && IsActive)
        {
            CalcRealSpeed ();
            CalcMaxSpeed ();
            MoveEngine ();            
        }
        if ( TWatcher.enabled )
            TWatcher.OnUpdate ();
        
    }

    public void AbsoluteStop()
    {
        InstructionsHandler = 0;
        engineLightning.NoLight ();
        Acceleration = 0;
    }

    public void HandlerZero()
    {
        InstructionsHandler = 0;
        engineLightning.NoLight ();
    }
    public void HandlerForward()
    {
        InstructionsHandler++;
        engineLightning.TurnAnyLight (InstructionsHandler);
    }
    public void HandlerBack()
    {
        InstructionsHandler--;
        engineLightning.TurnAnyLight (InstructionsHandler);
    }

    public void MoveEngine()
    {
        if ( Brakes && SpeedReal == 0 )
        {
            Acceleration = 0;
        }

        if ( SpeedReal != 0 && Math.Abs (SpeedReal) == MaxSpeed )
        {
            Brakes = false;
            Acceleration += 0;
        }
        else
        {
            if ( Math.Abs (SpeedReal) < MaxSpeed )
            {
                Brakes = false;
                Acceleration += accForce  * Direction;
            }
            else if ( Math.Abs (SpeedReal) > MaxSpeed )
            {
                Brakes = true;
                Acceleration -= Inertia.GetBreakeForce() * GetOpositeDirection();
            }

        }
        EngineStep = Acceleration * Time.deltaTime * koeff;
    }

    public int GetOpositeDirection()
    {
        return SpeedReal / Math.Abs (SpeedReal);
    }

    public void CalcMaxSpeed()
    {
        HandlerValidation ();
        Direction = InstructionsHandler > 0 ? 1 : -1;

        if ( InstructionsHandler == 0  )
        {
            MaxSpeed = 0;
            Brakes = true;
            Direction = 0;
        }
        else Brakes = false;

        absHandler = Mathf.Abs (InstructionsHandler);
        if ( absHandler == 1 ) MaxSpeed = 3;
        if ( absHandler == 2 ) MaxSpeed = 5;
        if ( absHandler == 3 ) MaxSpeed = 10;
        if ( absHandler == 4 ) MaxSpeed = 15;
        if ( absHandler == 5 ) MaxSpeed = 25;
        if ( absHandler == 6 ) MaxSpeed = 40;


    }  

    private void HandlerValidation()
    {
        if ( InstructionsHandler > 6 ) InstructionsHandler = 6;
        if ( InstructionsHandler < -6 ) InstructionsHandler = -6;
    }

    public void PauseMovingOn()
    {
        ForPauseTempAcceleration = EngineStep;
        EngineStep = 0;
        isPaused = true;
    }

    public void PauseMovingOff()
    {
        EngineStep = ForPauseTempAcceleration;
        isPaused = false;
    }

    private void CalcRealSpeed()
    {
        SpeedReal = (int)Mathf.Ceil (EngineStep * 10);
    }

    



}