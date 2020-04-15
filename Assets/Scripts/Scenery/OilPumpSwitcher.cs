using UnityEngine;

public class OilPumpSwitcher : MonoBehaviour
{
    OilTubeBehaviour [] oilTubes;
    public bool IsActivated;

    private void Awake()
    {
        oilTubes = FindObjectsOfType<OilTubeBehaviour> ();
    }

    

    public void ActivateOT( bool isInUse )
    {
        for ( int i = 0; i < oilTubes.Length; i++ )
        {
            oilTubes [i].SwitchTube (isInUse);
        }
        
        IsActivated = isInUse;
    }

}
