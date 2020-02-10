using UnityEngine;

public class TLTrigger : MonoBehaviour
{
    protected TrafficLight signal;
    private int direction;
    private RollingStock rs;

    void Start()
    {
        signal = GetComponent<TrafficLight> ();
    }

    public virtual void OnTriggerEnter( Collider collider )
    {
        rs = collider.GetComponent<RollingStock> ();
        direction = rs.Translation > 0 ? 1 : -1;
        if ( CheckViolation (direction) )
        {
            print (rs.name + " falt signal " + signal.name);
            GameManager.Instance.GameOver ();
        }
    }

    private bool CheckViolation( int dir )
    {
        return dir == signal.SignalDirection && signal.IsClosed;
    }
}
