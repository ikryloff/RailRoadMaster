using System;
using System.Collections;
using UnityEngine;

public class Switch : MonoBehaviour, IManageable {

    [SerializeField]
    private SwitchCurve switchCurve;
    [SerializeField]
    private SwitchStraightPart switchStraightPart;    
    
    private SwitchManager switchManager;    
    public bool IsLockedByRS { get; set; }
    public bool isSwitchInUse;
    [SerializeField]
    TrackCircuit[] trackCircuits;
    [SerializeField]
    private Animator animator;
    private string animMethod;

    
    public bool IsSwitchStraight { get; set; }

    public static event Action OnPathChanged = delegate { };

    public void Init()
    {
        switchCurve = transform.GetComponentInChildren<SwitchCurve>();
        switchStraightPart = transform.GetComponentInChildren<SwitchStraightPart>();
        trackCircuits = transform.GetComponentsInChildren<TrackCircuit>();
        animator = transform.GetComponentInChildren<Animator>();
        SetDirectionStraight();

    }   

    public enum SwitchDir
    {
        Straight,
        Turn,
        Change
    }   

    public void SetSwitchDirection(SwitchDir direction)
    {
        if (!IsLockedByRS)
        {
            if (direction == SwitchDir.Straight)
                SetDirectionStraight();
            if (direction == SwitchDir.Turn)
                SetDirectionTurn();
            if (direction == SwitchDir.Change)
                ChangeSwitchDirection();            
        }
        else Debug.Log("Locked");
    }

    private void SetDirectionStraight()
    {        
        if (animator)
            animator.SetBool("TurnSwitch", false);
        switchCurve.Hide();
        switchStraightPart.Show();        
        IsSwitchStraight = true;        
        
    }
    private void SetDirectionTurn()
    {
        
        if (animator)
            animator.SetBool("TurnSwitch", true);
        switchStraightPart.Hide();
        switchCurve.Show();        
        IsSwitchStraight = false;        
    }


    private void ChangeSwitchDirection()
    {

        if (IsSwitchStraight)
        {
            SetDirectionTurn();
        }
        else
        {
            SetDirectionStraight();
        }
        EventManager.PathChanged();
    }

    public void OnStart() {  }

}
