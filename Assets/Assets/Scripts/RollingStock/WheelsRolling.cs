using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRolling : MonoBehaviour {    
    public RollingStock car;
    public float speed;

    void Start()
    {
        car = gameObject.transform.root.GetComponent<RollingStock>();        
    }

    // Update is called once per frame
    void Update () {
        speed = car.force;
        transform.Rotate(-speed * 10f, 0, 0);
	}
}
