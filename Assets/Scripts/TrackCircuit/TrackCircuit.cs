using System.Collections.Generic;
using UnityEngine;

public class TrackCircuit : MonoBehaviour, IManageable
{
    private string trackName;
    public bool HasCarPresence { get; set; }
    public TrafficLight [] TrackLights { get; set; }
    public Switch SwitchTrack { get; set; }
    public Route route;
    public RollingStock engineRS;

    public bool IsInRoute { get; set; }
    public bool IsInPath { get; set; }
    public bool IsInUse { get; set; }
    public Engine engine;
    public TrackPathUnit [] paths;
    public IndicatorPath [] Indicators { get; set; }


    public void Init()
    {
        paths = transform.GetComponentsInChildren<TrackPathUnit> ();
        Indicators = transform.GetComponentsInChildren<IndicatorPath> ();
        SetTCToPaths ();
        SetTCToIndicators ();
        SwitchTrack = GetComponentInParent<Switch> ();
        TrackLights = new TrafficLight [2];
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
        CarPresenceOff ();
    }

    private void SetTCToPaths()
    {
        foreach ( TrackPathUnit item in paths )
        {
            item.TrackCircuit = this;
        }
    }

    public void CarPresenceOn()
    {
        HasCarPresence = true;
        if ( SwitchTrack )
            SwitchTrack.IsLockedByRS = true;
    }

    public void CarPresenceOff()
    {
        HasCarPresence = false;
        if ( SwitchTrack )
            SwitchTrack.IsLockedByRS = false;
    }

    private void Update()
    {


    }

    public void TrackCircuitColor()
    {



    }

    public void IndicationTrackInPath( List<TrackPathUnit> paths )
    {
        if ( TrackPath.Instance.TrackList != null && paths != null )
        {
            foreach ( TrackPathUnit item in TrackPath.Instance.TrackList )
            {
                if ( item.isActiveAndEnabled && item.TrackCircuit.IsInPath )
                    item.TrackCircuit.IsInPath = false;
            }
            foreach ( TrackPathUnit item in paths )
            {
                item.TrackCircuit.IsInPath = true;
            }
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







    public void SetCellsLight( SpriteRenderer [] cells, int color )
    {
        if ( cells != null )
        {
            foreach ( SpriteRenderer cell in cells )
            {
                if ( cell )
                {
                    if ( color == Constants.TC_WAIT )
                        cell.color = new Color32 (250, 240, 125, 255);
                    else if ( color == Constants.TC_OVER )
                        cell.color = new Color32 (215, 0, 0, 255);
                    else
                        cell.color = new Color32 (190, 190, 190, 255);
                    if ( HasCarPresence )
                        cell.color = new Color32 (215, 0, 0, 255);
                }
            }

        }
    }




}
