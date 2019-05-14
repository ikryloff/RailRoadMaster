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
    private Coupler coupler;
    public bool JustUncoupled;
    private float tempPosition;

    public void Init()
    {
        RollingStock = gameObject.GetComponent<RollingStock> ();
        coupler = GetComponentInChildren<Coupler> ();
        // set couplers to RS
        SetCouplers ();
    }

    public void OnStart()
    {
        if ( RightCar )
            CouplerPointRight.IsAbleToConnect = false;
    }

    public void MakeConnection( RSConnection otherCar )
    {
        IsConnectedRight = true;
        otherCar.IsConnectedLeft = true;
        RightCar = otherCar;
        RightCar.LeftCar = this;
        // this coupler is in connection
        coupler.IsInConnection = true;
        coupler.SetLeverActive ();
        CouplerPointRight.CouplerCollider.enabled = false;
        RightCar.CouplerPointLeft.CouplerCollider.enabled = false;
        //Global Update all compositions
        CompositionManager.Instance.UpdateCompositions ();
    }

    public void DestroyConnection()
    {
        CouplerPointRight.PointTransform.localPosition = CouplerPointRight.PointLocalPosition - new Vector3 (5, 0, 0);
        JustUncoupled = true;
        tempPosition = RollingStock.OwnRun;
        CouplerPointRight.CouplerCollider.enabled = true;
        RightCar.CouplerPointLeft.CouplerCollider.enabled = true;
        IsConnectedRight = false;
        RightCar.IsConnectedLeft = false;
        RightCar.LeftCar = null;
        RightCar.CouplerPointLeft.CouplerObject.OtherCouplerPoint = null;
        RightCar = null;
        CouplerPointRight.IsAbleToConnect = true;
        CouplerPointRight.CouplerObject.OtherCouplerPoint = null;
        CouplerRight.SetLeverUnactive ();
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
            if ( RollingStock.Translation > 0 || tempPosition - RollingStock.OwnRun > 3 )
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
        if ( RightCar && RightCar.RollingStock.OwnTrack == RollingStock.OwnTrack )
        {
            if ( RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset > 0.5 ||
                RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset < -0.5 )
            {
                print ("Improved  " + (RightCar.RollingStock.OwnPosition - RollingStock.OwnPosition - RSOffset));
                RightCar.RollingStock.OwnPosition = RollingStock.OwnPosition + RSOffset;
            }

        }
    }
}
