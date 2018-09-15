using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestRouteManager : Singleton<RequestRouteManager> {
    [SerializeField]
    private GameObject[] communicationList;
    [SerializeField]
    private GameObject[] routeStartList;
    public GameObject canvasParent;
    public RollingStock rollingStock;   
    [SerializeField]
    private GameObject cancelBtn;
    public TrackCircuit track;

    public TrackCircuit Track
    {
        get
        {
            return track;
        }

        set
        {
            track = value;
        }
    }
       

    public GameObject[] CommunicationList
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

    public GameObject[] RouteStartList
    {
        get
        {
            return routeStartList;
        }

        set
        {
            routeStartList = value;
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

    public void ShowList()
    {
        foreach (GameObject item in CommunicationList)
        {
            item.SetActive(true);
        }   
        GetTrack();
        foreach (GameObject item in RouteStartList)
        {
            item.SetActive(false);
        }
        CancelBtn.SetActive(true);


    }

    public void ShowRouteStartList()
    {
        foreach (GameObject item in RouteStartList)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in CommunicationList)
        {
            item.SetActive(false);
        }
        CancelBtn.SetActive(true);
    }

   

    public void GetTrack()
    {
        Track = rollingStock.TrackCircuit;        
    }

    private void Start()
    {
        CancelBtn.SetActive(true);
        foreach (GameObject item in CommunicationList)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in RouteStartList)
        {
            item.SetActive(false);
        }
    }

}
