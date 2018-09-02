using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 500f;
    public Vector2 mapBorder;
    public Vector2 mapLimit; 
    [SerializeField]
    private Rigidbody2D cameraTarget;
    private Vector3 offset;
    private Vector2 desiredPosition;
    Vector2 smoothedPosition;
    private float smoothSpeed = 8f;
    public Toggle myToggle;
    private float lastTime;
    private bool myUpdate;
    private bool canMoveCamera = true;

    public Rigidbody2D CameraTarget
    {
        get
        {
            return cameraTarget;
        }

        set
        {
            cameraTarget = value;
        }
    }

    public bool MyUpdate
    {
        get
        {
            return myUpdate;
        }

        set
        {
            myUpdate = value;
        }
    }

    public bool CanMoveCamera
    {
        get
        {
            return canMoveCamera;
        }

        set
        {
            canMoveCamera = value;
        }
    }

    private void Start()
    {
        offset = new Vector3(0, 0, -2);
        myToggle.isOn = false;
        lastTime = Time.realtimeSinceStartup;
    }


    void FixedUpdate ()
    {
       MoveCamera(Time.deltaTime);
    }

    private void Update()
    {
        if (MyUpdate)
        {
            float deltaTime = Time.realtimeSinceStartup - lastTime;
            MoveCamera(deltaTime);
            lastTime = Time.realtimeSinceStartup;
        }
    }

    void MoveCamera(float dt)
    {
        if (CanMoveCamera)
        {
            desiredPosition = transform.position;


            if (myToggle.isOn)
            {
                if (CameraTarget.velocity.x >= 1)
                    offset = new Vector3(0, 0, -2);
                else if (CameraTarget.velocity.x <= -1)
                    offset = new Vector3(0, 0, -2);
                desiredPosition = CameraTarget.transform.position + offset;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    desiredPosition.y += mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    desiredPosition.y -= mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    desiredPosition.x -= mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    desiredPosition.x += mapMovingSpeed;
                }
            }
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -mapLimit.x, mapLimit.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
            smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed * dt);
            transform.position = smoothedPosition;
        }
        
    }
}
