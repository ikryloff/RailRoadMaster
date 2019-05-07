using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    private void Update()
    {
        if ( Input.GetKeyDown (KeyCode.P) )
        {
            EventManager.PauseOn ();
        }

        if ( Input.GetKeyDown (KeyCode.O) )
        {
            EventManager.PauseOff ();
        }
    }



}
