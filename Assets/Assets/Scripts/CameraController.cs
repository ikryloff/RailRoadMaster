using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 40f;
    public Vector2 mapBorder;
    public Vector3 mapLimit;
    public float mapLimitMinZ = -400f;
    public float mapLimitMaxZ = 5f;
    public float mapLimitMinX = -1000f;
    public float mapLimitMaxX = 2697f;
    public float mapLimitMinY = -608f;
    public float mapLimitMaxY = -300f;
    [SerializeField]
    public Transform cameraTarget;   
    [SerializeField]
    private GameObject startPos;   
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 5f;
    private float lastTime;
    private bool myUpdate;
    private bool canMoveCamera = true;
    public float cameraSize;
    public Texture2D cursorForFocus;
    private bool isFocusModeIsOn;
    public float speed;
    float scrollSpeed = 10;

    private void Awake()
    {
#if UNITY_ANDROID
        QualitySettings.vSyncCount = 0;    
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
    }

    private void Start()
    {
        


        IsFocusModeIsOn = false;
        lastTime = Time.realtimeSinceStartup;
        
        
    }



    void FixedUpdate()
    {
        MoveCamera(Time.deltaTime);
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scroll * scrollSpeed;

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
                    cameraTarget = hit.collider.GetComponentInParent<Transform>();
                }
            }
        }
        else if (!IsFocusModeIsOn)
        {
            //CameraTarget = null;
        }

        //transform.position = cameraTarget.position - new Vector3(0, 130, 200f);

        // Moving camera by touching
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            transform.position = new Vector3
                (
                    Mathf.Clamp(transform.position.x, mapLimitMinX, mapLimitMaxX),
                    Mathf.Clamp(transform.position.y, mapLimitMinY, mapLimitMaxY),
                    Mathf.Clamp(transform.position.z, mapLimitMinZ, mapLimitMaxZ)
                );
        }

        // Zoom 
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchMagnitude = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchMagnitude - touchMagnitude;

            Camera.main.fieldOfView += deltaMagnitudeDiff * 0.1f;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 4f, 45f);

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
        
    }
    public void CameraZoomOut()
    {
        
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

            if (IsFocusModeIsOn && cameraTarget != null)
            {
                desiredPosition = cameraTarget.transform.position;                
            }
          
            if (Input.GetKey(KeyCode.W) )
            {
                desiredPosition.z += mapMovingSpeed;                
            }
            if (Input.GetKey(KeyCode.S) )
            {
                desiredPosition.z -= mapMovingSpeed;
            }
            if (Input.GetKey(KeyCode.A) )
            {
                desiredPosition.x -= mapMovingSpeed;
            }
            if (Input.GetKey(KeyCode.D) )
            {
                desiredPosition.x += mapMovingSpeed;
            }
            

            desiredPosition.x = Mathf.Clamp(desiredPosition.x, mapLimitMinX, mapLimitMaxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, mapLimitMinZ, mapLimitMaxZ);
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * dt);
            transform.position = smoothedPosition;

        }
        
    }
}
