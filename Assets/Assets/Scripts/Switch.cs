using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField]
    private GameObject switchPhysicsTurn;
    [SerializeField]
    private GameObject switchPhysicsStraight;
    [SerializeField]
    private GameObject turnIndicatorObj;
    [SerializeField]
    private GameObject straightIndicatorObj;
    private Renderer turnIndicator;
    private Renderer straightIndicator;
    private string HIDE_INDICATION_LAYER = "HideIndication";
    private string INDICATION_LAYER = "Indication";    

    private bool isSwitchStraight;
        

    // Use this for initialization
    void Start () {
        turnIndicator = turnIndicatorObj.GetComponent<Renderer>();
        straightIndicator = straightIndicatorObj.GetComponent<Renderer>();
        isSwitchStraight = true;
        directionStraight();

    }
    
    public void changeDirection()
    {
        if (isSwitchStraight == true)
        {
            directionTurn();
        }
        else
        {
            directionStraight();
        }
    } 
    
    
    void directionStraight()
    {
        switchPhysicsTurn.SetActive(false);
        switchPhysicsStraight.SetActive(true);
        turnIndicator.sortingLayerName = HIDE_INDICATION_LAYER;
        straightIndicator.sortingLayerName = INDICATION_LAYER;
        isSwitchStraight = true;
    }
    void directionTurn()
    {
        switchPhysicsStraight.SetActive(false);
        switchPhysicsTurn.SetActive(true);
        turnIndicator.sortingLayerName = INDICATION_LAYER;
        straightIndicator.sortingLayerName = HIDE_INDICATION_LAYER;
        isSwitchStraight = false;        
    }

    
}
