using UnityEngine;

public class RollingStock : MonoBehaviour
{
    
    private Rigidbody2D rollingStock;    
    private void Start()
    {
        rollingStock = gameObject.GetComponent<Rigidbody2D>();        
        
    }

    private void Update()
    {
        //railstock friction
        if (rollingStock.velocity.x > 0)
            rollingStock.AddForce(new Vector2(-0.2f, 0), ForceMode2D.Force);
        else if (rollingStock.velocity.x < 0)
            rollingStock.AddForce(new Vector2(0.2f, 0), ForceMode2D.Force);

    } 
   
}