using System.Linq;
using UnityEngine;

public class ConductorCameraController : MonoBehaviour
{
    private Transform connectedCamera;
    private Camera condCamera;
    private float mapMovingSpeed;
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 2f;
    private float speed = 1;
    float scrollSpeed = 10;
    float d = 0;
    public RollingStock Target;
    private float CameraPosHeight;
    private float CameraPosX;
    private float HeightKoef;
    private RSComposition composition;
    private Engine engine;
    public Transform targetTransform;
    public bool IsFreeCamera { get; set; }
    public bool IsActive { get; set; }
    public Vector3 CameraOffset;
    private float Zoom;
    public float XPath { get; set; }
    public float center;


    private void Awake()
    {
        connectedCamera = FindObjectOfType<YardCameraController> ().transform;
        condCamera = GetComponent<Camera> ();
    }

    private void Start()
    {
        CameraPosHeight = 300;
        targetTransform = Target.transform;
        engine = Target.GetComponent<Engine> ();
        composition = engine.EngineRS.RSComposition;       
        HeightKoef = 0.175f;
        Zoom = 300;
        XPath = 0;
    }

    
    void LateUpdate()
    {

        if (IsActive)
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
        XPath = transform.position.x;
    }

    public void SetPosition( float x )
    {
        transform.localPosition = new Vector3 (x, transform.localPosition.y, transform.localPosition.z);
    }

    private void SetCameraPosition()
    {
        if ( engine.InstructionsHandler > 0 )
        {
            Target = composition.CarComposition.Cars.Last ().RollingStock;
            targetTransform = Target.transform;
            CameraPosX = 1200 * HeightKoef;
        }

        else if ( engine.InstructionsHandler < 0 )
        {
            Target = composition.CarComposition.Cars.First ().RollingStock;
            targetTransform = Target.transform;
            CameraPosX = -1200 * HeightKoef;
        }
    }


    public void CameraZoomIn()
    {
        if ( CameraPosHeight < 300 )
        {
            CameraPosHeight += Zoom;            
            
            HeightKoef /= 1.2f;
        }
    }

    public void CameraZoomOut()
    {
        if( CameraPosHeight > -600 )
        {
            CameraPosHeight -= Zoom;            
           
           HeightKoef *= 1.2f;
        }
    }


    private void FollowTarget( float dt )
    {
        desiredPosition = targetTransform.position + new Vector3 (CameraPosX, targetTransform.position.z / 1.5f, CameraPosHeight - targetTransform.position.z);
        smoothedPosition = Vector3.Lerp (transform.localPosition, desiredPosition, smoothSpeed * dt);
        transform.localPosition = smoothedPosition;
    }


    private void MoveCamera( float dt )
    {
        
        mapMovingSpeed = HeightKoef * 700;
        desiredPosition = transform.localPosition;
        if ( Input.GetKey (KeyCode.W) )
        {
            desiredPosition.y += mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.S) )
        {
            desiredPosition.y -= mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.A) )
        {
            desiredPosition.x -= mapMovingSpeed;
            
        }
        if ( Input.GetKey (KeyCode.D) )
        {
            desiredPosition.x += mapMovingSpeed;
           
        }

        if ( Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved )
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Translate (-touchDeltaPosition.x * speed * HeightKoef, -touchDeltaPosition.y * speed * HeightKoef, 0);
            desiredPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        desiredPosition.z = CameraPosHeight;
        smoothedPosition = Vector3.Lerp (transform.localPosition, desiredPosition, smoothSpeed * dt);
        transform.localPosition = smoothedPosition;
        
        
    }
}
