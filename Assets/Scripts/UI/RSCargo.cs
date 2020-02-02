using System;

using UnityEngine;
using UnityEngine.UI;

public class RSCargo : MonoBehaviour
{
    public Image img;
    public Sprite Empty;
    public Sprite WithOil;
    public Sprite WithGoods;
    public Sprite WithWood;
    public Sprite WithPassengers;
    private int commodityNumber;

    private void Awake()
    {
        img = GetComponent<Image> ();
        Empty = ResourceHolder.Instance.Empty_Car_Icon;
        WithOil = ResourceHolder.Instance.WithOil_Car_Icon;
        WithGoods = ResourceHolder.Instance.WithGoods_Car_Icon;
        WithWood = ResourceHolder.Instance.WithWood_Car_Icon;
        WithPassengers = ResourceHolder.Instance.WithPassengers_Car_Icon;

    }

    // 7 - oil
    // 6 - wood
    // 2 - goods
    // 0 - passengers
    // -1 - Empty

    public void SetIcon(RollingStock rs)
    {
        if ( rs.CarProperties.IsLoaded )
        {
            commodityNumber = rs.CarProperties.commodityNumber;
            if ( commodityNumber == 7 )
                img.sprite = WithOil;
            else if ( commodityNumber == 6 )
                img.sprite = WithWood;
            else if ( commodityNumber == 2 )
                img.sprite = WithGoods;
            else if ( commodityNumber == 0 )
                img.sprite = WithPassengers;
        }        
        else
            img.sprite = Empty;
    }
}
