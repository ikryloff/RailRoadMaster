using UnityEngine;

public class Couple : MonoBehaviour {
    [SerializeField]
    private Couple otherCouple;
    [SerializeField]
    private Texture2D cursor;
    private Rigidbody2D otherCoupleRB;
    private RollingStock otherRollingStock;
    private RollingStock rollingStock;
    private bool isActiveCouple;
    private HingeJoint2D jointCar;    
    private Couple connectedToActive;

    void Awake()
    {
        rollingStock = transform.parent.GetComponent<RollingStock>();
        if (IsActiveCouple)
        {
            if (OtherCouple)
            {
                otherCoupleRB = OtherCouple.GetComponent<Rigidbody2D>();
                otherRollingStock = OtherCouple.transform.parent.GetComponent<RollingStock>();               
                jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = otherCoupleRB;
                jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;
                otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Couple>();
                ConnectedToActive = OtherCouple;
            }           
            
        }
    }
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsActiveCouple)
        {
            ContactPoint2D hitPoint = collision.contacts[0];
            if (collision.gameObject.tag == "PassiveCouple" && collision.relativeVelocity.magnitude > 10)
            {
                otherCoupleRB = collision.gameObject.GetComponent<Rigidbody2D>();
                otherRollingStock = otherCoupleRB.transform.parent.GetComponent<RollingStock>();

                if (hitPoint.point.x - gameObject.transform.position.x > 0)
                {
                    jointCar = gameObject.AddComponent<HingeJoint2D>();
                    jointCar.connectedBody = otherCoupleRB;
                    jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                    jointCar.autoConfigureConnectedAnchor = true;
                    otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Couple>();
                    OtherCouple = otherCoupleRB.GetComponent<Couple>();
                    ConnectedToActive = OtherCouple;
                }
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
     

    public bool IsActiveCouple
    {
        get
        {
            if (tag == "ActiveCouple")
                return true;
            else
                return false;
        }

        set
        {
            isActiveCouple = value;
        }
    }

    public Couple OtherCouple
    {
        get
        {
            return otherCouple;
        }

        set
        {
            otherCouple = value;
        }
    }

    public Couple ConnectedToActive
    {
        get
        {
            return OtherCouple;
        }

        set
        {
            connectedToActive = value;
        }
    }

}
