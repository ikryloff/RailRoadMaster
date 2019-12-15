using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour {

    private RSComposition rs;
    
    
   
    private void OnTriggerEnter( Collider collider )
    {
        rs = collider.GetComponent<RSComposition> ();
        rs.CarComposition.Hide (); 
    }
    
}
