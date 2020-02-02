﻿using UnityEngine;
using UnityEngine.EventSystems;

public class CouplerManager : Singleton<CouplerManager>, IManageable
{
    [SerializeField]
    private Texture2D cursor;
    private CouplerLever coupler;
    private CompositionManager cm;
    public GameObject[] couplerPictures;
    public Coupler [] couplers;
    private Camera mainCamera;
    private CouplerObject[] couplerObjects;

  
    public void Init()
    {
        cm = FindObjectOfType<CompositionManager> ();
        couplers = FindObjectsOfType<Coupler> ();
        couplerObjects = FindObjectsOfType<CouplerObject> ();
        foreach ( Coupler item in couplers )
        {
            item.SetCouplers ();
        }
    }

    public void OnStart()
    {
        mainCamera = FindObjectOfType<ConductorCameraController> ().GetComponent<Camera>();
        foreach ( Coupler item in couplers )
        {
            item.SetLevers ();
        }
    }

    public void OnUpdate()
    {
        UncoupleListener ();
        CouplerObjectRotation ();
    }



    private void CouplerObjectRotation()
    {
        for ( int i = 0; i < couplerObjects.Length; i++ )
        {
            couplerObjects [i].OnUpdate ();
        }
    }
    private void UncoupleListener()
    {
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

                if ( hit.collider != null && hit.collider.tag == "ActiveCoupler" )
                {
                    coupler = hit.collider.GetComponent<CouplerLever>();
                    coupler.Uncouple ();
                }
            }
        }       
    }
}