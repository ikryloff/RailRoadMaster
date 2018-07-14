﻿using System;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField]
    private GameObject switchPhysicsTurn;
    [SerializeField]
    private GameObject switchPhysicsStraight;
    [SerializeField]
    private GameObject turnIndicatorObj;
    [SerializeField]
    private GameObject straightIndicatorObj;
    private SpriteRenderer turnIndicator;
    private SpriteRenderer straightIndicator;
    private string HIDE_INDICATION_LAYER = "HideIndication";
    private string INDICATION_LAYER = "Indication";
    private int timesLocked = 0;
    

    private bool isSwitchStraight;
        

    // Use this for initialization
    void Start () {
        turnIndicator = turnIndicatorObj.GetComponent<SpriteRenderer>();
        straightIndicator = straightIndicatorObj.GetComponent<SpriteRenderer>();
        isSwitchStraight = true;
        directionStraight();       
    }

    private void OnGUI()
    {
        if (timesLocked > 0)
        {
            turnIndicator.color = new Color32(255, 0, 0, 160);
            straightIndicator.color = new Color32(255, 0, 0, 160);            
        }
        else if(timesLocked == 0)
        {
            turnIndicator.color = new Color32(255, 255, 255, 160);
            straightIndicator.color = new Color32(255, 255, 255, 160);
        }

    }

    public void changeDirection()
    {
        if (timesLocked == 0)
        {
            if (isSwitchStraight == true)
            {
                directionTurn();
            }
            else
            {
                directionStraight();
            }
        }
        else Debug.Log("Locked");
    } 
    
    public int SwitchLockCount
    {
        set
        {
            timesLocked = value;
        }
        get
        {
            return timesLocked;
        }
    }

    public Exception Exception { get; private set; }

    public void directionStraight()
    {
        switchPhysicsTurn.SetActive(false);
        switchPhysicsStraight.SetActive(true);
        turnIndicator.sortingLayerName = HIDE_INDICATION_LAYER;
        straightIndicator.sortingLayerName = INDICATION_LAYER;
        isSwitchStraight = true;
    }
    public void directionTurn()
    {
        switchPhysicsStraight.SetActive(false);
        switchPhysicsTurn.SetActive(true);
        turnIndicator.sortingLayerName = INDICATION_LAYER;
        straightIndicator.sortingLayerName = HIDE_INDICATION_LAYER;
        isSwitchStraight = false;        
    }

    
}
