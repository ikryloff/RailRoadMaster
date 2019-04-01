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
    private HingeJoint2D jointCar;
    [SerializeField]
    private Coupler connectedToActive;
    private CompositionManager cm;
    public CouplerManager cpm;

    public float offset;
    public float distance;
    public TrackPathUnit mathTemp;
    public TrackPath trackPath;
    public List<TrackPathUnit> ownTrackPath;    
    Transform couplerTransform;
    Transform rsTransform;
    
   
    Vector3 dir;
    float angle;
    float pointDist;
    // which is of 2 couplers (-1 = left; +1 = right)
    public int couplerPos;

    void Awake()
    {
                
        rollingStock = transform.parent.GetComponent<RollingStock>();
        rollingStockRB = rollingStock.GetComponent<Rigidbody2D>();
        rsTransform = rollingStock.GetComponent<Transform>();        
        
        couplerTransform = gameObject.transform;        
        couplerPos = offset > 0 ? 1 : -1;        
        if (IsActiveCoupler)
        {
            if (OtherCoupler)
            {
                PassiveToThisCoupler = OtherCoupler;
                otherCouplerRB = OtherCoupler.GetComponent<Rigidbody2D>();
                otherRollingStock = OtherCoupler.transform.parent.GetComponent<RollingStock>();
                jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = otherCouplerRB;
                jointCar.anchor = new Vector2(1, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = false;                
                otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();
                ConnectedToActive = PassiveToThisCoupler;                
                PassiveToThisCoupler.IsPassiveCoupleConnected = true;
                PassiveToThisCoupler.OtherCoupler = this;
                otherRollingStock.fork.SetActive(false);
            }

        }       
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();        
        cpm = GameObject.Find("CouplerManager").GetComponent<CouplerManager>();

        trackPath = FindObjectOfType<TrackPath>();      
        

    }

    private void Start()
    {
        distance = rollingStock.distance + offset;
        mathTemp = rollingStock.mathTemp;
        mathTemp.coupler = this;        
        

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
                    jointCar = gameObject.AddComponent<HingeJoint2D>();
                    jointCar.connectedBody = otherCouplerRB;
                    jointCar.anchor = new Vector2(1, 0); //hardcoded joint point                
                    jointCar.autoConfigureConnectedAnchor = false;                   
                    otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();                    
                    ConnectedToActive = PassiveToThisCoupler;
                    otherRollingStock.fork.SetActive(false);
                    

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


   
    private void Update()
    {

        distance += rollingStock.force;

        if (mathTemp)
        {
            
                               
            couplerTransform.position = mathTemp.math.CalcPositionByDistance(distance, true);
            
            ownTrackPath = rollingStock.ownTrackPath;


            if (rollingStock.force > 0 && mathTemp.math.GetDistance() - distance < 0.1)
            {
                mathTemp.coupler = null;
                mathTemp = trackPath.GetNextTrack(mathTemp, ownTrackPath);
                if (mathTemp)
                {
                    distance = 0;
                }
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.Last();
                    distance = mathTemp.math.GetDistance();
                }
            }
            if (rollingStock.force < 0 && distance < 0.1)
            {
                mathTemp.coupler = null;
                mathTemp = trackPath.GetPrevTrack(mathTemp, ownTrackPath);
                if (mathTemp)
                {
                    distance = mathTemp.math.GetDistance();
                }
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.First();
                    distance = 0;
                }
            }
            mathTemp.coupler = this;            
        }
        else
        {
            rollingStock.isMoving = 0;
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
        
    }

}
