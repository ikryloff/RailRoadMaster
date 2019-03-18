using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuit : MonoBehaviour {
    private string trackName;
    private int isCarPresence;
    private int useMode;
    private Switch switchTC;
    [SerializeField]
    private SpriteRenderer cellsTrack;
    [SerializeField]
    private SpriteRenderer cellsStraight;
    [SerializeField]
    private SpriteRenderer cellsTurn;
    private SpriteRenderer[] allCells;
    [SerializeField]
    private string[] trackLightsNames;
    [SerializeField]
    private TrafficLights[] trackLights;
    [SerializeField]
    private TrafficLightsManager trafficLightsManager;
    public PathHolder pathHolder;
    public int trackCircuitID;
    public bool isSwitch;
    public Switch switchTrack;
    public Route route;
    

    private void Awake()
    {
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        pathHolder = GameObject.Find("PathHolder").GetComponent<PathHolder>();
        isSwitch = GetComponentInParent<Switch>();
        if (isSwitch)
            switchTrack = GetComponentInParent<Switch>();
        GetTrackLightsByTrack();
        
    }




    private void Start()
    {
        route = GameObject.Find("Route").GetComponent<Route>();
        trackCircuitID = pathHolder.trackCircuitTC_ID[this];
        
        useMode = Constants.TC_DEFAULT;
        allCells = new SpriteRenderer[3];
        allCells[0] = cellsTrack;
        if (tag == "Switch")
        {
            switchTC = transform.parent.GetComponent<Switch>();
            allCells[1] = cellsStraight;
            allCells[2] = cellsTurn;
        }
        SetCellsLight(allCells, Constants.TC_DEFAULT);
        
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RollingStock")
        {
            if (tag == "Switch")
            {
                switchTC.SwitchLockCount += 1;
            }
            IsCarPresence += 1;
            SetCellsLight(ReturnCells(), Constants.TC_OVER);
            other.GetComponent<RollingStock>().TrackCircuit = this;
        }
        GetTrackLightsByTrack();
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RollingStock")
        {
            if (tag == "Switch")
            {
                transform.parent.GetComponent<Switch>().SwitchLockCount -= 1;
            }
            IsCarPresence -= 1;
            SetCellsLight(ReturnCells(), Constants.TC_DEFAULT);
        }        
    }

    public int IsCarPresence
    {
        get
        {
            return isCarPresence;
        }

        set
        {
            isCarPresence = value;

        }
    }


    public int UseMode
    {
        get
        {
            if (isCarPresence > 0)
            {
                useMode = Constants.TC_OVER;
            }
            if (useMode == Constants.TC_OVER && isCarPresence == 0)
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
                    if (IsCarPresence > 0)
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
