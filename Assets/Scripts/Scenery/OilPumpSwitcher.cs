using UnityEngine;

public class OilPumpSwitcher : MonoBehaviour
{
    OilTubeBehaviour [] oilTubes;
    public bool IsActivated;
    public TrafficLight [] gateSignals;

    private void Awake()
    {
        gateSignals = GetComponentsInChildren<TrafficLight> ();
        oilTubes = FindObjectsOfType<OilTubeBehaviour> ();
    }

    

    public void ActivateOT( bool isInUse )
    {
        for ( int i = 0; i < oilTubes.Length; i++ )
        {
            oilTubes [i].SwitchTube (isInUse);
        }

        for ( int i = 0; i < gateSignals.Length; i++ )
        {
            gateSignals [i].IsClosed = isInUse;
            gateSignals [i].Trigger.enabled = isInUse;
        }

        IsActivated = isInUse;
    }

}
