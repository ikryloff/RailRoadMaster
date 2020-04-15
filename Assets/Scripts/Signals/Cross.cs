using UnityEngine;

public class Cross : TrafficLight
{
    [SerializeField]
    private Switch dependSwitchStraight;
    [SerializeField]
    private Switch dependSwitchTurn;

    BoxCollider crossTrigger;

    protected override void Awake()
    {
        base.Awake ();
        crossTrigger = GetComponent<BoxCollider> ();
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

        ReloadTrigger ();

        EventManager.SignalChanged();
    }

    private void ReloadTrigger()
    {
        crossTrigger.enabled = false;
        crossTrigger.enabled = true;
    }

}
