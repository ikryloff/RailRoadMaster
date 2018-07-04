using UnityEngine;

public class SwitchManager : Singleton<GameManager>
{

    private GameObject switchObject;
    private GameObject[] indicators;
    private Renderer rend;
    private bool isSwitchModeOn;    

    void Start () {
        isSwitchModeOn = false;
        indicators = GameObject.FindGameObjectsWithTag("Lever");
        RunSwitchMode(isSwitchModeOn);
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0) && isSwitchModeOn)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            
            if (hit.collider.tag == "Lever")
            {
               
                switchObject = hit.collider.transform.parent.gameObject;
                Switch sw = switchObject.GetComponent<Switch>();
                sw.changeDirection();                
            }   
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSwitchModeOn = isSwitchModeOn ? false : true;            
            RunSwitchMode(isSwitchModeOn);

        }
        
    }

    void RunSwitchMode(bool isShow)
    {
        foreach (GameObject item in indicators)
        {
            rend = item.GetComponent<Renderer>();
            if(isShow)
                rend.gameObject.SetActive(true);
            else
                rend.gameObject.SetActive(false);
        }
    }




}
