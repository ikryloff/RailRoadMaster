using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coupler : MonoBehaviour
{
    public CouplerObject CouplerObject { get; set; }
    public CouplerPoint CouplerPoint { get; set; }
    public CouplerLever CouplerLever { get; private set; }
    public GameObject CouplerLeverObject { get; private set; }
    public bool IsInConnection { get; set; }

    private void Awake()
    {
        CouplerObject = GetComponentInChildren<CouplerObject> ();
        CouplerPoint = GetComponentInChildren<CouplerPoint> ();
        CouplerLever = GetComponentInChildren<CouplerLever> ();
        if(CouplerLever)
            CouplerLeverObject = CouplerLever.gameObject;
    }
    private void Start()
    {
        if ( !IsInConnection && CouplerLever )
            SetLeverUnactive ();
        else
            SetLeverActive ();
    }


    public void SetLeverUnactive()
    {
        if(CouplerLever)
            CouplerLeverObject.SetActive (false);
    }
    public void SetLeverActive()
    {
        if ( CouplerLever )
            CouplerLeverObject.SetActive (true);
    }

}
