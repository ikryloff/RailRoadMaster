using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathUnit : MonoBehaviour {

    
    public BGCcMath math;
    public BogeyPathScript bogey;
    public bool hasObjects;
    public TrackCircuit trackCircuit;

    private void Awake()
    {
        math = GetComponent<BGCcMath>();
    }

    private void Update()
    {
        CheckObjectAtPath();
    }

    public void CheckObjectAtPath()
    {
        if (bogey)
        {
            hasObjects = true;
        }
        else
        {
            hasObjects = false;
        }
    }
}
