using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelRouteBtn : MonoBehaviour {

    [SerializeField]
    private Button button;    
    [SerializeField]
    private TrafficLightsManager trafficLightsManager;
    public Route route;


    // Use this for initialization

    private void Awake()
    {
       
    }

    void Start()
    {
        button.onClick.AddListener(CancelRoute);        
    }

    private void CancelRoute()
    {
        
        if (trafficLightsManager.CancelRouteIsOn)
        {
            trafficLightsManager.CancelRouteIsOn = false;
            trafficLightsManager.ShowTrafficLightsButtons();
        }
            
        else
        {
            trafficLightsManager.CancelRouteIsOn = true;

            if (route.Routes != null)
            {
                foreach (var btn in trafficLightsManager.ListOfScriptedTLButtons)
                {
                    btn.IsInteractable = false;
                }

                foreach (var route in route.Routes)
                {
                    if (!route.StartLight.IsClosed)
                        trafficLightsManager.GetButtonByTLName(route.StartLight.Name).interactable = true;


                }
            }
           foreach (var tl in trafficLightsManager.trafficLights)
           {
                if (tl.tag == Constants.LIGHTS_IN_ROUTE)
                    trafficLightsManager.GetButtonByTLName(tl.Name).interactable = true;
           }
              
        }
            
    }
}
