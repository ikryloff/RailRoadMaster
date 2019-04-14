using UnityEngine;

public class Bogey : MovableObject
{
    [SerializeField]
    private RollingStock rollingStock;
    //  Class provides track with presence of bogey
    private TrackCircuitPresence presence;
    public float offset;
    public SwitchManager switchManager;
    private bool isRightBogey;

    
    // which is of 2 bogeys (-1 = left; +1 = right)
    public int bogeyPos;    

    private void Awake()
    {
        Path = FindObjectOfType<TrackPath>();
        OwnTransform = gameObject.GetComponent<Transform>();

        switchManager = FindObjectOfType<SwitchManager>();
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
        presence = new TrackCircuitPresence(OwnTrackCircuit);
        UpdatePath();
    }

    void Update()
    {
        MoveByPath();
        presence.SetPresence(this, OwnTrackCircuit);
        ImproveBogeysPosition();

    }
    void ImproveBogeysPosition()
    {
        if(rollingStock.OwnTrack == OwnTrack)
        {
            if(OwnPosition != rollingStock.OwnPosition + offset)
            {
                OwnPosition = rollingStock.OwnPosition + offset;
                //print("improve bogeys");
            }
        }

    }
   
}





