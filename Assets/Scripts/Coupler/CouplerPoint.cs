using UnityEngine;

public class CouplerPoint : MonoBehaviour
{

    public bool IsAbleToConnect { get; set; }
    public RSConnection RSConnection { get; set; }
    public RSConnection OtherRSConnection { get; set; }
    public CouplerObject CouplerObject { get; set; }
    public Transform PointTransform { get; private set; }
    public Vector3 PointLocalPosition { get; private set; }
    private CouplerPoint otherPoint;
    public Collider CouplerCollider { get; private set; }


    // Use this for initialization
    void Start()
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
            otherPoint = other.GetComponent<CouplerPoint> ();
            OtherRSConnection = otherPoint.RSConnection;
            RSConnection.MakeConnection (OtherRSConnection);           
            IsAbleToConnect = false;            
            CouplerObject.OtherCouplerPoint = otherPoint.PointTransform;
            otherPoint.CouplerObject.OtherCouplerPoint = PointTransform;
        }        
    }

}
