using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAI : MonoBehaviour {

    private Engine engine;
    private RSConnection  connection;
    private bool Enable;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        connection = engine.GetComponent<RollingStock>().GetComponent<RSConnection>();
        Enable = true;
    }       

    private void Update()
    {
       
        if ( Input.GetKeyDown (KeyCode.DownArrow) )
        {
            Stop ();
        }
              

    }

    public void MoveBackSupper()
    {
        engine.HandlerBack ();
        engine.InstructionsHandler = -6;
        
    }

    public void MoveForwardSupper()
    {
        engine.HandlerForward();
        engine.InstructionsHandler = 6;
    }

    public void Stop()
    {
        engine.HandlerZero ();
    }

   
}
