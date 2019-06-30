using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommunicationPanelManager : MonoBehaviour, IHideable
{
    [SerializeField]
    private GameObject actionList;    
    [SerializeField]
    private GameObject engineerList;
    [SerializeField]
    private GameObject zoomPanel;





    public void Show( bool isVisible )
    {
        actionList.SetActive (isVisible);
        engineerList.SetActive (isVisible);
        zoomPanel.SetActive (isVisible);
    }
    


}
