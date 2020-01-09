using UnityEngine;

public class OilTubeLamp : MonoBehaviour
{
    MeshRenderer lampColorRend;
    Material redColor;
    Material greenColor;
    Material noColor;
    Material yellowColor;

    private void Awake()
    {
        lampColorRend = GetComponent<MeshRenderer> ();
    }

    void Start()
    {
        redColor = ResourceHolder.Instance.Light_Red_Signal_Mat;
        greenColor = ResourceHolder.Instance.Light_Green_Signal_Mat;
        yellowColor = ResourceHolder.Instance.Light_Yellow_Signal_Mat;
        noColor = ResourceHolder.Instance.Light_No_Signal_Mat;
        lampColorRend.material = redColor;
    }

    public void TurnRedColor()
    {
        lampColorRend.material = redColor;
    }

    public void TurnGreenColor()
    {
        lampColorRend.material = greenColor;
    }

    public void TurnYellowColor()
    {
        lampColorRend.material = yellowColor;
    }

    public void TurnNoColor()
    {
        lampColorRend.material = noColor;
    }

}
