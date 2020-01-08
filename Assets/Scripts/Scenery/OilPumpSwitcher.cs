using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OilPumpSwitcher : MonoBehaviour
{
    OilTubeBehaviour [] oilTubes;
    private void Start()
    {
        oilTubes = FindObjectsOfType<OilTubeBehaviour> ();
    }

    void Update()
    {
        TurnPumpListener ();

    }

    private void TurnPumpListener()
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
                if ( hit.collider != null && hit.collider.CompareTag ("OilPump") )
                {
                    foreach ( OilTubeBehaviour item in oilTubes )
                    {
                        item.SwitchTube ();
                    }
                }
            }
        }
    }
}
