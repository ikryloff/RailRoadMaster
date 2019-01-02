using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : Singleton<PathMaker> {

    public PathHolder pathHolder;    
    public Engine engine;
    public int direction;
    public Route rt;
    List<Node> list;

    private void Awake()
    {
        list = pathHolder.nodesList;
    }

    // Use this for initialization
    void Start () {
        direction = engine.direction;
        StartCoroutine(GetStartTrack());
    }
	
	// Update is called once per frame
	void Update () {
        direction = engine.direction;

    }

    IEnumerator GetStartTrack()
    {        
        yield return new WaitForSecondsRealtime(0.2f);
        
    }

    public int GetID(TrackCircuit tc)
    {
        return pathHolder.trackCircuitTC_ID[tc];
    }
    public void ChangeSwitch(int numID)
    {

    }

    public void SwitchMinus(int numID)
    {

    }

    public void SwitchPlus(int numID)
    {
      
    }


    public TrackCircuit FindStart()
    {
        int start = GetID(engine.Track);
        
        
        return null;

    }
/*
    public void PrintPath(int _direction)
    {
        string route = "new _ ";
        int[] point = FindStart();

        bool isGoing = true;
        if(_direction >= 0)
        {
            for (int i = point[0]; i < 8; i++)
            {
                if (!isGoing)
                    break;
                for (int j = point[1]; j < 21; j++)
                {
                    if (stationMap[i, j] == 1000)
                        continue;
                    if (stationMap[i, j] < 0)
                    {
                        int temp = stationMap[i, j];
                        // switches > 21
                        if (stationMap[i, j] > -20)
                            i += 1 * _direction;
                        // switches > 40
                        else if (stationMap[i, j] < -49)
                            i -= 1 * _direction;
                        if (Math.Abs(stationMap[i, j] - temp) > 2)
                        {
                            isGoing = false;
                            break;
                        }
                        
                    }

                    if (stationMap[i, j] == 0)
                    {
                        isGoing = false;
                        break;
                    }
                    rt.fullEnginePath.Add(pathHolder.trackCircuitID_TC[Math.Abs(stationMap[i, j])]);
                    route += " -> " + stationMap[i, j];

                }

            }
        }
        else if(_direction < 0)
        {
            for (int i = point[0]; i < 8; i++)
            {
                // condition of quit from outter cicle
                if (!isGoing)
                    break;
                for (int j = point[1]; j >= 0; j--)
                {
                    if (stationMap[i, j] == 1000)
                        continue;
                    if (stationMap[i, j] < 0)
                    {
                        int temp = stationMap[i, j];
                        // switches > 21
                        if (stationMap[i, j] > -21)
                            i += 1 * _direction;
                        // switches > 40
                        else if (stationMap[i, j] < -49)
                            i -= 1 * _direction;
                        if (Math.Abs(stationMap[i, j] - temp) > 2)
                        {
                            isGoing = false;
                            break;
                        }
                        
                    }

                    if (stationMap[i, j] == 0)
                    {
                        isGoing = false;
                        break;
                    }
                    route += " -> " + stationMap[i, j];
                    //route += " -> " + pathHolder.trackCircuitID_TC[Math.Abs(stationMap[i, j])].name;
                    rt.fullEnginePath.Add(pathHolder.trackCircuitID_TC[Math.Abs(stationMap[i, j])]);
                }

            }
        }  
        print(route);
        print( "50 " + stationMap[2, 4]);
    }

    public class StationList  // graf
    {
        public string list = "";

        public void MakePath(Node track, int direction)
        {

            list += " -> " + track.Track;
            track.Next = direction == 1 ? track.NextPlus : track.PrevPlus;
            if (direction == 1)
            {
                if (track.NextPlus != null)
                    track.NextPlus.PrevPlus = track;
                if (track.IsSwitch && track.Track.Contains("-"))
                {
                    track.Next = track.NextMin;
                }
            }
            else if (direction == -1)
            {
                if (track.PrevPlus != null)
                    track.PrevPlus.NextPlus = track;
                if (track.IsSwitch && track.Track.Contains("-"))
                {
                    track.Next = track.PrevMin;
                }
            }

            if (track.Next == null)
                return;
            else
            {
                MakePath(track.Next, direction);
            }

        }

        public void PrintPath(Node track, int direction)
        {
            list = "";
            MakePath(track, direction);
            Console.WriteLine(list);

        }

    }*/
}
