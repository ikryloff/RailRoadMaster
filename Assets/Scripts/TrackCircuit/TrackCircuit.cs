using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackCircuit : MonoBehaviour, IManageable
{
    private string trackName;
    public bool HasCarPresence { get; set; }
    private int useMode;
   
    [SerializeField]
    private string[] trackLightsNames;
    [SerializeField]
    private TrafficLight[] trackLights;    
    public Switch SwitchTrack { get; set; }
    public Route route;
    public RollingStock engineRS;
    public Color32 colorPresence;
    public Color32 colorInRoute;
    public Color32 colorInPath;
    public Color32 colorTransparent;
    public Color32 colorInUse;

    public bool IsInRoute { get; set; }
    public bool IsInPath { get; set; }
    public bool isInUse;
    public Engine engine;
    public TrackPathUnit [] paths;
    public IndicatorPath [] Indicators { get; set; }


    public void Init()
    {
        paths = transform.GetComponentsInChildren<TrackPathUnit>();
        Indicators = transform.GetComponentsInChildren<IndicatorPath> ();
        SetTCToPaths ();
        SetTCToIndicators ();
        SwitchTrack = GetComponentInParent<Switch>();
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
        colorPresence = new Color32(255, 77, 77, 160);
        colorInRoute = new Color32(255, 210, 0, 160);
        colorInPath = new Color32(58, 227, 116, 160);
        colorInUse = new Color32(105, 165, 230, 160);
        colorTransparent = new Color32(255, 255, 255, 0);

        CarPresenceOff();


    }

    private void SetTCToPaths()
    {
        foreach (TrackPathUnit item in paths)
        {
            item.TrackCircuit = this;
        }        
    }

    public void CarPresenceOn()
    {
        HasCarPresence = true;
        if (SwitchTrack)
            SwitchTrack.IsLockedByRS = true;
    }

    public void CarPresenceOff()
    {
        HasCarPresence = false;
        if (SwitchTrack)
            SwitchTrack.IsLockedByRS = false;
    }

    private void Update()
    {        
        
        
    }
   
    public void TrackCircuitColor()
    {               

      

    }

    public void IndicationTrackInPath(List<TrackPathUnit> paths)
    {
        if ( TrackPath.Instance.TrackList != null && paths != null)
        {
            foreach (TrackPathUnit item in TrackPath.Instance.TrackList)
            {
                if (item.isActiveAndEnabled && item.TrackCircuit.IsInPath)
                    item.TrackCircuit.IsInPath = false;
            }
            foreach (TrackPathUnit item in paths)
            {
                item.TrackCircuit.IsInPath = true;
            }
        }
    }


 
    


    public int UseMode
    {
        get
        {
            if (HasCarPresence)
            {
                useMode = Constants.TC_OVER;
            }
            if (useMode == Constants.TC_OVER && !HasCarPresence)
            {
                useMode = Constants.TC_USED;
            }
            return useMode;
        }

        set
        {
            useMode = value;
           
        }
    }

    public string TrackName
    {
        get
        {
            return name;
        }

        set
        {
            trackName = value;
        }
    }

    public string[] TrackLightsNames
    {
        get
        {
            return trackLightsNames;
        }

        set
        {
            trackLightsNames = value;
        }
    }

    public TrafficLight[] TrackLights
    {
        get
        {
            return trackLights;
        }

        set
        {
            trackLights = value;
        }
    }

   

    public void SetCellsLight(SpriteRenderer[] cells, int color)
    {
        if (cells != null)
        {
            foreach (SpriteRenderer cell in cells)
            {
                if (cell)
                {
                    if (color == Constants.TC_WAIT)
                        cell.color = new Color32(250, 240, 125, 255);
                    else if (color == Constants.TC_OVER)
                        cell.color = new Color32(215, 0, 0, 255);
                    else
                        cell.color = new Color32(190, 190, 190, 255);
                    if (HasCarPresence)
                        cell.color = new Color32(215, 0, 0, 255);
                }
            }

        }
    }

    //order does matter

    public void SetTrackLights(TrafficLight _left, TrafficLight _right)
    {
        TrackLightsNames = new string[] { _left.Name, _right.Name };
    }

    public void GetTrackLightsByTrack()
    {
        if (name == "Track_10_14_18" )
        {
            TrackLightsNames = new string[] { null, null };
        }
        if (name == "TrackCircuitSw22")
        {
            TrackLightsNames = new string[] { "EndM3", null };
        }
        if (name == "TrackCircuitSw18" || name == "TrackCircuitSw20" || name == "TrackCircuitSw15")
        {
            TrackLightsNames = new string[] { null, null };
        }
        if (name == "Track_10")
        {
            TrackLightsNames = new string[] { "End10_20", "End10" };            
        }
        if (name == "Track_11")
        {
            TrackLightsNames = new string[] { "End11_20", "End10" };
        }
        if (name == "Track_12" )
        {
            TrackLightsNames = new string[] { "End12N", "End12CH" };
        }
        if (name == "Track_12A")
        {
            TrackLightsNames = new string[] { null, "End12" };
        }
        if (name == "Track_13")
        {
            TrackLightsNames = new string[] { "End13N", "End13CH" };
        }
        if (name == "Track_9")
        {
            TrackLightsNames = new string[] { "End9_18", "End10" };
        }
        if (name == "Track_6")
        {
            TrackLightsNames = new string[] { "End6", "M2" };
        }        
        if (name == "Track_2")
        {
            TrackLightsNames = new string[] { "N2", "CH2" };
        }
        if (name == "Track_I_16_15")
        {
            TrackLightsNames = new string[] { "NI", "CHI" };
        }
        if (name == "Track_3")
        {
            TrackLightsNames = new string[] { "N3", "CH3" };
        }
        if (name == "Track_4")
        {
            TrackLightsNames = new string[] { "N4", "CH4" };
        }
        if (name == "Track_5")
        {
            TrackLightsNames = new string[] { "N5", "CH5" };
        }
        if (name == "Track_7")
        {
            TrackLightsNames = new string[] { "M1", "End7" };
        }
        if (name == "Track_12_17_19" )
        {
            TrackLightsNames = new string[] { "M5", null };
        }
        if (name == "Track_8")
        {
            TrackLightsNames = new string[] { "End8", "M4" };
        }
        if (name == "TrackCircuitSw5_17Bot")
        {
            if (GetComponentInParent<Switch>().IsSwitchStraight)
                TrackLightsNames = new string[] { "End8", null };
            else
                TrackLightsNames = new string[] { null, null };            
        }
        if (name == "Track_14" )
        {
            TrackLightsNames = new string[] { "End14", "End14_22SW" };
        }

        if (name == "TrackCircuitSw14" )
        {
            if(GetComponentInParent<Switch>().IsSwitchStraight)
                TrackLightsNames = new string[] { null, "End22_14SW" };
            else
                TrackLightsNames = new string[] { null, null };
        }
        

        TrackLights = new TrafficLight[2];

        for (int i = 0; i < TrackLightsNames.Length; i++)
        {
            TrackLights[i] = TrafficLightsManager.Instance.GetTrafficLightByName(TrackLightsNames[i]);            
        }        
    }

    

}
