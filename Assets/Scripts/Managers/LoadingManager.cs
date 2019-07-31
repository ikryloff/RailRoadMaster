using UnityEngine;
public class LoadingManager : MonoBehaviour
{

    TrackCircuitSignals circuitSignals;

    void Awake()
    {
        Application.targetFrameRate = 60;

        IndicationManager.Instance.Init ();

        TrackPath.Instance.Init ();

        TrackCircuitManager.Instance.Init ();

        SwitchManager.Instance.Init ();

        TrafficLightsManager.Instance.Init ();

        RouteDictionary.Instance.Init ();

        CompositionManager.Instance.Init ();

        circuitSignals = FindObjectOfType<TrackCircuitSignals> ();
    }

    private void Start()
    {
        TrackPath.Instance.OnStart ();

        CompositionManager.Instance.OnStart ();
        
        circuitSignals.Init ();


    }
    
}
