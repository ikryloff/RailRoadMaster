using UnityEngine;
public class LoadingManager : MonoBehaviour
{

    TrackCircuitSignals circuitSignals;
    TestScript testScript;
    CarsHolder carsHolder;

    void Awake()
    {
        circuitSignals = FindObjectOfType<TrackCircuitSignals> ();
        testScript = FindObjectOfType<TestScript> ();
        carsHolder = FindObjectOfType<CarsHolder> ();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        

        IndicationManager.Instance.Init ();

        TrackPath.Instance.Init ();

        TrackCircuitManager.Instance.Init ();

        SwitchManager.Instance.Init ();

        TrafficLightsManager.Instance.Init ();

        RouteDictionary.Instance.Init ();

        CompositionManager.Instance.Init ();

        CouplerManager.Instance.Init ();

        testScript.Init ();

    }

    private void Start()
    {
        TrackPath.Instance.OnStart ();

        CompositionManager.Instance.OnStart ();

        circuitSignals.Init ();

        CouplerManager.Instance.OnStart ();

        IndicationManager.Instance.OnStart ();

        carsHolder.OnStart ();

        testScript.OnStart ();

    }

  
}
