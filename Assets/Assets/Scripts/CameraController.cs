using UnityEngine;

public class CameraController : MonoBehaviour {
    
    private float mapMovingSpeed = 2500f;
    public Vector2 mapBorder;
    public Vector2 mapLimit;
    

    void Update () {
        
        Vector2 pos = transform.position;
        /*
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - mapBorder.y)
        {
            pos.y += mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= mapBorder.y)
        {
            pos.y -= mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= mapBorder.x)
        {
            pos.x -= mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - mapBorder.x)
        {
            pos.x += mapMovingSpeed * Time.deltaTime;
        }
        */
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= mapMovingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += mapMovingSpeed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, -mapLimit.x, mapLimit.x);
        pos.y = Mathf.Clamp(pos.y, -mapLimit.y, mapLimit.y);
        transform.position = pos;
        
	}
}
