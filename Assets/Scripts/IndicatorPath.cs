using UnityEngine;

public class IndicatorPath : MonoBehaviour, IHideable
{
    public TrackCircuit IndTrackCircuit { get; set; }

    public void Show( bool isVisible )
    {
        gameObject.SetActive (isVisible);
    }

}
