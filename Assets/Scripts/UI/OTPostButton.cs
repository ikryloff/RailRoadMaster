public class OTPostButton : PostOrderButton
{
    public override void ButtonAction()
    {
        base.ButtonAction ();
        if ( !IsActivated )
        {
            CommonCamera.SetCameraPositionOnPost (CommonCamera.OTFocusPoint);
            IsActivated = true;
        }
        else
        {
            IsActivated = false;
        }
    }
}
