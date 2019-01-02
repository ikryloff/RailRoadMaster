#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2_4, sw6_8, sw10, sw12, sw14, sw16, sw1_3, sw5_17, sw7_9, sw11, sw13, sw15, sw18, sw20, sw19, sw21, sw22;

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
        tc14,
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
        tcsw14,
        tcsw22,
        tcsw16,
        tcsw10,
        tcsw11,
        tcsw12,
        tcsw13,        
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
    private TrackCircuit lastRouteTrackForward;
    private TrackCircuit lastRouteTrackBackward;
    private bool isPathCheckingForward;
    private Switch[] switches;
    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    IEnumerable<TrackCircuit> fullPath;
    public List<TrackCircuit> fullEnginePath;
    private bool isRoute;
    private string routeName;
    [SerializeField]
    private Text lightText;
    [SerializeField]
    private Button cancelRouteButton;
    SwitchManager switchManager;
    private TextBuilder textBuilder;
    private string messageText;
    public PathMaker pathMaker;

    private void Awake()
    {
        textBuilder = GameObject.FindObjectOfType<TextBuilder>();
        TrackCircuit[] tempArr = GameObject.FindObjectsOfType<TrackCircuit>();
        trackCircuits = new List<TrackCircuit>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            if (tempArr[i].tag == "Track")
                trackCircuits.Add(tempArr[i]);
        }
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
    }
    void Start()
    {
        routes = new List<RouteObject>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        Invoke("MakePathInBothDirections", 0.1f);

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

    

    public void MakePath(int _direction)
    {
                    
        fullEnginePath = new List<TrackCircuit>();
        startTrack = pathMaker.engine.Track;  
        
        print("new startPoint " + pathMaker.engine.Track);



        // print new full path
        string result = "new inside ";
        if (fullEnginePath != null)
        {
            
            foreach (var item in fullEnginePath)
            {

                result += " -> " + item.name;

            }
            print(result);

            /*OccupiedTrack = null;
            foreach (var track in fullEnginePath)
            {
                if (track.IsCarPresence > 0 && track != fullEnginePath.First())
                {
                    OccupiedTrack = track;
                    break;
                }
                if (OccupiedTrack == null)
                {
                    print("iM here");
                    OccupiedTrack = fullEnginePath.Last();
                }
                    

            }
            */

            if (_direction == 1)
            {
                foreach (var tr in fullEnginePath)
                {

                    if (tr.TrackLights[1] && tr.TrackLights[1].IsClosed)
                    {
                        LastRouteTrackForward = tr;
                        break;
                    }

                }

                if (LastRouteTrackForward)
                {
                    if (LastRouteTrackForward == tcsw14)
                    {
                        if (switch22.IsSwitchStraight)
                            LastRouteTrackForward = tc10_10;
                        else
                            LastRouteTrackForward = tcsw14;
                    }

                    if (LastRouteTrackForward == tc14)
                    {
                        if (switch22.IsSwitchStraight)
                            LastRouteTrackForward = tc14;
                        else
                            LastRouteTrackForward = tc10_10;
                    }

                    if (LastRouteTrackForward == tc12 || LastRouteTrackForward == tcsw19)
                    {
                        if (switch19.IsSwitchStraight)
                            LastRouteTrackForward = tc12_12;
                        else
                            LastRouteTrackForward = tc13;
                    }

                    if (LastRouteTrackForward == tc13)
                    {
                        if (switch19.IsSwitchStraight)
                            LastRouteTrackForward = tc13;
                        else
                            LastRouteTrackForward = tc12A;
                    }
                    if (LastRouteTrackForward == tc12_12)
                    {
                        if (switch19.IsSwitchStraight)
                            LastRouteTrackForward = tc12A;
                        else
                            LastRouteTrackForward = tc12_12;
                    }
                }
            }



            if (_direction == -1)
            {
                foreach (var tr in fullEnginePath)
                {

                    if (tr.TrackLights[0] != null && tr.TrackLights[0].IsClosed)
                    {
                        LastRouteTrackBackward = tr;
                        break;
                    }
                }

                if (LastRouteTrackBackward)
                {
                    if (LastRouteTrackBackward == tc10_10)
                    {
                        if (switch18.IsSwitchStraight && switch20.IsSwitchStraight)
                            LastRouteTrackBackward = tcsw22;
                        else
                            LastRouteTrackBackward = tc10_10;
                    }

                    if (LastRouteTrackBackward == tc9)
                    {
                        if (switch18.IsSwitchStraight)
                            LastRouteTrackBackward = tc9;
                        else
                            LastRouteTrackBackward = tcsw22;
                    }

                    if (LastRouteTrackBackward == tc11)
                    {
                        if (!switch20.IsSwitchStraight && switch18.IsSwitchStraight)
                            LastRouteTrackBackward = tcsw22;
                        else
                            LastRouteTrackBackward = tc11;
                    }

                    if (LastRouteTrackBackward == tc10)
                    {
                        if (switch22.IsSwitchStraight)
                            LastRouteTrackBackward = tcsw22;
                        else
                            LastRouteTrackBackward = tc14;
                    }

                    if (LastRouteTrackBackward == tc12A)
                    {
                        if (switch21.IsSwitchStraight)
                            LastRouteTrackBackward = tc12_12;
                        else
                            LastRouteTrackBackward = tc13;
                    }

                    if (LastRouteTrackBackward == tc12_12)
                    {
                        if (switch19.IsSwitchStraight)
                            LastRouteTrackBackward = tc12;
                        else
                            LastRouteTrackBackward = tc12_12;
                    }
                    if (LastRouteTrackBackward == tc13)
                    {
                        if (switch19.IsSwitchStraight)
                            LastRouteTrackBackward = tc13;
                        else
                            LastRouteTrackBackward = tc12;
                    }

                    if (LastRouteTrackBackward == tcsw22 || LastRouteTrackBackward == tcsw18 || LastRouteTrackBackward == tcsw20)
                    {
                        if (switch22.IsSwitchStraight)
                            LastRouteTrackBackward = tcsw22;
                        else
                            LastRouteTrackBackward = tc14;
                    }
                }
            }
        }
        else
        {
            
            OccupiedTrack = startTrack;
            if (_direction == 1)
                LastRouteTrackForward = startTrack;
            else if (_direction == -1)
                LastRouteTrackBackward = startTrack;
        }


        print("OccupiedTrack  " + OccupiedTrack);
        print("LastRouteTrackForward  " + LastRouteTrackForward);
        print("LastRouteTrackBackward  " + LastRouteTrackBackward);
        
    }



    

    public void MakePathInBothDirections()
    {
        MakePath(1);
        MakePath(-1);
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
        if (CheckRouteForDuplicates(startLight.Name))
        {
            TrafficLights[] tl = new TrafficLights[] { startLight, endLight };
            route = gameObject.AddComponent<RouteObject>();            
            route.TrafficLights = tl;
            routes.Add(route);
            route.RouteName = routeName;

            //Message
            messageText = string.Format("OK, prepare route from {0} at {1}", startLight.name, endLight.name);
            textBuilder.PrintMessage(messageText, "Yardmaster:");
            //
            RouteLightsManage(tl, true);
            RouteManage(route, routeName);
            cancelRouteButton.interactable = true;
        }
        else
        {
            //Message
            messageText = string.Format("We have another route from {0} ", startLight.name);
            textBuilder.PrintMessage(messageText, "Yardmaster:");
            ///
            print("Duplicate");
        }
            

        MakePathInBothDirections();
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
            ro.TrackCircuits = new TrackCircuit[] { tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tcsw22 };
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
            print(ro.RouteName);
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
                       // print("Make route direction " + ro.RouteName);
                        RouteDirection(ro.SwitchesStr, Constants.DIR_STR);
                        RouteDirection(ro.SwitchesTurn, Constants.DIR_TURN);
                        foreach (TrackCircuit tc in ro.TrackCircuits)
                        {
                            tc.UseMode = Constants.TC_WAIT;
                        }
                        //print("Route Locked " + ro.RouteName);
                        //Message
                        messageText = string.Format("Route {0} is ready. You can go as will", ro.RouteName);
                        textBuilder.PrintMessage(messageText, "Yardmaster:");
                        //
                    }
                    else
                    {
                        //print("Counter route");
                        DestroyRoute(ro, false);
                    }
                        
                }
                else
                {
                    //Message
                    messageText = string.Format("Can't do that, danger cross route");
                    textBuilder.PrintMessage(messageText, "Yardmaster:");
                    ///
                    DestroyRoute(ro, false);
                }   
            }
            else
            {
                print("Track presence");
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
        //print("Destroy");        
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
            //print("Unlock");
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
    private bool CheckRouteForDuplicates(string startLight)
    {
        if (routes != null)
        {
            foreach (RouteObject ro in routes)
            {
                if (startLight.Equals(ro.StartLight.Name))
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
       // print("Not presence path ="  + dangerPresence);
       // print("is Shunting: "  + IsShunting(trafficLights));
        //if it is a train route, all tracks must be free
        if (!IsShunting(trafficLights) && last.IsCarPresence > 0)
            dangerPresence = false;
        //print(" Not presence track =" + dangerPresence);
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

    public TrackCircuit LastRouteTrackForward
    {
        get
        {
            return lastRouteTrackForward;
        }

        set
        {
            lastRouteTrackForward = value;
        }
    }

    public TrackCircuit LastRouteTrackBackward
    {
        get
        {
            return lastRouteTrackBackward;
        }

        set
        {
            lastRouteTrackBackward = value;
        }
    }
}

