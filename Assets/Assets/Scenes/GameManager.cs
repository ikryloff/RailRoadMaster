using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TrafficLights CH2, M1, CH3, N, N3;
    public Route route;
    public TrafficLightsManager tlm;
    public Engine trainEngine;
    public Engine engine;
    public Transform behind;
    public Transform before;
    public Transform car;
    public Transform ch3Trigger;
    public Transform trainEngineT;
    public Transform engineT;
    public RollingStock uncoupleFromRS;
    Rigidbody2D trainEngineRB;
    Rigidbody2D engineRB;
    bool step0 = false;
    bool step1 = false;
    bool step2 = false;
    int speedAllowed = 0;
    [SerializeField]
    private AutoDriveManager auto;


    void Awake()
    {
        route = GameObject.Find("Route").GetComponent<Route>();
        tlm = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        engine = GameObject.Find("Engine").GetComponent<Engine>();
        trainEngine = GameObject.Find("TrainEngine").GetComponent<Engine>();

        trainEngineRB = trainEngine.GetComponent<Rigidbody2D>();
        engineRB = engine.GetComponent<Rigidbody2D>();

        trainEngineT = trainEngine.transform;
        engineT = engine.transform;

        N = GetTrafficLightByName("N");
        N3 = GetTrafficLightByName("N3");
        CH2 = GetTrafficLightByName("CH2");
        M1 = GetTrafficLightByName("M1");
        CH3 = GetTrafficLightByName("CH3");        

    }

    private void Start()
    {        
        StartCoroutine(TrainArriving());     

    }
    

    void Update()
    {
        //AutoDriving(speedAllowed);              
        if (step0 && !route.Routes.Contains(route.FindRouteByName("NN3")))
        {
            StartCoroutine(ShuntingFrom2to7()); 
        }

        if (step1 && !route.Routes.Contains(route.FindRouteByName("CH2M1")) && (int)(Time.deltaTime * engineRB.velocity.magnitude * 5) == 0)
        {
            StartCoroutine(ShuntingFrom7to3());
        }
        if (step2 && !route.Routes.Contains(route.FindRouteByName("M1CH3")) && (int)(Time.deltaTime * engineRB.velocity.magnitude * 5) == 0)
        {
            StartCoroutine(ShuntingUnCoupleFromComposition()); 
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
        auto.RunAutoDrive(trainEngine, trainEngineT, before, 80, false);                
        step0 = true;
    }



    IEnumerator ShuntingFrom2to7()
    {
        step0 = false;
       // auto.AutoDriveOn = false;
        tlm.MakeRouteIfPossible(CH2, M1);        
        auto.RunAutoDrive(engine, engineT, behind, 40, false);
        step1 = true;
        yield return null;
    }

    IEnumerator ShuntingFrom7to3()
    {
        step1 = false;  
        yield return new WaitForSecondsRealtime(3f);
        tlm.MakeRouteIfPossible(M1, CH3);
        auto.RunAutoDrive(engine, engineT, car, 25, true);
        yield return new WaitForSecondsRealtime(1f);
        step2 = true;

    }

    IEnumerator ShuntingUnCoupleFromComposition()
    {
        yield return new WaitForSecondsRealtime(2f);
        step2 = false;        
        auto.RunAutoDrive(engine, engineT, ch3Trigger, 25, false, uncoupleFromRS);
        yield return null;
    }
    
    public TrafficLights GetTrafficLightByName(string lightName)
    {
        return GameObject.Find(lightName).GetComponent<TrafficLights>();
    }


}
