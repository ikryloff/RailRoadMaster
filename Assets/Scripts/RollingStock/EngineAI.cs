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
        connection = engine.EngineRS.GetComponent<RSConnection> ();
        Enable = true;
    }       

    private void Update()
    {
        if ( Input.GetKeyDown (KeyCode.RightArrow) )
        {
            MoveForward ();
        }

        if ( Input.GetKeyDown (KeyCode.LeftArrow) )
        {
            MoveBack ();
        }

        if ( Input.GetKeyDown (KeyCode.DownArrow) )
        {
            Stop ();
        }

        if ( Input.GetKeyDown (KeyCode.M) )
        {
            MoveBackSupper ();
        }

        if ( Input.GetKeyDown (KeyCode.E) )
        {
            engine.EngineRS.IsEngine = !engine.EngineRS.IsEngine;
        }

        if ( Input.GetKeyDown (KeyCode.U) )
        {
            engine.EngineRS.SetEngineToRS(engine);
        }

        if (Enable && engine.InstructionsHandler == 0 && engine.EngineRS.OwnTrack.name.Equals("PathTr3") )
            Disconnect ();

    }

    public void MoveForward()
    {
        engine.HandlerForward();
    }

    public void MoveBack()
    {
        engine.HandlerBack();
    }

    public void MoveBackSupper()
    {
        MoveBack ();        
        engine.InstructionsHandler = -6;
    }

    public void Stop()
    {
        engine.HandlerZero ();
    }

    public void Disconnect()
    {
        Enable = false;
        connection.DestroyConnection ();
        engine.EngineRS.IsEngine = false;
    }
}
