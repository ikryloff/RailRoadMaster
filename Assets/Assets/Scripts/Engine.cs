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
	void Update () {
        engine.AddForce(new Vector2(400 * controllerPosition, 0), ForceMode2D.Force);        

        if (brakes)
        {
            if (engine.velocity.x > 0.5f)
                engine.AddForce(new Vector2(-1000, 0), ForceMode2D.Force);
            else if (engine.velocity.x < -0.5f)
                engine.AddForce(new Vector2(1000, 0), ForceMode2D.Force);
            else
                engine.velocity = new Vector2(0, 0);
        }        
    }

    public void engineControllerForward()    {
        
        if (((controllerPosition >= 0 && engine.velocity.x >= 0) || (controllerPosition < 0 && engine.velocity.x < 0)) && controllerPosition < 8)
        {
            brakes = false;
            controllerPosition++;
        }
    }

    public void engineControllerBackwards()
    {
        if (((controllerPosition <= 0 && engine.velocity.x <= 0) || (controllerPosition > 0 && engine.velocity.x > 0)) && controllerPosition > -8)
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
