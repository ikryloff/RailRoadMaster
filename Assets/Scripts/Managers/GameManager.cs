using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public GameObject gameOver;
    private bool isGameOn = true;
    private bool isPaused = false;
    

    private void Awake()
    {
        gameOver.SetActive (false);
        EventManager.onGameOver += GameOver;
        
        
    }

    private void Update()
    {
        if ( Input.GetKeyDown (KeyCode.P) )
        {
            if ( !isPaused )
            {
                EventManager.PauseOn ();
                isPaused = true;
            }
            else
            {
                EventManager.PauseOff ();
                isPaused = false;
            }            
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
