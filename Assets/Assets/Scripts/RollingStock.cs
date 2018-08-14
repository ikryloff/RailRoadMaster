using UnityEngine;

public class RollingStock : MonoBehaviour
{
    private Rigidbody2D rollingStock;
    [SerializeField]
    private string number;
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
        
        if (activeCouple.ConnectedToActive)
            Debug.Log(Number + " connected to " + activeCouple.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number);
        else
            Debug.Log(Number + " is single");

        if (ConnectedToPassive)
        {
            Debug.Log(Number + " passive connected to " + ConnectedToPassive.transform.parent.GetComponent<RollingStock>().Number);
        }
        else
            Debug.Log(Number + " no passive connection");

    }

    private void Start()
    {
        rollingStock = GetComponent<Rigidbody2D>();        
        activeCouple = transform.GetChild(0).GetComponent<Couple>();
        passiveCouple = transform.GetChild(1).GetComponent<Couple>();

    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            GetConsistsNumber();
        //railstock friction
        if (rollingStock.velocity.x > 0)
        {
            rollingStock.AddRelativeForce(new Vector2(-100f, 0), ForceMode2D.Force);            
        }
            
        else if (rollingStock.velocity.x < 0)
            rollingStock.AddRelativeForce(new Vector2(100, 0), ForceMode2D.Force);

    } 
   
}