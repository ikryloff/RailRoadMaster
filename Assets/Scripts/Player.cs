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
        engine.InstructionsHandler++;
    }

    public void MoveBack()
    {
        engine.InstructionsHandler--;
    }

    public void Stop()
    {
        engine.InstructionsHandler = 0;
    }
}
