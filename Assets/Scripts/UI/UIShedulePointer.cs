using UnityEngine;
using UnityEngine.UI;

public class UIShedulePointer : MonoBehaviour, IHideable
{
    Image image;
    public int PointerNumber { get; set; }
    public UIShedule Shedule { get; set; }

    private void Awake()
    {
        Shedule = FindObjectOfType<UIShedule> ();
        image = GetComponent<Image> ();
        Show (false);
        EventManager.onHourPassed += PointTheHour;
    }

    public void Show( bool isVisible )
    {
        image.enabled = isVisible;  
       
    }

    public void PointTheHour ()
    {
        if ( Shedule.gameObject.activeSelf )
        {            
            Show (TimeManager.Instance.TimeValue [0] == PointerNumber); 
        }
    }
}
