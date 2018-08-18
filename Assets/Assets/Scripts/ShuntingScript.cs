using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuntingScript : MonoBehaviour {

    public TrafficLights CH2, M1, CH3, N, N3;
    public Route route;
    public TrafficLightsManager tlm;
    public Engine trainEngine;
    public Engine engine;
    Rigidbody2D trEnRB;
    bool canWork;

    void Awake()
    {
        route = GameObject.Find("Route").GetComponent<Route>();
        tlm = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        trainEngine = GameObject.Find("TrainEngine").GetComponent<Engine>();

        trEnRB = trainEngine.GetComponent<Rigidbody2D>();

        N = GetTrafficLightByName("N");
        N3 = GetTrafficLightByName("N3");
        CH2 = GetTrafficLightByName("CH2");
        M1 = GetTrafficLightByName("M1");
        CH3 = GetTrafficLightByName("CH3");
        


    }

    private void Start()
    {
        StartCoroutine(Starting());        
        
        
    }

    void Update()
    {
        //Debug.Log(trEnRB.velocity.x);
        if (canWork && trEnRB.velocity.x > -1)
        {
            StartCoroutine(Shunting());
        }


    }

    IEnumerator Starting()
    {

        tlm.MakeRouteIfPossible(N, N3);
        engineGo(trainEngine, -99);
        yield return new WaitForSeconds(2);
        engineRelease(trainEngine);
        canWork = true;
        yield return new WaitForSeconds(7);
        engineStop(trainEngine);
    }


    IEnumerator Shunting()
    {
        canWork = false;
        tlm.MakeRouteIfPossible(CH2, M1);
        engineGo(engine, 8);
        yield return new WaitForSeconds(11f);
        engineStop(engine);
        yield return new WaitForSeconds(7f);
        tlm.MakeRouteIfPossible(M1, CH3);
        engineGo(engine, -8);
        yield return new WaitForSeconds(4f);
        engineRelease(engine);        
        yield return null;

    }   

    void engineGo(Engine e, int cp)
    {
        e.ControllerPosition = cp;
        Debug.Log(cp);
        Debug.Log("GO");

    }

    void engineStop(Engine e)
    {
        e.engineControllerUseBrakes();
        Debug.Log("Brakes");
    }

    void engineRelease(Engine e)
    {
        e.engineControllerReleaseAll();
        Debug.Log("Release");
    }

    public TrafficLights GetTrafficLightByName(string lightName)
    {
        return GameObject.Find(lightName).GetComponent<TrafficLights>();
    }

}
