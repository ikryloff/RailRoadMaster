﻿using UnityEngine;
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
    private float speed = 1;
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
        
    }

    


    void Update()
    {
        MoveCamera(Time.deltaTime);
        //transform.position = cameraTarget.position + new Vector3(0, 530, -400);
        if ( Input.GetKeyDown(KeyCode.Z) )
        {
            transform.position = cameraTarget.position + new Vector3 (0, 530, -400);
        }

            if ( Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved )
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Translate (-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            transform.position = new Vector3
                (
                    Mathf.Clamp (transform.position.x, mapLimitMinX, mapLimitMaxX),
                    Mathf.Clamp (transform.position.y, mapLimitMinY, mapLimitMaxY),
                    Mathf.Clamp (transform.position.z, mapLimitMinZ, mapLimitMaxZ)
                );
        }

        // Zoom 
        if ( Input.touchCount == 2 )
        {
            Touch touchZero = Input.GetTouch (0);
            Touch touchOne = Input.GetTouch (1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchMagnitude = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchMagnitude - touchMagnitude;
            Camera.main.fieldOfView += deltaMagnitudeDiff * 0.1f;
            Camera.main.fieldOfView = Mathf.Clamp (Camera.main.fieldOfView, 4f, 70f);

        }

    }

   

   
    public void CameraZoomIn()
    {
        
    }
    public void CameraZoomOut()
    {
        
    }

   

    void MoveCamera(float dt)
    {
        desiredPosition = transform.position;

        

        if ( Input.GetKey (KeyCode.W) )
        {
            desiredPosition.z += mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.S) )
        {
            desiredPosition.z -= mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.A) )
        {
            desiredPosition.x -= mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.D) )
        {
            desiredPosition.x += mapMovingSpeed;
        }
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        desiredPosition.y -= scroll * 200 * scrollSpeed;
        desiredPosition.z += scroll * 200 * scrollSpeed;

        desiredPosition.x = Mathf.Clamp (desiredPosition.x, mapLimitMinX, mapLimitMaxX);
        desiredPosition.y = Mathf.Clamp (desiredPosition.y, -mapLimit.y, mapLimit.y);
        desiredPosition.z = Mathf.Clamp (desiredPosition.z, mapLimitMinZ, mapLimitMaxZ);
        smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * dt);
        transform.position = smoothedPosition;

    }
}
