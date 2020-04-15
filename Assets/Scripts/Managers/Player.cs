using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public Engine PlayerEngine;
    public Engine TempEngine;
    public Engine HitEngine;
    public RollingStock rollingStock;
    public RollingStock tempRollingStock;
    private ConductorCameraController ccc;
    public RSViewer Viewer;
    public Camera cameraMain;



    private void Awake()
    {
        ccc = FindObjectOfType<ConductorCameraController> ();
        cameraMain = ccc.GetComponent<Camera> ();
        TempEngine = ResourceHolder.Instance.TempEngine;
        PlayerEngine = ResourceHolder.Instance.StationEngine;
        Viewer = FindObjectOfType<RSViewer> ();
        IndicationManager.Instance.engine = PlayerEngine;
        PlayerEngine.IsActive = true;
        PlayerEngine.IsPlayer = true;
    }
    private void Start()
    {
        ccc.Target = PlayerEngine.EngineRS;
        SetViewer (PlayerEngine.EngineRS);
    }

    
    public void RollingStockListener(Collider collider)
    {
        if ( collider.CompareTag ("Engine") )
        {
            HitEngine = collider.GetComponent<Engine> ();
            if ( !HitEngine.IsPlayer )
            {
                TempEngine = PlayerEngine;
                TempEngine.IsPlayer = false;
                //set outlines
                TempEngine.EngineRS.Model.DefaultOutline ();
                PlayerEngine = HitEngine;
                PlayerEngine.IsActive = true;
                PlayerEngine.IsPlayer = true;
                ccc.Target = PlayerEngine.EngineRS;
                ccc.UpdateCameraTarget ();
                IndicationManager.Instance.engine = PlayerEngine;
                SetPlayerEngineInComposition ();
            }
            SetViewer (PlayerEngine.EngineRS);
            PlayerEngine.EngineRS.Model.HighLightEngineOutline ();
            if ( tempRollingStock != null )
            {
                tempRollingStock.Model.DefaultOutline ();
                tempRollingStock = null;
            }
            EventManager.EngineChanged ();
        }
        else if ( collider.CompareTag ("RollingStock") )
        {
            rollingStock = collider.GetComponent<RollingStock> ();
            SetViewer (rollingStock);
            if ( tempRollingStock != null && !tempRollingStock.Equals (rollingStock) )
                tempRollingStock.Model.DefaultOutline ();
            tempRollingStock = rollingStock;

        }
    }

    public void SetPlayerEngineInComposition()
    {
        if ( PlayerEngine.EngineRS.RSComposition.CarComposition.Equals (TempEngine.EngineRS.RSComposition.CarComposition) )
        {
            PlayerEngine.Acceleration = TempEngine.Acceleration;
            PlayerEngine.InstructionsHandler = TempEngine.InstructionsHandler;
            TempEngine.IsActive = false;
            //reset old engine acceleration
            TempEngine.AbsoluteStop ();
            TempEngine.EngineRS.RSComposition.CarComposition.CompEngine = PlayerEngine;
        }
        else
            PlayerEngine.EngineRS.RSComposition.CarComposition.CompEngine = PlayerEngine;
    }
      

    private void SetViewer( RollingStock rs )
    {
        Viewer.SetText (rs.Number.ToString ());
        if ( rs.IsEngine )
        {
            Viewer.SetLocoUI ();
            Viewer.SetEngine (rs.Engine);
            Viewer.SetEngineForSpeed (rs.Engine);
            // to set camera taget
            EventManager.ThrottleChanged ();
        }
        else
        {
            Viewer.SetCarUI (rs);
            rs.Model.HighLightCarOutline ();
        }
        Viewer.SetIcon (rs);
        Viewer.SetReturnPoint (rs);

    }

    public void MoveForward()
    {
        PlayerEngine.HandlerForward ();
        if(PlayerEngine.InstructionsHandler <= 1)
            EventManager.ThrottleChanged ();
    }

    public void MoveBack()
    {
        PlayerEngine.HandlerBack ();
        if ( PlayerEngine.InstructionsHandler >= -1 )
            EventManager.ThrottleChanged ();
    }

    public void Stop()
    {
        PlayerEngine.HandlerZero ();
        EventManager.ThrottleChanged ();
    }



}
