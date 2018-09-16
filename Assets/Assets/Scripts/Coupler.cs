﻿using UnityEngine;

public class Coupler : MonoBehaviour
{
    [SerializeField]
    private Coupler otherCoupler;
    [SerializeField]
    private Texture2D cursor;
    private Rigidbody2D otherCouplerRB;
    private RollingStock otherRollingStock;
    private RollingStock rollingStock;
    private bool isActiveCoupler;
    [SerializeField]
    private bool isPassiveCoupleConnected;
    private HingeJoint2D jointCar;
    [SerializeField]
    private Coupler connectedToActive;
    private CompositionManager cm;
    public CouplerManager cpm;

    void Awake()
    {
                
        rollingStock = transform.parent.GetComponent<RollingStock>();
        if (IsActiveCoupler)
        {
            if (OtherCoupler)
            {
                otherCouplerRB = OtherCoupler.GetComponent<Rigidbody2D>();
                otherRollingStock = OtherCoupler.transform.parent.GetComponent<RollingStock>();
                jointCar = gameObject.AddComponent<HingeJoint2D>();
                jointCar.connectedBody = otherCouplerRB;
                jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                jointCar.autoConfigureConnectedAnchor = true;
                otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();
                ConnectedToActive = OtherCoupler;
                OtherCoupler.IsPassiveCoupleConnected = true;
            }

        }
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();        
        cpm = GameObject.Find("CouplerManager").GetComponent<CouplerManager>();        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsActiveCoupler)
        {
            ContactPoint2D hitPoint = collision.contacts[0];
            if (collision.gameObject.tag == "PassiveCoupler" && collision.relativeVelocity.magnitude > 20)
            {
                collision.gameObject.GetComponent<Coupler>().IsPassiveCoupleConnected = true;
                otherCouplerRB = collision.gameObject.GetComponent<Rigidbody2D>();
                otherRollingStock = otherCouplerRB.transform.parent.GetComponent<RollingStock>();

                if (hitPoint.point.x - gameObject.transform.position.x > 0)
                {
                    jointCar = gameObject.AddComponent<HingeJoint2D>();
                    jointCar.connectedBody = otherCouplerRB;
                    jointCar.anchor = new Vector2(10, 0); //hardcoded joint point                
                    jointCar.autoConfigureConnectedAnchor = true;
                    otherRollingStock.ConnectedToPassive = gameObject.GetComponent<Coupler>();
                    OtherCoupler = otherCouplerRB.GetComponent<Coupler>();
                    ConnectedToActive = OtherCoupler;
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


    public HingeJoint2D JointCar
    {
        get
        {
            return jointCar;
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

    public void Uncouple()
    {
        if (JointCar)
        {
            OtherCoupler.transform.parent.GetComponent<RollingStock>().ConnectedToPassive = null;
            OtherCoupler.IsPassiveCoupleConnected = false;
            OtherCoupler = null;
            Destroy(JointCar);
            cm.UpdateCompositionsInformation();
        }            
    }

}
