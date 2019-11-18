using UnityEngine;

public class IndicatorPath : MonoBehaviour, IHideable
{
    public TrackCircuit IndTrackCircuit { get; set; }
    public PathIndicationUnit [] pathUnits;

    public void Show( bool isVisible )
    {
        gameObject.SetActive (isVisible);
    }

    public void GetIndicatorsPathUnits()
    {
        pathUnits = GetComponentsInChildren<PathIndicationUnit> ();
    }

}
