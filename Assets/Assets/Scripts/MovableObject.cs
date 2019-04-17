﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MovableObject : MonoBehaviour
{

    //Engine that moves Movable objects
    public Engine OwnEngine { get; set; }

    //Whole Path of MovableObject
    public TrackPath Path { get; set; }
    
    public TrackCircuit OwnTrackCircuit { get; set; }

    public float OwnPosition { get; set; }
    public List<TrackPathUnit> OwnPath { get; set; }
    public TrackPathUnit OwnTrack { get; set; }

    public Transform OwnTransform { get; set; }
    public bool IsMoving { get; set; }
    // Moving distance per frame
    public float Translation { get; private set; }
    
    public void MoveByPath()
    {
        
        if (OwnTrack)
        {
            // if engine attached move as engine, else 0 speed 
            Translation = OwnEngine ? OwnEngine.acceleration : 0;            
            OwnPosition += Translation;
            // bogeys and RS rotate in different ways
            MoveAndRotate();
            if (Translation > 0 &&  OwnPosition > OwnTrack.trackMath.GetDistance())
            {
                OwnTrack = Path.GetNextTrack(OwnTrack, OwnPath);
                if (OwnTrack)
                {
                    OwnPosition = 0;
                }
                else
                {
                    IsMoving = false;                    
                    OwnPosition = OwnTrack.trackMath.GetDistance();

                }
            }
            if (Translation < 0 && OwnPosition < 0)
            {
                OwnTrack = Path.GetPrevTrack(OwnTrack, OwnPath);
                if (OwnTrack)
                {
                    OwnPosition = OwnTrack.trackMath.GetDistance();
                }

                else
                {
                   IsMoving = false;
                   OwnTrack = OwnPath.First();
                   OwnPosition = 0;

                }
            }
            OwnTrackCircuit = OwnTrack.TrackCircuit;           
        }
        else
        {
            IsMoving = false;
        }
    }

    public virtual void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance(OwnPosition, out tangent);
        OwnTransform.rotation = Quaternion.LookRotation(tangent);
        OwnTransform.rotation *= Quaternion.Euler(0, -90, 0);
    }

    public void UpdatePath()
    {        
        Path.GetTrackPath(this);     
    }

    

}

