using UnityEngine;

public class Cross : TrafficLight
{
    [SerializeField]
    private Switch dependSwitchStraight;
    [SerializeField]
    private Switch dependSwitchTurn;

    protected override void Awake()
    {
        base.Awake ();
        IsClosed = true;
    }
    void Start()
    {
        EventManager.onPathChanged += SetCrossIndication;
        GetPositionX = gameObject.transform.position.x;
        SetCrossIndication ();
    }

    private void SetCrossIndication()
    {
        if ( dependSwitchStraight && !dependSwitchStraight.IsSwitchStraight )
            IsClosed = true;
        else if ( dependSwitchStraight && dependSwitchStraight.IsSwitchStraight )
            IsClosed = false;

        if ( dependSwitchTurn && !dependSwitchTurn.IsSwitchStraight )
            IsClosed = false;
        else if ( dependSwitchTurn && dependSwitchTurn.IsSwitchStraight )
            IsClosed = true;
    }

}
