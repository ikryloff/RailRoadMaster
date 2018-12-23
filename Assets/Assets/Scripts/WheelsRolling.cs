using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRolling : MonoBehaviour {    
    Rigidbody2D car;

    void Start()
    {
        car = gameObject.transform.root.GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update () {
        
        transform.Rotate(-car.velocity.x * 0.7f, 0, 0);
	}
}
