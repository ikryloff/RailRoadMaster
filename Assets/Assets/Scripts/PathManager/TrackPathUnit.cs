using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathUnit : MonoBehaviour {

    
    public BGCcMath math;
    public BogeyPathScript bogey;
    public bool hasObjects;

    private void Awake()
    {
        math = GetComponent<BGCcMath>();
    }

    private void Update()
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
