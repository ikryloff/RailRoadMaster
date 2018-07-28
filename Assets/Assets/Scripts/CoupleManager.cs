using UnityEngine;

public class CoupleManager : Singleton<CoupleManager>
{
    [SerializeField]
    private Texture2D cursor;
    private Couple couple;


    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

        if (hit.collider != null && hit.collider.name == "ActiveCouple")
        {
            couple = hit.collider.GetComponent<Couple>();
            if (couple.PassiveCoupleObj)
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {

                if (couple.JointCar)
                {
                    couple.PassiveCoupleObj = null;
                    Destroy(couple.JointCar);
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