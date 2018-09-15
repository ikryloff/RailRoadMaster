using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TrafficLights CH2, M1, CH3, N, N3, M5;
    public Route route;
    public Switch sw19;
    public Switch sw21;
    public TrafficLightsManager tlm;
    public Engine trainEngine;
    public Engine engine;
    public Transform behind;
    public Transform before;
    public Transform car;
    public Transform ch3Transform;
    public Transform n3Transform;
    public Transform m1Transform;
    public Transform platform13End;
    public Transform switch21Transform;
    public Transform trainEngineT;
    public Transform engineT;    
    public RollingStock uncoupleFromRS;
    public RollingStock carUncoupleFromRS;
    Rigidbody2D trainEngineRB;
    Rigidbody2D engineRB;
    CameraController cc;
    bool step0 = false;
    bool step1 = false;
    bool step2 = false;
    bool step3 = false;
    bool step4 = false;
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
        M5 = GetTrafficLightByName("M5");
        CH3 = GetTrafficLightByName("CH3");
        ch3Transform = CH3.transform;
        n3Transform = N3.transform;
        m1Transform = M1.transform;
        cc = Camera.main.GetComponent<CameraController>();
        


    }

    private void Start()
    {
        
        //StartCoroutine(TrainArriving());
        //cc.CameraTarget = trainEngineRB;


    }
    

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            cc.MyUpdate = cc.MyUpdate != true ? true : false;
            Time.timeScale = Time.timeScale != 8 ? 8 : 1;
        }

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
        if (step3 && !auto.Runs.Contains(4) &&  (int)(Time.deltaTime * engineRB.velocity.magnitude * 5) == 0)
        {
            StartCoroutine(ShuntingFrom3To13());
        }
        if (step4 && !auto.Runs.Contains(5) && (int)(Time.deltaTime * engineRB.velocity.magnitude * 5) == 0)
        {
            StartCoroutine(ShuntingUnCoupleEngineFromComposition());
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
        auto.RunAutoDrive(1, trainEngine, trainEngineT, n3Transform, 200,  80, false);                
        step0 = true;
    }



    IEnumerator ShuntingFrom2to7()
    {
        step0 = false;
        cc.CameraTarget = engineRB;
        tlm.MakeRouteIfPossible(CH2, M1);        
        auto.RunAutoDrive(2, engine, engineT, m1Transform, 300,  40, false);
        step1 = true;
        yield return null;
    }

    IEnumerator ShuntingFrom7to3()
    {
        step1 = false;  
        yield return new WaitForSecondsRealtime(3f);
        tlm.MakeRouteIfPossible(M1, CH3);
        auto.RunAutoDrive(3, engine, engineT, car, 0,  25, true);
        yield return new WaitForSecondsRealtime(1f);
        step2 = true;

    }

    IEnumerator ShuntingUnCoupleFromComposition()
    {
        step2 = false;
        yield return new WaitForSecondsRealtime(2f);               
        auto.RunAutoDrive(4, engine, engineT, ch3Transform, -700, 25, false, uncoupleFromRS);
        step3 = true;

    }

    IEnumerator ShuntingFrom3To13()
    {
        step3 = false;
        yield return new WaitForSecondsRealtime(2f);
        tlm.MakeRouteIfPossible(CH3, M5);
        sw19.DirectionTurn();
        sw21.DirectionTurn();
        auto.RunAutoDrive(5, engine, engineT, platform13End, 100, 40, false);
        yield return null;
        step4 = true;
    }

    IEnumerator ShuntingUnCoupleEngineFromComposition()
    {
        step4 = false;
        yield return new WaitForSecondsRealtime(2f);        
        auto.RunAutoDrive(6, engine, engineT, switch21Transform, 200, 25, false, carUncoupleFromRS);
        yield return null;        

    }


    public TrafficLights GetTrafficLightByName(string lightName)
    {
        return GameObject.Find(lightName).GetComponent<TrafficLights>();
    }


}
