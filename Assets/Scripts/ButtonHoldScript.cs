using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHoldScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RemoteControlScript rcs;

    public void OnPointerDown(PointerEventData eventData)
    {
        rcs.ShowSwitches();        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rcs.DefaultSwitches();
    }
      
}
