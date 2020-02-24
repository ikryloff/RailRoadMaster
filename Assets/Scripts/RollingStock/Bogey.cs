using System;
using UnityEngine;

public class Bogey : MovableObject
{
    private RollingStock rollingStock;
    public MeshRenderer [] BogeysMeshRenderers;
    [SerializeField]
    private float offset;
    private bool isRightBogey;

    
    // which is of 2 bogeys (-1 = left; +1 = right)
    public int bogeyPos;    

    private void Awake()
    {
        OwnTransform = gameObject.GetComponent<Transform>();
        rollingStock = GetComponentInParent<RollingStock>();
        OwnEngine = rollingStock.GetComponent<Engine>();
        bogeyPos = offset > 0 ? 1 : -1;
        isRightBogey = bogeyPos == 1 ? true : false;
        
    }

    public void GetMeshRenderers()
    {
        BogeysMeshRenderers = GetComponentsInChildren<MeshRenderer> ();
    }

    private void Start()
    {
        ResetBogey ();
    }

    public void ResetBogey()
    {
        OwnPosition = rollingStock.OwnPosition + offset;
        OwnTrack = rollingStock.OwnTrack;
        OwnPath = rollingStock.OwnPath;
        OwnTrackCircuit = OwnTrack.TrackCircuit;
        OwnTrackCircuit.AddCars (this);
    }

   
    
    private void ImproveBogeysPosition()
    {
        if(rollingStock.OwnTrack.Equals( OwnTrack ) )
        {
            if(OwnTransform.localPosition.x != offset )
            {
                OwnPosition = rollingStock.OwnPosition + offset;
            }            
        }       
    }


    public override void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance (OwnPosition, out tangent);
        ImproveBogeysPosition ();        
        OwnTransform.rotation = Quaternion.LookRotation (tangent);
        OwnTransform.rotation *= Quaternion.Euler (0, -90, 0);

    }


}





