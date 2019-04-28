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

   

    public void Couple(Coupler passiveCoupler)
    {
        Rigidbody pass = passiveCoupler.GetComponent<Rigidbody>();
        jointCar = gameObject.AddComponent<HingeJoint>();
        jointCar.connectedBody = pass;
        
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
