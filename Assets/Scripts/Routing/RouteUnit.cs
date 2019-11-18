using UnityEngine;

public class RouteUnit : MonoBehaviour
{

    [SerializeField]
    private int num;

    public int Num
    {
        get
        {
            return num;
        }

    }
    public bool IsExist { get; set; }

    private void Start()
    {
        IsExist = false;
        DoRouteUnit (false);
    }

    public void DoRouteUnit( bool isVisible )
    {
        IsExist = isVisible;
    }
}
