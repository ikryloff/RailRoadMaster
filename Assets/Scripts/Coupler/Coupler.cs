using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coupler : MonoBehaviour
{
    public CouplerObject CouplerObject { get; set; }
    public CouplerPoint CouplerPoint { get; set; }

    private void Awake()
    {
        CouplerObject = GetComponentInChildren<CouplerObject> ();
        CouplerPoint = GetComponentInChildren<CouplerPoint> ();
    }




}
