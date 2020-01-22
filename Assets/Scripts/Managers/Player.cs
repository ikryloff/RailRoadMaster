using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public Engine PlayerEngine;
    public Engine TempEngine;
    public RollingStock rollingStock;
    private ConductorCameraController ccc;
    public RSViewer Viewer;
    

    private void Awake()
    {
        TempEngine = null;
        Viewer = FindObjectOfType<RSViewer> ();
        ccc = FindObjectOfType<ConductorCameraController> ();
        if(rollingStock.Engine)
            PlayerEngine = rollingStock.GetComponent<Engine> ();
        ccc.Target = rollingStock;
        IndicationManager.Instance.engine = PlayerEngine;
        PlayerEngine.IsActive = true;        
    }

    private void Update()
    {
        RollingStockChosenListener ();
    }

    private void Start()
    {
        SetViewer(rollingStock);
    }

    private void RollingStockChosenListener()
    {
        if ( !EventSystem.current.IsPointerOverGameObject () )
        {
            Vector3 click = Vector3.one;

            if ( Input.GetMouseButtonDown (0) )
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if ( Physics.Raycast (ray, out hit) )
                {
                    click = hit.point;
                }

                //print("hit " + hit.collider.name);
                if ( hit.collider != null )
                {
                    if( hit.collider.CompareTag ("Engine") )
                    {
                        TempEngine = PlayerEngine;   
                        PlayerEngine = hit.collider.GetComponent<Engine> ();
                        if( !PlayerEngine.Equals (TempEngine) )
                        {
                            PlayerEngine.IsActive = true;
                            ccc.Target = PlayerEngine.EngineRS;
                            ccc.UpdateCameraTarget ();
                            SetViewer (PlayerEngine.EngineRS);
                            IndicationManager.Instance.engine = PlayerEngine;
                            if ( PlayerEngine.EngineRS.RSComposition.CarComposition.Equals (TempEngine.EngineRS.RSComposition.CarComposition) )
                            {
                                PlayerEngine.Acceleration = TempEngine.Acceleration;
                                TempEngine.InstructionsHandler = 0;
                                TempEngine.Acceleration = 0;
                                TempEngine.IsActive = false;
                            }                            
                            CompositionManager.Instance.UpdateCompositions ();
                        }                        

                    }

                    else if( hit.collider.CompareTag ("RollingStock") )
                    {
                        rollingStock = hit.collider.GetComponent<RollingStock> ();
                        SetViewer (rollingStock);
                    }
                }
            }
        }
    }

    private void SetViewer(RollingStock rs)
    {
        Viewer.SetText (rs.Number.ToString ());
        if ( rs.IsEngine )
        {
            Viewer.SetEngine(rs.Engine);
            Viewer.SetEngineForSpeed(rs.Engine);            
        }
        Viewer.SetIcon (rs);

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
