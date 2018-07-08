using UnityEngine;

public class RollingStock : MonoBehaviour
{
    
    private Rigidbody2D rollingStock;    
    private void Start()
    {
        rollingStock = gameObject.GetComponent<Rigidbody2D>();        
        
    }

    void FixedUpdate()
    {
        //railstock friction
        if (rollingStock.velocity.x > 0)
            rollingStock.AddRelativeForce(new Vector2(-100f, 0), ForceMode2D.Force);
        else if (rollingStock.velocity.x < 0)
            rollingStock.AddRelativeForce(new Vector2(100, 0), ForceMode2D.Force);

    } 
   
}