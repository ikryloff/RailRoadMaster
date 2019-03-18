using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>
{

    private GameObject switchObject;
    [SerializeField]
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
    public PathMaker pathMaker;
  

    


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
        indicators = GameObject.FindGameObjectsWithTag("Indication");
        route = GameObject.Find("Route").GetComponent<Route>();
        foreach (GameObject sw in switchObj)
        {
            sw.layer = 2;
        }
    }
    void Start () {
        IsSwitchModeOn = true;
             
        RunIndicationMode();
    }
	
	void Update ()
    {       

        if (!rcs.IsRemoteControllerOn)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 click = Vector3.one;

                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast (ray, out hit))
                    {
                        click = hit.point;
                    }
                    
                    //print("hit " + hit.collider.name);
                    if (hit.collider != null && hit.collider.tag == "Lever")
                    {
                        switchObject = hit.collider.transform.parent.gameObject;
                        Switch sw = switchObject.GetComponent<Switch>();
                        sw.ChangeDirection();
                        pathMaker.GetFullPath(engine.direction);
                        engine.GetAllExpectedCarsByDirection(engine.direction);
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
            
            if (IsSwitchModeOn)
            {                
                item.gameObject.SetActive(true);                
            }
            else
            {
                item.gameObject.SetActive(false);
            }
                
        }
    }
    

}
