using System;
using UnityEngine;
using UnityEngine.UI;

public class UIChildButton : MonoBehaviour
{
    public UIComunicationalPanelButton PanelButton;

    public void Awake()
    {
        PanelButton = GetComponentInParent<UIComunicationalPanelButton> ();
        GetComponent<Button> ().onClick.AddListener (ButtonAction);
    }

    public virtual void ButtonAction()
    {
        throw new NotImplementedException ();
    }
}
