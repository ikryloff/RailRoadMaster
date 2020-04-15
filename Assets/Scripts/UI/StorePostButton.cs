public class StorePostButton : PostOrderButton
{
    public override void ButtonAction()
    {
        base.ButtonAction ();

        if ( !IsActivated )
        {
            CommonCamera.SetCameraPositionOnPost (CommonCamera.StoreFocusPoint);
            IsActivated = true;
        }
        else
        {
            IsActivated = false;
        }
    }
}
