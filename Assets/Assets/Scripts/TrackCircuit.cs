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


    private void Awake()
    {
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
    }




    private void Start()
    {
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
    public void GetTrackLightsByTrack(TrackCircuit track)
    {
        if(track.TrackName == "Track_10_14_18" || track.TrackName == "Track_10" || track.TrackName == "Track_9" || track.TrackName == "Track_11")
        {
            TrackLightsNames = new string[] { "M3", "End10" };
        }
        if (track.TrackName == "Track_6")
        {
            TrackLightsNames = new string[] { "End6", "M2" };
        }
        if (track.TrackName == "Track_2")
        {
            TrackLightsNames = new string[] { "N2", "CH2" };
        }
        if (track.TrackName == "Track_I_16_15")
        {
            TrackLightsNames = new string[] { "NI", "CHI" };
        }
        if (track.TrackName == "Track_3")
        {
            TrackLightsNames = new string[] { "N3", "CH3" };
        }
        if (track.TrackName == "Track_4")
        {
            TrackLightsNames = new string[] { "N4", "CH4" };
        }
        if (track.TrackName == "Track_5")
        {
            TrackLightsNames = new string[] { "N5", "CH5" };
        }
        if (track.TrackName == "Track_7")
        {
            TrackLightsNames = new string[] { "M1", "End7" };
        }
        if (track.TrackName == "Track_12_17_19" || track.TrackName == "Track_12" || track.TrackName == "Track_13" || track.TrackName == "Track_12A")
        {
            TrackLightsNames = new string[] { "M5", "End12" };
        }
        if (track.TrackName == "Track_8")
        {
            TrackLightsNames = new string[] { "End8", "M4" };
        }
        TrackLights = new TrafficLights[2];

        for (int i = 0; i < TrackLightsNames.Length; i++)
        {
            TrackLights[i] = trafficLightsManager.GetTrafficLightByName(TrackLightsNames[i]);
        }        
    }
}
