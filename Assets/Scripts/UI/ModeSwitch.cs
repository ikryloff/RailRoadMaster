using System;
using UnityEngine;

public class ModeSwitch : Singleton<ModeSwitch>
{
    [SerializeField]
    private Camera conductorCamera;
    [SerializeField]
    private Camera yardCamera;
    private ConductorCameraController ccc;
    private YardCameraController ycc;
    private SceneryManager sceneryManager;
    public Mode GameMode { get; set; }
    public enum Mode { Conductor, Yard };

    private void Awake()
    {
        ccc = conductorCamera.GetComponent<ConductorCameraController> ();
        ycc = yardCamera.GetComponent<YardCameraController> ();
        sceneryManager = FindObjectOfType<SceneryManager> ();
                
    }
    private void Start()
    {
        SwitchToConductorMode ();
    }


    public void SwitchToConductorMode()
    {        
        conductorCamera.enabled = true;
        yardCamera.enabled = false;
        ycc.IsActive = false;
        ccc.IsActive = true;
        IndicationManager.Instance.TurnPathIndicationOff ();
        GameMode = Mode.Conductor;
        ccc.SetPosition (ycc.XPath);
        sceneryManager.Show (false);
    }

    public void SwitchToYardMode()
    {
        ycc.IsActive = true;
        ccc.IsActive = false;
        conductorCamera.enabled = false;
        yardCamera.enabled = true;
        ycc.ShowPaths ();
        UIManager.Instance.RouteMode();
        GameMode = Mode.Yard;
        ycc.SetPosition (ccc.XPath);
        sceneryManager.Show (true);
    }

    public void ToggleModes()
    {
        if ( ccc.IsActive )
        {
            SwitchToYardMode ();
        }
        else
            SwitchToConductorMode ();

    }
}
