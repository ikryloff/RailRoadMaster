using UnityEngine;
using UnityEngine.EventSystems;

public class CouplerManager : Singleton<CouplerManager>
{
    [SerializeField]
    private Texture2D cursor;
    private CouplerLever coupler;
    private CompositionManager cm;
    public GameObject[] couplerPictures;
    private bool isCouplerModeIsOn;

    public bool IsCouplerModeIsOn
    {
        get
        {
            return isCouplerModeIsOn;
        }

        set
        {
            isCouplerModeIsOn = value;
        }
    }

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();

        IsCouplerModeIsOn = true;
        
    }

    private void UncoupleListener()
    {
        if ( !EventSystem.current.IsPointerOverGameObject () )
        {
            Vector3 click = Vector3.one;

            if ( Input.GetMouseButtonDown (0) )
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if ( Physics.Raycast (ray, out hit) )
                {
                    click = hit.point;
                }

                if ( hit.collider != null && hit.collider.tag == "ActiveCoupler" )
                {
                    coupler = hit.collider.GetComponent<CouplerLever>();
                    coupler.Uncouple ();
                }
            }
        }       
    }

    private void Update()
    {
        UncoupleListener ();
    }


}