using System;
using System.Collections;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField]
    private GameObject switchPhysicsTurn;
    [SerializeField]
    public GameObject switchPhysicsStraight;
    [SerializeField]
    public GameObject turnIndicatorObj;
    [SerializeField]
    private GameObject straightIndicatorObj;
    public SpriteRenderer turnIndicator;
    public SpriteRenderer straightIndicator;
    private SwitchManager switchManager;    
    private TrafficLightsManager tlm;    
    public bool isLockedByRS;
    public bool isLockedByRoute;
    public bool isSwitchInUse;
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
        route = GameObject.Find("Route").GetComponent<Route>();
       
                
    }
    private void Start()
    {
        
        if (transform.Find("Lever"))
            anim = transform.Find("Lever").GetComponent<Animator>();
        IsSwitchStraight = true;
        DirectionStraight();        
    }



    public void ChangeDirection()
    {        
        if (!isLockedByRoute && !isLockedByRS && !isSwitchInUse)
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
