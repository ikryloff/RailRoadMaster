using UnityEngine;

public class Bogey : MovableObject
{
    private RollingStock rollingStock;
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

    private void Update()
    {
        ImproveBogeysPosition ();
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
}





