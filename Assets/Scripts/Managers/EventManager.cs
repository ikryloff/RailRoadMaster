public class EventManager : Singleton<EventManager>
{

    public delegate void EventDelegate();

    public static EventDelegate onPathChanged;
    public static EventDelegate onPathUpdated;
    public static EventDelegate onCompositionChanged;
    public static EventDelegate onCarsCoupled;
    public static EventDelegate onTrainSignalChanged;
    public static EventDelegate onPause;
    public static EventDelegate offPause;
    public static EventDelegate onGameOver;
    public static EventDelegate onIndicationStateChanged;





    /// <summary>
    /// ///////////////////////////
    /// </summary>
    public static void GameOver()
    {
        if ( onGameOver != null )
            onGameOver ();
    }

    public static void IndicationStateChanged()
    {
        if ( onIndicationStateChanged != null )
            onIndicationStateChanged ();
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

    public static void CompositionChanged()
    {
        if ( onCompositionChanged != null )
            onCompositionChanged ();
    }

    public static void CarsCoupled()
    {
        if ( onCarsCoupled != null )
            onCarsCoupled ();
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
