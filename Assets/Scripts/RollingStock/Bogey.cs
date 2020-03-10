using System;
using UnityEngine;

public class Bogey : MovableObject
{
    private RollingStock rollingStock;
    public MeshRenderer [] BogeysMeshRenderers;
    [SerializeField]
    private float offset;
    private bool isRightBogey;
    private TrackCircuit tempTC;

    
    // which is of 2 bogeys (-1 = left; +1 = right)
    public int bogeyPos;

    public float Offset
    {
        get
        {
            return offset;
        }        
    }

    private void Awake()
    {
        OwnTransform = gameObject.GetComponent<Transform>();
        rollingStock = GetComponentInParent<RollingStock>();
        OwnEngine = rollingStock.GetComponent<Engine>();
        bogeyPos = Offset > 0 ? 1 : -1;
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
        OwnPosition = rollingStock.OwnPosition + Offset;
        OwnTrack = rollingStock.OwnTrack;
        OwnPath = rollingStock.OwnPath;
        OwnTrackCircuit = OwnTrack.TrackCircuit;
        tempTC = OwnTrackCircuit;
        OwnTrackCircuit.AddCars (this);
    }

   

    public override void MoveAndRotate()
    {
        Vector3 tangent;
        OwnTransform.position = OwnTrack.trackMath.CalcPositionAndTangentByDistance (OwnPosition, out tangent);
        OwnTransform.rotation = Quaternion.LookRotation (tangent);
        OwnTransform.rotation *= Quaternion.Euler (0, -90, 0);

        ProvidePresence ();
    }

    public void ProvidePresence()
    {
        if ( tempTC.Equals (OwnTrackCircuit) )
            return;
        tempTC.RemoveCars (this);
        if(OwnTrackCircuit)
            OwnTrackCircuit.AddCars (this);
        tempTC = OwnTrackCircuit;

    }


}





