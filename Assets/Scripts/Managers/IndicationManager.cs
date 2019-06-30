using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IndicationManager : Singleton<IndicationManager>, IManageable {
    Engine engine;
    [SerializeField]
    private Text handlerTxt;
    public bool IsIndicate { get; set; }
    public IndicatorPath [] Indicators { get; set; }
    public SwitchParts [] SwitchParts { get; set; }
    private List<TrackPathUnit> enginePath;

    public void Init()
    {
        engine = FindObjectOfType<Engine> ();
        handlerTxt.text = "  <<< 0 >>>  ";
        Indicators = FindObjectsOfType<IndicatorPath> ();
        SwitchParts = FindObjectsOfType<SwitchParts> ();
        enginePath = new List<TrackPathUnit> ();
    }

    private void Start()
    {
        TurnIndicationOff ();
    }

    private void Update()
    {
        PrintHandler ();

        if ( Input.GetKeyDown (KeyCode.Space) )
        {
            ToggleIndication ();
        }
    }

    public void PrintHandler()
    {
        if ( engine.Direction > 0 )
            handlerTxt.text = " >>> " + Mathf.Abs (engine.InstructionsHandler) + " >>>  " + engine.MaxSpeed;
        else if ( engine.Direction < 0 )
            handlerTxt.text = engine.MaxSpeed + " <<< " + Mathf.Abs (engine.InstructionsHandler) + " <<< ";
        else
            handlerTxt.text = "  <<< 0 >>>";
    }

    public void ToggleIndication()
    {
        if ( IsIndicate )
        {
            TurnIndicationOff ();
        }
        else
        {
            TurnIndicationOn ();

        }
    }

    public void TurnIndicationOn()
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
        IsIndicate = true;

    }
    public void TurnIndicationOff()
    {
        foreach ( IndicatorPath item in Indicators )
        {
            item.Show (false);
        }

        IsIndicate = false;
    }

    public void OnStart()
    {
        throw new System.NotImplementedException ();
    }
}
