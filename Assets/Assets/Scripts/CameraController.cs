using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 380f;
    public Vector2 mapBorder;
    public Vector2 mapLimit; 
    [SerializeField]
    private Rigidbody2D cameraTarget;   
    private Vector2 desiredPosition;
    Vector2 smoothedPosition;
    private float smoothSpeed = 8f;
    private float lastTime;
    private bool myUpdate;
    private bool canMoveCamera = true;
    private Joystick joystick;
    public float cameraSize;
    public Texture2D cursorForFocus;
    private bool isFocusModeIsOn;

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

    public bool IsFocusModeIsOn
    {
        get
        {
            return isFocusModeIsOn;
        }

        set
        {
            isFocusModeIsOn = value;
        }
    }

    private void Start()
    {
        IsFocusModeIsOn = false;
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

        if (IsFocusModeIsOn)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            //Debug.Log(hit.collider.name);

            if (hit.collider != null && hit.collider.tag == "FocusCarCollider")
            {
                Cursor.SetCursor(cursorForFocus, Vector2.zero, CursorMode.Auto);

                if (Input.GetMouseButtonDown(1))
                {
                    CameraTarget = hit.collider.GetComponentInParent<Rigidbody2D>();
                    Debug.Log(CameraTarget.name);
                }
            }
        }
        else if (!IsFocusModeIsOn)
        {
            CameraTarget = null;
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

    public void RunFocusMode()
    {
        IsFocusModeIsOn = IsFocusModeIsOn ? false : true;
    }


    void MoveCamera(float dt)
    {
        if (CanMoveCamera) // for remote 
        {
            desiredPosition = transform.position;

            if (IsFocusModeIsOn && CameraTarget != null)
            {
                desiredPosition = CameraTarget.transform.position;                
            }
          
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
                
          
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -mapLimit.x, mapLimit.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
            smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed * dt);
            transform.position = smoothedPosition;
        }
        
    }
}
