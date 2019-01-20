﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMaker : Singleton<PathMaker> {
    [SerializeField]
    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    [SerializeField]
    private TrafficLights end14_22SW, end22_14SW, end9_18, end10_20, end11_20, end12CH, end12N, end13CH, end13N;
    public PathHolder pathHolder;    
    public Engine engine;
    public int direction;
    public Route rt;
    List<Node> list;
    string route = " new ";
    string routeForward = " new ";
    string routeBackward = " new ";
    public TrafficLights[] ends;
    public List<TrackCircuit> fullEnginePath;
    public TrackCircuit occupiedTrack;
    public TrackCircuit lastRouteTrackForward;
    public TrackCircuit lastRouteTrackBackward;
    TrackCircuit engineTrack;
    SwitchManager switchManager;
    Switch[] switches;
    [SerializeField]
    private TrackCircuit                    
        tc9,
        tc10,
        tc10_10,
        tc11,
        tc12,
        tc12_12,
        tc12A,
        tc13,
        tc14,        
        tcsw14,
        tcsw12,
        tcsw22,
        tcsw18,
        tcsw19,
        tcsw20        
        ;

    private void Awake()
    {
        switchManager = GameObject.Find("SwitchManager").GetComponent<SwitchManager>();
        switches = FindObjectsOfType<Switch>();        

        // Cashing hand switches

        switch18 = GetSwitchByName("Switch_18");
        switch19 = GetSwitchByName("Switch_19");
        switch20 = GetSwitchByName("Switch_20");
        switch21 = GetSwitchByName("Switch_21");
        switch22 = GetSwitchByName("Switch_22");
        switch10 = GetSwitchByName("Switch_10");
        switch12 = GetSwitchByName("Switch_12");
        switch14 = GetSwitchByName("Switch_14");

        end14_22SW = GetEndByName("End14_22SW");
        end22_14SW = GetEndByName("End22_14SW");
        end9_18 = GetEndByName("End9_18");
        end10_20 = GetEndByName("End10_20");
        end11_20 = GetEndByName("End11_20");
        end12CH = GetEndByName("End12CH");
        end12N = GetEndByName("End12N");
        end13CH = GetEndByName("End13CH");
        end13N = GetEndByName("End13N");
        
    }


    void Start ()
    {
        fullEnginePath = new List<TrackCircuit>();
        list = pathHolder.nodesList;        
        engineTrack = engine.Track;
        
    }
	
	
	void Update () {       
        direction = engine.direction;
        // Update path each TrackCircuit
        if(engineTrack != engine.Track)
        {
            GetFullPath(direction);
            engineTrack = engine.Track;
        }
        

    }




    public int GetID(TrackCircuit tc)
    {
        return pathHolder.trackCircuitTC_ID[tc];
    }

    public void CheckHandSwitches(int _direction)
    {

        if (switch22.IsSwitchStraight)
        {
            end14_22SW.IsClosed = false;
            end22_14SW.IsClosed = true;
        }

        /*
        foreach (TrafficLights tc in ends)
        {
            tc.IsClosed = true;
        }

       
        foreach (TrackCircuit tr in fullEnginePath)
        {
            if (ends.Contains(tr.TrackLights[1]) && tr.TrackLights[1].IsClosed)
            {
                tr.TrackLights[1].IsClosed = false;
            }
            if (ends.Contains(tr.TrackLights[0]) && tr.TrackLights[0].IsClosed)
            {
                tr.TrackLights[0].IsClosed = false;
            }
        }

    */

    }
   
   
    public void GetFullPath(int _direction)
    {
        
        fullEnginePath.Clear();
        route = "";
        routeForward = "";
        routeBackward = "";
        MakePath(GetEngineNode(), _direction);
        print("real path " + route);
        
        if (fullEnginePath != null)
        {

            occupiedTrack = null;
            foreach (TrackCircuit track in fullEnginePath)
            {
                if (track.IsCarPresence > 0 && track != fullEnginePath.First())
                {
                    occupiedTrack = track;
                    break;
                }    
                
                occupiedTrack = fullEnginePath.Last();     
            }

            //CheckHandSwitches(_direction);

            if ( _direction >= 0)
            {
                foreach (TrackCircuit tr in fullEnginePath)
                {

                    routeForward += " - > " + tr.name;
                    if (tr.TrackLights[1] && tr.TrackLights[1].IsClosed)
                    {
                        lastRouteTrackForward = tr;
                        break;
                    }
                }
            }
           
            if (_direction == -1)
            {
                foreach (TrackCircuit tr in fullEnginePath)
                {
                    routeBackward += " - > " + tr.name;
                    if (tr.TrackLights[0] != null && tr.TrackLights[0].IsClosed)
                    {
                        lastRouteTrackBackward = tr;
                        break;
                    }
                }
            }
                
        }
        else
        {

            occupiedTrack = engine.Track;
            lastRouteTrackBackward = engine.Track;
            lastRouteTrackForward = engine.Track;
        }


        print("OccupiedTrack  " + occupiedTrack);
        print("lastRouteTrackForward  " + lastRouteTrackForward);
        print("pathForward  " + routeForward);
        print("lastRouteTrackBackward  " + lastRouteTrackBackward);
        print("pathBack  " + routeBackward);

    }

    public Switch GetSwitchByName(string _switchName)
    {
        foreach (var sw in switches)
        {
            if (sw.name == _switchName)
                return sw;
        }
        return null;
    }

    public TrafficLights GetEndByName(string _endName)
    {
        foreach (var e in ends)
        {
            if (e.name == _endName)
                return e;
        }
        return null;
    }

    public Node GetEngineNode()
    {
        // Find ID of Engine trackCircuit
        int start = GetID(engine.Track);                
        return pathHolder.nodesID_ND[start];
    }

    public Node GetNodeByID(int nodeID)
    {       
        return pathHolder.nodesID_ND[nodeID];
    }

    public void MakePath(Node node, int _direction)
    {
        route += " -> " + node.track.name;
        fullEnginePath.Add(node.track);

        Node next = null;
        TrackCircuit nextTC = null;
        // if we go forward or have no direction
        if (_direction >= 0)
        {
            if (node.track.isSwitch && !node.track.switchTrack.IsSwitchStraight)
            {
                nextTC = node.nextMin;
            }
            else
            {
                nextTC = node.nextPlus;
            }

            if (nextTC != null)
            {
                next = GetNodeByID(nextTC.trackCircuitID);
                // check if switch from the path is in right position

                if (nextTC.isSwitch)
                {
                    if (nextTC.switchTrack.IsSwitchStraight)
                    {
                        if (next.prevPlus != node.track)
                            nextTC = null;
                    }
                    else
                    {
                        if (next.prevMin != node.track)
                            nextTC = null;
                    }
                }
            }
        }
        // if we go backward 
        else
        {
            if (node.track.isSwitch && !node.track.switchTrack.IsSwitchStraight)
            {
                nextTC = node.prevMin;
            }
            else
            {
                nextTC = node.prevPlus;
            }

            if (nextTC != null)
            {
                next = GetNodeByID(nextTC.trackCircuitID);

                // check if switch from the path is in right position
                if (nextTC.isSwitch)
                {
                    if (nextTC.switchTrack.IsSwitchStraight)
                    {
                        if (next.nextPlus != node.track)
                            nextTC = null;
                    }
                    else
                    {
                        if (next.nextMin != node.track)
                            nextTC = null;
                    }
                }
            }

        }
        if (nextTC == null)
            return;
        else
        {
            MakePath(next, _direction);
        }

    }


}