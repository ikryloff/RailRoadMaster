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
                    startLight = hit.collider.GetComponent<TrafficLights>();
                    hit.collider.tag = "TrafficLightInRoute";
                    isStart = false;
                }
                else
                {
                    endLight = hit.collider.GetComponent<TrafficLights>();
                    hit.collider.tag = "TrafficLightInRoute";
                    if (endLight != startLight)
                    {
                        route.MakeRoute(startLight, endLight);
                        isStart = true;                        
                    }                    
                }
            }
            Debug.Log(hit.collider.name);
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "TrafficLightInRoute")
            {
                startLight = hit.collider.GetComponent<TrafficLights>(); 
                route.DestroyRoute(startLight);
            }

        }
    }
   
}
