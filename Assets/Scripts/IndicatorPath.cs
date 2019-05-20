using UnityEngine;

public class IndicatorPath : MonoBehaviour, IHideable
{
    public TrackCircuit IndTrackCircuit { get; set; }   

    public void Show( bool isVisible )
    {
        if ( isVisible )
        {
            gameObject.SetActive (true);
        }
        else
        {
            gameObject.SetActive (false);
        }
    }
   
}
