using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedText : MonoBehaviour
{
    public string [] nums;
    TextMeshProUGUI speed;
    string tmpText;
    float timer;
    public Engine Engine { get; set; }
    float DELAY = 0.5f;

    private void Awake()
    {
        nums = Constants.NUMBERS;
        speed = GetComponentInChildren<TextMeshProUGUI> ();
    }

    private void Start()
    {
        speed.text = nums [0];
    }

    private void Update()
    {
        if ( Engine )
        {
            SetText (Engine.SpeedReal);
        }
    }


    public void SetText(int _speed)
    {
        if(timer > DELAY )
        {
            _speed = _speed < 0 ? -_speed : _speed;
            speed.text = nums [_speed];
            timer = 0;
        }
        timer += Time.deltaTime;
        
    }
        
}
