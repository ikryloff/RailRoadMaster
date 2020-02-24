using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IndicationManager : Singleton<IndicationManager>, IManageable
{
    public Engine engine;
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
        if ( !IsPathIndicate )
            StartCoroutine (TurnPathCoroutin ());       
    }

    public IEnumerator TurnPathCoroutin()
    {
        TurnPathIndicationOn ();
        yield return new WaitForSecondsRealtime (3f);
        if( !ModeSwitch.Instance.GameMode.Equals( ModeSwitch.Mode.Yard ))
            TurnPathIndicationOff ();
    }

    public IEnumerator TurnCouplersCoroutin()
    {
        TurnCouplerIndicatorsOn ();
        yield return new WaitForSecondsRealtime (3f);
        TurnCouplerIndicatorsOff ();
    }

    public void ToggleCouplerIndication()
    {
        if ( !IsCouplerIndicate )
            StartCoroutine (TurnCouplersCoroutin ());
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
