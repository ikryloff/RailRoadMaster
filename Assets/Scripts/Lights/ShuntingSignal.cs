
using UnityEngine;

public class ShuntingSignal : TrafficLight {

    private MeshRenderer blue;
    private MeshRenderer white;


    public override void LightOn(RouteItem route)
    {
        LampSwitchOff (blue, BlueSignal);
        LampSwitchOn (white, WhiteSignal);

        IsClosed = false;       
    }

    public override void LightOff()
    {
        LampSwitchOn (blue, BlueSignal);
        LampSwitchOff (white, WhiteSignal);

        IsClosed = true;
        
    }

    protected override void Awake()
    {
        base.Awake ();
        GetPositionX = gameObject.transform.position.x;
        BlueSignal = GetComponentInChildren<TLBlueLamp>();
        WhiteSignal = GetComponentInChildren<TLWhiteLamp>();
        blue = BlueSignal.GetComponent<MeshRenderer>();
        white = WhiteSignal.GetComponent<MeshRenderer>();
        
    }

    private void Start()
    {
        IsClosed = true;
        LightOff ();
           
    }

   
}
