using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : TrafficLight {

    void Start()
    {
        GetPositionX = gameObject.transform.position.x;
        IsClosed = true;
    }
}
