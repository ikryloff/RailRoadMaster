using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;
using System;

public class RollingStock : MovableObject, IManageable
{
    public float PositionInPath { get; set; }

    [SerializeField]
    private TrackPathUnit thisRSTrack;
    public string Number { get; set; }
    public bool Brakes { get; set; }

    [SerializeField]
    public float breakeForce;
    public RSConnection rSConnection;
     
    [SerializeField]
    private float rsPosition;

    public bool isDirectionChanged;    

    public float acceleration;
    public float movingSpeed;
    public float pathLength;

   

    private Bogey[] bogeys;
    public Bogey bogeyLeft { get; set; }
    public Bogey bogeyRight { get; set; }
    private Transform bogeyLeftTransform;
    private Transform bogeyRightTransform;

    Vector3 dir;
    float angle;
    
    public void Init()
    {        
        EventManager.onPathChanged += UpdatePath;       
        OwnTransform = gameObject.GetComponent<Transform> ();
        OwnTrack = thisRSTrack;
        OwnEngine = GetComponent<Engine>();
        OwnPosition = rsPosition;
        rSConnection = gameObject.GetComponent<RSConnection> ();

        // set bogeys to RS
        SetBogeys ();
        OwnTrackCircuit = OwnTrack.TrackCircuit;
    }

    public void OnStart()
    {
        Brakes = true;
        acceleration = 0;
        IsMoving = true;
        UpdatePath ();               
    }
          
    void Update()
    {        
        MoveByPath(); 
    }

    public void UpdatePath()
    {
        TrackPath.Instance.GetTrackPath(this);
        bogeyLeft.OwnPath = OwnPath;
        bogeyRight.OwnPath = OwnPath;        
    }
   
    // forced changing direction
    public void ChangeDirection()
    {
        acceleration *= -1;
        if(!IsMoving)
        {
            IsMoving = true;
        }
       
    }

    public void CalcPositionInPath()
    {
        if ( OwnPath != null )
        {
            PositionInPath = 0;
            foreach ( TrackPathUnit item in OwnPath )
            {
                if ( item == OwnTrack )
                {
                    PositionInPath += OwnPosition;
                    break;
                }
                else
                    PositionInPath += item.PathLenght;
            }
        }
    }

    private void SetBogeys()
    {
        bogeys = GetComponentsInChildren<Bogey>();
        bogeyLeft = bogeys[0].transform.position.x < bogeys[1].transform.position.x ? bogeys[0] : bogeys[1];
        bogeyRight = bogeyLeft == bogeys[0] ? bogeys[1] : bogeys[0];
        bogeyLeftTransform = bogeyLeft.GetComponent<Transform>();
        bogeyRightTransform = bogeyRight.GetComponent<Transform>();
    }

    public override void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance(OwnPosition, out tangent);
        dir = bogeyRightTransform.position - bogeyLeftTransform.position;
        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        OwnTransform.rotation = Quaternion.Euler(0, angle, 0);
        OwnTransform.rotation *= Quaternion.Euler(0, -90, 0);
    }


}