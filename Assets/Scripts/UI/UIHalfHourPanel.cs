using UnityEngine;

public class UIHalfHourPanel : MonoBehaviour
{
    UIAlfaPanel alfaPanel;
    UIBravoPanel bravoPanel;
    UICharliePanel charliePanel;

    public void OnAwake()
    {
        alfaPanel = GetComponentInChildren<UIAlfaPanel> ();
        bravoPanel = GetComponentInChildren<UIBravoPanel> ();
        charliePanel = GetComponentInChildren<UICharliePanel> ();        
    }
}
