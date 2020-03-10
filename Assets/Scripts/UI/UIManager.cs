using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    private CommunicationPanelManager communicationPanel;
    private RoutePanelManager routePanel;
    private HelpPanel helpPanel;

    private void Awake()
    {
        communicationPanel = FindObjectOfType<CommunicationPanelManager> ();
        routePanel = FindObjectOfType<RoutePanelManager> ();
        helpPanel = FindObjectOfType<HelpPanel> ();
    }

    public void OnStart()
    {
        DriveMode ();
    }

    public void DriveMode()
    {
        routePanel.Show (false);
        communicationPanel.Show (true);
        helpPanel.gameObject.SetActive (false);
        ModeSwitch.Instance.SwitchToConductorMode ();
    }

    public void RouteMode()
    {
        routePanel.Show (true);
        communicationPanel.Show (false);
    }

    public void HelpMode()
    {
        communicationPanel.Show (false);
        helpPanel.gameObject.SetActive (true);
    }
}
