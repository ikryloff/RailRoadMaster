using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>, IManageable
{

    private GameObject switchObject;
    [SerializeField]
    private GameObject[] indicators;
    public Switch[] switches;
    private Renderer rend;
    public bool isSwitchModeOn;
    [SerializeField]
    private RemoteControlScript rcs;
    [SerializeField]
    private Engine engine;
    private TrafficLightsManager trafficLightsManager;
    public PathMaker pathMaker;
    MovableObject[] movables;




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
    public void Init()
    {
        switches = FindObjectsOfType<Switch>();
        SwitchesInitilisation();
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        indicators = GameObject.FindGameObjectsWithTag("Indication");
        movables = FindObjectsOfType<MovableObject>();
    }

    private void SwitchesInitilisation()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            Switch sw = switches[i];
            sw.Init();
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

    public void UpdatePathAfterSwitch()
    {
        foreach (MovableObject item in movables)
        {
            item.UpdatePath();
        }
    }

}
