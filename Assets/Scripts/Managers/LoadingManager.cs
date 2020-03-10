using UnityEngine;
public class LoadingManager : MonoBehaviour
{

    private TrackCircuitSignals circuitSignals;
    private CarsHolder carsHolder;
    private Scenario scenario;
    private CompositionManager cm;
    

    void Awake()
    {
        circuitSignals = FindObjectOfType<TrackCircuitSignals> ();
        carsHolder = FindObjectOfType<CarsHolder> ();
        scenario = FindObjectOfType<Scenario> ();
        
        
        IndicationManager.Instance.Init ();

        TrackPath.Instance.Init ();

        TrackCircuitManager.Instance.Init ();

        SwitchManager.Instance.Init ();

        TrafficLightsManager.Instance.Init ();

        RouteDictionary.Instance.Init ();

        CompositionManager.Instance.Init ();

        CouplerManager.Instance.Init ();

        carsHolder.Init ();

        scenario.OnAwake ();

    }

    private void Start()
    {
        TimeManager.Instance.OnStart ();

        TrackPath.Instance.OnStart ();

        CompositionManager.Instance.OnStart ();

        circuitSignals.OnStart ();

        CouplerManager.Instance.OnStart ();

        IndicationManager.Instance.OnStart ();

        carsHolder.OnStart ();

        scenario.OnStart ();

        UIManager.Instance.OnStart ();

    }

    private void Update()
    {
        CompositionManager.Instance.OnUpdate ();
        CouplerManager.Instance.OnUpdate ();
        carsHolder.UpdateConnections ();
        TimeManager.Instance.OnUpdate ();
        scenario.OnUpdate ();
        carsHolder.OnUpdate ();
    }

  
}
