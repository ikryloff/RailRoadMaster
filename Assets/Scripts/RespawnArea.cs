using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour {

    private RollingStock rs;
    private List<RSComposition> RSs;
    private CarsHolder carsHolder;

    private void Awake()
    {
        carsHolder = FindObjectOfType<CarsHolder> ();
        RSs = new List<RSComposition> ();
    }

    private void OnTriggerEnter( Collider collider )
    {
        rs = collider.GetComponent<RollingStock> ();
        RSs = rs.RSComposition.CarComposition.Cars;
        for ( int i = 0; i < RSs.Count; i++ )
        {
            carsHolder.SetUnactiveRS (RSs [i].Number);
        }
    }
    
}
