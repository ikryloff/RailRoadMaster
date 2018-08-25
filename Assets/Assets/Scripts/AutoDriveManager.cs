using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveManager : Singleton<AutoDriveManager>
{
   
    public void RunAutoDrive(Engine engine, Transform rollingStock, Transform aim, int _maxSpeed, bool _mustCouple, RollingStock uncoupleFromRollingstock = null)
    {
        if(rollingStock != null && aim != null)
        {
            RailRunObject rro = gameObject.AddComponent<RailRunObject>();                      
            rro.MakeRailRun(engine, rollingStock, aim, _maxSpeed, _mustCouple, uncoupleFromRollingstock);           
        }
        else
        {
            Debug.Log("AutoDrive wrong");            
        }
    }



}