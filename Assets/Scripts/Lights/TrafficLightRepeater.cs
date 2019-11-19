using UnityEngine;

public class TrafficLightRepeater : MonoBehaviour
{

    public MeshRenderer TLRepeaterColor { get; set; }
    private Material closedShunting;
    private Material closedTrain;
    private Material openedTrain;
    private Material openedShunting;


    void Awake()
    {
        TLRepeaterColor = GetComponentInChildren <Repeater> ().GetComponent<MeshRenderer> ();
        closedShunting = ResourceHolder.Instance.Light_Blue_Signal_Mat;
        closedTrain = ResourceHolder.Instance.Light_Red_Signal_Mat;
        openedTrain = ResourceHolder.Instance.Light_Green_Signal_Mat;
        openedShunting = ResourceHolder.Instance.Light_White_Signal_Mat;

    }

    public void RepeaterOnShunting()
    {
        TLRepeaterColor.material = openedShunting;

    }

    public void RepeaterOffShunting()
    {
        TLRepeaterColor.material = closedShunting;

    }

    public void RepeaterOnTrain()
    {
        TLRepeaterColor.material = openedTrain;

    }

    public void RepeaterOffTrain()
    {
        TLRepeaterColor.material = closedTrain;

    }


}
