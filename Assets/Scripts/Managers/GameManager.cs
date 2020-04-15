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


        if ( Input.GetKeyDown (KeyCode.Z) )
        {
            Time.timeScale = 20f;

        }

        if ( Input.GetKeyDown (KeyCode.X) )
        {
            Time.timeScale = 1f;

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
            PauseOn ();
        }
        else
        {
            PauseOff ();
        }
    }

    public void PauseOn()
    {
        EventManager.PauseOn ();
        isPaused = true;
        TimeManager.Instance.koef = 0;
    }

    public void PauseOff()
    {
        EventManager.PauseOff ();
        isPaused = false;
        TimeManager.Instance.koef = 1;
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
