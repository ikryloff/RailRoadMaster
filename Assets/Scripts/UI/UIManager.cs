public class UIManager : Singleton<UIManager>
{

    private CommunicationPanelManager communicationPanel;
    private RoutePanelManager routePanel;
    private UIShedule shedule;
    private ShedulePanel shedulePanel;

    private void Awake()
    {
        communicationPanel = FindObjectOfType<CommunicationPanelManager> ();
        routePanel = FindObjectOfType<RoutePanelManager> ();
        shedule = FindObjectOfType<UIShedule> ();
        shedulePanel = FindObjectOfType<ShedulePanel> ();
    }

    public void OnStart()
    {
        DriveMode ();
    }

    public void DriveMode()
    {
        routePanel.Show (false);
        communicationPanel.Show (true);
        shedulePanel.gameObject.SetActive (false);
        ModeSwitch.Instance.SwitchToConductorMode ();
        GameManager.Instance.PauseOff ();
    }

    public void RouteMode()
    {
        routePanel.Show (true);
        communicationPanel.Show (false);
        ModeSwitch.Instance.SwitchToYardMode ();
        GameManager.Instance.PauseOn ();
    }

    public void SheduleMode()
    {
        communicationPanel.Show (false);
        shedulePanel.gameObject.SetActive (true);
        shedule.FocusPosition ();
        ModeSwitch.Instance.SwitchToSheduleMode ();
        GameManager.Instance.PauseOn ();
    }
}
