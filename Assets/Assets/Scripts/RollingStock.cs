using System.Collections.Generic;
using UnityEngine;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody2D rollingStockRB;
    [SerializeField]
    private string number;
    string consist;
    private Couple activeCouple;
    private Couple passiveCouple;
    private Couple connectedToPassive;   

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

    public Couple ConnectedToPassive
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

    public void GetConsistsNumber()
    {
        consist = "";
        if (!rollingStock.ConnectedToPassive)
        {
            Debug.Log("consist is " + ConsistsNumber(rollingStock));
        }
        

/*
        if (activeCouple.ConnectedToActive)
            Debug.Log(Number + " connected to " + activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number);
        else
            Debug.Log(Number + " is single");

        if (ConnectedToPassive)
        {
            Debug.Log(Number + " passive connected to " + ConnectedToPassive.transform.parent.GetComponent<RollingStock>().Number);
        }
        else
            Debug.Log(Number + " no passive connection"); */

    }

    private void Start()
    {
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody2D>();        
        activeCouple = transform.GetChild(0).GetComponent<Couple>();
        passiveCouple = transform.GetChild(1).GetComponent<Couple>();

    }
    
    private string ConsistsNumber(RollingStock rollingStock)
    {
        if (!rollingStock.activeCouple.ConnectedToActive)
            return rollingStock.Number;
        consist += rollingStock.activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number + "-";
        ConsistsNumber(rollingStock.activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>());
        
        return rollingStock.Number + "-" + consist;
        
                
    }
    

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetConsistsNumber();            
            
        }
            
        //railstock friction
        if (rollingStockRB.velocity.x > 0)
        {
            rollingStockRB.AddRelativeForce(new Vector2(-100f, 0), ForceMode2D.Force);            
        }
            
        else if (rollingStockRB.velocity.x < 0)
            rollingStockRB.AddRelativeForce(new Vector2(100, 0), ForceMode2D.Force);

    } 
   
}