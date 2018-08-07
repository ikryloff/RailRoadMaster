using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightBtnScript : MonoBehaviour {
    [SerializeField]
    private Button button;
    [SerializeField]
    private TrafficLights tl;
    [SerializeField]
    private TrafficLightsManager tlm;


	// Use this for initialization
	void Start () {
        button.onClick.AddListener(GetTrafficLight);
	}

    private void GetTrafficLight()
    {
        tlm.SetRouteLights(tl);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
