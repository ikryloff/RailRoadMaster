using UnityEngine;
using UnityEngine.UI;

public class OTWorkSwitcher : MonoBehaviour
{
    OilPumpSwitcher pumpSwitcher;
    PostOrderButton orderButton;
    public Gates [] gates;
    private void Awake()
    {
        orderButton = FindObjectOfType<PostOrderButton> (); 
        pumpSwitcher = FindObjectOfType<OilPumpSwitcher> ();
        gates = pumpSwitcher.GetComponentsInChildren<Gates> ();
        GetComponent<Button> ().onClick.AddListener (ButtonAction);

    }

    private void Start()
    {
        pumpSwitcher.IsActivated = true;
        pumpSwitcher.ActivateOT (true);
        OpenGates (false);
    }

    public void ButtonAction()
    {
        if ( !pumpSwitcher.IsActivated )
        {
            pumpSwitcher.ActivateOT (true);
            OpenGates (false);
        }
        else
        {
            pumpSwitcher.ActivateOT (false);
            OpenGates (true);
        }
        EventManager.SignalChanged ();
    }

    private void OpenGates(bool isOpen)
    {
        gates [0].OpenGates (isOpen);
        gates [1].OpenGates (isOpen);
    }

}
