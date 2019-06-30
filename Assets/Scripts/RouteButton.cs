using System;
using UnityEngine;
using UnityEngine.UI;

public class RouteButton : MonoBehaviour
{
    private RoutePanelManager panelManager;
    [SerializeField]
    private int number;
    public Image buttonImage { get; set; }
    [SerializeField]
    private TrackCircuit [] tracks;
    Color32 colorPressed;
    Color32 colorOpened;
    Color32 colorHasPresence;
    Color32 colorDefault;


    public int Number
    {
        get
        {
            return number;
        }

    }

    public bool IsStartsRoute { get; set; }
    void Awake()
    {
        panelManager = FindObjectOfType<RoutePanelManager> ();
        GetComponent<Button> ().onClick.AddListener (SendNumber);
        buttonImage = GetComponent<Image> ();

        colorPressed = new Color32 (80, 90, 150, 255);
        colorOpened = new Color32 (218, 223, 230, 255);
        colorHasPresence = new Color32 (207, 72, 72, 255);
        colorDefault = new Color32 (90, 85, 85, 255);
    }
   

    private void SendNumber()
    {
        panelManager.GetInput (number);
        SetPressedColor ();
    }

    public void SetPressedColor()
    {
        buttonImage.color = colorPressed;
    }

    public void SetInRouteShuntingFirst()
    {
        SetInRouteImage ();
        IsStartsRoute = true;
    }

    public void UpdateButtonState()
    {
        if ( IsStartsRoute )
            return;
        foreach ( TrackCircuit item in tracks )
        {
            if ( item.HasCarPresence )
            {
                buttonImage.color = colorHasPresence;
                break;
            }
            else
                buttonImage.color = colorDefault;
        }
    }


    public void SetInRouteImage()
    {
        buttonImage.color = colorOpened;
    }

    public void SetRouteOff()
    {
        IsStartsRoute = false;
        buttonImage.color = colorDefault; 
    }
}
