using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelRouteBtn : MonoBehaviour {

    [SerializeField]
    private Button button;    
    [SerializeField]
    private TrafficLightsManager trafficLightsManager;


    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(CancelRoute);
    }

    private void CancelRoute()
    {
        trafficLightsManager.CancelRouteIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
