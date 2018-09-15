using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    public float mapMovingSpeed = 400f;
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
    private Joystick joystick;
    public float cameraSize;

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
        joystick = FindObjectOfType<Joystick>();                
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        MyUpdate = MyUpdate != true ? true : false;
        Time.timeScale = Time.timeScale != 0 ? 0 : 1;        
    }

    public void CameraZoomIn()
    {
        if(GetComponent<Camera>().orthographicSize > 250)
        {
            GetComponent<Camera>().orthographicSize -= 100f;
            mapLimit.x += 180;
            mapLimit.y += 100;           
            mapMovingSpeed -= 60;
        }
            
    }
    public void CameraZoomOut()
    {
        if (GetComponent<Camera>().orthographicSize < 900)
        {
            GetComponent<Camera>().orthographicSize += 100f;
            mapLimit.x -= 180;            
            mapLimit.y -= 100;            
            mapMovingSpeed += 60;
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
                if (Input.GetKey(KeyCode.W) )
                {
                    desiredPosition.y += mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.S) )
                {
                    desiredPosition.y -= mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.A) )
                {
                    desiredPosition.x -= mapMovingSpeed;
                }
                if (Input.GetKey(KeyCode.D) )
                {
                    desiredPosition.x += mapMovingSpeed;
                }
           
                desiredPosition.y += mapMovingSpeed * joystick.Vertical;
                desiredPosition.x += mapMovingSpeed * joystick.Horizontal;
                
            }
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -mapLimit.x, mapLimit.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
            smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed * dt);
            transform.position = smoothedPosition;
        }
        
    }
}
