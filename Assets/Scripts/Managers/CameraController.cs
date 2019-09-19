using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float mapMovingSpeed = 100f;
   
    private Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private float smoothSpeed = 5f;  
    private bool isFocusModeIsOn;
    private float speed = 1;
    float scrollSpeed = 10;
    float d = 0;
    public float ZoomZ { get; private set; }



    private void Start()
    {
        ZoomZ = transform.position.z;
    }




    void LateUpdate()
    {
        MoveCamera (Time.deltaTime);       
    }


    public void CameraZoomY( float newZoom )
    {
        ZoomZ = -newZoom;
    }

     

void MoveCamera( float dt )
    {
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
            transform.Translate (-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
            desiredPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        desiredPosition.z = ZoomZ;        
        smoothedPosition = Vector3.Lerp (transform.localPosition, desiredPosition, smoothSpeed * dt);
        transform.localPosition = smoothedPosition;
    }
}
