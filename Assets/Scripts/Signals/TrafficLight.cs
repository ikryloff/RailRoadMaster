using UnityEngine;

public class TrafficLight : MonoBehaviour
{


    [SerializeField]
    private string trafficLightName;
  

    public Lamp BlueSignal { get; set; }
    public Lamp WhiteSignal { get; set; }
    public Lamp RedSignal { get; set; }
    public Lamp TopYellowSignal { get; set; }
    public Lamp BottomYellowSignal { get; set; }
    public Lamp GreenSignal { get; set; }

    public bool IsClosed { get; set; }    
    public bool IsClosedByTrain { get; set; }    
    public BoxCollider Trigger { get; set; }
    public TrackCircuit PrevTC { get; set; }
    public TrafficLightRepeater TLRepeater { get; set; }

    //where signal is looking at. 1 - to left, -1 to right
    [SerializeField]
    private int signalDirection;
    public float GetPositionX;
    const float flashTime = 1f;
    protected Animator yellowSignalFlashing;

    public virtual void LightOn( RouteItem route ) { }
    public virtual void LightOff() { }

    protected void LampSwitchOff( MeshRenderer meshRenderer, Lamp lamp )
    {
        meshRenderer.material = lamp.NoLightMaterial;
        lamp.LightRay.Off ();

    }

    protected void LampSwitchOn( MeshRenderer meshRenderer, Lamp lamp )
    {
        meshRenderer.material = lamp.LightOnMaterial;
        lamp.LightRay.On ();

    }

    public void  StartSignalFlashing(Animator animator)
    {
        if ( animator )
            animator.enabled = true;        
    }

    public void StopSignalFlashing( Animator animator )
    {
        if ( animator )
            animator.enabled = false;
    }

    protected virtual void Awake()
    {
        Trigger = GetComponent<BoxCollider> ();
        TLRepeater = GetComponentInChildren<TrafficLightRepeater> ();
       
    }
    public string Name
    {
        get
        {
            return trafficLightName;
        }
    }

    public int SignalDirection
    {
        get
        {
            return signalDirection;
        }
       
    }
}
