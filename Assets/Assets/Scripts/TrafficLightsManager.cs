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

    string[,] possibleLights = new string[,]
   {

        {"NI", "M2", "CH","","","",""},
        {"N3", "M2", "CH","","","",""},
        {"N4", "M2", "CH","","","",""},
        {"N5", "M2", "CH","","","",""},
        {"N6", "M2", "CH","","","",""},
        {"M3", "M2", "", "", "","", ""},
        {"M2", "N3", "NI", "N4", "N5","N6","M3"},
        {"CH", "CH3", "CHI", "CH4", "CH5","CH6", ""}    

   };


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
                    if (IsPossibleLight(possibleLights, startLight.Name, endLight.Name))
                    {
                        endLight.tag = LIGHTS_IN_ROUTE;
                        route.MakeRoute(startLight, endLight);
                                                
                    }
                    else
                    {
                        startLight.tag = LIGHTS_FREE;
                        Debug.Log("Wrong light");
                    }
                    isStart = true;
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
                isStart = true;
            }

        }
    }
    private bool IsPossibleLight (String[,] arr, string start, string end  )
    {
        int n = arr.GetLength(0);
        int m = arr.GetLength(1);        
        for (int i = 0; i < n; i++)
        {
            if (start == arr[i, 0])
            {
                for (int j = 1; j < m; j++)
                {
                    if (end == arr[i, j])
                        return true;
                }                
            }
            
        }
        return false;
    }
   
}
