using UnityEngine;

public class YardCameraController : MonoBehaviour
{

    private float mapMovingSpeed;
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private Transform connectedCamera;
    private float smoothSpeed = 2f;
    public bool IsActive { get; set; }
    public float XPath { get; set; }

    private void Awake()
    {
        connectedCamera = FindObjectOfType<ConductorCameraController> ().transform;
    }

    void LateUpdate()
    {
        if ( IsActive )
            MoveCamera (Time.deltaTime);
    }

    public void ShowPaths()
    {
        IndicationManager.Instance.TurnPathsOn ();
    }

    public void SetPosition(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    
    private void MoveCamera( float dt )
    {

        mapMovingSpeed = 200;
        desiredPosition = transform.position;
        
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
            transform.Translate (-touchDeltaPosition.x * mapMovingSpeed / 250, 0, 0);
            desiredPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        }
        smoothedPosition = Vector3.Lerp (transform.localPosition, desiredPosition, smoothSpeed * dt);
        transform.localPosition = smoothedPosition;
        XPath = transform.position.x;
    }
}
