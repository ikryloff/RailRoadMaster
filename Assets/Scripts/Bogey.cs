using UnityEngine;

public class Bogey : MovableObject
{
    [SerializeField]
    private RollingStock rollingStock;
    
    public float offset;
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
        OwnPosition = rollingStock.OwnPosition + offset;
        OwnPath = rollingStock.OwnPath;
        OwnTrack = rollingStock.OwnTrack;        
        OwnTrackCircuit = OwnTrack.TrackCircuit;
       
        
    }

    void Update()
    {
        //base class method of moving
        MoveByPath();
        
        ImproveBogeysPosition();

    }
    
    void ImproveBogeysPosition()
    {
        if(rollingStock.OwnTrack == OwnTrack)
        {
            if(OwnPosition != rollingStock.OwnPosition + offset)
                OwnPosition = rollingStock.OwnPosition + offset;            
        }

    }
   
}





