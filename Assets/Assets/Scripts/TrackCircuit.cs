using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCircuit : MonoBehaviour {    
    private string trackName;
    private bool isCarPresence;
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
            IsCarPresence = true;            
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
            IsCarPresence = false;
        }
    }

    public bool IsCarPresence
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
}
