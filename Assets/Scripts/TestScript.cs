using UnityEngine;

public class TestScript : MonoBehaviour
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

    private void Awake()
    {
        TWFront = PassTrainFrontAI.GetComponent<TrafficWatcher> ();
        PassTrainFrontEngine = PassTrainFrontAI.GetComponent<Engine> ();
        PassTrainBackEngine = PassTrainBackAI.GetComponent<Engine> ();
        PassTrainFrontRS = PassTrainFrontAI.GetComponent<RollingStock> ();
        PassTrainFrontInertia = PassTrainFrontAI.GetComponent<EngineInertia> ();        
        PassTrainFrontInertia.enabled = false;
        TWFront.enabled = false;        
        PassTrainFrontEngine.Brakes = false; 
        PassTrainFrontEngine.enabled = false;
        PassTrainFrontAI.enabled = false;
        PassTrainFrontRS.IsEngine = false;
        PassTrainFrontRS.Brakes = false;        
    }

    void Start()
    {
        
        routePanel = FindObjectOfType<RoutePanelManager> ();   
        Invoke ("Make1Route", 1f);
        Invoke ("MovePassBackSupper", 1f);
        //Invoke ("MoveCargoForwardSupper", 1f);
    }


    public void Make1Route()
    {
        routePanel.SetRouteByNumber (5112);
        //routePanel.SetRouteByNumber (5214);
    }

    public void MovePassBackSupper()
    {
        PassTrainBackAI.MoveBackSupper ();
    }

    public void MoveCargoForwardSupper()
    {
        CargoTrainAI.MoveForwardSupper ();
    }

}
