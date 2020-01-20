public class UIManager : Singleton<UIManager>
{

    private CommunicationPanelManager communicationPanel;
    private RoutePanelManager routePanel;

    private void Awake()
    {
        communicationPanel = FindObjectOfType<CommunicationPanelManager> ();
        routePanel = FindObjectOfType<RoutePanelManager> ();
    }

    private void Start()
    {
        DriveMode ();
    }

    public void DriveMode()
    {
        routePanel.Show (false);
        communicationPanel.Show (true);
        ModeSwitch.Instance.SwitchToConductorMode ();
    }

    public void RouteMode()
    {
        routePanel.Show (true);
        communicationPanel.Show (false);
    }
}
