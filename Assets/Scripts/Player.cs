using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Engine engine;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
    }

    

    public void MoveForward()
    {
        engine.HandlerForward ();
    }

    public void MoveBack()
    {
        engine.HandlerBack ();
    }

    public void Stop()
    {
        engine.HandlerZero ();
    }
}
