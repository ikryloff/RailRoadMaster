using UnityEngine;

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
        base.Awake ();
        GetPositionX = gameObject.transform.position.x;
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
        IsClosed = false;
        UpdateSignals ();
        //repeater
        if ( TLRepeater )
            TLRepeater.RepeaterOnTrain ();
        EventManager.TrainSignalChanged ();
    }

    public override void LightOff()
    {
        LampsOff ();
        IsClosed = true;
        EventManager.TrainSignalChanged ();
        if ( TLRepeater )
            TLRepeater.RepeaterOffTrain ();        
    }

    private void LampsOff()
    {
        StopSignalFlashing (yellowSignalFlashing);
        LampSwitchOn (red, RedSignal);
        LampSwitchOff (white, WhiteSignal);
        LampSwitchOff (green, GreenSignal);
        LampSwitchOff (topYellow, TopYellowSignal);
        LampSwitchOff (botYellow, BottomYellowSignal);
    } 

    protected void UpdateSignals()
    {
        if ( depSignal )
        {
            if ( !IsClosed )
            {
                if ( depSignal.IsClosed )
                {
                    if ( isStraight )
                        EntranceStraightLightToClosedOn ();
                    else
                        EntranceTurnLightToClosedOn ();
                }
                else if ( !depSignal.IsClosed )
                {
                    if ( isStraight )
                        EntranceStraightLightToOpenedOn ();
                    else
                        EntranceTurnLightToOpenedOn ();
                }
                IsClosed = false;

                if ( TLRepeater )
                    TLRepeater.RepeaterOnTrain ();
            }
            else
            {
                if ( TLRepeater )
                    TLRepeater.RepeaterOffTrain ();
            }
        }
    }

    private void EntranceStraightLightToClosedOn()
    {
        LampsOff ();
        LampSwitchOn (topYellow, TopYellowSignal);
        LampSwitchOff (red, RedSignal);
       
    }

    private void EntranceTurnLightToClosedOn()
    {
        LampsOff ();
        LampSwitchOn (topYellow, TopYellowSignal);
        LampSwitchOn (botYellow, BottomYellowSignal);
        LampSwitchOff (red, RedSignal);
    }

    private void EntranceStraightLightToOpenedOn()
    {
        print (name + " here");
        LampsOff ();
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
