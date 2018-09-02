using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestRouteManager : Singleton<RequestRouteManager> {
    [SerializeField]
    private GameObject[] firstList;
    [SerializeField]
    private GameObject[] fromList;
    public GameObject canvasParent;
    public RollingStock rollingStock;
    [SerializeField]
    private GameObject startBtn;
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

    public GameObject StartBtn
    {
        get
        {
            return startBtn;
        }

        set
        {
            startBtn = value;
        }
    }

    public GameObject[] FirstList
    {
        get
        {
            return firstList;
        }

        set
        {
            firstList = value;
        }
    }

    public GameObject[] FromList
    {
        get
        {
            return fromList;
        }

        set
        {
            fromList = value;
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
        foreach (GameObject item in FirstList)
        {
            item.SetActive(true);
        }   
        GetTrack();        
        StartBtn.SetActive(false);
        CancelBtn.SetActive(true);

    }

    public void ShowFromList()
    {
        foreach (GameObject item in FromList)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in FirstList)
        {
            item.SetActive(false);
        }
        CancelBtn.SetActive(true);
    }

    public void RequestButton()
    {
        foreach (GameObject item in FirstList)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in FromList)
        {
            item.SetActive(false);
        }
        StartBtn.SetActive(true);
        CancelBtn.SetActive(false);

    }

    public void GetTrack()
    {
        Track = rollingStock.TrackCircuit;
        Debug.Log(Track.TrackName);
    }

    private void Start()
    {
        CancelBtn.SetActive(false);
        foreach (GameObject item in FirstList)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in FromList)
        {
            item.SetActive(false);
        }
    }

}
