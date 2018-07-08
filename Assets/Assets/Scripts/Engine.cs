using UnityEngine;

public class Engine : RollingStock {

    private Rigidbody2D engine;
    private bool brakes = true;
    private float controllerPosition = 0;
    // Use this for initialization
    void Start () {
        engine = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        engine.AddRelativeForce(new Vector2(500 * controllerPosition, 0), ForceMode2D.Force);        

        if (brakes)
        {
            if (engine.velocity.x > 2f)
                engine.AddRelativeForce(new Vector2(-2000, 0), ForceMode2D.Force);
            else if (engine.velocity.x < -2f)
                engine.AddRelativeForce(new Vector2(2000, 0), ForceMode2D.Force);
            else
                engine.velocity = new Vector2(0, 0);
        }        
    }

    public void engineControllerForward()    {
        
        if (controllerPosition < 8)
        {
            brakes = false;
            controllerPosition++;
        }
    }

    public void engineControllerBackwards()
    {
        if (controllerPosition > -8)
        {
            brakes = false;
            controllerPosition--;
        }
    }

    public void engineControllerReleaseAll()
    {
        controllerPosition = 0;
        brakes = false;
    }
    public void engineControllerUseBrakes()
    {
        controllerPosition = 0;
        brakes = true;
    }


}
