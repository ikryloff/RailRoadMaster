using UnityEngine;

public class SwitchManager : Singleton<GameManager>
{

    private GameObject switchObject;
    private GameObject[] indicators;
    private Renderer rend;
    private bool isSwitchModeOn;

    void Start () {
        isSwitchModeOn = false;
        switchMode(isSwitchModeOn);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
                     
            if (hit.collider.tag == "Lever" && isSwitchModeOn)
            {
               
                switchObject = hit.collider.transform.parent.parent.gameObject;
                Switch sw= switchObject.GetComponent<Switch>();
                sw.changeDirection();
                
            }               
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSwitchModeOn = isSwitchModeOn ? false : true;
        }
        switchMode(isSwitchModeOn);
    }

    void switchMode(bool isShow)
    {
        indicators = GameObject.FindGameObjectsWithTag("Lever");
        foreach (GameObject item in indicators)
        {
            rend = item.GetComponent<Renderer>();
            rend.enabled = isShow ? true : false;
        }
    }

}
