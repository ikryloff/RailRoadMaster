using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveManager : Singleton<AutoDriveManager>
{
    private List<int> runs = new List<int>();

    public List<int> Runs
    {
        get
        {
            return runs;
        }

        set
        {
            runs = value;
        }
    }

    public void RunAutoDrive(int runID, Engine engine, Transform rollingStock, Transform aim, float offset,  int _maxSpeed, bool _mustCouple, RollingStock uncoupleFromRollingstock = null)
    {
        if(rollingStock != null && aim != null)
        {
            RailRunObject rro = gameObject.AddComponent<RailRunObject>();                      
            rro.MakeRailRun(engine, rollingStock, aim, offset, _maxSpeed, _mustCouple, uncoupleFromRollingstock);
            Runs.Add(runID);
            rro.RailRunID = runID;
            Debug.Log("RunID " + runID);
        }
        else
        {
            Debug.Log("AutoDrive wrong");            
        }
    }



}