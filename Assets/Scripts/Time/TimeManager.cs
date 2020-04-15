using TMPro;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{

    [SerializeField]
    private TextMeshProUGUI timeText;
    private float timerSpeed = 1f;
    private float elapsed;
    public int [] TimeValue;
    public int koef;

    public void OnStart()
    {
        timeText.text = "00 : 00";
        TimeValue = new int [2];
        TimeValue[0] = 0;
        TimeValue[1] = 0;
        EventManager.HourPassed ();
    }

    public void OnUpdate()
    {
        elapsed += Time.deltaTime * koef;
        if ( elapsed >= timerSpeed )
        {
            elapsed = 0f;
            CountTime ();            
        }
    }

    private void CountTime()
    {
        TimeValue [1]++;
        if ( TimeValue [1] > 59 )
        {
            TimeValue [0]++;
            TimeValue [1] = 0;
            if ( TimeValue [0] > 23 )
                TimeValue [0] = 0;
            EventManager.HourPassed ();
        }
        EventManager.MinutePassed ();
        timeText.text = Constants.NUMBERS [TimeValue [0]] + " : " + Constants.NUMBERS [TimeValue [1]];
    }
}
