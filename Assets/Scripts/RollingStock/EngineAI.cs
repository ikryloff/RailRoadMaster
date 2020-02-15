using UnityEngine;

public class EngineAI : MonoBehaviour
{

    private Engine engine;
    private RSConnection connection;
    private bool Enable;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        connection = engine.GetComponent<RollingStock> ().GetComponent<RSConnection> ();
        Enable = true;
    }



    public void MoveForward( int throttle )
    {
        engine.IsActive = true;
        for ( int i = 0; i < throttle; i++ )
        {
            engine.HandlerForward ();
        }
    }

    public void MoveBack( int throttle )
    {
        engine.IsActive = true;
        for ( int i = 0; i < throttle; i++ )
        {
            engine.HandlerBack ();
        }
    }

    public void Stop()
    {
        engine.IsActive = true;
        engine.HandlerZero ();
    }


}
