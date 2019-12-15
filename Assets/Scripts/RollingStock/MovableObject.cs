using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MovableObject : MonoBehaviour
{

    //Engine that moves Movable objects
    public Engine OwnEngine { get; set; }
    public TrackCircuit OwnTrackCircuit { get; set; }
    public float OwnPosition { get; set; }
    public float OwnRun { get; set; }
    public List<TrackPathUnit> OwnPath { get; set; }
    public TrackPathUnit OwnTrack { get; set; }
    public Transform OwnTransform { get; set; }
    public bool IsMoving { get; set; }
    // Moving distance per frame
    public float Translation { get; private set; }
    
    public static int temp = 0;
    private float smooth = 0.5f;
    private float step;
    private float TOLERANCE = 0.5f;


    public void MoveByPath( float step )
    {

        if ( OwnTrack )
        {
            // if engine attached move as engine, else 0 speed 
            if ( OwnEngine )
                Translation = OwnEngine.Acceleration;
            else
                Translation = 0;
            // Moving                    
            OwnPosition += step;
            // Run of Movable Object
            OwnRun += step;
            //Set presense to OwnTC
            OwnTrackCircuit.AddCars(this);
            // bogeys and RS rotate in different ways
            MoveAndRotate ();

            if ( Translation > 0 && OwnPosition >= OwnTrack.PathLenght - TOLERANCE )
            {
                StepNextTrackPath (step);                
                //Set off presense from old OwnTC
                OwnTrackCircuit.RemoveCars (this);
            }
            else if ( Translation < 0 && OwnPosition < TOLERANCE )
            {                
                StepPrevTrackPath (step);
                //Set off presense from old OwnTC
                OwnTrackCircuit.RemoveCars (this);
            }           
            // Set new OwnTC
            OwnTrackCircuit = OwnTrack.TrackCircuit;
        }
        else
        {
            IsMoving = false;
        }
    }

    private void StepPrevTrackPath( float _step )
    {
        OwnTrack = TrackPath.Instance.GetPrevTrack (OwnTrack, OwnPath);
        if ( OwnTrack )
        {
            OwnPosition = OwnTrack.PathLenght;
        }
        else
        {
            IsMoving = false;
            OwnTrack = OwnPath.First ();
            OwnPosition = 0;
        }
    }

    private void StepNextTrackPath( float _step )
    {
        OwnTrack = TrackPath.Instance.GetNextTrack (OwnTrack, OwnPath);
        
        if ( OwnTrack )
        {
            OwnPosition = 0;
        }
        else
        {
            IsMoving = false;
            OwnPosition = OwnTrack.PathLenght;
        }
    }

    public virtual void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance (OwnPosition, out tangent);
        OwnTransform.rotation = Quaternion.LookRotation (tangent);
        OwnTransform.rotation *= Quaternion.Euler (0, -90, 0);
    }




}

