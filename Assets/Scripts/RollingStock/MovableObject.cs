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

    float trackPos;


    private float smooth = 0.5f;
    private float TOLERANCE = 0.5f;


    public void MoveByPath( float step )
    {

        if ( OwnTrack )
        {
            Translation = step;

            if ( step > 0 && OwnPosition > OwnTrack.PathLenght - step )
            {
                StepNextTrackPath (step);
            }
            else if ( step < 0 && OwnPosition < -step )
            {
                StepPrevTrackPath (step);
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

    public void CalcCompositionPosition( MovableObject _car, float _offset )
    {
        trackPos = _car.OwnPosition + _offset;
        tmpTrack = OwnTrack;
        if ( trackPos <= 0 )
        {
            //Set off presense from old OwnTC
            OwnTrack = TrackPath.Instance.GetPrevTrack (_car.OwnTrack, _car.OwnPath);
            if ( OwnTrack )
            {
                OwnPosition = OwnTrack.PathLenght + trackPos;
                OwnTrackCircuit = OwnTrack.TrackCircuit;
            }
            else
            {
                OwnTrack = tmpTrack;
                OwnPosition = 0;
                OwnEngine.EngineStep = 0;
            }
        }
        else if ( trackPos >= _car.OwnTrack.PathLenght )
        {
            //Set off presense from old OwnTC
            OwnTrack = TrackPath.Instance.GetNextTrack (_car.OwnTrack, _car.OwnPath);
            if ( OwnTrack )
            {
                OwnPosition = trackPos - _car.OwnTrack.PathLenght;
                OwnTrackCircuit = OwnTrack.TrackCircuit;
            }
            else
            {
                OwnTrack = tmpTrack;
                OwnPosition = tmpTrack.PathLenght;
                OwnEngine.EngineStep = 0;
            }
        }
        else
        {
            if ( !OwnTrack.Equals (_car.OwnTrack) )
            {
                OwnTrack = _car.OwnTrack;
                OwnTrackCircuit = OwnTrack.TrackCircuit;
            }
            OwnPosition = trackPos;
        }
        OwnRun = _car.OwnRun + _offset;
        Translation = _car.Translation;
        MoveAndRotate ();
    }

    private void StepNextTrackPath( float _step )
    {
        tmpTrack = OwnTrack;
        OwnTrack = TrackPath.Instance.GetNextTrack (OwnTrack, OwnPath);
        if ( OwnTrack )
            OwnPosition = _step / 2;
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
    }




}

