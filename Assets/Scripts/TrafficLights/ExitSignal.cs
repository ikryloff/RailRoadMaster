using System.Collections;
using UnityEngine;

public class ExitSignal : TrafficLight
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
        EventManager.onTrainSignalChanged += UpdateSignals;
        
    }

    public override void LightOn( RouteItem route )
    {
        // saving for Update lights 
        depSignal = route.DependsOnSignal;
        isStraight = route.IsStraight;

        if ( route.IsShunting )
        {
            ShuntingLightOn ();
        }
        else
        {
            if ( route.DependsOnSignal != null )
            {
                if ( !route.DependsOnSignal.IsClosedForTrains )
                {
                    if ( route.IsStraight )
                        ExitStraightLightOn ();
                    else
                        ExitTurnLightOn ();                    
                }
                else
                {
                    LightOff ();
                }
                
            }
            else
                print ("no depend signal");
        }
        EventManager.OnTrainSignalChanged ();
    }

    public override void LightOff()
    {
        IsClosedForShunting = true;
        IsClosedForTrains = true;        
        StopSignalFlashing (yellowSignalFlashing);
        LampSwitchOn (red, RedSignal);
        LampSwitchOff (white, WhiteSignal);
        LampSwitchOff (green, GreenSignal);
        LampSwitchOff (topYellow, TopYellowSignal);
        LampSwitchOff (botYellow, BottomYellowSignal);
        EventManager.OnTrainSignalChanged ();

    }

    protected void UpdateSignals()
    {
        if ( !IsClosedForTrains && depSignal )
        {
            if (!depSignal.IsClosedForTrains )
            {
                if ( isStraight )
                    ExitStraightLightOn ();
                else
                    ExitTurnLightOn ();
            }
            else
            {
                LightOff ();
            }
        }

    }


    private void ShuntingLightOn()
    {
        LightOff ();
        LampSwitchOn (white, WhiteSignal);
        LampSwitchOff (red, RedSignal);      
        IsClosedForShunting = false;

    }

    private void ExitStraightLightOn()
    {
        LightOff ();

        LampSwitchOn (green, GreenSignal);

        LampSwitchOff (red, RedSignal);
        IsClosedForTrains = false;
    }

    private void ExitTurnLightOn()
    {
        StartSignalFlashing (yellowSignalFlashing);
        LampSwitchOn (botYellow, BottomYellowSignal);       
        LampSwitchOff (red, RedSignal);
        LampSwitchOff (white, WhiteSignal);
        LampSwitchOff (green, GreenSignal);
        IsClosedForTrains = false;
    }

    

}
