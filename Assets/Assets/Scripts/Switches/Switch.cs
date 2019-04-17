using System;
using System.Collections;
using UnityEngine;

public class Switch : MonoBehaviour, IManageable {
    [SerializeField]
    private SwitchCurve switchCurve;
    [SerializeField]
    private SwitchStraightPart switchStraightPart;    
    
    private SwitchManager switchManager;    
    private TrafficLightsManager tlm;    
    public bool isLockedByRS;
    public bool isLockedByRoute;
    public bool isSwitchInUse;
    [SerializeField]
    TrackCircuit[] trackCircuits;
    [SerializeField]
    private Animator animator;
    private string animMethod;

    [SerializeField]
    private bool isSwitchStraight;
    
    


    public void Init()
    {
        switchCurve = transform.GetComponentInChildren<SwitchCurve>();
        switchStraightPart = transform.GetComponentInChildren<SwitchStraightPart>();
        trackCircuits = transform.GetComponentsInChildren<TrackCircuit>();
        switchManager = FindObjectOfType<SwitchManager>(); 
        tlm = FindObjectOfType<TrafficLightsManager>();
        animator = transform.GetComponentInChildren<Animator>();
        SetDirectionStraight();

    }    

    public void ChangeDirection()
    {        
        if (!isLockedByRoute && !isLockedByRS && !isSwitchInUse)
        {
            if (IsSwitchStraight)
            {                               
                SetDirectionTurn();
            }
            else
            {                               
                SetDirectionStraight();
            }
            tlm.CheckHandSwitches();
            switchManager.UpdatePathAfterSwitch();
        }
        else Debug.Log("Locked");
    } 
      
    public void SetDirectionStraight()
    {        
        if (animator)
            animator.SetBool("TurnSwitch", false);
        switchCurve.Hide();
        switchStraightPart.Show();        
        IsSwitchStraight = true;        
        
    }
    public void SetDirectionTurn()
    {
        
        if (animator)
            animator.SetBool("TurnSwitch", true);
        switchStraightPart.Hide();
        switchCurve.Show();        
        IsSwitchStraight = false;        
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
   

}
