using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuitManager : Singleton<TrackCircuitManager>, IManageable {

    public TrackCircuit[] TrackCircuits { get; set; }

    public Dictionary<string, TrackCircuit> TCDict { get; set; }

    public void Init()
    {
        TrackCircuits = FindObjectsOfType<TrackCircuit>();
        MakeTrackCircuitDictionary();
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    private void MakeTrackCircuitDictionary()
    {
        TCDict = new Dictionary<string, TrackCircuit>();
        foreach (TrackCircuit tc in TrackCircuits)
        {
            TCDict.Add(tc.name, tc);
            tc.Init();
        }
        //empty key / value
        TCDict.Add("", null);
    }
    
}
