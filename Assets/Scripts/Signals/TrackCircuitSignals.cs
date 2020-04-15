using UnityEngine;

public class TrackCircuitSignals : MonoBehaviour, IManageable
{


    public void SetSignals()
    {
        foreach ( TrackCircuit trackCircuit in TrackCircuitManager.Instance.TrackCircuits )
        {
            GetTrackLightsByTrack (trackCircuit);
        }
    }

    public void GetTrackLightsByTrack( TrackCircuit tc )
    {
        if ( tc.name.Equals ("Track_Cross9_10") )
            SetTrackLightsToTC (tc, "Cross9_10", null);

        else if ( tc.name.Equals ("Track_I_CH") )
            SetTrackLightsToTC (tc, null, "CH");

        else if ( tc.name.Equals ("Track_I_N") )
            SetTrackLightsToTC (tc, "N", null);

        else if ( tc.name.Equals ("Track_I_16_15") )
            SetTrackLightsToTC (tc, "NI", "CHI");

        else if ( tc.name.Equals ("Track_12_17_19") )
            SetTrackLightsToTC (tc, "M5", null);

        else if ( tc.name.Equals ("Track_2") )
            SetTrackLightsToTC (tc, "N2", "CH2");

        else if ( tc.name.Equals ("Track_3") )
            SetTrackLightsToTC (tc, "N3", "CH3");

        else if ( tc.name.Equals ("Track_4") )
            SetTrackLightsToTC (tc, "N4", "CH4");

        else if ( tc.name.Equals ("Track_5") )
            SetTrackLightsToTC (tc, "N5", "CH5");

        else if ( tc.name.Equals ("Track_6") )
            SetTrackLightsToTC (tc, "End6", "M2");

        else if ( tc.name.Equals ("Track_7") )
            SetTrackLightsToTC (tc, "M1", "End7");

        else if ( tc.name.Equals ("Track_8") )
            SetTrackLightsToTC (tc, "End8", "M4");

        else if ( tc.name.Equals ("Track_9") )
            SetTrackLightsToTC (tc, "Cross9", "End9");        

        else if ( tc.name.Equals ("Track_10") )
            SetTrackLightsToTC (tc, "Cross10_11", "End10");

        else if ( tc.name.Equals ("Track_11") )
            SetTrackLightsToTC (tc, "Cross11", "End11");

        else if ( tc.name.Equals ("Track_12") )
            SetTrackLightsToTC (tc, "Cross12N", "Cross12CH");

        else if ( tc.name.Equals ("Track_12A") )
            SetTrackLightsToTC (tc, null, "End12");

        else if ( tc.name.Equals ("Track_13") )
            SetTrackLightsToTC (tc, "Cross13N", "Cross13CH");

        else if ( tc.name.Equals ("Track_StoreYard") )
            SetTrackLightsToTC (tc, "M3", "CrossStoreYard");

        else if ( tc.name.Equals ("Track_14") )
            SetTrackLightsToTC (tc, "End14", "Cross14");

        else if ( tc.name.Equals ("Switch_19") )
            SetTrackLightsToTC (tc, null, null);

    }

    private void SetTrackLightsToTC( TrackCircuit tc, string signal_0, string signal_1 )
    {
       
        if ( signal_0 != null )
            tc.TrackLights [0] = TrafficLightsManager.Instance.GetTrafficLightByName (signal_0);
        if ( signal_1 != null )
            tc.TrackLights [1] = TrafficLightsManager.Instance.GetTrafficLightByName (signal_1);
        
    }

    public void Init()
    {       
    }

    public void OnStart()
    {
        SetSignals ();
    }
}
