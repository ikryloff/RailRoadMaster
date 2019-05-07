﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Engine : MonoBehaviour
{
    public static int InstructionsHandler;

    private Rigidbody engine;
    public RollingStock engineRS;

    
    private bool brakes = true;
    [SerializeField]
    private Text handlerTxt;
    public CompositionManager cm;

    public int direction;
    private int directionInstructions;
    public int maxSpeed;
    private bool isDrivingByInstructionsIsOn;
    
    public float ForPauseTempAcceleration { get; private set; }
    int speed;
    public float acceleration;
    public TrackPath trackPath;
    SwitchManager switchManager;

    public List<TrackCircuit> tcList;
    private bool isPaused;

    private void Awake()
    {
        EventManager.onPause += PauseMovingOn;
        EventManager.offPause += PauseMovingOff;
        engineRS = GetComponent<RollingStock> ();
    }

    void Start()
    {
        acceleration = 0;
    }

    void Update()
    {
        if ( !isPaused )
        {
            PrintHandler ();
            speed = (int)(engineRS.Translation * 15);
            DriveByInstructions ();
            MoveEngine ();
        }
    }

    public void MoveEngine()
    {
        if ( Math.Abs (speed) == maxSpeed )
            acceleration += 0;
        else
        {
            if ( Math.Abs (speed) < maxSpeed )
                acceleration += 0.1f * direction * Time.deltaTime;
            else if ( Math.Abs (speed) > maxSpeed )
            {
                acceleration -= 0.5f * speed / Math.Abs (speed) * Time.deltaTime;
            }
        }
        if ( brakes && speed == 0 )
        {
            acceleration = 0;
        }

    }


    public void ReleaseBrakes()
    {
        brakes = false;

    }

    public void EngineControllerReleaseAll()
    {
        ReleaseBrakes ();
    }

    public void EngineControllerUseBrakes()
    {
        brakes = true;
    }

    public void EngineInstructionStop()
    {
        brakes = true;
        direction = 0;
        InstructionsHandler = 0;
    }



    public void EngineInstructionsForward()
    {
        direction = direction == -1 && direction != 0 ? -1 : 1;

        ReleaseBrakes ();

        if ( InstructionsHandler != 0 )
        {
            InstructionsHandler++;
            if ( InstructionsHandler == 0 )
            {
                EngineInstructionStop ();

            }
        }
        else
        {
            if ( speed >= -1 )
            {
                InstructionsHandler++;
            }
            else
                InstructionsHandler = 0;
            engineRS.ChangeDirection ();
        }

        if ( InstructionsHandler == 7 )
            InstructionsHandler = 6;
    }

    public void EngineInstructionsBackwards()
    {
        direction = direction == 1 && direction != 0 ? 1 : -1;

        ReleaseBrakes ();

        if ( InstructionsHandler != 0 )
        {
            InstructionsHandler--;
            if ( InstructionsHandler == 0 )
            {
                EngineInstructionStop ();

            }
        }
        else
        {
            if ( speed <= 1 )
            {
                InstructionsHandler--;
            }
            else
                InstructionsHandler = 0;
            engineRS.ChangeDirection ();
        }
        if ( InstructionsHandler == -7 )
            InstructionsHandler = -6;
    }


    public void DriveByInstructions()
    {

        if ( InstructionsHandler == 0 )
        {
            maxSpeed = 0;
            EngineInstructionStop ();
        }

        int absHandler = Mathf.Abs (InstructionsHandler);
        if ( absHandler == 1 ) maxSpeed = 3;
        if ( absHandler == 2 ) maxSpeed = 5;
        if ( absHandler == 3 ) maxSpeed = 10;
        if ( absHandler == 4 ) maxSpeed = 15;
        if ( absHandler == 5 ) maxSpeed = 25;
        if ( absHandler == 6 ) maxSpeed = 40;

    }

    private void PrintHandler()
    {
        if ( direction > 0 )
            handlerTxt.text = "  >>> " + Mathf.Abs (InstructionsHandler) + ">>> " + maxSpeed;
        else if ( direction < 0 )
            handlerTxt.text = maxSpeed + "  <<< " + Mathf.Abs (InstructionsHandler) + " <<<";
        else
            handlerTxt.text = "  <<< 0 >>>";
    }

    public void PauseMovingOn()
    {
        ForPauseTempAcceleration = acceleration;
        acceleration = 0;
        isPaused = true;
    }

    public void PauseMovingOff()
    {
        acceleration = ForPauseTempAcceleration;
        isPaused = false;
    }



}