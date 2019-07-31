using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{

    [SerializeField]
    private TextMeshProUGUI timeText;
    private float timerSpeed = 1f;
    private float elapsed;
    public int [] TimeValue;
   
    private int minutes;
    private int hours;

    void Start()
    {
        timeText.text = "00 : 00";
        TimeValue = new int [2];
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= timerSpeed )
        {
            elapsed = 0f;
            CountTime ();
        }
    }

    private void CountTime()
    {
        minutes++;
        if ( minutes > 59 )
        {
            hours++;
            minutes = 0;
            if ( hours > 24 )
                hours = 0;
        }
        TimeValue [0] = hours;
        TimeValue [1] = minutes;
        timeText.text = TimeValue [0].ToString ("D2") + " : " + TimeValue [1].ToString ("D2"); 
    }
}
