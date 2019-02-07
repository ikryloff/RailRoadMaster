﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightsManager : Singleton<TrafficLightsManager> {

    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    private TrafficLights startLight;
    private TrafficLights endLight;
    [SerializeField]
    public TrafficLights[] trafficLights;
    private TrafficLights tempLight = null;
    [SerializeField]
    private Route route;   
    [SerializeField]
    private Text lightText;
    [SerializeField]
    private RemoteControlScript rcs;
    [SerializeField]
    private bool cancelRouteIsOn;
    [SerializeField]
    private List<Button> trafficlightsButtons;    
    private Button[] tempBtns;
    [SerializeField]
    private List<TrafficLightBtnScript> listOfScriptedTLButtons;
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Switch switch19, switch21, switch18, switch20, switch22, switch10, switch12, switch14;
    [SerializeField]
    private TrafficLights end14_22SW, end22_14SW, end9_18, end10_20, end11_20, end12CH, end12N, end13CH, end13N, m3, endM3, end10_18;
    Switch[] switches;
    public TrafficLights[] ends;




    public void SetRouteByLights(TrafficLights firstLight, TrafficLights secondLight)
    {
        SetLightsInRoute(firstLight);
        SetLightsInRoute(secondLight);
    }

    public Button GetButtonByTLName(string name)
    {
        foreach (var btn in ListOfScriptedTLButtons)
        {
            if (btn.TrafficLight.Name == name)
                return btn.GetComponent<Button>();
        }
        return null;
    } 


    public void SetLightsInRoute(TrafficLights light)
    {
        if (rcs.IsRemoteControllerOn)
        {
            // Canceling Route
            if (CancelRouteIsOn)
            {
                if (light != null && light.tag == Constants.LIGHTS_IN_ROUTE)
                {
                    route.DestroyRouteByLight(light);
                    IsStart = true;
                    lightText.text = "None";
                    ShowTrafficLightsButtons();
                    
                }
                lightText.text = "Done";
                cancelRouteIsOn = false;   
            }
            else
            {
                //Taking lights in route
                if (light.tag == Constants.LIGHTS_FREE)
                {
                    if (IsStart)
                    {
                        startLight = light;
                        SetLightsNames(startLight.Name);
                        startLight.tag = Constants.LIGHTS_IN_ROUTE;
                        IsStart = false;
                        ShowPossibleTrafficLightsButtons(startLight);                        
                    }
                    else
                    {
                        endLight = light;
                        MakeRouteIfPossible(startLight, endLight);
                        IsStart = true;
                        ShowTrafficLightsButtons();
                    }
                }
                else if (light.tag == Constants.LIGHTS_IN_ROUTE && !IsStart)
                {
                    startLight.tag = Constants.LIGHTS_FREE;
                    IsStart = true;
                    lightText.text = "Wrong light in route";
                }
            } 
        }  
    }

    public void ShowTrafficLightsButtons()
    {
        foreach (var btn in ListOfScriptedTLButtons)
        {
            btn.IsInteractable = true;
        }
        cancelButton.interactable = true;
    }

    private void ShowPossibleTrafficLightsButtons(TrafficLights _startLight)
    {
        foreach (var btn in ListOfScriptedTLButtons)
        {
            btn.IsInteractable = false;
        }
        foreach (var _endLight in trafficLights)
        {
            if(IsPossibleLight(Constants.POSSIBLE_LIGHTS, _startLight, _endLight))
            {
                GetButtonByTLName(_endLight.Name).interactable = true;
            }
        }



    }

    private void Awake()
    {
        trafficLights= FindObjectsOfType<TrafficLights>();

        tempBtns = Resources.FindObjectsOfTypeAll<Button>();
        TrafficlightsButtons = new List<Button>();
        for (int i = 0; i < tempBtns.Length; i++)
        {
            if (tempBtns[i].tag == "TrafficLightsButton")
            {                
                TrafficlightsButtons.Add(tempBtns[i]);
            }

        }

        ListOfScriptedTLButtons = new List<TrafficLightBtnScript>();
        for (int i = 0; i < TrafficlightsButtons.Count; i++)
        {

            if (TrafficlightsButtons[i].GetComponent<TrafficLightBtnScript>() != null)
            {
                ListOfScriptedTLButtons.Add(TrafficlightsButtons[i].GetComponent<TrafficLightBtnScript>());
            }

        }

        switches = FindObjectsOfType<Switch>();
        // Cashing hand switches

        switch18 = GetSwitchByName("Switch_18");
        switch19 = GetSwitchByName("Switch_19");
        switch20 = GetSwitchByName("Switch_20");
        switch21 = GetSwitchByName("Switch_21");
        switch22 = GetSwitchByName("Switch_22");
        switch10 = GetSwitchByName("Switch_10");
        switch12 = GetSwitchByName("Switch_12");
        switch14 = GetSwitchByName("Switch_14");

        end14_22SW = GetEndByName("End14_22SW");
        end22_14SW = GetEndByName("End22_14SW");
        end9_18 = GetEndByName("End9_18");
        end10_20 = GetEndByName("End10_20");
        end11_20 = GetEndByName("End11_20");
        end12CH = GetEndByName("End12CH");
        end12N = GetEndByName("End12N");
        end13CH = GetEndByName("End13CH");
        end13N = GetEndByName("End13N");
        m3 = GetEndByName("M3");
        endM3 = GetEndByName("EndM3");
        end10_18 = GetEndByName("End10_18");

    }

    private void Start()
    {
        lightText.text = "None";

        Invoke("CheckHandSwitches", 0.5f);        
    }
       

    // Check possible Routes By Lights
    public bool IsPossibleLight (String[][] arr, TrafficLights first, TrafficLights second )
    {
        string start = first.Name;
        string end = second.Name;
             
        for (int i = 0; i < arr.Length; i++)
        {
            if (start == arr[i][0])
            {
                for (int j = 1; j < arr[i].Length; j++)
                {
                    if (end == arr[i][j])
                        return true;
                }                
            }
            
        }
        return false;
    }

    public void MakeRouteIfPossible(TrafficLights startLight, TrafficLights endLight)
    {
        if (IsPossibleLight(Constants.POSSIBLE_LIGHTS, startLight, endLight))
        {
            route.MakeRoute(startLight, endLight);
            SetLightsNames(startLight.Name, endLight.Name);
        }
        else
        {
            startLight.tag = Constants.LIGHTS_FREE;
            lightText.text = "Wrong light";
        }
    }

    void SetLightsNames(String start, String end = "")
    {
        lightText.text = start + " -> " + end;
    }
    
    public TrafficLights GetTrafficLightByName(string lightName)
    {
        foreach (TrafficLights tl in trafficLights)
        {
            if (tl.name ==lightName)
            {                
                return tl;
            }            
        }
        return null;  
    }



    public TrafficLights StartLight
    {
        get
        {
            return startLight;
        }

        set
        {
            startLight = value;
        }
    }

    public TrafficLights EndLight
    {
        get
        {
            return endLight;
        }

        set
        {
            endLight = value;
        }
    }

    public bool CancelRouteIsOn
    {
        get
        {
            return cancelRouteIsOn;
        }

        set
        {
            cancelRouteIsOn = value;
            if(value)
                lightText.text = "Cancel..";
            else
                lightText.text = "not cancel..";

        }
    }

    public List<Button> TrafficlightsButtons
    {
        get
        {
            return trafficlightsButtons;
        }

        set
        {
            trafficlightsButtons = value;
        }
    }

    public List<TrafficLightBtnScript> ListOfScriptedTLButtons
    {
        get
        {
            return listOfScriptedTLButtons;
        }

        set
        {
            listOfScriptedTLButtons = value;
        }
    }

    public bool IsStart
    {
        get
        {
            return isStart;
        }

        set
        {
            isStart = value;
        }
    }

    public void CheckHandSwitches()
    {

        if (switch22.IsSwitchStraight)
        {
            end14_22SW.IsClosed = false;
            end22_14SW.IsClosed = true;
            endM3.IsClosed = false;
        }
        else
        {
            end14_22SW.IsClosed = true;
            end22_14SW.IsClosed = false;
            endM3.IsClosed = m3.IsClosed;
        }

        if (switch18.IsSwitchStraight)
        {            
            end9_18.IsClosed = true;
            end10_18.IsClosed = false;  
           
        }
        else
        {
            end9_18.IsClosed = false;
            end10_18.IsClosed = true;
        }

        if (switch20.IsSwitchStraight)
        {
            end10_20.IsClosed = false;
            end11_20.IsClosed = true;
        }
        else
        {
            end10_20.IsClosed = true;
            end11_20.IsClosed = false;
        }

        if (switch21.IsSwitchStraight)
        {
            end12CH.IsClosed = false;
            end13CH.IsClosed = true;
        }
        else
        {
            end12CH.IsClosed = true;
            end13CH.IsClosed = false;
        }

        if (switch19.IsSwitchStraight)
        {
            end12N.IsClosed = false;
            end13N.IsClosed = true;
        }
        else
        {
            end12N.IsClosed = true;
            end13N.IsClosed = false;
        }


    }


    public Switch GetSwitchByName(string _switchName)
    {
        foreach (var sw in switches)
        {
            if (sw.name == _switchName)
                return sw;
        }
        return null;
    }

    public TrafficLights GetEndByName(string _endName)
    {
        foreach (var e in trafficLights)
        {
            if (e.name == _endName)
                return e;
        }
        return null;
    }


}
