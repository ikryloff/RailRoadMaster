using System.Collections.Generic;
using UnityEngine;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody2D rollingStockRB;
    [SerializeField]
    private string number;    
    private int compositionNumberofRS;
    private string compositionNumberString;
    public bool brakes = true;    
    private Coupler activeCoupler;
    private Coupler passiveCoupler;
    private Coupler connectedToPassive; 
    [SerializeField]
    private TrackCircuit trackCircuit;
    public float breakeForce;
    public GameObject fork;

    public string Number
    {
        get
        {
            return number;
        }

        set
        {
            number = value;
        }
    }

    public Coupler ConnectedToPassive
    {
        get
        {
            return connectedToPassive;
        }

        set
        {
            connectedToPassive = value;
        }
    }

    public Coupler ActiveCoupler
    {
        get
        {
            return activeCoupler;
        }

        set
        {
            activeCoupler = value;
        }
    }

    public Coupler PassiveCoupler
    {
        get
        {
            return passiveCoupler;
        }

        set
        {
            passiveCoupler = value;
        }
    }

    public int CompositionNumberofRS
    {
        get
        {
            return compositionNumberofRS;
        }

        set
        {
            compositionNumberofRS = value;
        }
    }

    public bool Brakes
    {
        get
        {
            return brakes;
        }

        set
        {
            brakes = value;
        }
    }

    public string CompositionNumberString
    {
        get
        {
            return compositionNumberString;
        }

        set
        {
            compositionNumberString = value;
        }
    }

    public TrackCircuit TrackCircuit
    {
        get
        {
            return trackCircuit;
        }

        set
        {
            trackCircuit = value;
        }
    }

    private void Start()
    {
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody2D>();        
        ActiveCoupler = transform.GetChild(0).GetComponent<Coupler>();
        PassiveCoupler = transform.GetChild(1).GetComponent<Coupler>();
        fork = rollingStock.transform.Find("Fork").gameObject;
        Brakes = true;
    }
    

   

    void FixedUpdate()
    {

        if (Brakes)
        {
            
            if(breakeForce < 60)
                breakeForce += 0.9f;
            if (rollingStockRB.velocity.x > 0.3f)
                rollingStockRB.AddRelativeForce(new Vector2(-breakeForce, 0), ForceMode2D.Force);
            else if (rollingStockRB.velocity.x < -0.3f)
                rollingStockRB.AddRelativeForce(new Vector2(breakeForce, 0), ForceMode2D.Force);
            else
                rollingStockRB.velocity = new Vector2(0, 0);
            
        }
        else
        {
            breakeForce = 0;
            /*
            if (rollingStockRB.velocity.x > 0)
            {
                rollingStockRB.AddRelativeForce(new Vector2(-3f, 0), ForceMode2D.Force);
            }
            else if (rollingStockRB.velocity.x < 0)
                rollingStockRB.AddRelativeForce(new Vector2(3f, 0), ForceMode2D.Force);

            */
        }

    } 
   
}