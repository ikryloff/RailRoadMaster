using System;
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
    private SwitchManager switchManager;    
    private TrafficLightsManager tlm;    
    private int timesLocked = 0;
    [SerializeField]
    TrackCircuit[] trackCircuits;
    
    Route route;

    [SerializeField]
    private bool isSwitchStraight;
   
        

    void Awake ()
    {
        trackCircuits = transform.GetComponentsInChildren<TrackCircuit>();
        switchManager = GameObject.Find("SwitchManager").GetComponent<SwitchManager>();        
        tlm = FindObjectOfType<TrafficLightsManager>();        
        turnIndicator = turnIndicatorObj.GetComponent<SpriteRenderer>();
        straightIndicator = straightIndicatorObj.GetComponent<SpriteRenderer>();        
        route = GameObject.Find("Route").GetComponent<Route>();
    }
    private void Start()
    {
        IsSwitchStraight = true;
        DirectionStraight();
    }


    private void OnGUI()
    {
        if (timesLocked > 0)
        {
            turnIndicator.color = new Color32(255, 77, 77, 160);
            straightIndicator.color = new Color32(255, 77, 77, 160);
        }
        else if(timesLocked == 0)
        {
            turnIndicator.color = new Color32(255, 242, 0, 160);
            straightIndicator.color = new Color32(58, 227, 116, 160);
        }

    }

    public void ChangeDirection()
    {
        if (timesLocked == 0)
        {
            if (IsSwitchStraight == true)
            {
                DirectionTurn();
            }
            else
            {
                DirectionStraight();
            }
            tlm.CheckHandSwitches();
            route.MakePathInBothDirections();
            
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

    public bool IsSwitchStraight
    {
        get
        {
            return isSwitchStraight;
        }

        set
        {
            isSwitchStraight = value;
        }
    }

    public void DirectionStraight()
    {
        switchPhysicsTurn.SetActive(false);
        switchPhysicsStraight.SetActive(true);       
        IsSwitchStraight = true;        
    }
    public void DirectionTurn()
    {
        switchPhysicsStraight.SetActive(false);
        switchPhysicsTurn.SetActive(true);       
        IsSwitchStraight = false;        
    }   

}
