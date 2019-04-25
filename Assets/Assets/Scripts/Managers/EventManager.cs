using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> {

    public delegate void EventDelegate();
    public static EventDelegate onPathChanged;
    public static EventDelegate onCompositionChanged;

    public static void PathChanged()
    {
        if (onPathChanged != null)
            onPathChanged();
    }

    public static void OnCompositionChanged()
    {
        if (onCompositionChanged != null)
            onCompositionChanged();
    }
}
