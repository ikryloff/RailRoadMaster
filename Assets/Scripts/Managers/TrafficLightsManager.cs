using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightsManager : Singleton<TrafficLightsManager>, IManageable {

    private bool isStart = true;
    private string startRoute;
    private string endRoute;
    private TrafficLight startLight;
    private TrafficLight endLight;
    [SerializeField]
    public TrafficLight[] trafficLights;
    private TrafficLight tempLight = null;
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
   

    public Dictionary<string, TrafficLight> TLDict { get; set; }


   

    public Button GetButtonByTLName(string name)
    {
        foreach (var btn in ListOfScriptedTLButtons)
        {
            if (btn.TrafficLight.Name == name)
                return btn.GetComponent<Button>();
        }
        return null;
    } 

    public void Init()
    {
        trafficLights = FindObjectsOfType<TrafficLight>();
        MakeTLDictionary();
    }

   

    public void ShowTrafficLightsButtons()
    {
        foreach (var btn in ListOfScriptedTLButtons)
        {
            btn.IsInteractable = true;
        }
        cancelButton.interactable = true;
    }

    private void ShowPossibleTrafficLightsButtons(TrafficLight _startLight)
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

    public void MakeTLDictionary()
    {
        TLDict = new Dictionary<string, TrafficLight>();
        foreach (TrafficLight tl in trafficLights)
        {
            TLDict.Add(tl.name, tl);

        }
        TLDict.Add("", null);
    }

    private void Awake()
    {                
        tempBtns = Resources.FindObjectsOfTypeAll<Button>();
        TrafficlightsButtons = new List<Button>();
       

        ListOfScriptedTLButtons = new List<TrafficLightBtnScript>();
        for (int i = 0; i < TrafficlightsButtons.Count; i++)
        {

            if (TrafficlightsButtons[i].GetComponent<TrafficLightBtnScript>() != null)
            {
                ListOfScriptedTLButtons.Add(TrafficlightsButtons[i].GetComponent<TrafficLightBtnScript>());
            }

        }

        

    }

    
       

    // Check possible Routes By Lights
    public bool IsPossibleLight (String[][] arr, TrafficLight first, TrafficLight second )
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

   

    void SetLightsNames(String start, String end = "")
    {
        lightText.text = start + " -> " + end;
    }
    
    public TrafficLight GetTrafficLightByName(string lightName)
    {
        foreach (TrafficLight tl in trafficLights)
        {
            if (tl.name.Equals(lightName))
            {                
                return tl;
            }            
        }
        return null;  
    }



    public TrafficLight StartLight
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

    public TrafficLight EndLight
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

    

    public TrafficLight GetEndByName(string _endName)
    {
        foreach (var e in trafficLights)
        {
            if (e.name == _endName)
                return e;
        }
        return null;
    }

    public void OnStart()
    {
        throw new NotImplementedException();
    }
}
