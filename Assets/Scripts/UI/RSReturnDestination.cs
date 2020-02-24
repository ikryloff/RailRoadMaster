using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSReturnDestination : MonoBehaviour
{
    public Image img;
    public Sprite Alfa;
    public Sprite Charlie;
    public Sprite Bravo;
   
    private int returnNumber;

    private void Awake()
    {
        img = GetComponent<Image> ();
        Alfa = ResourceHolder.Instance.Alfa_Station_Icon;
        Bravo = ResourceHolder.Instance.Bravo_Station_Icon;
        Charlie = ResourceHolder.Instance.Charlie_Station_Icon;       

    }

    // Return Destination
    // 0 - Bravo
    // 1 - Charlie
    // 2 - AlfaS

    public void SetIcon( RollingStock rs )
    {
        returnNumber = rs.CarProperties.ReturnPoint;
        if ( returnNumber == 0 )
            img.sprite = Bravo;
        else if ( returnNumber == 1 )
            img.sprite = Charlie;
        else if ( returnNumber == 2 )
            img.sprite = Alfa;
    }
}
