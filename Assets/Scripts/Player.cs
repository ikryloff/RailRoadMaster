using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Engine engine;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
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

        if ( Input.GetKeyDown (KeyCode.UpArrow) )
        {
            UIManager.Instance.ToggleIndication ();
        }
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
