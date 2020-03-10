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
            routePanel.SetRouteByNumber (5112);
            routePanel.SetRouteByNumber (1252);
        }

        if ( hour == 0 && minute == 3 )
        {
            carsHolder.SetUnactiveRS (8701);
        }

        if ( hour == 0 && minute == 3 )
        {
            carsHolder.UnablePassEngine(116);
        }

        if ( hour == 0 && minute == 4 )
        {
            carsHolder.GetCar (114).Engine.InstructionsHandler = -6;
        }

        if ( hour == 0 && minute == 20 )
        {
            routePanel.SetRouteByNumber (5115);
        }

        if ( hour == 0 && minute == 22 )
        {
            carsHolder.SetActiveRS(8701);
            carsHolder.SetCarsPosition (8701, "PathTrI_N", 150);
            carsHolder.GetCar (8701).Engine.AI.MoveBack (6);
            
        }
       

        if ( hour == 0 && minute == 33 )
        {
            carsHolder.SetUnactiveRS( 114);
            carsHolder.SetUnactiveRS( 115);
            carsHolder.SetUnactiveRS( 116);
        }
    }

    private void GetGameTime()
    {
        gameHour = TimeManager.Instance.TimeValue [0];
        gameMinute = TimeManager.Instance.TimeValue [1];
    }
}
