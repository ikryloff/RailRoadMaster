using TMPro;
using UnityEngine;

public class UIShedulePanelTrainNumber : MonoBehaviour
{
    public TextMeshProUGUI TrainNum;

    public void OnAwake()
    {
        TrainNum = GetComponentInChildren<TextMeshProUGUI> ();
    }

    public void SetNumber(string num)
    {
        TrainNum.text = num;
    }
}
