using UnityEngine;

public class CoupleManager : Singleton<CoupleManager>
{
    [SerializeField]
    private Texture2D cursor;
    private Coupler coupler;
    private CompositionManager cm;

    private void Awake()
    {
        cm = GameObject.Find("CompositionManager").GetComponent<CompositionManager>();
    }

    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

        if (hit.collider != null && hit.collider.name == "ActiveCoupler")
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
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}