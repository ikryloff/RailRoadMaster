using System;
using TMPro;
using UnityEngine;

public class RSViewer : MonoBehaviour
{
    private TextMeshProUGUI rsName;
    public ThrottleIndicator ThrottleIndicator { get; set; }
    public SpeedText SpeedText { get; set; }
    public RSIcon Icon;


    void Awake()
    {
        Icon = GetComponentInChildren<RSIcon> ();
        rsName = GetComponentInChildren<RSNumber> ().GetComponent<TextMeshProUGUI> ();
        ThrottleIndicator = GetComponentInChildren<ThrottleIndicator> ();
        SpeedText = GetComponentInChildren<SpeedText> ();
    }

    public void SetText( string _text )
    {
        if(gameObject.activeSelf)
            rsName.text = _text;
    }

    public void SetEngine(Engine engine )
    {
        ThrottleIndicator.Engine = engine;
    }

    public void SetEngineForSpeed( Engine engine )
    {
        SpeedText.Engine = engine;
    }

    public void SetIcon( RollingStock rs )
    {
        Icon.CalcAndSetIcon (rs.Number);
    }
}
