using UnityEngine;

public class CouplerPoint : MonoBehaviour
{

    public bool IsAbleToConnect { get; set; }
    public RSConnection RSConnection { get; set; }
    public RSConnection OtherRSConnection { get; set; }
    public CouplerObject CouplerObject { get; set; }
    public Transform PointTransform { get; private set; }
    public Vector3 PointLocalPosition { get; private set; }
    public CouplerPoint OtherPoint { get; set; }
    public Collider CouplerCollider { get; private set; }


    // Use this for initialization
    private void Awake()
    {
        CouplerObject = transform.parent.GetComponentInChildren<CouplerObject> ();
        CouplerCollider = GetComponent<Collider> ();
        RSConnection = GetComponentInParent<RSConnection> ();
        PointTransform = GetComponent<Transform> ();
        PointLocalPosition = transform.localPosition;

    }

    private void OnTriggerEnter( Collider other )
    {
        if ( IsAbleToConnect )
        {
            OtherPoint = other.GetComponent<CouplerPoint> ();
            OtherRSConnection = OtherPoint.RSConnection;
            RSConnection.MakeConnection (OtherRSConnection);
        }
    }

    public void MakePointConnection( RSConnection otherCar )
    {
        OtherPoint = otherCar.CouplerPointLeft;
        SetCouplerObjectToLookAt (OtherPoint);
        IsAbleToConnect = false;
        CouplerCollider.enabled = false;
        otherCar.CouplerPointLeft.CouplerCollider.enabled = false;
        
    }

    public void DestroyPointConnection()
    {
        PointTransform.localPosition = PointLocalPosition - new Vector3 (5, 0, 0);
        CouplerCollider.enabled = true;
        OtherPoint.CouplerCollider.enabled = true;
        IsAbleToConnect = true;        
        NullCouplerObjectToLookAt ();
    }

    private void SetCouplerObjectToLookAt( CouplerPoint point )
    {
        CouplerObject.OtherCouplerPoint = point.PointTransform;
        point.CouplerObject.OtherCouplerPoint = PointTransform;
    }

    private void NullCouplerObjectToLookAt( )
    {
        OtherPoint.CouplerObject.OtherCouplerPoint = null;
        OtherPoint.CouplerObject.SetDefaultRotation ();
        CouplerObject.OtherCouplerPoint = null;
        CouplerObject.SetDefaultRotation ();
    }



}
