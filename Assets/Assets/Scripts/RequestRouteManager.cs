using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestRouteManager : Singleton<RequestRouteManager> {
    [SerializeField]
    private GameObject communicationList;
    [SerializeField]
    private GameObject routeButtonsList;
    [SerializeField]
    private GameObject engineerList;    
    [SerializeField]
    private GameObject cancelBtn;
    [SerializeField]
    private Button [] routeButtons;
    private bool isRouteRequestRun;
    private string resultRoute = "";

    
       

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

    public GameObject CancelBtn
    {
        get
        {
            return cancelBtn;
        }

        set
        {
            cancelBtn = value;
        }
    }

    public bool IsRouteRequestRun
    {
        get
        {
            return isRouteRequestRun;
        }

        set
        {
            isRouteRequestRun = value;
        }
    }

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

    public void ShowCommunicationList()
    {
        CommunicationList.SetActive(true);
        IsRouteRequestRun = false;
        ResultRoute = "";
        RouteButtonsList.SetActive(false);
        EngineerList.SetActive(false);
    }
    public void ShowRouteList()
    {
        IsRouteRequestRun = true;
        RouteButtonsList.SetActive(true);
        CommunicationList.SetActive(false);
        
        foreach (Button btn in routeButtons)
        {
            if (!btn.IsInteractable())
            {
                btn.interactable = true;                
            }
            
        }
    }

    public void ShowEngineerList()
    {
        CommunicationList.SetActive(false);        
        RouteButtonsList.SetActive(false);
        EngineerList.SetActive(true);
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
            Debug.Log("result " + ResultRoute);            
            ShowCommunicationList();
        }
    }

    private void LeavePossibleRoutesButtons(Button button)
    {
        if(button.name == "6")
        {
            foreach (Button btn in routeButtons)
            {
                if (btn.name == "7" || btn.name == "T" || btn.name == "8")
                {
                    btn.interactable = false;
                }                    

            }
        }
        else if (button.name == "S")
        {
            foreach (Button btn in routeButtons)
            {
                if (btn.name == "6")
                {
                    btn.interactable = true;
                }
                else
                    btn.interactable = false;

            }
        }
    }

  


    private void Start()
    {
        IsRouteRequestRun = false;
        CommunicationList.SetActive(true);
        RouteButtonsList.SetActive(false);
        EngineerList.SetActive(false);
    }

}
