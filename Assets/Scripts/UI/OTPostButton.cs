public class OTPostButton : PostOrderButton
{
    public override void ButtonAction()
    {
        base.ButtonAction ();
        if ( !IsActivated )
        {
            CommonCamera.SetCameraPositionOnPost (CommonCamera.OTFocusPoint);
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
