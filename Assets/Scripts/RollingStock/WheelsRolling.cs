using UnityEngine;

public class WheelsRolling : MonoBehaviour {
    private RollingStock car;
    private float speed;
    private Transform wheel;

    void Start()
    {
        car = gameObject.transform.root.GetComponent<RollingStock>();
        wheel = GetComponent<Transform>();
    }
        
    void Update () {
        if(car.OwnEngine)
            speed = car.OwnEngine.Acceleration;
        wheel.Rotate(0, 0, -speed * 10f);
	}
}
