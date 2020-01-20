using TMPro;
using UnityEngine;

public class RSViewer : MonoBehaviour
{
    private TextMeshProUGUI rsName;
    public ThrottleIndicator ThrottleIndicator { get; set; }
    public SpeedText SpeedText { get; set; }


    void Awake()
    {
        rsName = GetComponentInChildren<RSNumber> ().GetComponent<TextMeshProUGUI> ();
        ThrottleIndicator = GetComponentInChildren<ThrottleIndicator> ();
        SpeedText = GetComponentInChildren<SpeedText> ();
    }

    public void SetText( string _text )
    {
        if(gameObject.activeSelf)
            rsName.text = _text;
    }

}
