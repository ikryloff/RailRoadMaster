using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;

public class RollingStock : MonoBehaviour
{
    private RollingStock rollingStock;
    private Rigidbody rollingStockRB;
    [SerializeField]
    private string number;    
    private int compositionNumberofRS;
    private string compositionNumberString;
    public bool brakes = true;    
    private Coupler activeCoupler;
    private Coupler passiveCoupler;
    private Coupler connectedToPassive; 
    [SerializeField]
    private TrackCircuit trackCircuit;
    public float breakeForce;
    public GameObject fork;
    public Engine engine;
    public SwitchManager switchManager;

    /// <summary>
    /// experimental
    /// </summary>
     


    public BGCcMath mathTemp;

    public Transform rsTransform;

    public float distance;
    public bool isDirectionChanged;
    public int isMoving;

    public float acceleration;
    public float force;
    public List<BGCcMath> ownTrackPath;
    public TrackPath trackPath;
    /// <summary>
    /// 
    /// </summary>

    private void Awake()
    {
        trackPath = FindObjectOfType<TrackPath>();
        switchManager = FindObjectOfType<SwitchManager>();
        engine = GetComponent<Engine>();
        rsTransform = gameObject.GetComponent<Transform>();
    }



    private void Start()
    {
        rollingStock = GetComponent<RollingStock>();
        rollingStockRB = GetComponent<Rigidbody>();        
        ActiveCoupler = transform.GetChild(0).GetComponent<Coupler>();
        PassiveCoupler = transform.GetChild(1).GetComponent<Coupler>();
        //fork = rollingStock.transform.Find("Fork").gameObject;
        Brakes = true;

        // exp

        acceleration = 0;
        isMoving = 1;
        distance = 150f;
                
        
    }

    void Update()
    {
        

        if (engine)
        {
            acceleration = engine.acceleration;
        }
        force = Time.fixedDeltaTime * acceleration * isMoving;
        
        
        distance += force;
        if (mathTemp)
        {
            Vector3 tangent;

            rsTransform.position = mathTemp.CalcPositionAndTangentByDistance(distance, out tangent);
            rsTransform.rotation = Quaternion.LookRotation(tangent);

            if (force > 0 && mathTemp.GetDistance() - distance < 0.1)
            {
                mathTemp = trackPath.GetNextTrack(mathTemp, ownTrackPath);
                distance = 0;
            }
            if (force < 0 && distance < 0.1)
            {
                mathTemp = trackPath.GetPrevTrack(mathTemp, ownTrackPath);
                if (mathTemp)
                    distance = mathTemp.GetDistance();
                
            }
           
        }
        else
        {
            isMoving = 0;
        }

        

    }

    // forced changing direction
    public void ChangeDirection()
    {
        acceleration *= -1;
        if(isMoving == 0)
        {
            isMoving = 1;
        }
       
    }

    void FixedUpdate()
    {

        if (Brakes)
        {
            
            if(breakeForce < 60)
                breakeForce += 0.9f;
            if (rollingStockRB.velocity.x > 0.3f)
                rollingStockRB.AddRelativeForce(new Vector2(-breakeForce, 0), ForceMode.Force);
            else if (rollingStockRB.velocity.x < -0.3f)
                rollingStockRB.AddRelativeForce(new Vector2(breakeForce, 0), ForceMode.Force);
            else
                rollingStockRB.velocity = new Vector2(0, 0);
            
        }
        else
        {
            breakeForce = 0;
            /*
            if (rollingStockRB.velocity.x > 0)
            {
                rollingStockRB.AddRelativeForce(new Vector2(-3f, 0), ForceMode2D.Force);
            }
            else if (rollingStockRB.velocity.x < 0)
                rollingStockRB.AddRelativeForce(new Vector2(3f, 0), ForceMode2D.Force);

            */
        }

    }


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

    public Coupler ConnectedToPassive
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

    public Coupler ActiveCoupler
    {
        get
        {
            return activeCoupler;
        }

        set
        {
            activeCoupler = value;
        }
    }

    public Coupler PassiveCoupler
    {
        get
        {
            return passiveCoupler;
        }

        set
        {
            passiveCoupler = value;
        }
    }

    public int CompositionNumberofRS
    {
        get
        {
            return compositionNumberofRS;
        }

        set
        {
            compositionNumberofRS = value;
        }
    }

    public bool Brakes
    {
        get
        {
            return brakes;
        }

        set
        {
            brakes = value;
        }
    }

    public string CompositionNumberString
    {
        get
        {
            return compositionNumberString;
        }

        set
        {
            compositionNumberString = value;
        }
    }

    public TrackCircuit TrackCircuit
    {
        get
        {
            return trackCircuit;
        }

        set
        {
            trackCircuit = value;
        }
    }

}