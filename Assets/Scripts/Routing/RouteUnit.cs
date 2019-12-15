using UnityEngine;

public class RouteUnit : MonoBehaviour
{

    [SerializeField]
    private int num;
    public string RouteStringName;
    public bool IsInUse;

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
        IsInUse = false;
    }

    public void DoRouteUnit( bool isOn  )
    {
        IsExist = isOn;
    }
}
