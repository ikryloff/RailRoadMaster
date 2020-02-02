using UnityEngine;

public class CarProperties : MonoBehaviour
{

    public bool IsLoaded;
    public bool IsWaggon;
    public int Destination;
    public int ReturnPoint;
    Commodity commodity;
    public int commodityNumber;

    // 7 - oil
    // 6 - wood
    // 2 - goods
    // 0 - passengers

    private void Awake()
    {
        commodity = GetComponentInChildren<Commodity> ();
        if ( commodity )
        {
            IsWaggon = true;
            CalcCommodity (GetComponent<RollingStock> ().Number);
        }
        else
            commodityNumber = -1;
        
    }
       

    private void CalcCommodity(int num)
    {
        int firstNum = (int)Mathf.Floor (num / 1000);

        commodityNumber = firstNum;

    }

}
