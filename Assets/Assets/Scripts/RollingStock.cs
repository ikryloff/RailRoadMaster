using System.Collections.Generic;
using UnityEngine;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody2D rollingStockRB;
    [SerializeField]
    private string number;
    [SerializeField]
    private CompositionManager cm;
    private string compositionNumber;
    string composition;
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

    public string CompositionNumber
    {
        get
        {
            return compositionNumber;
        }

        set
        {
            compositionNumber = value;
        }
    }

    public void GetCompositionNumber()
    {
        composition = "";
        if (!rollingStock.ConnectedToPassive)
        {
           // Debug.Log("composition is " + CompositionNumberFromCars(rollingStock));
            CompositionNumber = CompositionNumberFromCars(rollingStock);
            cm.Compositions.Add(CompositionNumber);
        }
        
    }

    private void Start()
    {
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody2D>();        
        activeCouple = transform.GetChild(0).GetComponent<Couple>();
        passiveCouple = transform.GetChild(1).GetComponent<Couple>();

    }
    
    private string CompositionNumberFromCars(RollingStock rs)
    {
        if (!rs.activeCouple.ConnectedToActive)
            return rs.Number;
        composition += rs.activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number;
        CompositionNumberFromCars(rs.activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>());
        return rs.Number + composition;
    }
    

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetCompositionNumber();            
            
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