using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : Singleton<TrafficLightsManager> {
    
    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    [SerializeField]
    private Route route;
    // Use this for initialization
    void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "TrafficLight")
            {
                if(isStart)
                {
                    startRoute = hit.collider.GetComponent<TrafficLights>().Name;
                    hit.collider.tag = "TrafficLightInRoute";
                    Debug.Log(startRoute);
                    isStart = false;
                }
                else
                {
                    endRoute = hit.collider.GetComponent<TrafficLights>().Name;
                    hit.collider.tag = "TrafficLightInRoute";
                    Debug.Log(endRoute);
                    if(endRoute != startRoute)
                    {
                        route.MakeRoute(startRoute, endRoute);
                        isStart = true;                        
                    }                    
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "TrafficLightInRoute")
            {
                if (isStart)
                {
                    TrafficLights light = hit.collider.GetComponent<TrafficLights>();
                    startRoute = light.Name;                    
                    light.SetLightColor(0);
                    Debug.Log(startRoute);
                    hit.collider.tag = "TrafficLight";
                    isStart = false;
                }
                else
                {
                    endRoute = hit.collider.GetComponent<TrafficLights>().Name;
                    hit.collider.tag = "TrafficLight";
                    Debug.Log(endRoute);
                    if (endRoute != startRoute)
                    {
                        route.DestroyRoute(startRoute, endRoute);
                        isStart = true;
                    }
                }
                
            }

        }
    }
   
}
