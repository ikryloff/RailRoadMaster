using UnityEngine;
public class LoadingManager : MonoBehaviour
{

    private TrackCircuitSignals circuitSignals;
    private SwitchManager switchManager;
    private CarsHolder carsHolder;
    private TrainManager trainManager;
    private Scenario scenario;
    private CompositionManager cm;
    

    void Awake()
    {
        circuitSignals = FindObjectOfType<TrackCircuitSignals> ();
        carsHolder = FindObjectOfType<CarsHolder> ();
        trainManager = FindObjectOfType<TrainManager> ();
        switchManager = FindObjectOfType<SwitchManager> ();
        scenario = FindObjectOfType<Scenario> ();
        
        
        IndicationManager.Instance.Init ();

        TrackPath.Instance.Init ();

        TrackCircuitManager.Instance.Init ();

        switchManager.Init ();

        TrafficLightsManager.Instance.Init ();

        RouteDictionary.Instance.Init ();

        carsHolder.Init ();

        CompositionManager.Instance.Init ();

        trainManager.OnAwake ();

        CouplerManager.Instance.Init ();


        scenario.OnAwake ();

    }

    private void Start()
    {
        TimeManager.Instance.OnStart ();

        TrackPath.Instance.OnStart ();

        carsHolder.OnStart ();

        CompositionManager.Instance.OnStart ();

        circuitSignals.OnStart ();

        CouplerManager.Instance.OnStart ();

        IndicationManager.Instance.OnStart ();

        trainManager.OnStart ();

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
