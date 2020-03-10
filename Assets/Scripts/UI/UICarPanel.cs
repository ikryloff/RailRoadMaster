using UnityEngine;
using UnityEngine.UI;

public class UICarPanel : MonoBehaviour
{
    public int CarPanelID;
    public Image CarPanelImage;
    public Sprite DefaultSprite;

    public Sprite Gondola;
    public Sprite Loco;
    public Sprite BoxCar;
    public Sprite OilCar;
    public Sprite PassCar;

    private void Awake()
    {
        DefaultSprite = ResourceHolder.Instance.Square_Icon;
        Gondola = ResourceHolder.Instance.Gondola_icon;
        Loco = ResourceHolder.Instance.Engine_icon;
        BoxCar = ResourceHolder.Instance.BoxCar_icon;
        OilCar = ResourceHolder.Instance.Oilcar_icon;
        PassCar = ResourceHolder.Instance.PassCar_icon;

        CarPanelImage = GetComponent<Image> ();

        CarPanelImage.sprite = DefaultSprite;        
    }

    public void CalcAndSetIcon( int num)
    {        

        if ( num == 5 )
        {
            CarPanelImage.sprite = DefaultSprite;
        }
        else if ( num == 6 )
        {
            CarPanelImage.sprite = Gondola;
        }
        else if ( num == 2 )
        {
            CarPanelImage.sprite = BoxCar;
        }
        else if ( num == 7 )
        {
            CarPanelImage.sprite = OilCar;
        }
        else if ( num == 8 )
        {
            CarPanelImage.sprite = Loco;
        }
        else if ( num == 0 )
        {
            CarPanelImage.sprite = PassCar;
        }
        else
        {
            CarPanelImage.sprite = DefaultSprite;
        }
      
    }
}
