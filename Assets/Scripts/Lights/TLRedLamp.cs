

using UnityEngine;

public class TLRedLamp : Lamp
{

    protected override void Awake()
    {
        base.Awake ();
        LightOnMaterial = ResourceHolder.Instance.Light_Red_Signal_Mat;
        LightRay.GetComponent<MeshRenderer> ().material = ResourceHolder.Instance.Ray_Red_Signal_Mat;
    }
}
