using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMaker : Singleton<PathMaker> {

    public PathHolder pathHolder;    
    public Engine engine;
    public int direction;
    public Route rt;
    List<Node> list;
    //string route = " new ";
    public Switch testSw;
    public List<TrackCircuit> fullEnginePath;



    private void Awake()
    {
       
    }
    // Use this for initialization
    void Start ()
    {
        fullEnginePath = new List<TrackCircuit>();
        list = pathHolder.nodesList;        
        StartCoroutine(MakePathCoroutine());        
       
        
    }
	
	// Update is called once per frame
	void Update () {       
        direction = engine.direction;       
    }

    IEnumerator MakePathCoroutine()
    {        
        yield return new WaitForSecondsRealtime(0.2f);
        MakePath(GetEngineNode(), direction);
        //print(route);
        //route = "";        
    }



    public int GetID(TrackCircuit tc)
    {
        return pathHolder.trackCircuitTC_ID[tc];
    }
   
    public List<TrackCircuit> GetFullPath(int _direction)
    {
        fullEnginePath.Clear();
        MakePath(GetEngineNode(), _direction);
        return fullEnginePath;    
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
        //route += " -> " + node.track.name;
        fullEnginePath.Add(node.track);
        
        Node next = null;
        TrackCircuit nextTC = null;
        // if we go forward or have no direction
        if(direction >= 0)
        {
            if (node.track.isSwitch && !node.track.GetComponentInParent<Switch>().IsSwitchStraight)
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
                    if (nextTC.GetComponentInParent<Switch>().IsSwitchStraight)
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
            if (node.track.isSwitch && !node.track.GetComponentInParent<Switch>().IsSwitchStraight)
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
                    if (nextTC.GetComponentInParent<Switch>().IsSwitchStraight)
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
