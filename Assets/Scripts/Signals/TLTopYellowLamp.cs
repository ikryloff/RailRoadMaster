using UnityEngine;

public class TLTopYellowLamp : Lamp
{

    protected override void Awake()
    {
        base.Awake ();
        LightOnMaterial = ResourceHolder.Instance.Light_Yellow_Signal_Mat;        
        LightRay.GetComponent<MeshRenderer> ().material = ResourceHolder.Instance.Ray_Yellow_Signal_Mat;

    }
}
