using System;
using System.Collections;
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
    public Animator anim;
    private string animMethod;
    public GameTime gameTime;
    
    Route route;

    [SerializeField]
    private bool isSwitchStraight;
    
    


    void Awake ()
    {
               
        trackCircuits = transform.GetComponentsInChildren<TrackCircuit>();
        switchManager = FindObjectOfType<SwitchManager>(); 
        tlm = FindObjectOfType<TrafficLightsManager>();
        gameTime = FindObjectOfType<GameTime>();
        turnIndicator = turnIndicatorObj.GetComponent<SpriteRenderer>();
        straightIndicator = straightIndicatorObj.GetComponent<SpriteRenderer>();        
        route = GameObject.Find("Route").GetComponent<Route>();
       
                
    }
    private void Start()
    {
        
        if (transform.Find("Lever"))
            anim = transform.Find("Lever").GetComponent<Animator>();
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
            if (IsSwitchStraight)
            {                               
                DirectionTurn();
            }
            else
            {                               
                DirectionStraight();
            }
            tlm.CheckHandSwitches();
            switchManager.UpdatePathAfterSwitch();
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
        
        if (anim)
            anim.SetBool("TurnSwitch", false);
        switchPhysicsTurn.SetActive(false);
        switchPhysicsStraight.SetActive(true);        
        IsSwitchStraight = true;
        
        
    }
    public void DirectionTurn()
    {
        
        if (anim)
            anim.SetBool("TurnSwitch", true);
        switchPhysicsStraight.SetActive(false);
        switchPhysicsTurn.SetActive(true);        
        IsSwitchStraight = false;        
    }   

   

}
