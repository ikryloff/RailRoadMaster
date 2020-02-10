using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLBotYellowLamp : Lamp {
    protected override void Awake()
    {
        base.Awake ();
        LightOnMaterial = ResourceHolder.Instance.Light_Yellow_Signal_Mat;
        LightRay.GetComponent<MeshRenderer> ().material = ResourceHolder.Instance.Ray_Yellow_Signal_Mat;

    }
}
