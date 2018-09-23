using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>
{

    private GameObject switchObject;
    private GameObject[] indicators;
    public Switch[] switches;
    public GameObject[] switchObj;
    private Renderer rend;
    private Route route;
    private bool isSwitchModeOn;
    [SerializeField]
    private RemoteControlScript rcs;
    [SerializeField]
    private Engine engine;
    private TrafficLightsManager trafficLightsManager;   
    

    public bool IsSwitchModeOn
    {
        get
        {
            return isSwitchModeOn;
        }

        set
        {
            isSwitchModeOn = value;
        }
    }
    private void Awake()    {

        switches = FindObjectsOfType<Switch>();
        switchObj = GameObject.FindGameObjectsWithTag("RailSwitch");
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();        
        route = GameObject.Find("Route").GetComponent<Route>();
        foreach (GameObject sw in switchObj)
        {
            sw.layer = 2;
        }
    }
    void Start () {
        IsSwitchModeOn = true;
        indicators = GameObject.FindGameObjectsWithTag("Indication");
        UpdatePathEnds();
        RunIndicationMode();
    }
	
	void Update () {
        if (!rcs.IsRemoteControllerOn)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0) && IsSwitchModeOn)
                {
                    Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
                    if (hit.collider != null && hit.collider.tag == "Indication")
                    {

                        switchObject = hit.collider.transform.parent.gameObject;
                        Switch sw = switchObject.GetComponent<Switch>();
                        sw.ChangeDirection();
                        UpdatePathEnds();
                        route.MakePath();
                        engine.GetAllExpectedCarsByDirection(engine.Direction);
                        engine.GetExpectedCar();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                RunIndicationMode();
            }
            
        }
        
    }

    public void RunIndicationMode()
    {
        IsSwitchModeOn = IsSwitchModeOn ? false : true;
        foreach (GameObject item in indicators)
        {
            rend = item.GetComponent<Renderer>();
            if (IsSwitchModeOn)
            {
                foreach (GameObject sw in switchObj)
                {
                    sw.layer = 9;
                }
                rend.gameObject.SetActive(true);
                
            }

            else
            {
                foreach (GameObject sw in switchObj)
                {
                    sw.layer = 2;
                }
                rend.gameObject.SetActive(false);
            }
                
        }
    }

    public void UpdatePathEnds()
    {
        foreach (var _switch in switches)
        {
            if (_switch.name == "Switch_21")
            {
                if (_switch.IsSwitchStraight)
                {
                    route.GetTrackCircuitByName("Track_12").SetTrackLights(trafficLightsManager.GetTrafficLightByName("M5"), trafficLightsManager.GetTrafficLightByName("End12"));
                    route.GetTrackCircuitByName("Track_13").SetTrackLights(trafficLightsManager.GetTrafficLightByName("M5"), trafficLightsManager.GetTrafficLightByName("End12_13CH"));
                }
                else if (!_switch.IsSwitchStraight)
                {
                    route.GetTrackCircuitByName("Track_12").SetTrackLights(trafficLightsManager.GetTrafficLightByName("M5"), trafficLightsManager.GetTrafficLightByName("End12_13CH"));
                    route.GetTrackCircuitByName("Track_13").SetTrackLights(trafficLightsManager.GetTrafficLightByName("M5"), trafficLightsManager.GetTrafficLightByName("End12"));
                }

            }
        }
    }



}
