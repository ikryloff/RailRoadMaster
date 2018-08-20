using System.Collections.Generic;
using UnityEngine;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody2D rollingStockRB;
    [SerializeField]
    private string number;    
    private int compositionNumberofRS;
    
    private Coupler activeCoupler;
    private Coupler passiveCoupler;
    private Coupler connectedToPassive;   

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

    private void Start()
    {
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody2D>();        
        activeCoupler = transform.GetChild(0).GetComponent<Coupler>();
        passiveCoupler = transform.GetChild(1).GetComponent<Coupler>();

    }
    
  

    void FixedUpdate()
    {

        //railstock friction
        if (rollingStockRB.velocity.x > 0)
        {
            rollingStockRB.AddRelativeForce(new Vector2(-100f, 0), ForceMode2D.Force);            
        }
            
        else if (rollingStockRB.velocity.x < 0)
            rollingStockRB.AddRelativeForce(new Vector2(100, 0), ForceMode2D.Force);

    } 
   
}