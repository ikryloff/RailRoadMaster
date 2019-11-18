using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLGreenLamp : Lamp {

    protected override void Awake()
    {
        base.Awake ();
        LightOnMaterial = ResourceHolder.Instance.Light_Green_Signal_Mat;
        LightRay.GetComponent<MeshRenderer> ().material = ResourceHolder.Instance.Ray_Green_Signal_Mat;

    }
}
