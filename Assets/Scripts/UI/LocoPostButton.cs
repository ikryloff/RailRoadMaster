using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocoPostButton : PostOrderButton
{
    public override void ButtonAction()
    {
        base.ButtonAction ();
        if ( !IsActivated )
        {
            CommonCamera.SetCameraPositionOnPost (CommonCamera.LocoPostFocusPoint);
            SetButtonsActive (true);
            IsActivated = true;
        }
        else
        {
            SetButtonsActive (false);
            IsActivated = false;
        }
    }
}
