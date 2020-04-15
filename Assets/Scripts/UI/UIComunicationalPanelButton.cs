using UnityEngine;
using UnityEngine.UI;

public class UIComunicationalPanelButton : MonoBehaviour
{
    private GameObject [] childButtObjects;
    private UIChildButton [] childButtons;
    private UIComunicationalPanelButton [] panelButtons;

    public void Awake()
    {
        panelButtons = FindObjectsOfType<UIComunicationalPanelButton> ();
        childButtons = GetComponentsInChildren<UIChildButton> ();
        childButtObjects = new GameObject [childButtons.Length];
        for ( int i = 0; i < childButtons.Length; i++ )
        {
            childButtObjects [i] = childButtons[i].gameObject;
        }
        GetComponent<Button> ().onClick.AddListener (OpenMenu);
    }

    public void OpenMenu()
    {
        CloseAllPanels ();

        for ( int i = 0; i < childButtObjects.Length; i++ )
        {
            childButtObjects [i].SetActive (true);
        }
    }

    public void CloseMenu()
    {
        for ( int i = 0; i < childButtObjects.Length; i++ )
        {
            childButtObjects [i].SetActive (false);
        }
    }

    void Start()
    {
        CloseMenu ();
    }

    public void CloseAllPanels()
    {
        for ( int i = 0; i < panelButtons.Length; i++ )
        {
            panelButtons [i].CloseMenu();
        }
    }


}
