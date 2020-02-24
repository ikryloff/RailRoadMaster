using UnityEngine;
using UnityEngine.EventSystems;

public class YardCameraController : MonoBehaviour
{

    private float mapMovingSpeed;
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 2f;
    public bool IsActive { get; set; }
    public float XPath { get; set; }
    private float BORDER_X_RIGHT = 2000;
    private float BORDER_X_LEFT = -1400;

    private void Awake()
    {
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

        mapMovingSpeed = 300;
        desiredPosition = transform.position;

        if ( Input.GetKey (KeyCode.A) )
        {
            if(transform.position.x >= BORDER_X_LEFT)
                desiredPosition.x -= mapMovingSpeed;
        }
        if ( Input.GetKey (KeyCode.D) )
        {
            if ( transform.position.x <= BORDER_X_RIGHT )
                desiredPosition.x += mapMovingSpeed;
        }


        if ( Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved )
        {
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Translate (-touchDeltaPosition.x * mapMovingSpeed / 250, 0, 0);
            CameraBorder ();
            desiredPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        }
        smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * dt);
        transform.position = smoothedPosition;
        XPath = transform.position.x;
    }

    private void CameraBorder()
    {
        if ( transform.position.x > BORDER_X_RIGHT )
            transform.position = new Vector3 (BORDER_X_RIGHT, transform.position.y, transform.position.z);
        if ( transform.position.x < BORDER_X_LEFT )
            transform.position = new Vector3 (BORDER_X_LEFT, transform.position.y, transform.position.z);
    }
}
