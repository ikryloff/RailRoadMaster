using UnityEngine;

public class CouplerPoint : MonoBehaviour
{

    public bool IsInConnection { get; set; }
    public bool IsAbleToConnect { get; set; }
    public RSConnection RSConnection { get; set; }
    public RSConnection OtherRSConnection { get; set; }
    public CouplerObject CouplerObject { get; set; }
    public Transform PointTransform { get; private set; }
    private CouplerPoint otherPoint;

    // Use this for initialization
    void Start()
    {
        CouplerObject = transform.parent.GetComponentInChildren<CouplerObject> ();
        RSConnection = GetComponentInParent<RSConnection> ();
        PointTransform = GetComponent<Transform> ();
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( IsAbleToConnect )
        {
            otherPoint = other.GetComponent<CouplerPoint> ();
            OtherRSConnection = otherPoint.RSConnection;
            RSConnection.MakeConnection (OtherRSConnection);            
            EventManager.OnCompositionChanged ();
            IsAbleToConnect = false;
            CouplerObject.OtherCouplerPoint = otherPoint.PointTransform;
            otherPoint.CouplerObject.OtherCouplerPoint = PointTransform;
        }
        
    }
}
