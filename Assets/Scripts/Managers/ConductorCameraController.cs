using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConductorCameraController : MonoBehaviour
{
    public Transform GroupCamera;
    private Camera condCamera;
    private float MAP_SPEED = 10;
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 3f;
    private float speed = 0.3f;
    float scrollSpeed = 10;
    public RollingStock Target;
    private RSComposition composition;
    private Engine engine;
    public Transform targetTransform;
    public Transform tempTarget;
    public Transform StoreFocusPoint;
    public Transform OTFocusPoint;
    public Transform LocoPostFocusPoint;
    public bool IsFreeCamera { get; set; }
    public bool IsActive { get; set; }
    public bool IsPostView { get; set; }
    public int ZoomLevel;
    public float XPath { get; set; }

    public Toggle CameraFreeButton;

    public float center;
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    private float OFFSET_X = 0;
    private float OFFSET_Y = 250;
    private float OFFSET_Z = -200;
    private float BORDER_X_RIGHT = 2400;
    private float BORDER_X_LEFT = -1500;
    private float BORDER_Z_TOP = 130;
    private float BORDER_Z_BOTTOM = -650;
    private float [] tempOffset;
    Vector2 touchDeltaPosition;

    private void Awake()
    {
        condCamera = GetComponent<Camera> ();
        GroupCamera = transform.parent.GetComponent<Transform> ();
        tempOffset = new float [3];
        EventManager.onPlayerUsedThrottle += SetCameraPosition;
    }

    private void Start()
    {
        UpdateCameraTarget ();
        //to connect position of yard and cond cameras
        XPath = 0;
        OffsetX = OFFSET_X;
        OffsetY = OFFSET_Y;
        OffsetZ = OFFSET_Z;
        ZoomLevel = 1;

        GroupCamera.position = new Vector3 (0, OFFSET_Y, OFFSET_Z);
    }

    private Vector3 GetViewPosition( int level )
    {
        return new Vector3 (OffsetX * level, OFFSET_Y * level, OFFSET_Z * level);
    }

    public void UpdateCameraTarget()
    {
        targetTransform = Target.transform;
        engine = Target.Engine;
        composition = Target.RSComposition;
    }

    void LateUpdate()
    {

        if ( IsActive )
            UpdateCamera ();
    }

    public void ToggleFreeCamera()
    {
        IsFreeCamera = !IsFreeCamera;

    }

    private void UpdateCamera()
    {
        XPath = GroupCamera.position.x;

        if ( IsFreeCamera || engine.InstructionsHandler == 0 )
        {
            MoveCamera (Time.deltaTime);
            return;
        }               
        FollowTarget (Time.deltaTime);

    }

    //for connection with yardcamera 
    public void SetPosition( float x )
    {
        GroupCamera.position = new Vector3 (x, GroupCamera.position.y, GroupCamera.position.z);
    }

    private void SetCameraPosition()
    {
        if ( engine.InstructionsHandler == 0 )
        {
            OffsetX = 0;
        }
        else if ( engine.InstructionsHandler > 0  )
        {
            OffsetX = 100;
            Target = composition.CarComposition.RightCar;
            targetTransform = Target.transform;
        }

        else if ( engine.InstructionsHandler < 0 )
        {
            OffsetX = -100;
            Target = composition.CarComposition.LeftCar;
            targetTransform = Target.transform;
        }

    }

    IEnumerator FollowProcess( Transform _targ )
    {
        while ( Vector3.Distance (GroupCamera.position, targetTransform.position + GetViewPosition (ZoomLevel)) > 50 )
        {
            FollowTarget (Time.deltaTime);
            yield return null;
        }
        CameraFreeButton.isOn = false;
        targetTransform = tempTarget;        
    }

    public void SetCameraPositionOnPost( Transform post )
    {
        ZoomLevel = 2;
        OffsetX = 0;
        OffsetY = 0;
        tempTarget = targetTransform;
        targetTransform = post.transform;        
        StartCoroutine (FollowProcess (post));
    }


    public void CameraZoomIn()
    {
        if ( ZoomLevel > 1 )
        {
            GroupCamera.position += new Vector3 (0, -OFFSET_Y, -OFFSET_Z);
            ZoomLevel -= 1;
        }
    }

    public void CameraZoomOut()
    {
        if ( ZoomLevel < 4 )
        {
            GroupCamera.position += new Vector3 (0, OFFSET_Y, OFFSET_Z);
            ZoomLevel += 1;
        }
    }

    private void FollowTarget( float dt )
    {
        
        desiredPosition = targetTransform.position + GetViewPosition (ZoomLevel);        
        desiredPosition.x = Mathf.Clamp (desiredPosition.x, BORDER_X_LEFT + ZoomLevel * 100, BORDER_X_RIGHT - ZoomLevel * 200);
        desiredPosition.z = Mathf.Clamp (desiredPosition.z, BORDER_Z_BOTTOM - ZoomLevel * 105, BORDER_Z_TOP - ZoomLevel * 300);
        desiredPosition.y = Mathf.Clamp (desiredPosition.y, OFFSET_Y * ZoomLevel, OFFSET_Y * ZoomLevel);
        if ( targetTransform.position.x < -1800 || targetTransform.position.x > 2600 )
            CameraFreeButton.isOn = false;
        smoothedPosition = Vector3.Lerp (GroupCamera.position, desiredPosition, smoothSpeed * dt);
        GroupCamera.position = smoothedPosition;
    }


    private void MoveCamera( float dt )
    {
        desiredPosition = GroupCamera.position;

        if ( Input.GetKey (KeyCode.W) )
        {
            GroupCamera.position += new Vector3 (0, 0, MAP_SPEED);
        }
        if ( Input.GetKey (KeyCode.S) )
        {
            GroupCamera.position -= new Vector3 (0, 0, MAP_SPEED);
        }
        if ( Input.GetKey (KeyCode.A) )
        {
            GroupCamera.position -= new Vector3 (MAP_SPEED, 0, 0);
        }
        if ( Input.GetKey (KeyCode.D) )
        {
            GroupCamera.position += new Vector3 (MAP_SPEED, 0, 0);

        }



        if ( Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) && Input.GetTouch (0).phase == TouchPhase.Moved )
        {
            touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            GroupCamera.Translate (-touchDeltaPosition.x * speed * ZoomLevel, -touchDeltaPosition.y * speed * ZoomLevel, -touchDeltaPosition.y * speed * ZoomLevel);
        }
        desiredPosition = new Vector3 (GroupCamera.position.x, GroupCamera.position.y, GroupCamera.position.z);
        desiredPosition.x = Mathf.Clamp (desiredPosition.x, BORDER_X_LEFT + ZoomLevel * 100, BORDER_X_RIGHT - ZoomLevel * 200);
        desiredPosition.z = Mathf.Clamp (desiredPosition.z, BORDER_Z_BOTTOM - ZoomLevel * 105, BORDER_Z_TOP - ZoomLevel * 300);
        desiredPosition.y = Mathf.Clamp (desiredPosition.y, OFFSET_Y * ZoomLevel, OFFSET_Y * ZoomLevel);
        smoothedPosition = Vector3.Lerp (GroupCamera.position, desiredPosition, smoothSpeed * dt);
        GroupCamera.position = smoothedPosition;
    }


}
