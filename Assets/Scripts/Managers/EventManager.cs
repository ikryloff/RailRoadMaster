using System;

public class EventManager : Singleton<EventManager>
{

    public delegate void EventDelegate();

    public static EventDelegate onPathChanged;
    public static EventDelegate onPathUpdated;
    public static EventDelegate onCompositionChanged;
    public static EventDelegate onCarsCoupled;
    public static EventDelegate onTrainSignalChanged;
    public static EventDelegate onSignalChanged;
    public static EventDelegate onPause;
    public static EventDelegate offPause;
    public static EventDelegate onGameOver;
    public static EventDelegate onIndicationStateChanged;
    public static EventDelegate onTrackCircuitsStateChanged;

    
    public static EventDelegate onPlayerUsedThrottle;
    public static EventDelegate onPlayerChangeEngine;
    public static EventDelegate onHourPassed;
    public static EventDelegate onMinutePassed;
    public static EventDelegate onFollowProcessFinished;





    /// <summary>
    /// ///////////////////////////
    /// </summary>
    public static void GameOver()
    {
        if ( onGameOver != null )
            onGameOver ();
    }

    public static void FollowProcessFinished()
    {
        if ( onFollowProcessFinished != null )
            onFollowProcessFinished ();
    }

    public static void PathChanged()
    {
        if ( onPathChanged != null )
            onPathChanged ();
    }

    public static void HourPassed()
    {
        if ( onHourPassed != null )
            onHourPassed ();
    }

    public static void MinutePassed()
    {
        if ( onMinutePassed != null )
            onMinutePassed ();
    }

    public static void EngineChanged()
    {
        if ( onPlayerChangeEngine != null )
            onPlayerChangeEngine ();
    }

    public static void ThrottleChanged()
    {
        if ( onPlayerUsedThrottle != null )
            onPlayerUsedThrottle ();
    }

    public static void TrackCircuitsStateChanged()
    {
        if ( onTrackCircuitsStateChanged != null )
            onTrackCircuitsStateChanged ();
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

    public static void TrainSignalChanged()
    {
        if ( onTrainSignalChanged != null )
            onTrainSignalChanged ();
    }

    public static void SignalChanged()
    {
        if ( onSignalChanged != null )
            onSignalChanged ();
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
