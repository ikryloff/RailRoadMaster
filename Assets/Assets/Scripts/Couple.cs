using UnityEngine;

public class Couple : MonoBehaviour {
    private GameObject activeCouple;
    [SerializeField]
    private GameObject passiveCoupleObj;
    [SerializeField]
    private Texture2D cursor;
    Rigidbody2D passiveCouple;
    private HingeJoint2D jointCar;
    void Awake()
    {
        if (passiveCoupleObj)
        {
            jointCar = gameObject.AddComponent<HingeJoint2D>();
            passiveCouple = passiveCoupleObj.GetComponent<Rigidbody2D>();
            jointCar.connectedBody = passiveCouple;
            jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
            jointCar.autoConfigureConnectedAnchor = true;
        }
        
    }
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D hitPoint = collision.contacts[0];
        if (collision.gameObject.tag == "PassiveCouple" && collision.relativeVelocity.magnitude > 10)
        {
            passiveCoupleObj = collision.gameObject;
            passiveCouple = passiveCoupleObj.GetComponent<Rigidbody2D>();

            if (hitPoint.point.x - gameObject.transform.position.x > 0)
            {
                jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = passiveCouple;
                jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;

            }
        }
    }

    
    public HingeJoint2D JointCar
    {
        get
        {
            return jointCar;
        }
        
    }
    
    public GameObject PassiveCoupleObj
    {
        get
        {
            return passiveCoupleObj;
        }

        set
        {
            passiveCoupleObj = value;
        }
    }
}
