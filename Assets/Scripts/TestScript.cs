using UnityEngine;

public class TestScript : MonoBehaviour,IManageable
{

    private RoutePanelManager routePanel;
    public EngineAI PassTrainBackAI;
    public EngineAI CargoTrainAI;
    public EngineAI PassTrainFrontAI;
    public Engine PassTrainBackEngine;
    public Engine PassTrainFrontEngine;
    public RollingStock PassTrainFrontRS;
    public EngineInertia PassTrainFrontInertia;
    public TrafficWatcher TWFront;

    


    public void Make1Route()
    {
        routePanel.SetRouteByNumber (5112);
        routePanel.SetRouteByNumber (1252);
    }

    public void MovePassBackSupper()
    {
        PassTrainBackAI.MoveBackSupper ();
    }

    public void MoveCargoForwardSupper()
    {
        CargoTrainAI.MoveForwardSupper ();
    }

    public void Init()
    {
        TWFront = PassTrainFrontAI.GetComponent<TrafficWatcher> ();
        PassTrainFrontEngine = PassTrainFrontAI.GetComponent<Engine> ();
        PassTrainBackEngine = PassTrainBackAI.GetComponent<Engine> ();
        PassTrainFrontRS = PassTrainFrontAI.GetComponent<RollingStock> ();
        PassTrainFrontInertia = PassTrainFrontAI.GetComponent<EngineInertia> ();
        PassTrainFrontInertia.enabled = false;
        PassTrainFrontEngine.Brakes = false;
        PassTrainFrontEngine.enabled = false;
        PassTrainFrontAI.enabled = false;
        PassTrainFrontRS.IsEngine = false;
        TWFront.enabled = false;
    }

    public void OnStart()
    {
        routePanel = FindObjectOfType<RoutePanelManager> ();
        //Invoke ("Make1Route", 1f);
        //Invoke ("MovePassBackSupper", 2f);
        //Invoke ("MoveCargoForwardSupper", 1f);
    }
}
