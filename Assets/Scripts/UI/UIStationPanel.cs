using UnityEngine;

public class UIStationPanel : MonoBehaviour
{
    public UICarPanel [] CarPanels { get; set; }

    public void Awake()
    {
        UICarPanel temp;
        CarPanels = GetComponentsInChildren<UICarPanel> ();
        for ( int i = 0; i < CarPanels.Length; i++ )
        {
            for ( int j = 0; j < CarPanels.Length - 1 - i ; j++ )
            {
                if ( CarPanels [j + 1].CarPanelID < CarPanels [j].CarPanelID )
                {
                    temp = CarPanels [j];
                    CarPanels [j] = CarPanels [j + 1];
                    CarPanels [j + 1] = temp;
                }
            }
        }
    }
}
