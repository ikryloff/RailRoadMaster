using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour {

    private RollingStock rs;
    private CarsHolder carsHolder;

    private void Awake()
    {
        carsHolder = FindObjectOfType<CarsHolder> ();
    }

    private void OnTriggerEnter( Collider collider )
    {
        rs = collider.GetComponent<RollingStock> ();
        carsHolder.SetUnactiveRS (rs.Number);
    }
    
}
