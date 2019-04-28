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
        
        
            
    }
}
