using System;
using TMPro;
using UnityEngine;

public class UISheduleItem : MonoBehaviour
{
    public int ItemNumber { get; set; }
    private UIShedulePointer pointer;
    public UIShedule Shedule { get; set; }
    public TextMeshProUGUI TimeText;
    public RectTransform RectTransform;
    public UIHalfHourFirst HalfHourFirst;
    public UIHalfHourSecond HalfHourSecond;
    public UISheduleFirstTrainNumber firstTrainNumber;
    public UISheduleSecondTrainNumber secondTrainNumber;
    private float HEIGHT = -150f;

    public void OnAwake()
    {
        Shedule = FindObjectOfType<UIShedule> ();
        RectTransform = GetComponent<RectTransform> ();

        RectTransform.localPosition = new Vector2 (RectTransform.localPosition.x, RectTransform.localPosition.y + ItemNumber * HEIGHT);

        pointer = GetComponentInChildren<UIShedulePointer> ();
        pointer.PointerNumber = ItemNumber;
        TimeText = GetComponentInChildren<UIOverTime> ().GetComponentInChildren<TextMeshProUGUI> ();
        SetText (ItemNumber);

        firstTrainNumber = pointer.GetComponentInChildren<UISheduleFirstTrainNumber> ();
        firstTrainNumber.OnAwake ();
        secondTrainNumber = pointer.GetComponentInChildren<UISheduleSecondTrainNumber> ();
        secondTrainNumber.OnAwake ();

        HalfHourFirst = GetComponentInChildren<UIHalfHourFirst> ();
        HalfHourFirst.OnAwake ();
        HalfHourSecond = GetComponentInChildren<UIHalfHourSecond> ();
        HalfHourSecond.OnAwake ();
    }

    private void SetText( int itemNumber )
    {
        if(itemNumber < 9 )
        {            
            TimeText.text = "0" + (itemNumber) + ".00";
        }
        else
        {
           TimeText.text =  (itemNumber) + ".00";
        }
    }

    public void IncreaseUpPosition()
    {
        RectTransform.localPosition = new Vector2 (RectTransform.localPosition.x, RectTransform.localPosition.y - HEIGHT);
    }

    public void IncreaseDownPosition()
    {
        RectTransform.localPosition = new Vector2 (RectTransform.localPosition.x, RectTransform.localPosition.y + HEIGHT);
    }

}
