using UnityEngine;
using UnityEngine.UI;

public class UICarPanel : MonoBehaviour
{
    public int CarPanelID;
    public Image CarPanelImage;
    public Sprite DefaultSprite;

    public Sprite Gondola;
    public Sprite Loco;
    public Sprite LocoLeft;
    public Sprite BoxCar;
    public Sprite OilCar;
    public Sprite PassCar;

    //operations 
    // 0 - stop
    // 1 - passanger stop
    // 2 - fast
    // 3 - normal
    // 4 - make
    // 5 - break
    public Sprite TrainFF;
    public Sprite TrainFB;
    public Sprite TrainForward;
    public Sprite TrainBackward;
    public Sprite TrainStop;
    public Sprite TrainBreak;
    public Sprite TrainMake;
    public Sprite PassStopTrain;

    private void Awake()
    {
        DefaultSprite = ResourceHolder.Instance.Square_Icon;
        Gondola = ResourceHolder.Instance.Gondola_icon;
        Loco = ResourceHolder.Instance.Engine_icon;
        LocoLeft = ResourceHolder.Instance.Engine_left_icon;
        BoxCar = ResourceHolder.Instance.BoxCar_icon;
        OilCar = ResourceHolder.Instance.Oilcar_icon;
        PassCar = ResourceHolder.Instance.PassCar_icon;

        TrainFF = ResourceHolder.Instance.TrainFF;
        TrainFB = ResourceHolder.Instance.TrainFB;
        TrainForward = ResourceHolder.Instance.TrainForward;
        TrainBackward = ResourceHolder.Instance.TrainBackward;
        TrainStop = ResourceHolder.Instance.TrainStop;
        TrainBreak = ResourceHolder.Instance.TrainBreak;
        TrainMake = ResourceHolder.Instance.TrainMake;
        PassStopTrain = ResourceHolder.Instance.PassStopTrain;

        CarPanelImage = GetComponent<Image> ();

        CarPanelImage.sprite = DefaultSprite;        
    }

    public void CalcAndSetCarIcon( int num )
    {
        num = (int)Mathf.Floor (num / 1000);
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
            if( CarPanelID == 1)
                CarPanelImage.sprite = LocoLeft;
            else
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

    public void CalcAndSetOperationIcon( int operation, int direction )
    {
        if( direction == 1 )
        {
            if(operation == 2)
                CarPanelImage.sprite = TrainFF;
            if(operation == 3)
                CarPanelImage.sprite = TrainForward;
        }

        else if ( direction == -1 )
        {
            if ( operation == 2 )
                CarPanelImage.sprite = TrainFB;
            if ( operation == 3 )
                CarPanelImage.sprite = TrainBackward;
        }

        if ( operation == 0 )
            CarPanelImage.sprite = TrainStop;
        else if ( operation == 1 )
            CarPanelImage.sprite = PassStopTrain;
        else if ( operation == 4 )
            CarPanelImage.sprite = TrainMake;
        else if ( operation == 5 )
            CarPanelImage.sprite = TrainBreak;
    }
}
