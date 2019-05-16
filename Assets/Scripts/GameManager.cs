using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public GameObject gameOver;
    public bool isGameOn = true;

    private void Awake()
    {
        gameOver.SetActive (false);
        EventManager.oNGameOver += GameOver;
    }

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

        if ( Input.GetKeyDown (KeyCode.F2) )
        {
            if ( isGameOn )
            {                
                EventManager.GameOver ();
            }
            else
            {
                GameContinue ();
                
            }
                
        }

    }


    public void GameOver()
    {
        isGameOn = false;
        gameOver.SetActive (true);
    }
    public void GameContinue()
    {
        gameOver.SetActive (false);
        isGameOn = true;
    }

}
