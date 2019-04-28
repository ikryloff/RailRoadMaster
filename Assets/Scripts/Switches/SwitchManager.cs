using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>, IManageable
{

    private GameObject switchObject;
    [SerializeField]
    private GameObject[] indicators;
    public Switch[] Switches { get; private set; }
    private Renderer rend;
    public bool isSwitchModeOn;
    private TrafficLightsManager trafficLightsManager;
    MovableObject[] movables;

    public Dictionary<string, Switch> SwitchDict { get; set; }

    

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
        Switches = FindObjectsOfType<Switch>();
        SwitchesInitilisation();
        trafficLightsManager = GameObject.Find("TrafficLightsManager").GetComponent<TrafficLightsManager>();
        indicators = GameObject.FindGameObjectsWithTag("Indication");
    }

    private void SwitchesInitilisation()
    {
        SwitchDict = new Dictionary<string, Switch>();
        foreach (Switch sw in Switches)
        { 
            // save Switch in dictionary
            SwitchDict.Add(sw.name, sw);
            //initialize
            sw.Init();
        }
        SwitchDict.Add("", null);

    }

    void Start () {
        IsSwitchModeOn = true;             
        RunIndicationMode();
    }
	
	void Update ()
    {
        TurnHandSwitchListener();

    }

    private void TurnHandSwitchListener()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 click = Vector3.one;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    click = hit.point;
                }

                //print("hit " + hit.collider.name);
                if (hit.collider != null && hit.collider.tag == "Lever")
                {
                    switchObject = hit.collider.transform.parent.gameObject;
                    Switch sw = switchObject.GetComponent<Switch>();
                    sw.SetSwitchDirection(Switch.SwitchDir.Change);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RunIndicationMode();
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
   

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }
}
