using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSIcon : MonoBehaviour
{
    public Image img;
    public Sprite Gondola;
    public Sprite Loco;
    public Sprite BoxCar;
    public Sprite OilCar;
    public Sprite PassCar;
    public int firstNum;

    private void Awake()
    {
        img = GetComponent<Image> ();
        Gondola = ResourceHolder.Instance.Gondola_icon;
        Loco = ResourceHolder.Instance.Engine_icon;
        BoxCar = ResourceHolder.Instance.BoxCar_icon;
        OilCar = ResourceHolder.Instance.Oilcar_icon;
        PassCar = ResourceHolder.Instance.PassCar_icon;
    }

    public void CalcAndSetIcon( int number )
    {
        firstNum = (int)Mathf.Floor (number / 1000);

        if ( firstNum == 8 )
        {
            img.sprite = Loco;
        }
        else if ( firstNum == 6 )
        {
            img.sprite = Gondola;
        }
        else if ( firstNum == 2 )
        {
            img.sprite = BoxCar;
        }
        else if ( firstNum == 7 )
        {
            img.sprite = OilCar;
        }
        else if ( firstNum == 0 )
        {
            img.sprite = PassCar;
        }
    }
}
