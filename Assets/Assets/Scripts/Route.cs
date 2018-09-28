#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2_4, sw6_8, sw10, sw12, sw14, sw16, sw1_3, sw5_17, sw7_9, sw11, sw13, sw15, sw18, sw20, sw19, sw21;

    [SerializeField]
    private TrackCircuit 
        tcI_CH,       
        tcI_N,       
        tcI_CH_2,
        tcI_1_N,
        tcI_16_15,
        tc2,
        tc3,
        tc4,
        tc5,
        tc6,        
        tc7,        
        tc8,        
        tc9,        
        tc10,
        tc10_10,
        tc11,
        tc12,
        tc12_12,
        tc12A,
        tc13,
        tcsw_1_3top,
        tcsw_1_3bot,
        tcsw_5_17top,
        tcsw_5_17bot,
        tcsw_7_9top,
        tcsw_7_9bot,
        tcsw_2_4top,
        tcsw_2_4bot,
        tcsw_6_8top,
        tcsw_6_8bot,
        tcsw16,
        tcsw10,
        tcsw11,
        tcsw12,
        tcsw13,
        tcsw14,
        tcsw15,
        tcsw18,    
        tcsw19,    
        tcsw20,   
        tcsw21   
        ;    

    List<RouteObject> routes;
    private RouteObject route;
    private TrackCircuit[] forwardPath;
    [SerializeField]
    private List <TrackCircuit> trackCircuits;
    public Engine engine;
    private TrackCircuit startTrack;
    private TrackCircuit occupiedTrack;
    private bool isPathCheckingForward;
    private Switch[] switches;

    IEnumerable<TrackCircuit> fullPath;
    private bool isRoute;
    private string routeName;
    [SerializeField]
    private Text lightText;
    [SerializeField]
    private Button cancelRouteButton;
    SwitchManager switchManager;


    public class PathPart : IEquatable<PathPart>
    {
        public int PartId { get; set; }
        public TrackCircuit[] PartsArr { get; set; }

        

        public override string ToString()
        {
            string res = "ID: " + PartId;

            foreach (TrackCircuit s in PartsArr)
            {
                res += " " + s.name;
            }
            return res;
        }

        
        public int GetID()
        {
            return PartId;
        }

        bool IEquatable<PathPart>.Equals(PathPart other)
        {
            return PartId.Equals(other.PartId);
        }
    }

    public TrackCircuit GetTrackCircuitByName( string _track)
    {
        foreach (var tc in trackCircuits)
        {
            if (tc.TrackName == _track)
                return tc;
        }
        return null;
    }

    private void Awake()
    {
        TrackCircuit [] tempArr = GameObject.FindObjectsOfType<TrackCircuit>();
        trackCircuits = new List<TrackCircuit>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            if (tempArr[i].tag == "Track")
                trackCircuits.Add(tempArr[i]);
        }
        switchManager = GameObject.Find("SwitchManager").GetComponent<SwitchManager>();
        switches = FindObjectsOfType<Switch>();
    }

    public void MakePath()
    {

        if (engine.IsEngineGoesAhead())
        {
            List<PathPart> parts = new List<PathPart>();
            parts.Clear();
            startTrack = engine.Track;
            FullPath = null;
            if (IsPathCheckingForward)
            {
                //forward parts
                parts.Add(new PathPart() { PartId = 0, PartsArr = new TrackCircuit[] { tcI_CH, tcI_CH_2 } });
                parts.Add(new PathPart() { PartId = 1, PartsArr = new TrackCircuit[] { tcI_CH_2, tcsw_2_4top } });
                parts.Add(new PathPart() { PartId = 2, PartsArr = new TrackCircuit[] { tc6, tcsw_2_4bot } });
                parts.Add(new PathPart() { PartId = 3, PartsArr = new TrackCircuit[] { tcsw_2_4top, tcsw_2_4bot } });
                parts.Add(new PathPart() { PartId = 4, PartsArr = new TrackCircuit[] { tcsw_2_4top, tcsw_6_8top } });
                parts.Add(new PathPart() { PartId = 5, PartsArr = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot } });
                parts.Add(new PathPart() { PartId = 6, PartsArr = new TrackCircuit[] { tcsw_6_8bot, tcsw_6_8top } });
                parts.Add(new PathPart() { PartId = 7, PartsArr = new TrackCircuit[] { tcsw_6_8bot, tcsw10 } });
                parts.Add(new PathPart() { PartId = 8, PartsArr = new TrackCircuit[] { tcsw16, tc2 } });
                parts.Add(new PathPart() { PartId = 9, PartsArr = new TrackCircuit[] { tc2, tcsw15 } });
                parts.Add(new PathPart() { PartId = 10, PartsArr = new TrackCircuit[] { tcsw15, tcsw_7_9top } });
                parts.Add(new PathPart() { PartId = 11, PartsArr = new TrackCircuit[] { tcsw16, tcI_16_15 } });
                parts.Add(new PathPart() { PartId = 12, PartsArr = new TrackCircuit[] { tcI_16_15, tcsw15 } });
                parts.Add(new PathPart() { PartId = 13, PartsArr = new TrackCircuit[] { tcsw10, tc3 } });
                parts.Add(new PathPart() { PartId = 14, PartsArr = new TrackCircuit[] { tc3, tcsw11 } });
                parts.Add(new PathPart() { PartId = 15, PartsArr = new TrackCircuit[] { tcsw10, tcsw12 } });
                parts.Add(new PathPart() { PartId = 16, PartsArr = new TrackCircuit[] { tcsw12, tc4 } });
                parts.Add(new PathPart() { PartId = 17, PartsArr = new TrackCircuit[] { tc4, tcsw13 } });
                parts.Add(new PathPart() { PartId = 18, PartsArr = new TrackCircuit[] { tcsw12, tcsw14 } });
                parts.Add(new PathPart() { PartId = 19, PartsArr = new TrackCircuit[] { tcsw14, tc5 } });
                parts.Add(new PathPart() { PartId = 20, PartsArr = new TrackCircuit[] { tc5, tcsw13 } });
                parts.Add(new PathPart() { PartId = 21, PartsArr = new TrackCircuit[] { tcsw14, tc10 } });
                parts.Add(new PathPart() { PartId = 22, PartsArr = new TrackCircuit[] { tcsw_7_9top, tcsw_1_3top } });
                parts.Add(new PathPart() { PartId = 23, PartsArr = new TrackCircuit[] { tcsw_7_9top, tcsw_7_9bot } });
                parts.Add(new PathPart() { PartId = 24, PartsArr = new TrackCircuit[] { tcsw_7_9bot, tcsw_5_17top } });
                parts.Add(new PathPart() { PartId = 25, PartsArr = new TrackCircuit[] { tcsw_1_3top, tcI_1_N } });
                parts.Add(new PathPart() { PartId = 26, PartsArr = new TrackCircuit[] { tcI_1_N, tcI_N } });
                parts.Add(new PathPart() { PartId = 27, PartsArr = new TrackCircuit[] { tcsw_5_17top, tcsw_1_3bot } });
                parts.Add(new PathPart() { PartId = 28, PartsArr = new TrackCircuit[] { tcsw_5_17top, tcsw_5_17bot } });
                parts.Add(new PathPart() { PartId = 29, PartsArr = new TrackCircuit[] { tcsw_5_17bot, tc12 } });
                parts.Add(new PathPart() { PartId = 30, PartsArr = new TrackCircuit[] { tcsw_1_3bot, tcsw_1_3top } });
                parts.Add(new PathPart() { PartId = 31, PartsArr = new TrackCircuit[] { tc8, tcsw_5_17bot } });
                parts.Add(new PathPart() { PartId = 32, PartsArr = new TrackCircuit[] { tcsw13, tcsw11 } });
                parts.Add(new PathPart() { PartId = 33, PartsArr = new TrackCircuit[] { tcsw11, tcsw_7_9bot } });
                parts.Add(new PathPart() { PartId = 34, PartsArr = new TrackCircuit[] { tcsw_1_3bot, tc7 } });
                parts.Add(new PathPart() { PartId = 35, PartsArr = new TrackCircuit[] { tcsw_6_8top, tcsw16 } });
                parts.Add(new PathPart() { PartId = 36, PartsArr = new TrackCircuit[] { tc10, tcsw18 } });
                parts.Add(new PathPart() { PartId = 37, PartsArr = new TrackCircuit[] { tcsw18, tc9 } });
                parts.Add(new PathPart() { PartId = 38, PartsArr = new TrackCircuit[] { tcsw18, tcsw20 } });
                parts.Add(new PathPart() { PartId = 39, PartsArr = new TrackCircuit[] { tcsw20, tc10_10 } });
                parts.Add(new PathPart() { PartId = 90, PartsArr = new TrackCircuit[] { tcsw20, tc11 } });
                parts.Add(new PathPart() { PartId = 91, PartsArr = new TrackCircuit[] { tc12, tcsw19 } });
                parts.Add(new PathPart() { PartId = 92, PartsArr = new TrackCircuit[] { tcsw19, tc12_12 } });
                parts.Add(new PathPart() { PartId = 93, PartsArr = new TrackCircuit[] { tc12_12, tcsw21 } });
                parts.Add(new PathPart() { PartId = 94, PartsArr = new TrackCircuit[] { tcsw21, tc12A } });
                parts.Add(new PathPart() { PartId = 95, PartsArr = new TrackCircuit[] { tcsw19, tc13 } });
                parts.Add(new PathPart() { PartId = 96, PartsArr = new TrackCircuit[] { tc13, tcsw21 } });

            }
            if (!IsPathCheckingForward)
            {
                //backward parts
                parts.Add(new PathPart() { PartId = 40, PartsArr = new TrackCircuit[] { tcI_1_N, tcI_N } });
                parts.Add(new PathPart() { PartId = 41, PartsArr = new TrackCircuit[] { tcI_N, tcsw_1_3top } });
                parts.Add(new PathPart() { PartId = 42, PartsArr = new TrackCircuit[] { tcsw_1_3top, tcsw_7_9top } });
                parts.Add(new PathPart() { PartId = 43, PartsArr = new TrackCircuit[] { tcsw_1_3top, tcsw_1_3bot } });
                parts.Add(new PathPart() { PartId = 44, PartsArr = new TrackCircuit[] { tc7, tcsw_1_3bot } });
                parts.Add(new PathPart() { PartId = 45, PartsArr = new TrackCircuit[] { tcsw_1_3bot, tcsw_5_17top } });
                parts.Add(new PathPart() { PartId = 46, PartsArr = new TrackCircuit[] { tc12, tcsw_5_17bot } });
                parts.Add(new PathPart() { PartId = 47, PartsArr = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top } });
                parts.Add(new PathPart() { PartId = 48, PartsArr = new TrackCircuit[] { tcsw_5_17bot, tc8 } });
                parts.Add(new PathPart() { PartId = 49, PartsArr = new TrackCircuit[] { tcsw_5_17top, tcsw_7_9bot } });
                parts.Add(new PathPart() { PartId = 50, PartsArr = new TrackCircuit[] { tcsw_7_9bot, tcsw_7_9top } });
                parts.Add(new PathPart() { PartId = 51, PartsArr = new TrackCircuit[] { tcsw_7_9bot, tcsw11 } });
                parts.Add(new PathPart() { PartId = 52, PartsArr = new TrackCircuit[] { tcsw_7_9top, tcsw15 } });
                parts.Add(new PathPart() { PartId = 53, PartsArr = new TrackCircuit[] { tcsw15, tc2 } });
                parts.Add(new PathPart() { PartId = 54, PartsArr = new TrackCircuit[] { tc2, tcsw16 } });
                parts.Add(new PathPart() { PartId = 55, PartsArr = new TrackCircuit[] { tcsw15, tcI_16_15 } });
                parts.Add(new PathPart() { PartId = 56, PartsArr = new TrackCircuit[] { tcI_16_15, tcsw16 } });
                parts.Add(new PathPart() { PartId = 57, PartsArr = new TrackCircuit[] { tcsw16, tcsw_6_8top } });
                parts.Add(new PathPart() { PartId = 58, PartsArr = new TrackCircuit[] { tcsw_6_8top, tcsw_6_8bot } });
                parts.Add(new PathPart() { PartId = 59, PartsArr = new TrackCircuit[] { tcsw_6_8top, tcsw_2_4top } });
                parts.Add(new PathPart() { PartId = 60, PartsArr = new TrackCircuit[] { tcsw_2_4top, tcI_CH_2 } });
                parts.Add(new PathPart() { PartId = 61, PartsArr = new TrackCircuit[] { tcI_CH_2, tcI_CH } });
                parts.Add(new PathPart() { PartId = 62, PartsArr = new TrackCircuit[] { tcsw11, tc3 } });
                parts.Add(new PathPart() { PartId = 63, PartsArr = new TrackCircuit[] { tc3, tcsw10 } });
                parts.Add(new PathPart() { PartId = 64, PartsArr = new TrackCircuit[] { tcsw10, tcsw_6_8bot } });
                parts.Add(new PathPart() { PartId = 65, PartsArr = new TrackCircuit[] { tcsw_6_8bot, tcsw_2_4bot } });
                parts.Add(new PathPart() { PartId = 66, PartsArr = new TrackCircuit[] { tcsw_2_4bot, tcsw_2_4top } });
                parts.Add(new PathPart() { PartId = 67, PartsArr = new TrackCircuit[] { tcsw_2_4bot, tc6 } });
                parts.Add(new PathPart() { PartId = 68, PartsArr = new TrackCircuit[] { tcsw11, tcsw13 } });
                parts.Add(new PathPart() { PartId = 69, PartsArr = new TrackCircuit[] { tcsw13, tc4 } });
                parts.Add(new PathPart() { PartId = 70, PartsArr = new TrackCircuit[] { tc4, tcsw12 } });
                parts.Add(new PathPart() { PartId = 71, PartsArr = new TrackCircuit[] { tcsw12, tcsw10 } });
                parts.Add(new PathPart() { PartId = 72, PartsArr = new TrackCircuit[] { tcsw13, tc5 } });
                parts.Add(new PathPart() { PartId = 73, PartsArr = new TrackCircuit[] { tc5, tcsw14 } });
                parts.Add(new PathPart() { PartId = 74, PartsArr = new TrackCircuit[] { tcsw14, tcsw12 } });
                parts.Add(new PathPart() { PartId = 75, PartsArr = new TrackCircuit[] { tc10, tcsw14 } });
                parts.Add(new PathPart() { PartId = 76, PartsArr = new TrackCircuit[] { tc12A, tcsw21 } });
                parts.Add(new PathPart() { PartId = 77, PartsArr = new TrackCircuit[] { tcsw21, tc12_12 } });
                parts.Add(new PathPart() { PartId = 78, PartsArr = new TrackCircuit[] { tc12_12, tcsw19 } });
                parts.Add(new PathPart() { PartId = 79, PartsArr = new TrackCircuit[] { tcsw19, tc12 } });
                parts.Add(new PathPart() { PartId = 80, PartsArr = new TrackCircuit[] { tcsw21, tc13 } });
                parts.Add(new PathPart() { PartId = 81, PartsArr = new TrackCircuit[] { tc13, tcsw19 } });
                parts.Add(new PathPart() { PartId = 82, PartsArr = new TrackCircuit[] { tc9, tcsw18 } });
                parts.Add(new PathPart() { PartId = 83, PartsArr = new TrackCircuit[] { tcsw18, tc10 } });
                parts.Add(new PathPart() { PartId = 84, PartsArr = new TrackCircuit[] { tc10_10, tcsw20 } });
                parts.Add(new PathPart() { PartId = 85, PartsArr = new TrackCircuit[] { tcsw20, tcsw18 } });
                parts.Add(new PathPart() { PartId = 86, PartsArr = new TrackCircuit[] { tc11, tcsw20 } });
            }


            if (sw2_4.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 3 });
                parts.Remove(new PathPart() { PartId = 66 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 2 });
                parts.Remove(new PathPart() { PartId = 4 });
                parts.Remove(new PathPart() { PartId = 59 });
            }

            if (sw6_8.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 6 });
                parts.Remove(new PathPart() { PartId = 58 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 4 });
                parts.Remove(new PathPart() { PartId = 7 });
                parts.Remove(new PathPart() { PartId = 59 });
                parts.Remove(new PathPart() { PartId = 64 });
            }

            if (sw16.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 8 });
                parts.Remove(new PathPart() { PartId = 54 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 11 });
                parts.Remove(new PathPart() { PartId = 56 });
            }

            if (sw10.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 15 });
                parts.Remove(new PathPart() { PartId = 71 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 13 });
                parts.Remove(new PathPart() { PartId = 63 });
            }

            if (sw12.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 16 });
                parts.Remove(new PathPart() { PartId = 70 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 18 });
                parts.Remove(new PathPart() { PartId = 74 });
            }

            if (sw14.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 19 });
                parts.Remove(new PathPart() { PartId = 73 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 21 });
                parts.Remove(new PathPart() { PartId = 75 });
            }

            if (sw13.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 17 });
                parts.Remove(new PathPart() { PartId = 69 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 20 });
                parts.Remove(new PathPart() { PartId = 72 });
            }

            if (sw15.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 9 });
                parts.Remove(new PathPart() { PartId = 53 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 12 });
                parts.Remove(new PathPart() { PartId = 55 });
            }

            if (sw11.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 32 });
                parts.Remove(new PathPart() { PartId = 68 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 14 });
                parts.Remove(new PathPart() { PartId = 62 });
            }

            if (sw7_9.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 23 });
                parts.Remove(new PathPart() { PartId = 50 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 22 });
                parts.Remove(new PathPart() { PartId = 33 });
                parts.Remove(new PathPart() { PartId = 42 });
                parts.Remove(new PathPart() { PartId = 51 });
            }

            if (sw5_17.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 28 });
                parts.Remove(new PathPart() { PartId = 47 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 27 });
                parts.Remove(new PathPart() { PartId = 31 });
                parts.Remove(new PathPart() { PartId = 45 });
                parts.Remove(new PathPart() { PartId = 48 });
            }

            if (sw1_3.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 30 });
                parts.Remove(new PathPart() { PartId = 43 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 22 });
                parts.Remove(new PathPart() { PartId = 34 });
                parts.Remove(new PathPart() { PartId = 42 });
                parts.Remove(new PathPart() { PartId = 44 });
            }

            if (sw18.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 37 });
                parts.Remove(new PathPart() { PartId = 82 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 38 });
                parts.Remove(new PathPart() { PartId = 85 });                
            }

            if (sw20.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 90 });
                parts.Remove(new PathPart() { PartId = 86 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 39 });
                parts.Remove(new PathPart() { PartId = 84 });
            }

            if (sw19.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 95 });
                parts.Remove(new PathPart() { PartId = 81 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 92 });
                parts.Remove(new PathPart() { PartId = 78 });
            }

            if (sw21.IsSwitchStraight)
            {
                parts.Remove(new PathPart() { PartId = 96 });
                parts.Remove(new PathPart() { PartId = 80 });
            }
            else
            {
                parts.Remove(new PathPart() { PartId = 93 });
                parts.Remove(new PathPart() { PartId = 77 });
            }


            //find start point
            foreach (var item in parts)
            {
                if (item.PartsArr.First() == startTrack)
                    FullPath = item.PartsArr;
            }
            Debug.Log("startPoint " + startTrack);

            // make full path
            foreach (var p in parts)
            {
                foreach (var part in parts)
                {
                    if (FullPath != null)
                    {
                        if (part.PartsArr.First() == FullPath.Last())
                        {
                            FullPath = FullPath.Union(part.PartsArr);
                        }
                    }                    
                }

            }

            // print full path
            string res = "";
            if (FullPath != null)
            {
                foreach (var item in FullPath)
                {

                    res += " -> " + item.name;

                }
                Debug.Log(res);
                OccupiedTrack = null;
                foreach (var track in FullPath)
                {
                    if (track.IsCarPresence > 0 && track != FullPath.First())
                    {
                        OccupiedTrack = track;                        
                        break;
                    }
                    if(OccupiedTrack == null)
                        OccupiedTrack = FullPath.Last();    
                }
            }
            else
                OccupiedTrack = startTrack;
            Debug.Log("Occupied  " + OccupiedTrack);
        }        
        

    }


    void Start()
    {
        routes = new List<RouteObject>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        Invoke("MakePath", 0.1f);        


    }

    private void FixedUpdate()
    {
        if(routes != null)
        {
            for (int i = 0; i < routes.Count; i++)
            {
                if (routes[i].TrackCircuits[0].IsCarPresence > 0)
                {
                    if(!IsShunting(routes[i].TrafficLights))
                        routes[i].StartLight.SetLightColor(0);                    
                }
                if (CheckRouteUsed(routes[i]))
                {
                    if (IsShunting(routes[i].TrafficLights))
                        routes[i].StartLight.SetLightColor(0);
                    DestroyRoute(routes[i]);
                }
            }

        }
    }


    /// Make routes
    /// 

    /// Make routes manager
    public void MakeRoute(TrafficLights startLight, TrafficLights endLight)
    {
                
        routeName = startLight.Name + endLight.Name;
        Debug.Log(routeName);
        if (CheckRouteForDuplicates(routeName))
        {
            TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
            route = gameObject.AddComponent<RouteObject>();
            Debug.Log("Made route");
            route.TrafficLights = tl;
            routes.Add(route);
            route.RouteName = routeName;

            RouteLightsManage(tl, true);
            Debug.Log(route.RouteName);
            RouteManage(route, routeName);
            cancelRouteButton.interactable = true;
        }
        else Debug.Log("Duplicate");

        MakePath();
    }      

    private void RouteManage(RouteObject ro,  string routeName)
    {
        // Routes NI done
        if (ro.RouteName == "NIM2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw16, tcsw_6_8top, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }
        else if (ro.RouteName == "NICH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
            ro.TrackCircuits = new TrackCircuit[] { tcsw16, tcsw_6_8top, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        
        // Routes N2 done
        else if (ro.RouteName == "N2CH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw16, tcsw_6_8top, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N2M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw16, tcsw_6_8top, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }
       
        // Routes N3 done
        else if (ro.RouteName == "N3CH")
        {
            ro.SwitchesStr = new Switch[] { sw10 , sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N3M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes N4 done
        else if (ro.RouteName == "N4CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N4M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes N5 done
        else if (ro.RouteName == "N5CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N5M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes M3 done
        else if (ro.RouteName == "M3M2")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes CH done
        else if (ro.RouteName == "CHCH2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH_2, tcsw_2_4top, tcsw_6_8top, tcsw16, tc2 };
        }   
        else if (ro.RouteName == "CHCHI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH_2, tcsw_2_4top, tcsw_6_8top, tcsw16, tcI_16_15 };
        }
        else if (ro.RouteName == "CHCH3")
        {
            ro.SwitchesStr = new Switch[] { sw10, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tc3};
        }
        else if (ro.RouteName == "CHCH4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tc4 };
        }
        else if (ro.RouteName == "CHCH5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] {tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc5 };
        }

        // Routes M2 done
        else if (ro.RouteName == "M2NI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] {  tcsw_2_4bot, tcsw_6_8bot, tcsw_6_8top, tcsw16, tcI_16_15 };
        }
        else if (ro.RouteName == "M2N2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw_6_8top, tcsw16, tc2 };
        }
        else if (ro.RouteName == "M2N3")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw10, tc3 };
        }
        else if (ro.RouteName == "M2N4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tc4 };
        }
        else if (ro.RouteName == "M2N5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc5 };
        }       
        else if (ro.RouteName == "M2M3")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc10 };
        }

        // Routes CHI done
        else if (ro.RouteName == "CHIM1")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CHIM5")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] {  tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CHIN")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
            ro.TrackCircuits = new TrackCircuit[] { tcsw15, tcsw_7_9top, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH2 done
        else if(ro.RouteName == "CH2M1")
        {
            ro.TrackCircuits = new TrackCircuit[] { tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw15, sw7_9 };
            Debug.Log(ro.RouteName);
        }
        else if (ro.RouteName == "CH2M5")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH2N")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw15, tcsw_7_9top, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH3 done
        else if (ro.RouteName == "CH3M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw11, sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH3M5")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17};
            ro.TrackCircuits = new TrackCircuit[] { tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH3N")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9, sw5_17};
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH4 done
        else if (ro.RouteName == "CH4M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH4M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17};
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH4N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17 };
            ro.SwitchesTurn = new Switch[] { sw11, sw13, sw1_3};
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH5 done
        else if (ro.RouteName == "CH5M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH5M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH5N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes M4 done
        else if (ro.RouteName == "M4M5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tc12 };
        }

        // Routes M5 done
        else if (ro.RouteName == "M5M4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tc8 };
        }
        else if (ro.RouteName == "M5CH2")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "M5CHI")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tcI_16_15 };
        }
        else if (ro.RouteName == "M5CH3")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "M5CH4")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "M5CH5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc5 };
        }

        // Routes M1 done
       
        else if (ro.RouteName == "M1CH2")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw15 };            
            ro.TrackCircuits = new TrackCircuit[] {  tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "M1CHI")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] {  tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tcI_16_15 };
        }
        else if (ro.RouteName == "M1CH3")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9, sw11 };            
            ro.TrackCircuits = new TrackCircuit[] { tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "M1CH4")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "M1CH5")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc5 };
        }

        // Routes N done
        else if (ro.RouteName == "NNI")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_1_N, tcsw_1_3top, tcsw_7_9top, tcsw15, tcI_16_15 };            
        }
        else if (ro.RouteName == "NN2")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_1_N, tcsw_1_3top, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "NN3")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw11 };
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "NN4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11, sw13 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "NN5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        ///////
        else DestroyRoute(ro);

        if (ro)
        {                        
            if (CheckRootByPresence(ro.TrackCircuits, ro.TrafficLights))
            {                
                if (CheckRouteBySwitches(ro.SwitchesStr, Constants.DIR_STR) && CheckRouteBySwitches(ro.SwitchesTurn, Constants.DIR_TURN))
                {
                    // if the track of reception of train is NOT used in other route
                    if((!IsShunting(ro.TrafficLights) && ro.TrackCircuits.Last().UseMode != Constants.TC_WAIT) || IsShunting(ro.TrafficLights))
                    {
                        Debug.Log("Make route direction " + ro.RouteName);
                        RouteDirection(ro.SwitchesStr, Constants.DIR_STR);
                        RouteDirection(ro.SwitchesTurn, Constants.DIR_TURN);
                        foreach (TrackCircuit tc in ro.TrackCircuits)
                        {
                            tc.UseMode = Constants.TC_WAIT;
                        }                        
                        Debug.Log("Route Locked " + ro.RouteName);
                    }
                    else
                    {
                        Debug.Log("Counter route");
                        DestroyRoute(ro, false);
                    }
                        
                }
                else
                {
                    Debug.Log("Danger cross route");                    
                    DestroyRoute(ro, false);
                }   
            }
            else
            {
                Debug.Log("Track presence");
                DestroyRoute(ro, false);
            }            
        }
    }

    public void DestroyRouteByRouteName(string _routeName)
    {
        DestroyRoute(GetRouteByName(_routeName));
    }
    

    private void DestroyRoute(RouteObject ro, bool withUnlock = true)
    {
        Debug.Log("Destroy");
        lightText.text = "None";
        RouteLightsManage(ro.TrafficLights, false);
        if (withUnlock)
        {
            RouteSwitchesUnlock(ro.SwitchesStr);
            RouteSwitchesUnlock(ro.SwitchesTurn);
            foreach (TrackCircuit tc in ro.TrackCircuits)
            {
                tc.UseMode = Constants.TC_DEFAULT;
            }
            Debug.Log("Unlock");
        }        
        routes.Remove(ro);
        Destroy(ro);
        if (!Routes.Any())
            cancelRouteButton.interactable = false;
    }

    public void DestroyRouteByLight(TrafficLights hitLight)
    {
        if (String.IsNullOrEmpty(hitLight.NameRouteOfLight))
        {
            hitLight.tag = Constants.LIGHTS_FREE;            
        }
        else
        {
            // Remove route object
            foreach (RouteObject routeObject in routes)
            {
                if (routeObject.RouteName == hitLight.NameRouteOfLight)
                {
                    DestroyRoute(routeObject);
                    break;
                }
            }
        } 
    }

    // return true if switches in route are free
    private bool CheckRouteBySwitches(Switch[] switches, string dir)
    {
        if (switches != null)
        {
            foreach (Switch sw in switches)
            {
                if ((dir == Constants.DIR_STR && !sw.IsSwitchStraight && sw.SwitchLockCount > 0) || (dir == Constants.DIR_TURN && sw.IsSwitchStraight && sw.SwitchLockCount > 0))
                {
                    return false;
                }
            }
        }
        return true;
    }

    // return true if the name is unique
    private bool CheckRouteForDuplicates(String _routeName)
    {
        if (routes != null)
        {
            foreach (RouteObject ro in routes)
            {
                if (_routeName == ro.RouteName )
                {
                    return false;
                }
            }
        }
        return true;
    }

    // return true if al is ok, if track circuits are free
    private bool CheckRootByPresence(TrackCircuit[] trackCircuits, TrafficLights [] trafficLights)
    {
        bool dangerPresence = true;
        TrackCircuit last = trackCircuits.Last();
     
        //check all except first and last track circuit
        for (int i = 0; i < trackCircuits.Length - 1; i++)
        {
            
            if (trackCircuits[i].IsCarPresence > 0)
            {
                
                dangerPresence = false;
            }
                
        }
        Debug.Log("Not presence path ="  + dangerPresence);
        Debug.Log("is Shunting: "  + IsShunting(trafficLights));
        //if it is a train route, all tracks must be free
        if (!IsShunting(trafficLights) && last.IsCarPresence > 0)
            dangerPresence = false;
        Debug.Log(" Not presence track =" + dangerPresence);
        return dangerPresence;
    }

    private bool CheckRouteUsed(RouteObject ro)
    {
        TrackCircuit[] trackCircuits = ro.TrackCircuits;
        TrackCircuit last = trackCircuits.Last();
        TrackCircuit control = trackCircuits[trackCircuits.Length - 2];
        bool wasRouteUsed = false;
        
        for (int i = 0; i < trackCircuits.Length - 1; i++)
        {
            
            if (trackCircuits[i].UseMode == Constants.TC_USED)
            {
                wasRouteUsed = true;
            }
            else
                wasRouteUsed = false;

        }       

        return wasRouteUsed;
        

    }

    public void RouteSwitchesUnlock(Switch[] arr)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                sw.SwitchLockCount -= 1;
            }
        }
    }

    public void RouteLightsManage(TrafficLights[] lights, bool isRoute)
    {
        TrafficLights startLight = lights[0];
        TrafficLights endLight = lights[1];
        
        if (isRoute)
        {
            startLight.tag = Constants.LIGHTS_IN_ROUTE;
            startLight.NameRouteOfLight = route.RouteName;
            if (IsShunting(lights))
            {
                startLight.SetLightColor(Constants.COLOR_WHITE);
                endLight.SetLightColor(Constants.COLOR_BLUE);
            }
                
        }
        else
        {
            if (String.IsNullOrEmpty(route.EndLight.NameRouteOfLight))
                route.EndLight.SetLightColor(Constants.COLOR_DEFAULT);
            startLight.SetLightColor(Constants.COLOR_DEFAULT);
            startLight.tag = Constants.LIGHTS_FREE;
            startLight.NameRouteOfLight = "";
            
        }
        

    }

    public void RouteDirection(Switch[] arr, string direction)
    {
       if(arr != null)
        {
            foreach (Switch sw in arr)
            {
                if (direction == Constants.DIR_STR)
                {
                    sw.DirectionStraight();                    
                }
                else
                {
                    sw.DirectionTurn();                   
                }
                sw.SwitchLockCount += 1;
            }
        }
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
    
    private bool IsShunting(TrafficLights[] trafficLights)
    {
        return trafficLights[0].Name != "CH" && trafficLights[0].Name != "N" && trafficLights[1].Name != "CH" && trafficLights[1].Name != "N";
    }

    public RouteObject GetRouteByName(string name)
    {
        foreach (RouteObject ro in Routes)
        {
            if (ro.RouteName == name)
                return ro;
        }
        return null;
    }

    internal List<RouteObject> Routes
    {
        get
        {
            return routes;
        }

        set
        {
            routes = value;
        }
    }

    public TrackCircuit[] ForwardPath
    {
        get
        {
            return forwardPath;
        }

        set
        {
            forwardPath = value;
        }
    }

    public IEnumerable<TrackCircuit> FullPath
    {
        get
        {
            return fullPath;
        }

        set
        {
            fullPath = value;
        }
    }

    public bool IsPathCheckingForward
    {
        get
        {
            return isPathCheckingForward;
        }

        set
        {
            isPathCheckingForward = value;
        }
    }

    public TrackCircuit OccupiedTrack
    {
        get
        {
            return occupiedTrack;
        }

        set
        {
            occupiedTrack = value;
        }
    }

}

