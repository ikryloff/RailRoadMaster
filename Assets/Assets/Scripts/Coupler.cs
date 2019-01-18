using UnityEngine;

public class Coupler : MonoBehaviour
{
    [SerializeField]
    private Coupler otherCoupler;
    [SerializeField]
    private Coupler passiveToThisCoupler;
    [SerializeField]
    private Texture2D cursor;
    private Rigidbody2D otherCouplerRB;
    private RollingStock otherRollingStock;
    private Rigidbody2D otherRollingStockRB;
    public Transform pos;
    private RollingStock rollingStock;
    private Rigidbody2D rollingStockRB;
    private bool isActiveCoupler;
    [SerializeField]
    private bool isPassiveCoupleConnected;
    private FixedJoint2D jointCar;    
    [SerializeField]
    private Coupler connectedToActive;
    private CompositionManager cm;
    public CouplerManager cpm;
    Vector2 position;

    void Awake()
    {
                
        rollingStock = transform.parent.GetComponent<RollingStock>();
        rollingStockRB = rollingStock.GetComponent<Rigidbody2D>();
        if (IsActiveCoupler)
        {
            if (OtherCoupler)
            {
                PassiveToThisCoupler = OtherCoupler;
                otherCouplerRB = OtherCoupler.GetComponent<Rigidbody2D>();
                otherRollingStock = OtherCoupler.transform.parent.GetComponent<RollingStock>();
                jointCar = gameObject.AddComponent<FixedJoint2D>();
                jointCar.connectedBody = otherCouplerRB;
                jointCar.anchor = new Vector2(1, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;
                jointCar.frequency = 0;
                jointCar.dampingRatio = 0;
                otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();
                ConnectedToActive = PassiveToThisCoupler;                
                PassiveToThisCoupler.IsPassiveCoupleConnected = true;
                PassiveToThisCoupler.OtherCoupler = this;
            }

        }       
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();        
        cpm = GameObject.Find("CouplerManager").GetComponent<CouplerManager>();
        position = transform.localPosition;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsActiveCoupler)
        {
            ContactPoint2D hitPoint = collision.contacts[0];            
            if (collision.gameObject.tag == "PassiveCoupler" && collision.relativeVelocity.magnitude > 0.1)
            {
                PassiveToThisCoupler = collision.gameObject.GetComponent<Coupler>();
                PassiveToThisCoupler.IsPassiveCoupleConnected = true;
                PassiveToThisCoupler.OtherCoupler = this;

                otherCouplerRB = collision.gameObject.GetComponent<Rigidbody2D>();                
                otherRollingStock = otherCouplerRB.transform.parent.GetComponent<RollingStock>();
                otherRollingStockRB = otherRollingStock.GetComponent<Rigidbody2D>();
                if (hitPoint.point.x - gameObject.transform.position.x > 0)
                {
                    jointCar = gameObject.AddComponent<FixedJoint2D>();
                    jointCar.connectedBody = otherCouplerRB;
                    jointCar.anchor = new Vector2(1, 0); //hardcoded joint point                
                    jointCar.autoConfigureConnectedAnchor = true;
                    jointCar.frequency = 0;
                    jointCar.dampingRatio = 0;
                    otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();
                    OtherCoupler = otherCouplerRB.GetComponent<Coupler>();
                    ConnectedToActive = PassiveToThisCoupler;
                    
                    

                    if (cpm.IsCouplerModeIsOn)
                    {
                        //Magic reset Couplermode                        
                        cpm.ResetUncoupleMode();
                        
                    }
                }
            }
            cm.UpdateCompositionsInformation();
        }
    }


    public FixedJoint2D JointCar
    {
        get
        {
            return jointCar;
        }

    }

    private void Start()
    {
        InvokeRepeating("UpdateCoupleralignment", 1f, 1f);  //1s delay, repeat every 1s
        
        
    }

    private void FixedUpdate()
    {
        if (!OtherCoupler)
        {
            gameObject.transform.rotation = rollingStock.transform.rotation;
            transform.localPosition = position;
        }
        

        
        
       
        
    }

    private void UpdateCoupleralignment()
    {
        if (OtherCoupler)
        {
            OtherCoupler.transform.rotation = gameObject.transform.rotation;            
        }
       
    }    


    public bool IsActiveCoupler
    {
        get
        {
            if (tag == "ActiveCoupler")
                return true;
            else
                return false;
        }

        set
        {
            isActiveCoupler = value;
        }
    }

    public Coupler OtherCoupler
    {
        get
        {
            return otherCoupler;
        }

        set
        {
            otherCoupler = value;
        }
    }

    public Coupler ConnectedToActive
    {
        get
        {
            return OtherCoupler;
        }

        set
        {
            connectedToActive = value;
        }
    }

    public bool IsPassiveCoupleConnected
    {
        get
        {
            return isPassiveCoupleConnected;
        }

        set
        {
            isPassiveCoupleConnected = value;
        }
    }

    public Coupler PassiveToThisCoupler
    {
        get
        {
            return passiveToThisCoupler;
        }

        set
        {
            passiveToThisCoupler = value;
        }
    }

    public void Uncouple()
    {
        if (JointCar)
        {
            OtherCoupler.transform.parent.GetComponent<RollingStock>().ConnectedToPassive = null;
            OtherCoupler.IsPassiveCoupleConnected = false;            
            PassiveToThisCoupler.OtherCoupler = null;
            OtherCoupler = null;
            Destroy(JointCar);
            cm.UpdateCompositionsInformation();
        }            
    }

}
