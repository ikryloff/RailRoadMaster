﻿using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    private Rigidbody2D engine;
    private bool brakes = true;
    private float controllerPosition = 0;
    [SerializeField]
    private Text speed;
    private float mSpeed;
    private float brakesPower;

    public float ControllerPosition
    {
        get
        {
            return controllerPosition;
        }

        set
        {
            controllerPosition = value;
        }
    }

    public float BrakesPower
    {
        get
        {
            return brakesPower;
        }

        set
        {
            brakesPower = value;
        }
    }

    // Use this for initialization
    void Start () {
        engine = gameObject.GetComponent<Rigidbody2D>();
        speed.text = "Speed: ";
        BrakesPower = 1;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        mSpeed = (int)(Time.deltaTime * engine.velocity.magnitude * 5);
        speed.text = "Speed: " + mSpeed;

        engine.AddRelativeForce(new Vector2(500 * controllerPosition, 0), ForceMode2D.Force);        

        if (brakes)
        {
            if (engine.velocity.x > 2f)
                engine.AddRelativeForce(new Vector2(-2000 * BrakesPower , 0), ForceMode2D.Force);
            else if (engine.velocity.x < -2f)
                engine.AddRelativeForce(new Vector2(2000 * BrakesPower, 0), ForceMode2D.Force);
            else
                engine.velocity = new Vector2(0, 0);
        }
        else
        {
            if (engine.velocity.x > 0)
            {
                engine.AddRelativeForce(new Vector2(-100f, 0), ForceMode2D.Force);
            }

            else if (engine.velocity.x < 0)
                engine.AddRelativeForce(new Vector2(100, 0), ForceMode2D.Force);
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
