﻿using UnityEngine;

public class TestScript : MonoBehaviour
{

    private RoutePanelManager routePanel;
    private EngineAI engineAI;

    void Start()
    {
        routePanel = FindObjectOfType<RoutePanelManager> ();
        engineAI = FindObjectOfType<EngineAI> ();
        Invoke ("Make1Route", 1f);
        Invoke ("MoveBackSupper", 1f);
    }


    public void Make1Route()
    {
        routePanel.SetRouteByNumber (611);
        routePanel.SetRouteByNumber (160);
    }

    public void MoveBackSupper()
    {
        engineAI.MoveBackSupper ();
    }

}
