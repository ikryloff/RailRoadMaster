using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuit : MonoBehaviour, IManageable
{
    public bool HasCarPresence { get; set; }
    public TrafficLight [] TrackLights { get; set; }
    public Switch SwitchTrack { get; set; }
    public Route route;
    public RollingStock engineRS;
    public List<MovableObject> Cars { get; set; }
    public bool IsInRoute { get; set; }
    public bool IsInUse { get; set; }
    public bool IsInPath { get; set; }
    public Engine engine;
    public TrackPathUnit [] paths;
    public IndicatorPath [] Indicators { get; set; }
    private Material colorBlocked;
    private Material colorDefault;
    private Material colorRoute;

    public static event Action TrackCircuitsStateChanged = delegate { };

    public void Init()
    {
        Cars = new List<MovableObject> ();
        paths = transform.GetComponentsInChildren<TrackPathUnit> ();
        Indicators = transform.GetComponentsInChildren<IndicatorPath> ();
        GetTCIndicatorPathUnits ();
        SetTCToPaths ();
        SetTCToPathUnits ();
        SetTCToIndicators ();
        SwitchTrack = GetComponentInParent<Switch> ();
        TrackLights = new TrafficLight [2];


    }


    private void SetTCToIndicators()
    {
        foreach ( IndicatorPath item in Indicators )
        {
            item.IndTrackCircuit = this;
        }
    }




    public void OnStart()
    {
    }

    private void Start()
    {
        SetTCToSignals ();
        UpdateCarPresence ();        
    }

    private void SetTCToPaths()
    {
        foreach ( TrackPathUnit item in paths )
        {
            item.TrackCircuit = this;
        }
    }

    private void SetTCToSignals()
    {
        foreach ( TrafficLight item in TrackLights )
        {
            if ( item )
                item.PrevTC = this;
        }
    }


    public void GetTCIndicatorPathUnits()
    {
        foreach ( IndicatorPath item in Indicators )
        {
            item.GetIndicatorsPathUnits ();
        }
    }

    public void AddCars( MovableObject car )
    {
        if ( !Cars.Contains (car) )
        {
            Cars.Add (car);
            UpdateCarPresence ();
        }

    }


    public void RemoveCars( MovableObject car )
    {
        if ( Cars.Contains (car) )
        {
            Cars.Remove (car);
            UpdateCarPresence ();
        }
    }

    public void CarPresenceOn()
    {
        HasCarPresence = true;
        if ( SwitchTrack )
            SwitchTrack.IsLockedByRS = true;
    }

    public void CarPresenceOff()
    {
        HasCarPresence = false;
        if ( SwitchTrack )
            SwitchTrack.IsLockedByRS = false;
    }

    private void UpdateCarPresence()
    {
        if ( Cars.Count == 0 && HasCarPresence )
        {
            CarPresenceOff ();
        }

        else if ( Cars.Count > 0 && !HasCarPresence )
        {
            CarPresenceOn ();
        }

        GameEventManager.SendEvent ("StateTrack", this);
        EventManager.TrackCircuitsStateChanged();
    }

    public void IndicationTrackInPath( List<TrackPathUnit> paths )
    {
        if ( TrackPath.Instance.TrackList != null && paths != null )
        {
            foreach ( TrackPathUnit item in TrackPath.Instance.TrackList )
            {
                if ( item.isActiveAndEnabled && item.TrackCircuit.IsInPath )
                    item.TrackCircuit.IsInPath = false;
            }
            foreach ( TrackPathUnit item in paths )
            {
                item.TrackCircuit.IsInPath = true;
            }
        }
    }


    public void SetTCToPathUnits()
    {

        foreach ( IndicatorPath ip in Indicators )
        {
            foreach ( PathIndicationUnit piu in ip.pathUnits )
            {
                piu.TrackCircuit = this;
                piu.PIUMeshRend = piu.GetComponent<MeshRenderer> ();
            }
        }
    }











}
