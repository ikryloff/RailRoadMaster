using UnityEngine;

public class Switch : MonoBehaviour {
    private GameObject SwitchPhysicsTurn;
    private GameObject SwitchPhysicsStraight;    
    private bool isSwitchStraight = true;    

    // Use this for initialization
    void Start () {
        SwitchPhysicsTurn = transform.Find("curveTrackPhysics").gameObject;
        SwitchPhysicsStraight = transform.Find("straightTrackPhysicsSwitch").gameObject;        
     
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
        SwitchPhysicsTurn.SetActive(false);
        SwitchPhysicsStraight.SetActive(true);  
        isSwitchStraight = true;
    }
    void directionTurn()
    {
        SwitchPhysicsStraight.SetActive(false);
        SwitchPhysicsTurn.SetActive(true);
        isSwitchStraight = false;
    }
}
