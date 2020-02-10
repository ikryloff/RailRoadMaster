using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLBlueLamp : Lamp {

    protected override void Awake()
    {
        base.Awake();
        LightOnMaterial = ResourceHolder.Instance.Light_Blue_Signal_Mat;
        LightRay.GetComponent<MeshRenderer> ().material = ResourceHolder.Instance.Ray_Blue_Signal_Mat;
    }
}
