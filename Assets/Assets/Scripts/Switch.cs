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
    private int timesLocked = 0;

    [SerializeField]
    private bool isSwitchStraight;
        

    void Awake () {
        switchManager = GameObject.Find("SwitchManager").GetComponent<SwitchManager>();        
        turnIndicator = turnIndicatorObj.GetComponent<SpriteRenderer>();
        straightIndicator = straightIndicatorObj.GetComponent<SpriteRenderer>();
        IsSwitchStraight = true;
        DirectionStraight();
        
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
        turnIndicator.sortingLayerName = Constants.HIDE_INDICATION_LAYER;
        straightIndicator.sortingLayerName = Constants.INDICATION_LAYER;
        IsSwitchStraight = true;
    }
    public void DirectionTurn()
    {
        switchPhysicsStraight.SetActive(false);
        switchPhysicsTurn.SetActive(true);
        turnIndicator.sortingLayerName = Constants.INDICATION_LAYER;
        straightIndicator.sortingLayerName = Constants.HIDE_INDICATION_LAYER;
        IsSwitchStraight = false;        
    }   

}
