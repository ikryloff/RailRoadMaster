using UnityEngine;
public class LoadingManager : MonoBehaviour
{    

    void Awake()
    {
       
        TrackPath.Instance.Init();

        TrackCircuitManager.Instance.Init();
       
        SwitchManager.Instance.Init();  
        
        TrafficLightsManager.Instance.Init();

        RouteDictionary.Instance.Init();

        CompositionManager.Instance.Init();
    }

    private void Start()
    {
        TrackPath.Instance.OnStart();

        CompositionManager.Instance.OnStart();
    }


    void Update()
    {


    }


}
