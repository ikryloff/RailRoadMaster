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
    public Button RButton { get; set; }


    public int Number
    {
        get
        {
            return number;
        }

    }

    void Awake()
    {
        panelManager = FindObjectOfType<RoutePanelManager> ();
        GetComponent<Button> ().onClick.AddListener (SendNumber);
        buttonImage = GetComponent<Image> ();
        RButton = GetComponent<Button> ();

        colorPressed = new Color32 (80, 90, 150, 255);
        colorOpened = new Color32 (218, 223, 230, 255);
        colorHasPresence = new Color32 (207, 72, 72, 255);
        colorDefault = new Color32 (90, 85, 85, 255);        
    }

   

    private void SendNumber()
    {
        SetPressedColor ();        
        panelManager.GetInput (number);        
    }

    public void SetPressedColor()
    {
        RButton.interactable = false;
    }   

    public void UpdateButtonState()
    {
        for ( int i = 0; i < tracks.Length; i++ )
        {
            TrackCircuit tc = tracks [i];
            if ( tc.HasCarPresence )
            {
                buttonImage.color = colorHasPresence;
                break;
            }
            else
                buttonImage.color = colorDefault;            
        }
    }


    public void SetRouteOff()
    {
        buttonImage.color = colorDefault;
    }
}
