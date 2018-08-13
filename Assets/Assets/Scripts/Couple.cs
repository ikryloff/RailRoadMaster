using UnityEngine;

public class Couple : MonoBehaviour {
    
    [SerializeField]
    private GameObject passiveCoupleObj;
    [SerializeField]
    private Texture2D cursor;
    private Rigidbody2D otherPassiveCouple;
    private RollingStock otherRollingStock;
    private RollingStock rollingStock;
    private HingeJoint2D jointCar;

    void Awake()
    {
        rollingStock = transform.parent.GetComponent<RollingStock>();
        if (PassiveCoupleObj)
        {
            otherRollingStock = PassiveCoupleObj.transform.parent.GetComponent<RollingStock>();
            otherPassiveCouple = PassiveCoupleObj.GetComponent<Rigidbody2D>();
            jointCar = gameObject.AddComponent<HingeJoint2D>();            
            jointCar.connectedBody = otherPassiveCouple;
            jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
            jointCar.autoConfigureConnectedAnchor = true;
            otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Couple>();
            rollingStock.ConnectedToActive = PassiveCoupleObj;            
        }   
    }
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D hitPoint = collision.contacts[0];
        if (collision.gameObject.tag == "PassiveCouple" && collision.relativeVelocity.magnitude > 10)
        {
            PassiveCoupleObj = collision.gameObject;
            otherPassiveCouple = PassiveCoupleObj.GetComponent<Rigidbody2D>();
            otherRollingStock = PassiveCoupleObj.transform.parent.GetComponent<RollingStock>();

            if (hitPoint.point.x - gameObject.transform.position.x > 0)
            {
                jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = otherPassiveCouple;
                jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;
                otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Couple>();
                rollingStock.ConnectedToActive = PassiveCoupleObj;
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
