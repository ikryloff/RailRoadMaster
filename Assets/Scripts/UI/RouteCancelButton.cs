using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteCancelButton : MonoBehaviour
{
    public int RouteExistNumber { get; set; }
    private TextMeshProUGUI routeCancelNumber;
    public Button CancelRouteButton;
    private RoutePanelManager panelManager;

    private void Awake()
    {
        panelManager = FindObjectOfType<RoutePanelManager> ();
        CancelRouteButton = GetComponent<Button> ();
        CancelRouteButton.onClick.AddListener (CancelRoute);
        routeCancelNumber = GetComponentInChildren<TextMeshProUGUI> ();
    }

    void Start()
    {

    }

    public void SetTextRouteNumber(string routeName)
    {
        routeCancelNumber.text = routeName;
    }

    public void CancelRoute()
    {
        panelManager.CallOffRouteByNumber (RouteExistNumber);
    }
}
