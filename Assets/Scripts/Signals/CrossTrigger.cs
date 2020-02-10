using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossTrigger : TLTrigger
{
    public override void OnTriggerEnter( Collider collider )
    {
        if ( signal.IsClosed )
        {
            print (" falt cross " + signal.name);
            GameManager.Instance.GameOver ();
        }
            
    }
}
