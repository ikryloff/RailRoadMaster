using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailRunObject : MonoBehaviour {

    public Engine engine;
    public Rigidbody2D engineRB;
    public float distance;
    public int maxSpeed;
    public Transform rollingStockTransform;
    public Transform aimPosition;
    public int direction;
    private int mSpeed;
    public bool mustCouple;
    public RollingStock rollingStock; 
    public string startCompositionNumberString;
    public CompositionManager cm;
    private float offset;
    private int railRunID;
    private AutoDriveManager adm;

    public float Distance
    {
        get
        {
            if (RollingStockTransform && AimPosition)
                return RollingStockTransform.position.x - AimPosition.position.x - Offset;
            else
                return 0;
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

    
    public Engine Engine
    {
        get
        {
            return engine;
        }

        set
        {
            engine = value;
        }
    }

    public Rigidbody2D EngineRB
    {
        get
        {
            return engineRB;
        }

        set
        {
            engineRB = value;
        }
    }

    public Transform AimPosition
    {
        get
        {
            return aimPosition;
        }

        set
        {
            aimPosition = value;
        }
    }

    public Transform RollingStockTransform
    {
        get
        {
            return rollingStockTransform;
        }

        set
        {
            rollingStockTransform = value;
        }
    }

    public bool MustCouple
    {
        get
        {
            return mustCouple;
        }

        set
        {
            mustCouple = value;
        }
    }

    public RollingStock RollingStock
    {
        get
        {
            return rollingStock;
        }

        set
        {
            rollingStock = value;
        }
    }

    public float Offset
    {
        get
        {
            return offset;
        }

        set
        {
            offset = value;
        }
    }

    public int RailRunID
    {
        get
        {
            return railRunID;
        }

        set
        {
            railRunID = value;
        }
    }

    public int MSpeed
    {
        get
        {
            return mSpeed;
        }

        set
        {
            mSpeed = value;
        }
    }

    void Start () {        
        EngineRB = Engine.GetComponent<Rigidbody2D>();
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
        adm = GameObject.Find("AutoDriveManager").GetComponent<AutoDriveManager>();
        cm.UpdateCompositionsInformation();
        startCompositionNumberString = RollingStock.CompositionNumberString;
    }
    	
	
	void FixedUpdate ()
    {
        //Debug.Log(RailRunID);
        //Debug.Log(RollingStock.CompositionNumberString);
        //Debug.Log("Mathf.Abs(Distance) " + Mathf.Abs(Distance));        
        if (EngineRB)
        {
            MSpeed = (int)(Time.deltaTime * EngineRB.velocity.magnitude * 5);
            //Debug.Log(Engine.ControllerPosition + " Dist " + Mathf.Abs(Distance));

        }
        
        if (MSpeed > MaxSpeed)
        {
            Engine.EngineControllerUseBrakes();
            //Debug.Log("More  " + (MaxSpeed - (int)(Time.deltaTime * EngineRB.velocity.magnitude * 5)));
        }
        else
        {
            Engine.ReleaseBrakes();
            if (Mathf.Abs(Distance) > 4000)
            {
                if (MSpeed < 10)
                    Engine.ControllerPosition = 1 * Direction;
                if(MSpeed >= 10 && MSpeed < 15)
                    Engine.ControllerPosition = 2 * Direction;
                if (MSpeed >= 15 && MSpeed < 25)
                    Engine.ControllerPosition = 4 * Direction;
                if (MSpeed >= 25 )
                    Engine.ControllerPosition = 8 * Direction;
                
                MaxSpeed = maxSpeed;
            }
            if (Mathf.Abs(Distance) < 4000 && Mathf.Abs(Distance) > 1500)
            {
                MaxSpeed = 25;
                Engine.ControllerPosition = 4 * Direction;
            }
            if (Mathf.Abs(Distance) < 1500 && Mathf.Abs(Distance) > 500)
            {
                MaxSpeed = 15;
                Engine.ControllerPosition = 2 * Direction;
            }
            if (Mathf.Abs(Distance) <= 500)
            {
                if (!MustCouple)
                {
                    if (Mathf.Abs(Distance) > 150)
                    {
                        MaxSpeed = 5;
                        Engine.ControllerPosition = 1 * Direction;
                    }
                    if (Mathf.Abs(Distance) <= 150)
                    {
                        Engine.EngineControllerUseBrakes();
                    }                        
                }
                else if(MustCouple)
                {                    
                    if (RollingStock.CompositionNumberString != startCompositionNumberString)
                    {
                        Engine.EngineControllerUseBrakes();
                        Debug.Log("Changed" + RollingStock.CompositionNumberString);                        
                        Debug.Log("Run is over");
                        adm.Runs.Remove(RailRunID);
                        Destroy(this);
                    }
                    else
                    {
                        MaxSpeed = 5;
                        Engine.ControllerPosition = 1 * Direction;
                    }                        
                }
                if (MSpeed == 0)
                {
                    Engine.EngineControllerUseBrakes();
                    Debug.Log("Run is over");
                    adm.Runs.Remove(RailRunID);
                    Destroy(this);
                }
                              
            }
            
        }
    }

    public void MakeRailRun(Engine engine, Transform rollingStock, Transform aim, float _offset,  int maxSpeed, bool _mustCouple, RollingStock uncouplingRS)
    {            
        RollingStockTransform = rollingStock;
        AimPosition = aim;
        Distance = rollingStock.position.x - aim.position.x;
        Engine = engine;
        MaxSpeed = maxSpeed;
        MustCouple = _mustCouple;
        RollingStock = Engine.GetComponent<RollingStock>();
        Offset = _offset;
        
        if (uncouplingRS)
        {
            uncouplingRS.ActiveCoupler.Uncouple();
        }                   
    }
}
