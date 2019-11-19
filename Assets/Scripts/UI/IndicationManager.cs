﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IndicationManager : Singleton<IndicationManager>, IManageable {
    Engine engine;
    [SerializeField]
    public bool IsPathIndicate { get; set; }
    public bool IsCouplerIndicate { get; set; }
    public IndicatorPath [] PathIndicators { get; set; }
    public Coupler [] CouplerIndicators { get; set; }
    public SwitchParts [] SwitchParts { get; set; }
    private List<TrackPathUnit> enginePath;

    public void Init()
    {
        EventManager.onCarsCoupled += UpdateCouplerIndication;
        EventManager.onPathUpdated += UpdatePathIndication;
        EventManager.onPathUpdated += UpdateYardPathIndication;
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        PathIndicators = FindObjectsOfType<IndicatorPath> ();
        CouplerIndicators = FindObjectsOfType<Coupler> ();
        SwitchParts = FindObjectsOfType<SwitchParts> ();
        enginePath = new List<TrackPathUnit> ();
    }

    private void UpdatePathIndication()
    {
        // if we turn swithces in indication mode
        if ( IsPathIndicate )
            TurnPathIndicationOn ();
        //TurnPathsOn ();
    }

    public void UpdateYardPathIndication()
    {
        // if we turn swithces in yard mode     
        //TurnPathIndicationOn ();
        if ( ModeSwitch.Instance.GameMode == ModeSwitch.Mode.Yard )
            TurnPathsOn ();
    }

    public void OnStart()
    {
        TurnPathIndicationOff ();
        TurnCouplerIndicatorsOff ();
        UpdateCouplerIndication ();
    }
   

    private void Update()
    {
        PrintHandler ();
              
    }

    public void PrintHandler()
    {
       //TODO
    }

    public void TogglePathIndication()
    {
        if ( IsPathIndicate )
        {
            TurnPathIndicationOff ();
        }
        else
        {
            TurnPathIndicationOn ();

        }
    }

    public void ToggleCouplerIndication()
    {
        if ( IsCouplerIndicate )
            TurnCouplerIndicatorsOff ();
        else
            TurnCouplerIndicatorsOn ();
    }

    public void UpdateCouplerIndication()
    {
        if ( IsCouplerIndicate )
            TurnCouplerIndicatorsOn ();
        else
            TurnCouplerIndicatorsOff ();
    }


    public void TurnPathIndicationOn()
    {
        enginePath = engine.EngineRS.OwnPath;
        foreach ( TrackPathUnit item in enginePath )
        {
            if (
                    item.TrackCircuit.SwitchTrack && item.Equals (enginePath.Last ()) ||
                    item.TrackCircuit.SwitchTrack && item.Equals (enginePath.First ())
               )
            {
                foreach ( IndicatorPath ind in item.TrackCircuit.Indicators )
                {
                    ind.Show (false);
                }
            }
            else
            {
                foreach ( IndicatorPath ind in item.TrackCircuit.Indicators )
                {
                    ind.Show (true);
                }
            }

        }
        IsPathIndicate = true;

    }
    public void TurnPathIndicationOff()
    {
        foreach ( IndicatorPath item in PathIndicators )
        {
            item.Show (false);
        }

        IsPathIndicate = false;
    }

    public void TurnCouplerIndicatorsOff()
    {        
        foreach ( Coupler item in CouplerIndicators )
        {
            item.SetLeverUnactive ();
        }
        IsCouplerIndicate = false;
    }

    public void TurnCouplerIndicatorsOn()
    {        
        foreach ( Coupler item in CouplerIndicators )
        {
            item.SetLeverActive ();
        }
        IsCouplerIndicate = true;
    }

    public void TurnPathsOn()
    {
        foreach ( IndicatorPath ind in PathIndicators )
        {
            ind.Show (true);
        }        
    }

    
}