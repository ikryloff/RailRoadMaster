using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 500f;
    public Vector2 mapBorder;
    public Vector2 mapLimit;    
    public Rigidbody2D target;
    private Vector3 offset;
    private Vector2 desiredPosition;
    Vector2 smoothedPosition;
    private float smoothSpeed = 8f;
    public Toggle myToggle;

    private void Start()
    {
        offset = new Vector3(0, 0, -2);
        myToggle.isOn = false;
    }


    void FixedUpdate () {

        desiredPosition = transform.position;
       

        if (myToggle.isOn)
        {
            if (target.velocity.x >= 1)
                offset = new Vector3(850, 0, -2);
            else if(target.velocity.x <= -1)
                offset = new Vector3(-850, 0, -2);
            desiredPosition = target.transform.position + offset;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                desiredPosition.y += mapMovingSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                desiredPosition.y -= mapMovingSpeed ;
            }
            if (Input.GetKey(KeyCode.A))
            {
                desiredPosition.x -= mapMovingSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                desiredPosition.x += mapMovingSpeed;
            }            
        }
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, -mapLimit.x, mapLimit.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, -mapLimit.y, mapLimit.y);
        smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }    
}
