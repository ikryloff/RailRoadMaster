using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lamp : MonoBehaviour {

    public Material LightOnMaterial { get; set; }
    public Material NoLightMaterial { get; set; }

    public LightRay LightRay { get; set; }
   
    protected virtual void Awake()
    {        
        NoLightMaterial = ResourceHolder.Instance.Light_No_Signal_Mat;
        LightRay = GetComponentInChildren<LightRay>();        
    }
}
