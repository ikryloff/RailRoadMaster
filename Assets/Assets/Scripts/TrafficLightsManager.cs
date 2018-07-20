using System;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightsManager : Singleton<TrafficLightsManager> {

    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    private TrafficLights startLight;
    private TrafficLights endLight;
    [SerializeField]
    private Route route;   
    [SerializeField]
    private Text lightText;


    private string[,] possibleLights = new string[,]
   {
       //Even part
        {"NI", "M2", "CH","","","",""},
        {"N2", "M2", "CH","","","",""},
        {"N3", "M2", "CH","","","",""},
        {"N4", "M2", "CH","","","",""},
        {"N5", "M2", "CH","","","",""},
        {"M3", "M2", "", "", "","", ""},
        {"M2", "N2", "NI", "N3", "N4","N5","M3"},
        {"CH", "CH2", "CHI", "CH3", "CH4","CH5", ""},
        //Odd part
        {"CHI", "M1", "N","M5","","",""},
        {"CH2", "M1", "N","M5","","",""},
        {"CH3", "M1", "N","M5","","",""},
        {"CH4", "M1", "N","M5","","",""},
        {"CH5", "M1", "N","M5","","",""},        
        {"M1", "CH2", "CHI", "CH3", "CH4","CH5", ""},
        {"M5", "CH2", "CHI", "CH3", "CH4","CH5", "M4"},
        {"M4", "M5", "", "", "","",""},
        {"N", "N2", "NI", "N3", "N4","N5", ""},

   };

    private void Start()
    {
        lightText.text = "None";
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            // if you hit a free traffic light
            if (hit.collider != null)
            { 
                if (hit.collider.tag == Constants.LIGHTS_FREE)
                {
                    // its your first light
                    if (isStart)
                    {
                        startLight = hit.collider.GetComponent<TrafficLights>();
                        startLight.tag = Constants.LIGHTS_IN_ROUTE;
                        SetLightsNames(startLight.Name);
                        isStart = false;
                    }
                    //its your second light
                    else
                    {
                        endLight = hit.collider.GetComponent<TrafficLights>();
                        if (IsPossibleLight(possibleLights, startLight.Name, endLight.Name))
                        {
                            route.MakeRoute(startLight, endLight);
                            SetLightsNames(startLight.Name, endLight.Name);
                        }
                        else
                        {
                            startLight.tag = Constants.LIGHTS_FREE;
                            lightText.text = "Wrong light";
                        }
                        isStart = true;
                    }
                    Debug.Log(hit.collider.name);
                }
                else if (hit.collider.tag == Constants.LIGHTS_IN_ROUTE && !isStart)
                {
                    startLight.tag = Constants.LIGHTS_FREE;
                    isStart = true;
                    lightText.text = "Wrong light in route";
                }
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == Constants.LIGHTS_IN_ROUTE)
            {
                startLight = hit.collider.GetComponent<TrafficLights>();                
                route.DestroyRouteByLight(startLight);
                isStart = true;
                lightText.text = "None";
            }

        }
    }

    // Check possible Routes By Lights
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

    void SetLightsNames(String start, String end = "")
    {
        lightText.text = start + " -> " + end;
    }
   
}
