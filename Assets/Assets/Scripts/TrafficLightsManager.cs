using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : Singleton<TrafficLightsManager> {
    
    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    private TrafficLights startLight;
    private TrafficLights endLight;
    [SerializeField]
    private Route route;
    const string LIGHTS_FREE = "TrafficLight";
    const string LIGHTS_IN_ROUTE = "TrafficLightInRoute";


    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

            if (hit.collider != null && hit.collider.tag == LIGHTS_FREE)
            {
                if(isStart)
                {
                    startLight = hit.collider.GetComponent<TrafficLights>();
                    startLight.tag = LIGHTS_IN_ROUTE;
                    isStart = false;
                }
                else
                {
                    endLight = hit.collider.GetComponent<TrafficLights>();
                    endLight.tag = LIGHTS_IN_ROUTE;
                    if (endLight != startLight)
                    {
                        route.MakeRoute(startLight, endLight);
                        isStart = true;                        
                    }                    
                }
                Debug.Log(hit.collider.name);
            }
            else if (hit.collider != null && hit.collider.tag == LIGHTS_IN_ROUTE && !isStart)
            {
                startLight.tag = LIGHTS_FREE;
                isStart = true;
                Debug.Log("Locked in route");
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == LIGHTS_IN_ROUTE)
            {
                startLight = hit.collider.GetComponent<TrafficLights>(); 
                route.DestroyRoute(startLight);
            }

        }
    }
   
}
