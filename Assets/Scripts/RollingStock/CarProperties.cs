using UnityEngine;

public class CarProperties : MonoBehaviour
{

    public bool IsLoaded;
    public bool IsWaggon;
    public int Destination;
    public int ReturnPoint;
    private RollingStock rollingStock;
    Commodity commodity;
    public int commodityNumber;

    //commodity
    // 7 - oil
    // 6 - wood
    // 2 - goods
    // 0 - passengers
    // Return Destination
    // 0 - Bravo
    // 1 - Charlie
    // 2 - Alfa

    private void Awake()
    {
        rollingStock = GetComponent<RollingStock> ();
        commodity = GetComponentInChildren<Commodity> ();
        if ( commodity )
        {
            IsWaggon = true;
            CalcCommodity (GetComponent<RollingStock> ().Number);
        }
        else
            commodityNumber = -1;

        CalcReturnPoint ();

    }


    private void CalcCommodity( int num )
    {
        int firstNum = (int)Mathf.Floor (num / 1000);

        commodityNumber = firstNum;

    }

    private void CalcReturnPoint()
    {
        ReturnPoint = Random.Range (1, 3);
        if ( rollingStock.Number == 8888 )
            ReturnPoint = 0;
    }

    public void SetReturnPoint(int num)
    {
        ReturnPoint = num;
    }

}
