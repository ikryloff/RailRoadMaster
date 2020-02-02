using System;
using TMPro;
using UnityEngine;

public class RSViewer : MonoBehaviour
{
    private TextMeshProUGUI rsName;
    public ThrottleIndicator ThrottleIndicator { get; set; }
    public SpeedText SpeedText { get; set; }
    public RSCargo CargoIcon { get; set; }
    public RSIcon Icon;
    public GameObject CarUI;
    public GameObject LocoUI;


    void Awake()
    {
        Icon = GetComponentInChildren<RSIcon> ();
        CargoIcon = GetComponentInChildren<RSCargo> ();
        rsName = GetComponentInChildren<RSNumber> ().GetComponent<TextMeshProUGUI> ();
        ThrottleIndicator = GetComponentInChildren<ThrottleIndicator> ();
        SpeedText = GetComponentInChildren<SpeedText> ();
        LocoUI = GetComponentInChildren<LocoUI> ().gameObject;
        CarUI = GetComponentInChildren<CarUI> ().gameObject;
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

    public void SetLocoUI()
    {
        CarUI.SetActive (false);
        LocoUI.SetActive (true);
    }

    public void SetCarUI(RollingStock rs)
    {
        LocoUI.SetActive (false);
        CarUI.SetActive (true);
        UpdateCargoIcon (rs);
    }

    public void UpdateCargoIcon( RollingStock rs )
    {
        if ( CarUI.activeSelf )
        {
            CargoIcon.SetIcon (rs);
        }
    }
}
