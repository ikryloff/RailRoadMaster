using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRolling : MonoBehaviour {    
    public RollingStock car;
    public float speed;
    private Transform wheel;

    void Start()
    {
        car = gameObject.transform.root.GetComponent<RollingStock>();
        wheel = GetComponent<Transform>();
    }
        
    void Update () {
        if(car.OwnEngine)
            speed = car.OwnEngine.acceleration;
        wheel.Rotate(0, 0, -speed * 10f);
	}
}
