using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightBtnScript : MonoBehaviour {
    [SerializeField]
    private Button button;
    [SerializeField]
    private TrafficLight trafficLight;
    [SerializeField]
    private TrafficLightsManager trafficLightsManager;
    private bool isInteractable;

    public TrafficLight TrafficLight
    {
        get
        {
            return trafficLight;
        }

        set
        {
            trafficLight = value;
        }
    }

    public bool IsInteractable
    {
        set
        {
            GetComponent<Button>().interactable = value;            
        }
    }

    

    
}
