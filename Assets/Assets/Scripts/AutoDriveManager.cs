using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveManager : Singleton<AutoDriveManager> {

    public Engine engine;
    private Rigidbody2D engineRB;
    public Transform composition;
    public Transform aim;
    private float distance;
    bool autoDriveOn = true;
    bool upd = true;
    private int maxSpeed;
    private int direction;
    float koef;

    public CompositionManager cm;

    public float Distance
    {
        get
        {
            return composition.position.x - aim.position.x;
        }

        set
        {
            distance = value;
        }
    }

    public int MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public int Direction
    {
        get
        {
            if (Distance > 0)
                return -1;
            else
                return 1;
        }

        set
        {
            direction = value;
        }
    }



   
    void Start () {
        engineRB = engine.GetComponent<Rigidbody2D>();
        MaxSpeed = 60;
        engine.ControllerPosition = 70;
        
        

    }
	
	// Update is called once per frame
	void Update () {

        if (upd)
        {
            upd = false;
            cm.UpdateCompositionsInformation();
            koef = cm.GetNumberOfRSFromCompositionNumber(engine.GetComponent<RollingStock>().CompositionNumberofRS);
            engine.BrakesPower = koef * 0.7f; 
            Debug.Log("Brr  " + engine.BrakesPower);
        }



        if (autoDriveOn)
        {
            
            if ((int)(Time.deltaTime * engineRB.velocity.magnitude * 5) > MaxSpeed)
            {
                engine.engineControllerUseBrakes();
                Debug.Log("More  " + (MaxSpeed - (int)(Time.deltaTime * engineRB.velocity.magnitude * 5)));
            }
            else
            {
                if (Mathf.Abs(Distance) > 3000)
                {
                    engine.ControllerPosition = 8 * Direction * koef;
                    MaxSpeed = 40;
                }                    
                if (Mathf.Abs(Distance) < 3000 && Mathf.Abs(Distance) > 500)
                {
                    MaxSpeed = 25;
                    engine.ControllerPosition = 4 * Direction * koef;
                }                    
                if (Mathf.Abs(Distance) < 700 && Mathf.Abs(Distance) > 30)
                {
                    MaxSpeed = 10;
                    engine.ControllerPosition = 1 * Direction * koef;
                }                    
                if (Mathf.Abs(Distance) < 30)
                    engine.engineControllerUseBrakes();
            }
        }

        
	}
   
}
