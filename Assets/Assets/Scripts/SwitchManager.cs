using UnityEngine;

public class SwitchManager : Singleton<GameManager>
{

    private GameObject switchObject;
    private GameObject[] ind;
    private Renderer renderer;
    private bool isShowIndicators;

    void Start () {
        isShowIndicators = true;
        showIndicators(isShowIndicators);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
                     
            if (hit.collider.tag == "Lever")
            {
               
                switchObject = hit.collider.transform.parent.parent.gameObject;
                Switch sw= switchObject.GetComponent<Switch>();
                sw.changeDirection();
                
            }               
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShowIndicators = isShowIndicators ? false : true;
        }
        showIndicators(isShowIndicators);
    }

    void showIndicators(bool isShow)
    {
        ind = GameObject.FindGameObjectsWithTag("Lever");
        foreach (GameObject item in ind)
        {
            renderer = item.GetComponent<Renderer>();
            renderer.enabled = isShow ? true : false;
        }
    }

}
