using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : Singleton<SwitchManager>
{

    private GameObject switchObject;
    private GameObject[] indicators;
    private Renderer rend;
    private bool isSwitchModeOn;
    [SerializeField]
    private RemoteControlScript rcs;

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

    void Start () {
        IsSwitchModeOn = true;
        indicators = GameObject.FindGameObjectsWithTag("Indication");
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
                rend.gameObject.SetActive(true);
            else
                rend.gameObject.SetActive(false);
        }
    }




}
