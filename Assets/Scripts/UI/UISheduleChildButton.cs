public class UISheduleChildButton : UIChildButton
{

    public override void ButtonAction()
    {
        PanelButton.CloseMenu ();
        UIManager.Instance.SheduleMode ();
    }
}
