using System.Collections.Generic;
using System.Linq;
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
    private HingeJoint jointCar;
    [SerializeField]
    private Coupler connectedToActive;
    private CompositionManager cm;
    public CouplerManager cpm;
    
    // which is of 2 couplers (-1 = left; +1 = right)
    public int couplerPos;

    void Awake()
    {
                
        rollingStock = transform.parent.GetComponent<RollingStock>();
        rollingStockRB = rollingStock.GetComponent<Rigidbody2D>();   
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();        
        cpm = GameObject.Find("CouplerManager").GetComponent<CouplerManager>();

           
        

    }

    private void Start()
    {
        
    }

    public void Couple(Coupler passiveCoupler)
    {
        Rigidbody pass = passiveCoupler.GetComponent<Rigidbody>();
        jointCar = gameObject.AddComponent<HingeJoint>();
        jointCar.connectedBody = pass;
        
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
                    OtherCoupler = otherCouplerRB.GetComponent<Coupler>();  
                    jointCar.autoConfigureConnectedAnchor = false;                   
                    otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();                    
                    ConnectedToActive = PassiveToThisCoupler;                    
                    

                    if (cpm.IsCouplerModeIsOn)
                    {
                        //Magic reset Couplermode                        
                        cpm.ResetUncoupleMode();
                        
                    }
                }
            }

        }
    }


   
    private void Update()
    {
        
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
        
    }

}
