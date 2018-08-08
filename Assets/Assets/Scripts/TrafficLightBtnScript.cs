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


    // Use this for initialization
    void Start () {
        button.onClick.AddListener(GetTrafficLight);
	}

    private void GetTrafficLight()
    {
        trafficLightsManager.SetLightsInRoute(TrafficLight);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
