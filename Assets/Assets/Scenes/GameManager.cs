using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public TrafficLights ch2, m1, ch4, N, N3 ;
    [SerializeField]
    private Route route;
    [SerializeField]
    private TrafficLightsManager tlm;
    [SerializeField]
    private Engine engine;
    [SerializeField]


    void Start ()
    {
        Invoke("Script", 2f);        
    }

    void Update()
    {
        
    }

    void Script()
    {
        float timeZ = 0f;
        tlm.MakeRouteIfPossible(N, N3);
        engineGo();
        Invoke("engineRelease", timeZ + 2f);
        Invoke("engineStop", timeZ + 9f);
    }

    void engineGo()
    {
        engine.ControllerPosition = -99;
        Debug.Log("GO");
        
    }

    void engineStop()
    {
        engine.engineControllerUseBrakes();
        Debug.Log("Brakes");
    }

    void engineRelease()
    {
        engine.engineControllerReleaseAll();
        Debug.Log("Release");
    }



}
