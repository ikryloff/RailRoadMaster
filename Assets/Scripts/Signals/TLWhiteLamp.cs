
using UnityEngine;

public class TLWhiteLamp : Lamp
{

    protected override void Awake()
    {
        base.Awake();
        LightOnMaterial = ResourceHolder.Instance.Light_White_Signal_Mat;
        LightRay.GetComponent<MeshRenderer>().material = ResourceHolder.Instance.Ray_White_Signal_Mat;

    }
}
