using UnityEngine;

public class RSConnection : MonoBehaviour, IManageable
{
    public RSConnection RightCar;
    public RSConnection LeftCar { get; set; }
    public RollingStock RollingStock { get; private set; }
    private Coupler [] couplers;
    public Coupler CouplerRight { get; private set; }
    public Coupler CouplerLeft { get; private set; }
    public CouplerPoint CouplerPointRight { get; private set; }
    public CouplerPoint CouplerPointLeft { get; private set; }
    public RollingStock TempCar { get; private set; }
    public bool IsConnectedRight { get; set; }
    public RSComposition RSComposition { get; set; }
    private float rSOffset = 80f;
    private Coupler coupler;
    public bool JustUncoupled;
    private float tempDist;
    private float checkDist;
    private float smooth = 0.5f;

    public void Init()
    {
        RollingStock = gameObject.GetComponent<RollingStock> ();
        // set couplers to RS
        SetCouplers ();
    }

    public void OnStart()
    {
        if ( RightCar )
        {
            MakeConnection (RightCar);
        }

    }

    public void MakeConnection( RSConnection otherCar )
    {
        IsConnectedRight = true;
        RightCar = otherCar;
        RightCar.LeftCar = this;
        // this coupler is in connection
        CouplerRight.MakeCouplerConnection ();
        CouplerPointRight.MakePointConnection (otherCar);
        //Global Update all compositions
        CompositionManager.Instance.UpdateCompositions ();
        //stops Engine after coupling
        EventManager.CarsCoupled ();
    }

    public void DestroyConnection()
    {
        IsConnectedRight = false;
        JustUncoupled = true;
        TempCar = RightCar.RollingStock;
        tempDist = TempCar.OwnRun - RollingStock.OwnRun;
        RightCar.LeftCar = null;
        RightCar = null;
        CouplerRight.DestroyCouplerConnection ();
        CouplerPointRight.DestroyPointConnection ();
        //Global Update all compositions
        CompositionManager.Instance.UpdateCompositions ();
    }

    private void Update()
    {
        ImproveRSPositionWithConnection ();
        CheckUncouplingListener ();
    }

    private void CheckUncouplingListener()
    {
        if ( JustUncoupled )
        {
            //delta movement after Uncouple
            checkDist = TempCar.OwnRun - RollingStock.OwnRun - tempDist;
            // if we move towards uncoupled car, we connect again. If we ve moved opposite direction triggers enable again
            if ( checkDist < -0.6 || checkDist > 3 )
            {
                CouplerPointRight.transform.localPosition = CouplerPointRight.PointLocalPosition;
                JustUncoupled = false;
            }
        }
    }

    private void SetCouplers()
    {
        couplers = GetComponentsInChildren<Coupler> ();
        CouplerLeft = couplers [0].transform.position.x < couplers [1].transform.position.x ? couplers [0] : couplers [1];
        CouplerRight = CouplerLeft == couplers [0] ? couplers [1] : couplers [0];
        CouplerPointRight = CouplerRight.GetComponentInChildren<CouplerPoint> ();
        CouplerPointLeft = CouplerLeft.GetComponentInChildren<CouplerPoint> ();
        //only Right coupler can connect
        CouplerPointRight.IsAbleToConnect = true;
        CouplerPointLeft.IsAbleToConnect = false;
    }

    void ImproveRSPositionWithConnection()
    {
        if ( RightCar && RightCar.RollingStock.OwnTrack.Equals (RollingStock.OwnTrack) )
        {
            if ( Mathf.Abs (RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - rSOffset) > 0.5 )
            {
                //print ("Improved  " + (RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - rSOffset));                
                RightCar.RollingStock.OwnPosition = Mathf.Lerp(RightCar.RollingStock.OwnPosition, RollingStock.OwnPosition + rSOffset, smooth);              

            }

        }
    }
}
