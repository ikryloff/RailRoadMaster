using System.Linq;
using UnityEngine;

public class ConductorCameraController : MonoBehaviour
{
    public Transform GroupCamera;
    private Camera condCamera;
    private float MAP_SPEED = 10;
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 3f;
    private float speed = 0.1f;
    float scrollSpeed = 10;
    float d = 0;
    public RollingStock Target;
    private RSComposition composition;
    private Engine engine;
    public Transform targetTransform;
    public bool IsFreeCamera { get; set; }
    public bool IsActive { get; set; }
    public int ZoomLevel;
    public float XPath { get; set; }
    public float center;
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    private float OFFSET_X = 0;
    private float OFFSET_Y = 250;
    private float OFFSET_Z = -250;
    private float [] tempOffset ;


    private void Awake()
    {
        condCamera = GetComponent<Camera> ();
        GroupCamera = transform.parent.GetComponent<Transform> ();
        tempOffset = new float [3];
    }

    private void Start()
    {
        targetTransform = Target.transform;
        engine = Target.GetComponent<Engine> ();
        composition = engine.EngineRS.RSComposition;
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
        if ( !IsFreeCamera && engine.InstructionsHandler != 0 )
        {
            SetCameraPosition ();
            FollowTarget (Time.deltaTime);
        }
        else
            MoveCamera (Time.deltaTime);
        XPath = GroupCamera.position.x;
    }

    //for connection with yardcamera 
    public void SetPosition( float x )
    {
        GroupCamera.position = new Vector3 (x, GroupCamera.position.y, GroupCamera.position.z);        
    }

    private void SetCameraPosition()
    {
        if ( engine.InstructionsHandler > 0 )
        {
            OffsetX = 120;
            Target = composition.CarComposition.Cars.Last ().RollingStock;
            targetTransform = Target.transform;
        }

        else if ( engine.InstructionsHandler < 0 )
        {
            OffsetX = -120;
            Target = composition.CarComposition.Cars.First ().RollingStock;
            targetTransform = Target.transform;

        }
    }


    public void CameraZoomIn()
    {
        if( ZoomLevel > 1 )
        {
            GroupCamera.position += new Vector3 (0, -OFFSET_Y, -OFFSET_Z);
            ZoomLevel -= 1;
        }        
    }

    public void CameraZoomOut()
    {
        if(ZoomLevel < 4)
        {
            GroupCamera.position += new Vector3 (0, OFFSET_Y, OFFSET_Z);
            ZoomLevel += 1;
        }        
    }
    
    private void FollowTarget( float dt )
    {
        desiredPosition = targetTransform.position + GetViewPosition (ZoomLevel);
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
            GroupCamera.position -= new Vector3( MAP_SPEED, 0, 0);
        }
        if ( Input.GetKey (KeyCode.D) )
        {
            GroupCamera.position += new Vector3 (MAP_SPEED, 0, 0);

        }

        desiredPosition = new Vector3 (GroupCamera.position.x, GroupCamera.position.y, GroupCamera.position.z);

        if ( Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved )
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            GroupCamera.Translate (-touchDeltaPosition.x * speed * ZoomLevel, -touchDeltaPosition.y * speed * ZoomLevel, 0);
            desiredPosition = new Vector3 (GroupCamera.position.x, GroupCamera.position.y, GroupCamera.position.z);
        }       
        
        smoothedPosition = Vector3.Lerp (GroupCamera.position, desiredPosition, smoothSpeed * dt);
        GroupCamera.position = smoothedPosition;


    }
}
