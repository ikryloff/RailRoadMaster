using UnityEngine;

public class Scenario : MonoBehaviour
{
    CarsHolder carsHolder;
    private RoutePanelManager routePanel;
    private int gameHour;
    private int gameMinute;
    private int tempMinute;

    public void OnAwake()
    {
        carsHolder = FindObjectOfType<CarsHolder> ();
    }
    public void OnStart()
    {
        routePanel = FindObjectOfType<RoutePanelManager> ();
    }

    public void OnUpdate()
    {
        GetGameTime ();
        if ( tempMinute != gameMinute )
        {
            ExecuteEvent (gameHour, gameMinute);
            tempMinute = gameMinute;
        }
    }

    private void ExecuteEvent( int hour, int minute )
    {
        if ( hour == 0 && minute == 2 )
        {
            EventManager.PathUpdated ();
            //routePanel.SetRouteByNumber (5112);
            //routePanel.SetRouteByNumber (1252);
        }

        if ( hour == 0 && minute == 3 )
        {
        }

        if ( hour == 0 && minute == 3 )
        {
        }

        if ( hour == 0 && minute == 4 )
        {
        }

        if ( hour == 0 && minute == 20 )
        {
            //routePanel.SetRouteByNumber (5115);
        }

        if ( hour == 0 && minute == 22 )
        {
            
            
        }
       

        if ( hour == 0 && minute == 33 )
        {
            
        }
    }

    private void GetGameTime()
    {
        gameHour = TimeManager.Instance.TimeValue [0];
        gameMinute = TimeManager.Instance.TimeValue [1];
    }
}
