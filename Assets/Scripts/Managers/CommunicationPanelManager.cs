using UnityEngine;

public class CommunicationPanelManager : MonoBehaviour, IHideable
{

    [SerializeField]
    private GameObject engineerList;
    [SerializeField]
    private GameObject zoomPanel;
    [SerializeField]
    private GameObject help;
    [SerializeField]
    private GameObject yardMaster;
    [SerializeField]
    private GameObject conductor;
    [SerializeField]
    private GameObject freightMaster;
    [SerializeField]
    private GameObject RSpanel;





    public void Show( bool isVisible )
    {
        engineerList.SetActive (isVisible);
        zoomPanel.SetActive (isVisible);
        help.SetActive (isVisible);
        yardMaster.SetActive (isVisible);
        conductor.SetActive (isVisible);
        freightMaster.SetActive (isVisible);
        RSpanel.SetActive (isVisible);
    }



}
