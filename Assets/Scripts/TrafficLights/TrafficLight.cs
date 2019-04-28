using BansheeGz.BGSpline.Components;
using System.Collections;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{


    [SerializeField]
    private string trafficLightName;

    //Set objects in Editor
    public Lamp BlueSignal;
    public Lamp WhiteSignal;
    public Lamp RedSignal;
    public Lamp TopYellowSignal;
    public Lamp BottomYellowSignal;
    public Lamp GreenSignal;

    public bool IsClosedForShunting { get; set; }
    public bool IsClosedForTrains { get; set; }
    public float distance;
    const float flashTime = 1f;
    protected Animator yellowSignalFlashing;

    public BGCcMath mathTemp;

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
        
    }

   
    public string Name
    {
        get
        {
            return trafficLightName;
        }
    }

}
