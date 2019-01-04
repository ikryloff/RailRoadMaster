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
    

    private void Awake()
    {
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        pathHolder = GameObject.Find("PathHolder").GetComponent<PathHolder>();
        isSwitch = GetComponentInParent<Switch>();
        
    }




    private void Start()
    {
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
        GetTrackLightsByTrack(this);
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
        GetTrackLightsByTrack(this);
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
    public void GetTrackLightsByTrack(TrackCircuit track)
    {
        if (track.name == "Track_10_14_18" || track.name == "TrackCircuitSw18" || track.name == "TrackCircuitSw20" || track.name == "TrackCircuitSw22" || track.name == "Track_9" )
        {
            TrackLightsNames = new string[] { "M3", "End10" };
        }
        if (track.name == "Track_10" || track.name == "Track_11")
        {
            TrackLightsNames = new string[] { "End10_11", "End10" };
        }
        if (track.name == "Track_12" )
        {
            TrackLightsNames = new string[] { "End12_13N", "End12_13CH" };
        }
        if (track.name == "Track_13")
        {
            TrackLightsNames = new string[] { "End12_13N", "End12_13CH" };
        }
        if (track.name == "Track_9")
        {
            TrackLightsNames = new string[] { "End9", "End10" };
        }
        if (track.name == "Track_6")
        {
            TrackLightsNames = new string[] { "End6", "M2" };
        }        
        if (track.name == "Track_2")
        {
            TrackLightsNames = new string[] { "N2", "CH2" };
        }
        if (track.name == "Track_I_16_15")
        {
            TrackLightsNames = new string[] { "NI", "CHI" };
        }
        if (track.name == "Track_3")
        {
            TrackLightsNames = new string[] { "N3", "CH3" };
        }
        if (track.name == "Track_4")
        {
            TrackLightsNames = new string[] { "N4", "CH4" };
        }
        if (track.name == "Track_5")
        {
            TrackLightsNames = new string[] { "N5", "CH5" };
        }
        if (track.name == "Track_7")
        {
            TrackLightsNames = new string[] { "M1", "End7" };
        }
        if (track.name == "Track_12_17_19" || track.name == "Track_12A" || track.name == "TrackCircuitSw21" || track.name == "TrackCircuitSw19")
        {
            TrackLightsNames = new string[] { "M5", "End12" };
        }
        if (track.name == "Track_8")
        {
            TrackLightsNames = new string[] { "End8", "M4" };
        }
        if (track.name == "TrackCircuitSw5_17Bot")
        {
            TrackLightsNames = new string[] { "End8", "End12" };
        }
        if (track.name == "Track_14" )
        {
            TrackLightsNames = new string[] { "End14", "End14SW" };
        }

        if (track.name == "TrackCircuitSw14" )
        {
            TrackLightsNames = new string[] { "End6", "End14SW" };
        }     

        TrackLights = new TrafficLights[2];

        for (int i = 0; i < TrackLightsNames.Length; i++)
        {
            TrackLights[i] = trafficLightsManager.GetTrafficLightByName(TrackLightsNames[i]);            
        }        
    }
}
