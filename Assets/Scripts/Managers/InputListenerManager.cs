using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputListenerManager : MonoBehaviour
{
    Camera mainCamera;
    CouplerManager couplerManager;
    SwitchManager switchManager;
    Player player;

    private void Awake()
    {
        mainCamera = FindObjectOfType<ConductorCameraController> ().GetComponent<Camera> ();
        couplerManager = FindObjectOfType<CouplerManager> ();
        switchManager = FindObjectOfType<SwitchManager> ();
        player = FindObjectOfType<Player> ();
    }

    void Update()
    {
        if ( Input.touchCount == 1 && EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) )
            return;

        if ( !EventSystem.current.IsPointerOverGameObject () )
        {
            Vector3 click = Vector3.one;

            if ( Input.GetMouseButtonDown (0) )
            {
                Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if ( Physics.Raycast (ray, out hit) )
                {
                    click = hit.point;
                }
                
                if(hit.collider != null )
                {
                    SelectEvent (hit.collider);
                }
            }
        }
    }

    
    private void SelectEvent( Collider collider )
    {
        if ( collider.CompareTag ("ActiveCoupler") )
            couplerManager.UncoupleListener (collider);
        else if ( collider.CompareTag ("Engine") )
            player.RollingStockListener (collider);
        else if ( collider.CompareTag ("RollingStock") )
            player.RollingStockListener (collider);
        else if ( collider.CompareTag ("Lever") )
            switchManager.TurnHandSwitchListener (collider);

    }
}
