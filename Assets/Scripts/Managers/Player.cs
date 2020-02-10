using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public Engine PlayerEngine;
    public Engine TempEngine;
    public Engine HitEngine;
    public RollingStock rollingStock;
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
        EventManager.onCompositionChanged += SetPlayerEngineInCompositionAfterCoupling;
    }

    private void Update()
    {
        RollingStockChosenListener ();
    }


    private void RollingStockChosenListener()
    {
        if ( Input.touchCount == 1 && EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) )
            return;

        if ( !EventSystem.current.IsPointerOverGameObject () )
        {
            Vector3 click = Vector3.one;

            if ( Input.GetMouseButtonDown (0) )
            {
                Ray ray = cameraMain.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if ( Physics.Raycast (ray, out hit) )
                {
                    click = hit.point;
                }

                //print("hit " + hit.collider.name);
                if ( hit.collider != null )
                {
                    if ( hit.collider.CompareTag ("Engine") )
                    {
                        HitEngine = hit.collider.GetComponent<Engine> ();
                        if ( !HitEngine.IsPlayer )
                        {
                            TempEngine = PlayerEngine;
                            TempEngine.IsPlayer = false;
                            PlayerEngine = HitEngine;
                            PlayerEngine.IsActive = true;
                            PlayerEngine.IsPlayer = true;
                            ccc.Target = PlayerEngine.EngineRS;
                            ccc.UpdateCameraTarget ();
                            IndicationManager.Instance.engine = PlayerEngine;
                            SetPlayerEngineInComposition ();
                            CompositionManager.Instance.UpdateCompositions ();
                        }
                        SetViewer (PlayerEngine.EngineRS);
                        PlayerEngine.EngineRS.Model.Blink ();
                    }
                    else if ( hit.collider.CompareTag ("RollingStock") )
                    {
                        rollingStock = hit.collider.GetComponent<RollingStock> ();
                        SetViewer (rollingStock);
                    }
                }
            }
        }
    }

    public void SetPlayerEngineInComposition()
    {
        if ( PlayerEngine.EngineRS.RSComposition.CarComposition.Equals (TempEngine.EngineRS.RSComposition.CarComposition) )
        {
            PlayerEngine.Acceleration = TempEngine.Acceleration;
            PlayerEngine.InstructionsHandler = TempEngine.InstructionsHandler;
            TempEngine.IsActive = false;
            PlayerEngine.EngineRS.RSComposition.CarComposition.CompEngine = PlayerEngine;
            PlayerEngine.EngineRS.RSComposition.CarComposition.SetEngineToAllCars ();
        }
    }

    public void SetPlayerEngineInCompositionAfterCoupling()
    {
        if ( PlayerEngine.EngineRS.RSComposition.CarComposition.Equals (TempEngine.EngineRS.RSComposition.CarComposition) )
        {
            TempEngine.IsActive = false;
            PlayerEngine.EngineRS.RSComposition.CarComposition.CompEngine = PlayerEngine;
            PlayerEngine.EngineRS.RSComposition.CarComposition.SetEngineToAllCars ();
        }
    }

    private void SetViewer( RollingStock rs )
    {
        Viewer.SetText (rs.Number.ToString ());
        if ( rs.IsEngine )
        {
            Viewer.SetLocoUI ();
            Viewer.SetEngine (rs.Engine);
            Viewer.SetEngineForSpeed (rs.Engine);
        }
        else
        {
            Viewer.SetCarUI (rs);
        }
        Viewer.SetIcon (rs);
        rs.Model.Blink ();

    }

    public void MoveForward()
    {
        PlayerEngine.HandlerForward ();
    }

    public void MoveBack()
    {
        PlayerEngine.HandlerBack ();
    }

    public void Stop()
    {
        PlayerEngine.HandlerZero ();
    }



}
