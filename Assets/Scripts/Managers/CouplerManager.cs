using UnityEngine;

public class CouplerManager : Singleton<CouplerManager>, IManageable
{
    [SerializeField]
    private Texture2D cursor;
    private CouplerLever coupler;
    private CompositionManager cm;
    public GameObject [] couplerPictures;
    public Coupler [] couplers;
    private CouplerObject [] couplerObjects;


    public void Init()
    {
        cm = FindObjectOfType<CompositionManager> ();
        couplers = FindObjectsOfType<Coupler> ();
        couplerObjects = FindObjectsOfType<CouplerObject> ();
        foreach ( Coupler item in couplers )
        {
            item.SetCouplers ();
        }
    }

    public void OnStart()
    {
        foreach ( Coupler item in couplers )
        {
            item.SetLevers ();
        }
    }

    public void OnUpdate()
    {
        CouplerObjectRotation ();
    }
       
    private void CouplerObjectRotation()
    {
        for ( int i = 0; i < couplerObjects.Length; i++ )
        {
            couplerObjects [i].OnUpdate ();
        }
    }
    public void UncoupleListener(Collider collider)
    {
        coupler = collider.GetComponent<CouplerLever> ();
        coupler.Uncouple ();
    }
}