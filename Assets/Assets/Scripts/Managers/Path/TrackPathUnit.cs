using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPathUnit : MonoBehaviour {

    // Class connecting BGCCMATH Asset with TrackCircuit 
   
    public BGCcMath trackMath;   
    public TrackCircuit TrackCircuit { get; set; }
    

    private void Awake()
    {
        trackMath = GetComponent<BGCcMath>();       
    }   
   
}
