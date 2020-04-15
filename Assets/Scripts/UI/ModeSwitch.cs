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
    public enum Mode { Conductor, Yard, Shedule };
    public static float CameraXPosition;

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
        if(GameMode == Mode.Yard )
            CameraXPosition = ycc.XPath;
        ccc.SetPosition (CameraXPosition);
        GameMode = Mode.Conductor;
        sceneryManager.Show (false);
    }

    public void SwitchToYardMode()
    {
        ycc.IsActive = true;
        ccc.IsActive = false;
        conductorCamera.enabled = false;
        yardCamera.enabled = true;
        ycc.ShowPaths ();
        if(GameMode == Mode.Conductor)
            CameraXPosition = ccc.XPath;
        ycc.SetPosition (CameraXPosition);
        GameMode = Mode.Yard;
        sceneryManager.Show (true);
    }

    public void SwitchToSheduleMode()
    {
        CameraXPosition = ccc.XPath;
        GameMode = Mode.Shedule;
    }

    
}
