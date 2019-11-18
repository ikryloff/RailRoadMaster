using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    [Serializable]
    public class GameEvent : UnityEvent<TrackCircuit> { };
    private static GameEventManager _gameEventManager;
    private Dictionary<string, GameEvent> _eventDictionary;

    public static GameEventManager instance
    {
        get
        {
            if ( !_gameEventManager )
            {
                _gameEventManager = FindObjectOfType (typeof (GameEventManager)) as GameEventManager;
                if ( !_gameEventManager )
                    Debug.LogError ("Needs tobe only one..");
                else
                    _gameEventManager.Init ();
            }
            return _gameEventManager;
        }
    }

    void Init()
    {
        if ( _eventDictionary == null )
            _eventDictionary = new Dictionary<string, GameEvent> ();
    }

    public static void StartListening (string eventName, UnityAction <TrackCircuit> listener )
    {
        GameEvent thisEvent = null;
        if ( instance._eventDictionary.TryGetValue (eventName, out thisEvent) )
            thisEvent.AddListener (listener);
        else
        {
            thisEvent = new GameEvent ();
            thisEvent.AddListener (listener);
            instance._eventDictionary.Add (eventName, thisEvent);
        }
    }

    public static void StopListening( string eventName, UnityAction<TrackCircuit> listener )
    {
        if ( _gameEventManager == null )
            return;
        GameEvent thisEvent = null;
        if ( instance._eventDictionary.TryGetValue (eventName, out thisEvent) )
            thisEvent.RemoveListener (listener);
    }

    public static void SendEvent (string eventName, TrackCircuit param = null )
    {
        GameEvent thisEvent = null;
        if ( instance._eventDictionary.TryGetValue (eventName, out thisEvent) )
            thisEvent.Invoke (param);
    }
}
