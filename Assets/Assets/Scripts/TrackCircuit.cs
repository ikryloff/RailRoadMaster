using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuit : MonoBehaviour {    
    private string trackName;
    private int isCarPresence;
    private int wasUsed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
             
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RollingStock")
        {            
            if (tag == "Switch")
            {
                transform.parent.GetComponent<Switch>().SwitchLockCount += 1;
            }
            IsCarPresence += 1;            
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RollingStock")
        {           
            if (tag == "Switch")
            {
                transform.parent.GetComponent<Switch>().SwitchLockCount -= 1;
            }
            IsCarPresence -= 1;
        }
    }

    public int IsCarPresence
    {
        get
        {
            return isCarPresence;
        }

        set
        {
            isCarPresence = value;
        }
    }

    public string Name
    {
        get
        {
            return trackName;
        }

        set
        {
            trackName = value;
        }
    }

    public int WasUsed
    {
        get
        {
            if (isCarPresence > 0)
            {
                wasUsed = Constants.TC_OVER;
            }
            if (wasUsed == Constants.TC_OVER && isCarPresence == 0)
            {
                wasUsed = Constants.TC_USED;
            }
            return wasUsed;
        }

        set
        {
            wasUsed = value;
        }
    }
}
