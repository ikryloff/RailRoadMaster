using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public GameObject gameOver;
    private bool isGameOn = true;
    private bool isPaused = false;
    private RoutePanelManager routePanel;
    

    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameOver.SetActive (false);
        EventManager.onGameOver += GameOver;
        routePanel = FindObjectOfType<RoutePanelManager> ();        
        
    }

    private void Update()
    {        
        if ( Input.GetKeyDown (KeyCode.P) )
        {
            Pause ();
        }


        if ( Input.GetKeyDown (KeyCode.F2) )
        {
            GameOverOn ();
        }

       
        if ( Input.GetKeyDown (KeyCode.T) )
        {
            routePanel.CallOffRouteByNumber (613);

        }

    }

    private void GameOverOn()
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

    public void Pause()
    {
        if ( !isPaused )
        {
            EventManager.PauseOn ();
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            EventManager.PauseOff ();
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void PauseOn()
    {
        EventManager.PauseOn ();
        isPaused = true;
    }

    public void PauseOff()
    {
        EventManager.PauseOff ();
        isPaused = false;
    }


    public void GameOver()
    {
        isGameOn = false;
        PauseOn ();
        gameOver.SetActive (true);
    }
    public void GameContinue()
    {
        gameOver.SetActive (false);
        isGameOn = true;
    }

    public void QuitGame()
    {
        Application.Quit ();
    }



}
