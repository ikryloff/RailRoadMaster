#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2_4, sw6_8, sw10, sw12, sw14, sw16, sw1_3, sw5_17, sw7_9, sw11, sw13, sw15;

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
        tc10,
        tc12,
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
        tcsw15;    

    List<RouteObject> routes;
    private RouteObject route;
   
    private bool isRoute;
    private string routeName;
    [SerializeField]
    private Text lightText;

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

    void Start()
    {
        routes = new List<RouteObject>();            
    }

    private void FixedUpdate()
    {
        if(routes != null)
        {
            for (int i = 0; i < routes.Count; i++)
            {
                if (routes[i].TrackCircuits[1].IsCarPresence > 0)
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
        }
        else Debug.Log("Duplicate");
        
    }      

    private void RouteManage(RouteObject ro,  string routeName)
    {
        // Routes NI done
        if (ro.RouteName == "NIM2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tcI_16_15, tcsw16, tcsw_6_8top, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }
        else if (ro.RouteName == "NICH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
            ro.TrackCircuits = new TrackCircuit[] { tcI_16_15, tcsw16, tcsw_6_8top, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        
        // Routes N2 done
        else if (ro.RouteName == "N2CH")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc2, tcsw16, tcsw_6_8top, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N2M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tc2, tcsw16, tcsw_6_8top, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }
       
        // Routes N3 done
        else if (ro.RouteName == "N3CH")
        {
            ro.SwitchesStr = new Switch[] { sw10 , sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc3, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N3M2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tc3, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes N4 done
        else if (ro.RouteName == "N4CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc4, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N4M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc4, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes N5 done
        else if (ro.RouteName == "N5CH")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc5, tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tcsw_2_4top, tcI_CH_2, tcI_CH };
        }
        else if (ro.RouteName == "N5M2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc5, tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes M3 done
        else if (ro.RouteName == "M3M2")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc10, tcsw14, tcsw12, tcsw10, tcsw_6_8bot, tcsw_2_4bot, tc6 };
        }

        // Routes CH done
        else if (ro.RouteName == "CHCH2")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcI_CH_2, tcsw_2_4top, tcsw_6_8top, tcsw16, tc2 };
        }   
        else if (ro.RouteName == "CHCHI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw6_8, sw16 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcI_CH_2, tcsw_2_4top, tcsw_6_8top, tcsw16, tcI_16_15 };
        }
        else if (ro.RouteName == "CHCH3")
        {
            ro.SwitchesStr = new Switch[] { sw10, sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tc3};
        }
        else if (ro.RouteName == "CHCH4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tc4 };
        }
        else if (ro.RouteName == "CHCH5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10, sw2_4 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_CH, tcI_CH_2, tcsw_2_4top, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc5 };
        }

        // Routes M2 done
        else if (ro.RouteName == "M2NI")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw16 };
            ro.SwitchesTurn = new Switch[] { sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw_6_8top, tcsw16, tcI_16_15 };
        }
        else if (ro.RouteName == "M2N2")
        {
            ro.SwitchesStr = new Switch[] { sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw16, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw_6_8top, tcsw16, tc2 };
        }
        else if (ro.RouteName == "M2N3")
        {
            ro.SwitchesStr = new Switch[] { sw2_4, sw10, sw6_8 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tc3 };
        }
        else if (ro.RouteName == "M2N4")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw12, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tc4 };
        }
        else if (ro.RouteName == "M2N5")
        {
            ro.SwitchesStr = new Switch[] { sw6_8, sw2_4, sw12 };
            ro.SwitchesTurn = new Switch[] { sw14, sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc4 };
        }       
        else if (ro.RouteName == "M2M3")
        {
            ro.SwitchesStr = new Switch[] { sw14, sw6_8, sw12, sw2_4 };
            ro.SwitchesTurn = new Switch[] { sw10 };
            ro.TrackCircuits = new TrackCircuit[] { tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw12, tcsw14, tc10 };
        }

        // Routes CHI done
        else if (ro.RouteName == "CHIM1")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] { tcI_16_15, tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CHIM5")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tcI_16_15, tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CHIN")
        {
            ro.SwitchesStr = new Switch[] { sw15, sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_GREEN);
            ro.TrackCircuits = new TrackCircuit[] { tcI_16_15, tcsw15, tcsw_7_9top, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH2 done
        else if(ro.RouteName == "CH2M1")
        {
            ro.TrackCircuits = new TrackCircuit[] { tc2, tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3 };
            ro.SwitchesTurn = new Switch[] { sw15, sw7_9 };
            Debug.Log(ro.RouteName);
        }
        else if (ro.RouteName == "CH2M5")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.TrackCircuits = new TrackCircuit[] { tc2, tcsw15, tcsw_7_9top, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH2N")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc2, tcsw15, tcsw_7_9top, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH3 done
        else if (ro.RouteName == "CH3M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw11, sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] { tc3, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH3M5")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17};
            ro.TrackCircuits = new TrackCircuit[] { tc3, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH3N")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9, sw5_17};
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc3, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH4 done
        else if (ro.RouteName == "CH4M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tc4, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH4M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17};
            ro.TrackCircuits = new TrackCircuit[] { tc4, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH4N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17 };
            ro.SwitchesTurn = new Switch[] { sw11, sw13, sw1_3};
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc4, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes CH5 done
        else if (ro.RouteName == "CH5M1")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw1_3, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tc5, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tc7 };
        }
        else if (ro.RouteName == "CH5M5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc5, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17bot, tc12 };
        }
        else if (ro.RouteName == "CH5N")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw5_17, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW_TOP_FLASH);
            ro.TrackCircuits = new TrackCircuit[] { tc5, tcsw13, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_1_3top, tcI_1_N, tcI_N };
        }

        // Routes M4 done
        else if (ro.RouteName == "M4M5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc8, tcsw_5_17bot, tc12 };
        }

        // Routes M5 done
        else if (ro.RouteName == "M5M4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tc8 };
        }
        else if (ro.RouteName == "M5CH2")
        {
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17, sw15 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "M5CHI")
        {
            ro.SwitchesStr = new Switch[] { sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tcI_16_15 };
        }
        else if (ro.RouteName == "M5CH3")
        {
            ro.SwitchesStr = new Switch[] { sw11, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "M5CH4")
        {
            ro.SwitchesStr = new Switch[] { sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "M5CH5")
        {
            ro.SwitchesStr = new Switch[] { sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11, sw5_17 };
            ro.TrackCircuits = new TrackCircuit[] { tc12, tcsw_5_17bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc5 };
        }

        // Routes M1 done
       
        else if (ro.RouteName == "M1CH2")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17 };
            ro.SwitchesTurn = new Switch[] { sw7_9, sw15 };            
            ro.TrackCircuits = new TrackCircuit[] { tc7, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "M1CHI")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw15 };
            ro.SwitchesTurn = new Switch[] { sw7_9 };
            ro.TrackCircuits = new TrackCircuit[] { tc7, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw_7_9top, tcsw15, tcI_16_15 };
        }
        else if (ro.RouteName == "M1CH3")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9, sw11 };            
            ro.TrackCircuits = new TrackCircuit[] { tc7, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "M1CH4")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw13, sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tc7, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "M1CH5")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw5_17, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw11 };
            ro.TrackCircuits = new TrackCircuit[] { tc7, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }

        // Routes N done
        else if (ro.RouteName == "NNI")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_N, tcI_1_N, tcsw_1_3top, tcsw_7_9top, tcsw15, tcI_16_15 };            
        }
        else if (ro.RouteName == "NN2")
        {
            ro.SwitchesStr = new Switch[] { sw1_3, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw15 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_N, tcI_1_N, tcsw_1_3top, tcsw_7_9top, tcsw15, tc2 };
        }
        else if (ro.RouteName == "NN3")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw11 };
            ro.SwitchesTurn = new Switch[] { sw1_3 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_N, tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tc3 };
        }
        else if (ro.RouteName == "NN4")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11, sw13 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_N, tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        else if (ro.RouteName == "NN5")
        {
            ro.SwitchesStr = new Switch[] { sw5_17, sw7_9, sw13 };
            ro.SwitchesTurn = new Switch[] { sw1_3, sw11 };
            ro.StartLight.SetLightColor(Constants.COLOR_YELLOW);
            ro.TrackCircuits = new TrackCircuit[] { tcI_N, tcI_1_N, tcsw_1_3top, tcsw_1_3bot, tcsw_5_17top, tcsw_7_9bot, tcsw11, tcsw13, tc4 };
        }
        ///////
        else DestroyRoute(ro);

        Debug.Log(ro.TrackCircuits.Last().WasUsed);

        if (ro)
        {                        
            if (CheckRootByPresence(ro.TrackCircuits, ro.TrafficLights))
            {                
                if (CheckRouteBySwitches(ro.SwitchesStr, Constants.DIR_STR) && CheckRouteBySwitches(ro.SwitchesTurn, Constants.DIR_TURN))
                {
                    // if the track of reception of train is NOT used in other route
                    if((!IsShunting(ro.TrafficLights) && ro.TrackCircuits.Last().WasUsed != Constants.TC_WAIT) || IsShunting(ro.TrafficLights))
                    {
                        Debug.Log("Make route direction " + ro.RouteName);
                        RouteDirection(ro.SwitchesStr, Constants.DIR_STR);
                        RouteDirection(ro.SwitchesTurn, Constants.DIR_TURN);
                        foreach (TrackCircuit tc in ro.TrackCircuits)
                        {
                            tc.WasUsed = Constants.TC_WAIT;
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
                tc.WasUsed = Constants.TC_DEFAULT;
            }
            Debug.Log("Unlock");
        }        
        routes.Remove(ro);
        Destroy(ro);
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
        for (int i = 1; i < trackCircuits.Length - 1; i++)
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
        
        for (int i = 1; i < trackCircuits.Length - 1; i++)
        {
            
            if (trackCircuits[i].WasUsed == Constants.TC_USED)
            {
                wasRouteUsed = true;
            }
            else
                wasRouteUsed = false;

        }
        if (!IsShunting(ro.TrafficLights))
        {
            if (trackCircuits[0].WasUsed == Constants.TC_USED && control.WasUsed == Constants.TC_USED && last.WasUsed == Constants.TC_OVER)
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
                    sw.directionStraight();                    
                }
                else
                {
                    sw.directionTurn();                   
                }
                sw.SwitchLockCount += 1;
            }
        }
    }
    
    private bool IsShunting(TrafficLights[] trafficLights)
    {
        return trafficLights[0].Name != "CH" && trafficLights[0].Name != "N" && trafficLights[1].Name != "CH" && trafficLights[1].Name != "N";
    }
}

