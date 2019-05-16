using UnityEngine;

public class CouplerObject : MonoBehaviour
{

    public Transform OtherCouplerPoint { get; set; }
    private Transform couplerTransform;
    private Quaternion defaultRotation;

    //values for internal use
    private Quaternion lookRotation;
    private Vector3 direction;
    private void Awake()
    {
        couplerTransform = GetComponent<Transform> ();
        defaultRotation = couplerTransform.localRotation;
    }

    private void Update()
    {
        if ( OtherCouplerPoint )
        {
            CouplerRotation (OtherCouplerPoint);
        }

    }

    public void CouplerRotation( Transform other )
    {
        if ( other )
        {
            //find the vector pointing from our position to the target
            direction = (other.position - couplerTransform.position).normalized;
            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation (direction);
            lookRotation *= Quaternion.Euler (0, -90, 0);
            //rotate us over time according to speed until we are in the required rotation
            couplerTransform.rotation = lookRotation;
        }

    }

    public void SetDefaultRotation()
    {
        couplerTransform.localRotation = defaultRotation;
    }
}
