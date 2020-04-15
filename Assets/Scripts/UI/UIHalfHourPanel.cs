using UnityEngine;

public class UIHalfHourPanel : MonoBehaviour
{
    public UIAlfaPanel AlfaPanel;
    public UIBravoPanel BravoPanel;
    public UICharliePanel CharliePanel;
    public bool IsFull;

    public void OnAwake()
    {
        AlfaPanel = GetComponentInChildren<UIAlfaPanel> ();
        BravoPanel = GetComponentInChildren<UIBravoPanel> ();
        CharliePanel = GetComponentInChildren<UICharliePanel> ();        
    }
}
