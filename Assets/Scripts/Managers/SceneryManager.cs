using UnityEngine;

public class SceneryManager : MonoBehaviour, IHideable
{
    [SerializeField]
    private GameObject [] yardModeOjectsToHide;

    public void Show( bool isVisible )
    {
        for ( int i = 0; i < yardModeOjectsToHide.Length; i++ )
        {
            yardModeOjectsToHide [i].SetActive (isVisible);
        }
    }

    
}
