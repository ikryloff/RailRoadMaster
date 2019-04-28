using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommunicationPanelManager : Singleton<CommunicationPanelManager>
{
    [SerializeField]
    private GameObject communicationList;
    [SerializeField]
    private GameObject routeButtonsList;
    [SerializeField]
    private GameObject engineerList;
    [SerializeField]
    private GameObject cancelRouteList;
    [SerializeField]
    private GameObject cancelBtn;
    [SerializeField]
    private Button[] routeButtons;
    private bool isRouteRequestRun;
    private string resultRoute = "";
    private TrafficLightsManager tlm;
    private Route route;
    [SerializeField]
    private Engine engine;
    private List<string[]> routeParseList;
    private TrafficLight startLight;
    private TrafficLight endLight;
    [SerializeField]
    private List<Button> routeBtns;
    private string[] routes;
    private TextBuilder textBuilder;

    private void Awake()
    {
        textBuilder = GameObject.FindObjectOfType<TextBuilder>();
        tlm = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        route = GameObject.Find("Route").GetComponent<Route>();
        routeParseList = Constants.CONDUCTOR_ROUTE_ASK;
        routeBtns = new List<Button>();
        routes = new string[3];
        foreach (Transform btn in CancelRouteList.transform)
        {
            Button tBtn = btn.GetComponent<Button>();
            routeBtns.Add(tBtn);
            tBtn.gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        IsRouteRequestRun = false;
        CommunicationList.SetActive(true);
        RouteButtonsList.SetActive(false);
        EngineerList.SetActive(true);
        CancelRouteList.SetActive(false);
    }


    public void ShowCommunicationList()
    {
        CommunicationList.SetActive(true);
        IsRouteRequestRun = false;
        ResultRoute = "";
        RouteButtonsList.SetActive(false);
        CancelRouteList.SetActive(false);
    }
    public void ShowRouteList()
    {
        CancelRouteList.SetActive(false);
        IsRouteRequestRun = true;
        RouteButtonsList.SetActive(true);
        CommunicationList.SetActive(false);

    }

    public void ShowCancelRouteList()
    {
        CancelRouteList.SetActive(true);
        RouteButtonsList.SetActive(false);
        CommunicationList.SetActive(false);

        foreach (Button btn in routeBtns)
        {
            btn.gameObject.SetActive(false);
        }


        
    }

    


    public void CancelRouteByButton(Button button)
    {
        
    }



    public void GetStringRoute(Button placeBtn)
    {
        if (IsRouteRequestRun)
        {
            ResultRoute += placeBtn.name;
            placeBtn.interactable = false;
            IsRouteRequestRun = false;
            LeavePossibleRoutesButtons(placeBtn);
        }
        else
        {
            IsRouteRequestRun = true;
            ResultRoute += placeBtn.name;
            MakeRouteForConductor(ResultRoute);
            foreach (Button btn in RouteButtons)
            {
                btn.interactable = true;
            }
            ShowCommunicationList();
        }
    }

    private void MakeRouteForConductor(string routeAsk)
    {
     
        foreach (string[] light in routeParseList)
        {
            if (light[0].Equals(routeAsk))
            {
                startLight = tlm.GetTrafficLightByName(light[1]);
                endLight = tlm.GetTrafficLightByName(light[2]);
                //Message
                if (startLight && endLight)
                {
                    string messageText = string.Format("YM, need a route from {0} to {1}", routeAsk.Substring(0, 1), routeAsk.Substring(1, 1));
                    textBuilder.PrintMessage(messageText, "Conductor:");
                }
                ///
                tlm.MakeRouteIfPossible(startLight, endLight);
            }
        }

        
    }

    private void LeavePossibleRoutesButtons(Button button)
    {
        if (button.name == "6")
            FindPossibleRoutesButtons(new string[] { "6", "7", "T", "8" });
        else if (button.name == "S")
            FindPossibleRoutesButtons(new string[] { "S", "8", "T", "I", "2", "3", "4", "5", "7" });
        else if (button.name == "T")
            FindPossibleRoutesButtons(new string[] { "6", "7", "S", "T" });
        else if (button.name == "7")
            FindPossibleRoutesButtons(new string[] { "6", "S", "8", "T", "7" });
        else if (button.name == "8")
            FindPossibleRoutesButtons(new string[] { "S", "7", "I", "2", "3", "4", "5", "6", "8" });
        else
            FindPossibleRoutesButtons(new string[] { "S", "I", "2", "3", "4", "5", "8" });
    }


    private void FindPossibleRoutesButtons(string[] btnFalseArr)
    {
        foreach (Button btn in RouteButtons)
        {
            btn.interactable = true;
        }

        foreach (string fButton in btnFalseArr)
        {
            foreach (Button btn in RouteButtons)
            {
                if (btn.name == fButton)
                {
                    btn.interactable = false;
                }
            }
        }

    }



    public GameObject CommunicationList
    {
        get
        {
            return communicationList;
        }

        set
        {
            communicationList = value;
        }
    }

    public GameObject RouteButtonsList
    {
        get
        {
            return routeButtonsList;
        }

        set
        {
            routeButtonsList = value;
        }
    }

    public GameObject CancelBtn { get; set; }

    public bool IsRouteRequestRun { get; set; }

    public string ResultRoute
    {
        get
        {
            return resultRoute;
        }

        set
        {
            resultRoute = value;
        }
    }

    public GameObject EngineerList
    {
        get
        {
            return engineerList;
        }

        set
        {
            engineerList = value;
        }
    }

    public Button[] RouteButtons
    {
        get
        {
            return routeButtons;
        }

        set
        {
            routeButtons = value;
        }
    }

    public GameObject CancelRouteList
    {
        get
        {
            return cancelRouteList;
        }

        set
        {
            cancelRouteList = value;
        }
    }
}
