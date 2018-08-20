using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TrafficLights CH2, M1, CH3, N, N3;
    public Route route;
    public TrafficLightsManager tlm;
    public Engine trainEngine;
    public Engine engine;
    Rigidbody2D trainEngineRB;
    Rigidbody2D engineRB;
    bool step0 = false;
    bool step1 = false;
    bool step2 = false;
    int speedAllowed = 0;
    bool autoDrive = false;


    void Awake()
    {
        route = GameObject.Find("Route").GetComponent<Route>();
        tlm = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        trainEngine = GameObject.Find("TrainEngine").GetComponent<Engine>();

        trainEngineRB = trainEngine.GetComponent<Rigidbody2D>();
        engineRB = engine.GetComponent<Rigidbody2D>();

        N = GetTrafficLightByName("N");
        N3 = GetTrafficLightByName("N3");
        CH2 = GetTrafficLightByName("CH2");
        M1 = GetTrafficLightByName("M1");
        CH3 = GetTrafficLightByName("CH3");


    }

    private void Start()
    {
        Invoke("DD", 0.1f);
        //StartCoroutine(TrainArriving());        

    }
    void DD()
    {
        tlm.MakeRouteIfPossible(N, N3);
    }

    void Update()
    {
        //AutoDriving(speedAllowed);              
        if (step0 && !route.Routes.Contains(route.FindRouteByName("NN3")))
        {
            StartCoroutine(ShuntingFrom2to7()); 
        }

        if (step1 && !route.Routes.Contains(route.FindRouteByName("CH2M1")))
        {
            StartCoroutine(ShuntingFrom7to3());
        }
        if (step2 && !route.Routes.Contains(route.FindRouteByName("M1CH3")))
        {
            StartCoroutine(ShuntingCoupleToComposition()); 
        }
    }

    IEnumerator WaitSomeSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        
    }

    IEnumerator TrainArriving()
    {
        yield return new WaitForSecondsRealtime(1f);
        tlm.MakeRouteIfPossible(N, N3);
        EngineGo(trainEngine, -75);
        yield return new WaitForSecondsRealtime(3f);       
        EngineStop(trainEngine);
        step0 = true;
    }



    IEnumerator ShuntingFrom2to7()
    {
        step0 = false;
        tlm.MakeRouteIfPossible(CH2, M1);        
        EngineGo(engine, 8);
        speedAllowed = 40;
        step1 = true;
        yield return null;
    }

    IEnumerator ShuntingFrom7to3()
    {
        step1 = false;        
        EngineStop(engine);
        yield return new WaitForSecondsRealtime(3f);
        tlm.MakeRouteIfPossible(M1, CH3);
        yield return new WaitForSecondsRealtime(1f);
        EngineGo(engine, -8);
        speedAllowed = 25;
        step2 = true;
    }

    IEnumerator ShuntingCoupleToComposition()
    {
        yield return new WaitForSecondsRealtime(1f);
        step2 = false;
        EngineStop(engine);
    }


    void EngineGo(Engine e, int cp)
    {
        autoDrive = true;
        e.ControllerPosition = cp;
        Debug.Log("GO");

    }

    void EngineStop(Engine e)
    {
        autoDrive = false;
        e.engineControllerUseBrakes();
        Debug.Log("Brakes");
    }

    void EngineRelease(Engine e)
    {
        autoDrive = false;
        e.engineControllerReleaseAll();
        Debug.Log("Release");
    }

    
    public TrafficLights GetTrafficLightByName(string lightName)
    {
        return GameObject.Find(lightName).GetComponent<TrafficLights>();
    }


}
