using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackCircuit : MonoBehaviour
{
    private string trackName;
    public bool hasCarPresence;
    private int useMode;
    private Switch switchTC;
    [SerializeField]
    private SpriteRenderer cellsTrack;
    [SerializeField]
    private SpriteRenderer cellsStraight;
    [SerializeField]
    private SpriteRenderer cellsTurn;
    private SpriteRenderer[] allCells;
    public SpriteRenderer[] indicator;

    [SerializeField]
    private string[] trackLightsNames;
    [SerializeField]
    private TrafficLights[] trackLights;
    [SerializeField]    
    private TrafficLightsManager trafficLightsManager;
    public PathHolder pathHolder;    
    public bool isSwitch;
    public Switch switchTrack;
    public Route route;
    public RollingStock engineRS;
    public Color32 colorPresence;
    public Color32 colorInRoute;
    public Color32 colorInPath;
    public Color32 colorTransparent;
    public Color32 colorInUse;

    public bool isInRoute;    
    public bool isInPath;
    public bool isInUse;
    public Engine engine;
    SwitchManager switchManager;
    public TrackPathUnit [] paths;
    private TrackPath trackPath;

    private void Awake()
    {
        switchManager = FindObjectOfType<SwitchManager>();
        trackPath = FindObjectOfType<TrackPath>();
        trafficLightsManager = FindObjectOfType<TrafficLightsManager>();
        engine = FindObjectOfType<Engine>(); ;
        isSwitch = GetComponentInParent<Switch>();
        engineRS = engine.GetComponent<RollingStock>();
        switchTrack = GetComponentInParent<Switch>();
        if (!isSwitch)
        {
            paths = transform.GetComponentsInChildren<TrackPathUnit>();
            indicator =  GetComponentsInChildren<SpriteRenderer>();
        }
        else if(isSwitch && tag == "SingleSwitch")
        {
            paths = transform.parent.GetComponentsInChildren<TrackPathUnit>(true);
            indicator = transform.parent.GetComponentsInChildren<SpriteRenderer>();
        }
        
        

        foreach (TrackPathUnit item in paths)
        {
            item.TrackCircuit = this; 
        }
        GetTrackLightsByTrack();

        route = GameObject.Find("Route").GetComponent<Route>();
        colorPresence = new Color32(255, 77, 77, 160);
        colorInRoute = new Color32(255, 210, 0, 160);
        colorInPath = new Color32(58, 227, 116, 160);
        colorInUse =  new Color32(105, 165, 230, 160);
        colorTransparent = new Color32(255, 255, 255, 0);
}

    private void Update()
    {        
        PresenceFunction();
        //IndicationTrackInPath(engineRS.OwnPath);
        //TrackCircuitColor();
        CheckInRoute();
        
    }
   

    public void CheckInRoute()
    {
        if (isSwitch)
        {
            if (isInRoute)
                switchTC.isLockedByRoute = true;
            else
                switchTC.isLockedByRoute = false;        
        }
            
            
    }

    private void Start()
    {
           
        useMode = Constants.TC_DEFAULT;
        allCells = new SpriteRenderer[3];
        allCells[0] = cellsTrack;
        if (isSwitch)
        {
            switchTC = transform.parent.GetComponent<Switch>();
            allCells[1] = cellsStraight;
            allCells[2] = cellsTurn;
        }
        SetCellsLight(allCells, Constants.TC_DEFAULT);
        
    }

    
    public void TrackCircuitColor()
    {               

        if (isInPath)
        {
            if (isInUse)
            {
                if (indicator != null) //temp
                {
                    foreach (SpriteRenderer item in indicator)
                    {
                        item.color = colorInUse;
                    }
                }

            }
            if (isInRoute && !isInUse)
            {
                if (indicator != null) //temp
                {
                    foreach (SpriteRenderer item in indicator)
                    {
                        item.color = colorInRoute;
                    }
                }
            }
            if (hasCarPresence)
            {
                if (indicator != null) //temp
                {
                    foreach (SpriteRenderer item in indicator)
                    {
                        item.color = colorPresence;
                    }
                }

            }
            else if (!hasCarPresence && !isInRoute && !isInUse)
            {
                if (indicator != null) //temp
                {
                    foreach (SpriteRenderer item in indicator)
                    {
                        item.color = colorInPath;
                    }
                }

            }
        }
        else
        {
            foreach (SpriteRenderer item in indicator)
            {
                item.color = colorTransparent;
            }

        }


    }

    public void IndicationTrackInPath(List<TrackPathUnit> paths)
    {
        if (switchManager.IsSwitchModeOn && trackPath.trackList != null && paths != null)
        {
            foreach (TrackPathUnit item in trackPath.trackList)
            {
                if (item.isActiveAndEnabled && item.TrackCircuit.isInPath)
                    item.TrackCircuit.isInPath = false;
            }
            foreach (TrackPathUnit item in paths)
            {
                item.TrackCircuit.isInPath = true;
            }
        }
    }


    public void PresenceFunction()
    {
        if (hasCarPresence)
        {
            if (tag == "SingleSwitch" || tag == "DoubleSwitch")
            {
                switchTC.isLockedByRS = true;
            }            
            SetCellsLight(ReturnCells(), Constants.TC_OVER);            
        }
        else
        {
            if (tag == "SingleSwitch" || tag == "DoubleSwitch")
            {
                switchTC.isLockedByRS = false;
            }            
            SetCellsLight(ReturnCells(), Constants.TC_DEFAULT);

        }


        GetTrackLightsByTrack();
        
    }
       
    


    public int UseMode
    {
        get
        {
            if (hasCarPresence)
            {
                useMode = Constants.TC_OVER;
            }
            if (useMode == Constants.TC_OVER && !hasCarPresence)
            {
                useMode = Constants.TC_USED;
            }
            return useMode;
        }

        set
        {
            useMode = value;
            SetCellsLight(ReturnCells(), value);
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

    public TrafficLights[] TrackLights
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

    public SpriteRenderer[] ReturnCells()
    {
        SpriteRenderer[] sr = new SpriteRenderer[2];
        sr[0] = cellsTrack;
        if (tag == "Switch")
        {
            if (switchTC.IsSwitchStraight)
                sr[1] = cellsStraight;
            else
                sr[1] = cellsTurn;
        }
        return sr;
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
                    if (hasCarPresence)
                        cell.color = new Color32(215, 0, 0, 255);
                }
            }

        }
    }

    //order does matter

    public void SetTrackLights(TrafficLights _left, TrafficLights _right)
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
        

        TrackLights = new TrafficLights[2];

        for (int i = 0; i < TrackLightsNames.Length; i++)
        {
            TrackLights[i] = trafficLightsManager.GetTrafficLightByName(TrackLightsNames[i]);            
        }        
    }
}
