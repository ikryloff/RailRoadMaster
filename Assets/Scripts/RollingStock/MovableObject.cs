using System.Collections.Generic;
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
    public TrackPathUnit tmpTrack { get; set; }
    public Transform OwnTransform { get; set; }
    // Moving distance per frame
    public float Translation { get; set; }
    
    private float smooth = 0.5f;
    private float TOLERANCE = 0.5f;


    public void MoveByPath( float step )
    {

        if ( OwnTrack )
        {
            Translation = step;           
            //Set presense to OwnTC
            OwnTrackCircuit.AddCars(this);           
            if ( step > 0 && OwnPosition > OwnTrack.PathLenght - step )
            {
                StepNextTrackPath (step);                
                //Set off presense from old OwnTC
                OwnTrackCircuit.RemoveCars (this);
            }
            else if ( step < 0 && OwnPosition < -step )
            {                
                StepPrevTrackPath (step);
                //Set off presense from old OwnTC
                OwnTrackCircuit.RemoveCars (this);
            }
            else
            {
                // Moving                    
                OwnPosition += step;                
            }
            // Run of Movable Object
            OwnRun += step;
            // Set new OwnTC
            OwnTrackCircuit = OwnTrack.TrackCircuit;
            // bogeys and RS rotate in different ways
            MoveAndRotate ();
        }        
    }

    private void StepNextTrackPath(float _step)
    {
        tmpTrack = OwnTrack;
        OwnTrack = TrackPath.Instance.GetNextTrack (OwnTrack, OwnPath);
        if(OwnTrack)
            OwnPosition = _step/2;
        else
        {
            OwnTrack = tmpTrack;
            OwnPosition = tmpTrack.PathLenght;
            OwnEngine.EngineStep = 0;
        }
    }

    private void StepPrevTrackPath( float _step )
    {
        tmpTrack = OwnTrack;
        OwnTrack = TrackPath.Instance.GetPrevTrack (OwnTrack, OwnPath);
        if ( OwnTrack )
            OwnPosition = OwnTrack.PathLenght + _step / 2;
        else
        {
            OwnTrack = tmpTrack;
            OwnPosition = 0;
            OwnEngine.EngineStep = 0;
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

