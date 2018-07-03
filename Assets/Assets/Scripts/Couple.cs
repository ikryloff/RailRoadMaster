using UnityEngine;

public class Couple : MonoBehaviour {
    private GameObject activeCouple;
    private GameObject passiveCoupleObj;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {       

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D hitPoint = collision.contacts[0];
        if (collision.gameObject.tag == "Couple")
        {
            passiveCoupleObj = collision.gameObject;
            Rigidbody2D passiveCouple = passiveCoupleObj.GetComponent<Rigidbody2D>();

            if (hitPoint.point.x - gameObject.transform.position.x > 0)
            {
                HingeJoint2D jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = passiveCouple;
                jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;

            }
        }
    }
}
