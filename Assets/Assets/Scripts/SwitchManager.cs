using UnityEngine;

public class SwitchManager : Singleton<GameManager>
{

    private GameObject switchObject;    
    // Use this for initialization
    void Start () {        

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            Debug.Log(hit.collider);            
            if (hit.collider.tag == "Lever")
            {
               
                switchObject = hit.collider.transform.parent.parent.gameObject;
                Switch sw= switchObject.GetComponent<Switch>();
                sw.changeDirection();
                
            }               
            
        }
    }
    
}
