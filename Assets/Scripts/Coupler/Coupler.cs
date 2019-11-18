using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coupler : MonoBehaviour, IHideable
{
    public CouplerObject CouplerObject { get; set; }
    public CouplerPoint CouplerPoint { get; set; }
    public CouplerLever CouplerLever { get; private set; }
    public GameObject CouplerLeverObject { get; set; }
    public bool IsInConnection { get; set; }

    public void SetCouplers()
    {
        CouplerObject = GetComponentInChildren<CouplerObject> ();
        CouplerPoint = GetComponentInChildren<CouplerPoint> ();
        CouplerLever = GetComponentInChildren<CouplerLever> ();
        if(CouplerLever)
            CouplerLeverObject = CouplerLever.gameObject;
    }
    public void SetLevers()
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
        if ( CouplerLever && IsInConnection)
            CouplerLeverObject.SetActive (true);
    }

    public void MakeCouplerConnection()
    {
        IsInConnection = true;
        SetLeverActive ();
    }

    public void DestroyCouplerConnection()
    {
        IsInConnection = false;
        SetLeverUnactive ();
    }

    public void Show( bool isVisible )
    {
        CouplerLeverObject.SetActive (isVisible);
    }
}
