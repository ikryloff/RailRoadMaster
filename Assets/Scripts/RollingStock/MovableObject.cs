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
    private int koeff = 25;
    public static int temp = 0;

    
    

    public void MoveByPath()
    {
        
        if (OwnTrack)
        {
            // if engine attached move as engine, else 0 speed 
            Translation = OwnEngine ? OwnEngine.Acceleration : 0; 
            
            // Moving
            OwnPosition += Translation * Time.deltaTime * koeff;
            // Run of Movable Object
            OwnRun += Translation * Time.deltaTime * koeff;
            //Set presense to OwnTC
            OwnTrackCircuit.CarPresenceOn();

            // bogeys and RS rotate in different ways
            MoveAndRotate();

            if (Translation > 0 &&  OwnPosition > OwnTrack.PathLenght)
            {
                StepNextTrackPath();

                //Set off presense from old OwnTC
                OwnTrackCircuit.CarPresenceOff();
            }
            if (Translation < 0 && OwnPosition < 0)
            {
                StepPrevTrackPath();

                //Set off presense from old OwnTC
                OwnTrackCircuit.CarPresenceOff();
            }

            // Set new OWnTC
            OwnTrackCircuit = OwnTrack.TrackCircuit;           
        }
        else
        {
            IsMoving = false;
        }
    }

    private void StepPrevTrackPath()
    {
        OwnTrack = TrackPath.Instance.GetPrevTrack(OwnTrack, OwnPath);
        if (OwnTrack)
        {
            OwnPosition = OwnTrack.PathLenght + Translation * Time.deltaTime * koeff;
        }
        else
        {
            IsMoving = false;
            OwnTrack = OwnPath.First();
            OwnPosition = 0;
        }
    }

    private void StepNextTrackPath()
    {
        OwnTrack = TrackPath.Instance.GetNextTrack(OwnTrack, OwnPath);
        if (OwnTrack)
        {
            OwnPosition = Translation * Time.deltaTime * koeff;
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
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance(OwnPosition, out tangent);
        OwnTransform.rotation = Quaternion.LookRotation(tangent);
        OwnTransform.rotation *= Quaternion.Euler(0, -90, 0);
    }

    


}

