using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuitManager : Singleton<TrackCircuitManager>, IManageable {

    TrackCircuit[] trackCircuits;

    public Dictionary<string, TrackCircuit> TCDict { get; set; }

    public void Init()
    {
        trackCircuits = FindObjectsOfType<TrackCircuit>();
        MakeTrackCircuitDictionary();
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    private void MakeTrackCircuitDictionary()
    {
        TCDict = new Dictionary<string, TrackCircuit>();
        foreach (TrackCircuit tc in trackCircuits)
        {
            TCDict.Add(tc.name, tc);
            tc.Init();
        }
        //empty key / value
        TCDict.Add("", null);
    }
    
}
