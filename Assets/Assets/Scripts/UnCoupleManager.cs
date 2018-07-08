using UnityEngine;

public class UnCoupleManager : Singleton<UnCoupleManager> {

    private GameObject car;
    private Couple couple;    
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "ActiveCouple")
            {
                couple = hit.collider.GetComponent<Couple>();
                if (couple.JointCar)
                {
                    couple.PassiveCoupleObj = null;                    
                    Destroy(couple.JointCar);
                }
                                   
            }
        }
    }
}
