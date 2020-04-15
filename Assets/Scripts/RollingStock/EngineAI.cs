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

    public void MoveWithDirection( int throttle )
    {
        engine.IsActive = true;
        if(throttle > 0 )
        {
            for ( int i = 0; i < throttle; i++ )
            {
                engine.HandlerForward ();
            }
        }
        else if( throttle < 0 )
        {
            throttle = Mathf.Abs (throttle);
            for ( int i = 0; i < throttle; i++ )
            {
                engine.HandlerBack ();
            }
        }
        else
            engine.HandlerZero ();


    }

    public void Stop()
    {
        engine.IsActive = true;
        engine.HandlerZero ();
    }


}
