using UnityEngine;

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
        routePanel.SetRouteByNumber (612);
        //routePanel.SetRouteByNumber (360);
    }

    public void MoveBackSupper()
    {
        engineAI.MoveBackSupper ();
    }

}
