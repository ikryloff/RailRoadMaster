
using UnityEngine;

public class ShuntingSignal : TrafficLight {

    private MeshRenderer blue;
    private MeshRenderer white;


    public override void LightOn(RouteItem route)
    {
        LampSwitchOff (blue, BlueSignal);
        LampSwitchOn (white, WhiteSignal);

        IsClosedForShunting = false;       
    }

    public override void LightOff()
    {
        LampSwitchOn (blue, BlueSignal);
        LampSwitchOff (white, WhiteSignal);

        IsClosedForShunting = true;
        
    }

    protected override void Awake()
    {
        BlueSignal = GetComponentInChildren<TLBlueLamp>();
        WhiteSignal = GetComponentInChildren<TLWhiteLamp>();
        blue = BlueSignal.GetComponent<MeshRenderer>();
        white = WhiteSignal.GetComponent<MeshRenderer>();
        
    }

    private void Start()
    {
        IsClosedForTrains = true;
        LightOff ();
           
    }

   
}
