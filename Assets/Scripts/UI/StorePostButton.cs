public class StorePostButton : PostOrderButton
{
    public override void ButtonAction()
    {
        base.ButtonAction ();

        if ( !IsActivated )
        {
            CommonCamera.SetCameraPositionOnPost (CommonCamera.StoreFocusPoint);
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
