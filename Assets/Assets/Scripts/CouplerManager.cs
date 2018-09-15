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
        RunUncoupleMode();
    }

    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

        if (hit.collider != null && hit.collider.name == "ActiveCoupler" && IsCouplerModeIsOn)
        {
            coupler = hit.collider.GetComponent<Coupler>();
            if (coupler.OtherCoupler)
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (coupler.JointCar)
                {
                    coupler.Uncouple();
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    coupler.transform.Find("CouplerPicture").gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void RunUncoupleMode()
    {
        if (!IsCouplerModeIsOn)
        {
            foreach (GameObject cp in couplerPictures)
            {
                if (cp.GetComponentInParent<Coupler>().JointCar)
                {
                    cp.SetActive(true);
                }
                else
                    cp.SetActive(false);
                    
            }
            IsCouplerModeIsOn = true;
        }
        else
        {
            foreach (GameObject cp in couplerPictures)
            {
                cp.SetActive(false);
            }
            IsCouplerModeIsOn = false;
        }

    }

    public void ResetUncoupleMode()
    {
        RunUncoupleMode();
        RunUncoupleMode();
    }
}