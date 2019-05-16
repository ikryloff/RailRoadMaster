public class EventManager : Singleton<EventManager>
{

    public delegate void EventDelegate();
    public static EventDelegate onPathChanged;
    public static EventDelegate onPathUpdated;
    public static EventDelegate onCompositionChanged;
    public static EventDelegate onTrainSignalChanged;
    public static EventDelegate onPause;
    public static EventDelegate offPause;
    public static EventDelegate oNGameOver;


    public static void GameOver()
    {
        if ( oNGameOver != null )
            oNGameOver ();
    }

    public static void PathChanged()
    {
        if ( onPathChanged != null )
            onPathChanged ();
    }

    public static void PathUpdated()
    {
        if ( onPathUpdated != null )
            onPathUpdated ();
    }

    public static void OnCompositionChanged()
    {
        if ( onCompositionChanged != null )
            onCompositionChanged ();
    }

    public static void OnTrainSignalChanged()
    {
        if ( onTrainSignalChanged != null )
            onTrainSignalChanged ();
    }

    public static void PauseOn()
    {
        if ( onPause != null )
            onPause ();
    }

    public static void PauseOff()
    {
        if ( offPause != null )
            offPause ();
    }
}
