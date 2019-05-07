﻿using UnityEngine;

public class EntranceSignal : TrafficLight
{

    private MeshRenderer red;
    private MeshRenderer white;
    private MeshRenderer green;
    private MeshRenderer topYellow;
    private MeshRenderer botYellow;

    private TrafficLight depSignal;
    private bool isStraight;

    protected override void Awake()
    {
        RedSignal = GetComponentInChildren<TLRedLamp> ();
        WhiteSignal = GetComponentInChildren<TLWhiteLamp> ();
        GreenSignal = GetComponentInChildren<TLGreenLamp> ();
        TopYellowSignal = GetComponentInChildren<TLTopYellowLamp> ();
        BottomYellowSignal = GetComponentInChildren<TLBotYellowLamp> ();

        red = RedSignal.GetComponent<MeshRenderer> ();
        white = WhiteSignal.GetComponent<MeshRenderer> ();
        green = GreenSignal.GetComponent<MeshRenderer> ();
        topYellow = TopYellowSignal.GetComponent<MeshRenderer> ();
        botYellow = BottomYellowSignal.GetComponent<MeshRenderer> ();

        yellowSignalFlashing = GetComponent<Animator> ();
    }

    private void Start()
    {
        LightOff ();
        IsClosedForShunting = true;
        EventManager.onTrainSignalChanged += UpdateSignals;
    }

    public override void LightOn( RouteItem route )
    {
        // saving for Update lights 
        depSignal = route.DependsOnSignal;
        isStraight = route.IsStraight;

        if ( route.DependsOnSignal != null )
        {
            if ( route.DependsOnSignal.IsClosedForTrains )
            {
                if ( route.IsStraight )
                    EntranceStraightLightToClosedOn ();
                else
                    EntranceTurnLightToClosedOn ();
            }
            else
            {
                if ( route.IsStraight )
                    EntranceStraightLightToOpenedOn ();
                else
                    EntranceTurnLightToOpenedOn ();
            }

            IsClosedForTrains = false;
        }
        else
            print ("no depend signal");
        EventManager.OnTrainSignalChanged ();
    }

    public override void LightOff()
    {
        StopSignalFlashing (yellowSignalFlashing);
        LampSwitchOn (red, RedSignal);
        LampSwitchOff (white, WhiteSignal);
        LampSwitchOff (green, GreenSignal);
        LampSwitchOff (topYellow, TopYellowSignal);
        LampSwitchOff (botYellow, BottomYellowSignal);
        IsClosedForTrains = true;

        EventManager.OnTrainSignalChanged ();
    }

    protected void UpdateSignals()
    {
        if ( !IsClosedForTrains )
        {
            if ( depSignal.IsClosedForTrains )
            {
                if ( isStraight )
                    EntranceStraightLightToClosedOn ();
                else
                    EntranceTurnLightToClosedOn ();
            }
            else
            {
                if ( isStraight )
                    EntranceStraightLightToOpenedOn ();
                else
                    EntranceTurnLightToOpenedOn ();
            }
            IsClosedForTrains = false;
            print ("Updated");
        }

    }

    private void EntranceStraightLightToClosedOn()
    {
        LightOff ();
        LampSwitchOn (topYellow, TopYellowSignal);
        LampSwitchOff (red, RedSignal);
       
    }

    private void EntranceTurnLightToClosedOn()
    {
        LightOff ();
        LampSwitchOn (topYellow, TopYellowSignal);
        LampSwitchOn (botYellow, BottomYellowSignal);
        LampSwitchOff (red, RedSignal);
    }

    private void EntranceStraightLightToOpenedOn()
    {
        LightOff ();
        LampSwitchOn (green, GreenSignal);
        LampSwitchOff (red, RedSignal);
    }

    private void EntranceTurnLightToOpenedOn()
    {
        StartSignalFlashing (yellowSignalFlashing);
        LampSwitchOn (botYellow, BottomYellowSignal);
        LampSwitchOff (green, GreenSignal);
        LampSwitchOff (red, RedSignal);
        LampSwitchOff (white, WhiteSignal);
    }

    
}