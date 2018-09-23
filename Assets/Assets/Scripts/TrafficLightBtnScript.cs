using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightBtnScript : MonoBehaviour {
    [SerializeField]
    private Button button;
    [SerializeField]
    private TrafficLights trafficLight;
    [SerializeField]
    private TrafficLightsManager trafficLightsManager;
    private bool isInteractable;

    public TrafficLights TrafficLight
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

    void Start () {
        button.onClick.AddListener(GetTrafficLight);
	}

    private void GetTrafficLight()
    {
        trafficLightsManager.SetLightsInRoute(TrafficLight);
    }
   
}
