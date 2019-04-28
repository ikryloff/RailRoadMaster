public class EventManager : Singleton<EventManager>
{

    public delegate void EventDelegate();
    public static EventDelegate onPathChanged;
    public static EventDelegate onPathUpdated;
    public static EventDelegate onCompositionChanged;
    public static EventDelegate onTrainSignalChanged;


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

}
