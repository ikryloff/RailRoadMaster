using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 20f;
    public Vector2 mapBorder;
    public Vector3 mapLimit; 
    public float mapLimitMinZ; 
    [SerializeField]
    private Rigidbody2D cameraTarget;   
    [SerializeField]
    private Rigidbody2D startPos;   
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 5f;
    private float lastTime;
    private bool myUpdate;
    private bool canMoveCamera = true;
    public float cameraSize;
    public Texture2D cursorForFocus;
    private bool isFocusModeIsOn;

    float scrollSpeed = 10f;

    private void Start()
    {
        IsFocusModeIsOn = false;
        lastTime = Time.realtimeSinceStartup;
        transform.position = startPos.position - new Vector2(0, 30f);
        
    }



    void FixedUpdate()
    {
        MoveCamera(Time.deltaTime);
       

    }

    private void LateUpdate()
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

            if (hit.collider != null && hit.collider.tag == "FocusCarCollider")
            {
                Cursor.SetCursor(cursorForFocus, Vector2.zero, CursorMode.Auto);

                if (Input.GetMouseButtonDown(1))
                {
                    CameraTarget = hit.collider.GetComponentInParent<Rigidbody2D>();
                }
            }
        }
        else if (!IsFocusModeIsOn)
        {
            CameraTarget = null;
        }


    }

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

   

    public void PauseGame()
    {
        MyUpdate = MyUpdate != true ? true : false;
        Time.timeScale = Time.timeScale != 0 ? 0 : 1;        
    }

    public void CameraZoomIn()
    {
        GetComponent<Camera>().fieldOfView -= 10;
    }
    public void CameraZoomOut()
    {
        GetComponent<Camera>().fieldOfView += 10;
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
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            desiredPosition.z += scroll * 100 * scrollSpeed;

            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -mapLimit.x, mapLimit.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, -mapLimit.z, mapLimitMinZ);
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * dt);
            transform.position = smoothedPosition;

        }
        
    }
}
