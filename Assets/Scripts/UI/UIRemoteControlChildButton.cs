public class UIRemoteControlChildButton : UIChildButton
{
    public override void ButtonAction()
    {
        PanelButton.CloseMenu ();
        UIManager.Instance.RouteMode ();
    }
}
