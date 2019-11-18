using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>, IManageable
{
    private Camera mainCamera;
    private GameObject switchObject;
    [SerializeField]
    private GameObject [] indicators;
    public Switch [] Switches { get; private set; }
    private Renderer rend;
    private TrafficLightsManager trafficLightsManager;
    MovableObject [] movables;

    public Dictionary<string, Switch> SwitchDict { get; set; }

    public void Init()
    {
        Switches = FindObjectsOfType<Switch> ();
        indicators = GameObject.FindGameObjectsWithTag ("Indication");
        SwitchesInitilisation ();
        trafficLightsManager = GameObject.Find ("TrafficLightsManager").GetComponent<TrafficLightsManager> ();
        mainCamera = FindObjectOfType<ConductorCameraController> ().GetComponent<Camera> ();
    }

    private void SwitchesInitilisation()
    {
        SwitchDict = new Dictionary<string, Switch> ();
        foreach ( Switch sw in Switches )
        {
            // save Switch in dictionary
            SwitchDict.Add (sw.name, sw);
            //initialize
            sw.Init ();
        }
        SwitchDict.Add ("", null);

    }


    void Update()
    {
        TurnHandSwitchListener ();

    }

    private void TurnHandSwitchListener()
    {
        if ( !EventSystem.current.IsPointerOverGameObject () )
        {
            Vector3 click = Vector3.one;

            if ( Input.GetMouseButtonDown (0) )
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if ( Physics.Raycast (ray, out hit) )
                {
                    click = hit.point;
                }

                //print("hit " + hit.collider.name);
                if ( hit.collider != null && hit.collider.CompareTag ("Lever") )
                {
                    switchObject = hit.collider.transform.parent.gameObject;
                    Switch sw = switchObject.GetComponent<Switch> ();
                    sw.SetSwitchDirection (Switch.SwitchDir.Change);
                }
            }
        }
    }


    public void OnStart()
    {

    }
}
