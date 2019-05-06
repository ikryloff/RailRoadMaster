using UnityEngine;

public class CouplerManager : Singleton<CouplerManager>
{
    [SerializeField]
    private Texture2D cursor;
    private Coupler coupler;
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
        couplerPictures = GameObject.FindGameObjectsWithTag("CouplerMode");
        IsCouplerModeIsOn = true;
        
    }

    
   
}