using System.Linq;
using UnityEngine;

public class RSConnection : MonoBehaviour, IManageable
{
    public RollingStock RollingStock { get; private set; }
    public RSConnection LeftCar;
    public RSConnection RightCar;
    
    private Coupler [] couplers;
    public Coupler CouplerRight { get; private set; }
    public Coupler CouplerLeft { get; private set; }
    public CouplerPoint CouplerPointRight { get; private set; }
    public CouplerPoint CouplerPointLeft { get; private set; }
    
    public bool IsConnectedRight { get; set; }
    public bool IsConnectedLeft { get; set; }
    public RSComposition RSComposition { get; set; }
    private float RSOffset = 96.5f;


    public void Init()
    {        
        RollingStock = gameObject.GetComponent<RollingStock> ();
        // set couplers to RS
        SetCouplers ();
    }

    public void OnStart()
    {
        if ( RightCar )
            CouplerPointRight.IsAbleToConnect = false;
    }

    public void MakeConnection(RSConnection otherCar)
    {
        SetEngineToConnectedCar (otherCar.RollingStock);
        IsConnectedRight = true;
        otherCar.IsConnectedLeft = true;
        RightCar = otherCar;
        otherCar.LeftCar = this;        
    }

    private void Update()
    {
        ImproveRSPositionWithConnection ();
    }


    public void SetEngineToConnectedCar( RollingStock connectedRS )
    {
        if ( RollingStock.OwnEngine )
        {
            connectedRS.OwnEngine = RollingStock.OwnEngine;
            connectedRS.bogeyLeft.OwnEngine = RollingStock.OwnEngine;
            connectedRS.bogeyRight.OwnEngine = RollingStock.OwnEngine;
        }
        else
        {
            RollingStock.OwnEngine = connectedRS.OwnEngine;
            RollingStock.bogeyLeft.OwnEngine = connectedRS.OwnEngine;
            RollingStock.bogeyRight.OwnEngine = connectedRS.OwnEngine;
        }
    }
       
    private void SetCouplers()
    {
        couplers = GetComponentsInChildren<Coupler> ();
        CouplerLeft = couplers [0].transform.position.x < couplers [1].transform.position.x ? couplers [0] : couplers [1];
        CouplerRight = CouplerLeft == couplers [0] ? couplers [1] : couplers [0];
        CouplerPointRight = CouplerRight.GetComponentInChildren<CouplerPoint> ();
        CouplerPointLeft = CouplerLeft.GetComponentInChildren<CouplerPoint> ();
        CouplerPointRight.IsAbleToConnect = true;
        CouplerPointLeft.IsAbleToConnect = false;
    }

    void ImproveRSPositionWithConnection()
    {
        if (RightCar && RightCar.RollingStock.OwnTrack == RollingStock.OwnTrack )
        {
            if ( RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset > 0.5 ||
                RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset  < - 0.5)
            {
                print ("Improved  " + (RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset));
                RightCar.RollingStock.OwnPosition = RollingStock.OwnPosition + RSOffset;                
            }
            
        }
    }
}
