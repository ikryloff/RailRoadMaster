
using UnityEngine;

public class LocoWhiteLamp : Lamp
{

    protected override void Awake()
    {
        base.Awake();
        LightOnMaterial = ResourceHolder.Instance.Light_White_Loco_Lamp_Mat;

    }
}
