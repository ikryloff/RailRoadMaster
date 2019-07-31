using UnityEngine;

public class TLTrigger : MonoBehaviour
{
    private TrafficLight signal;
    private int direction;

    void Start()
    {        
        signal = GetComponent<TrafficLight> ();
    }

    private void OnTriggerEnter( Collider collider )
    {
        direction = collider.GetComponent<MovableObject> ().Translation > 0 ? 1 : -1;
        if(CheckViolation (direction))
            GameManager.Instance.GameOver ();

    }

    private bool CheckViolation(int dir)
    {
            return dir == signal.SignalDirection && signal.IsClosed;       
    }
}
