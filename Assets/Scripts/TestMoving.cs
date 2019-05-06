using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour {

    public float speed = 1.0f;
    public Transform first;
    public Transform second;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.LookAt (first.position);
        transform.position = Vector3.MoveTowards (transform.position, first.position, step);

        if(Vector3.Distance(transform.position, first.position) < 0.1 )
        {            
            transform.LookAt (second.position);
            first.position = second.position;
        }
	}
}
