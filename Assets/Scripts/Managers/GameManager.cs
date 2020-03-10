using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private bool isGameOn = true;
    private bool isPaused = false;
    private RoutePanelManager routePanel;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
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
            Time.timeScale = 20f;
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

    }
    public void GameContinue()
    {
        isGameOn = true;
        PauseOff ();
    }

    public void QuitGame()
    {
        Application.Quit ();
    }




}
