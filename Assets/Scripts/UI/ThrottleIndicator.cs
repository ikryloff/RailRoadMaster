using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleIndicator : MonoBehaviour
{
    ThrottleUnit [] throttleUnits;
    GameObject [] throttleUnitsGO;
    public Engine Engine { get; set; }
    public int TempInstructionsHandler;

    void Awake()
    {
        throttleUnits = GetComponentsInChildren<ThrottleUnit> ();        
    }

    private void Start()
    {
        TempInstructionsHandler = -300;
        ShowStopValue ();
    }

    private void Update()
    {
        if ( Engine )
        {
            ShowValueByHandler (Engine.InstructionsHandler);
        }
    }
    

    public void ShowStopValue()
    {
        for ( int i = 0; i < throttleUnits.Length; i++ )
        {
            if ( throttleUnits [i].Value == 0 )
                throttleUnits [i].go.SetActive (true);
            else if ( throttleUnits [i].Value == 6 || throttleUnits [i].Value == -6 )
            {
                throttleUnits [i].go.SetActive (true);
                throttleUnits [i].SetFullImage ();
            }
            else
                throttleUnits [i].go.SetActive (false);
        }
    }

    public void ShowValueByHandler(int value)
    {
        if ( value == TempInstructionsHandler )
            return;
        
        if (value > 0 )
        {
            for ( int i = 0; i < throttleUnits.Length; i++ )
            {
                if( throttleUnits [i].Value > 0 )
                {
                    throttleUnits [i].go.SetActive (true);
                    if ( throttleUnits [i].Value <= value )
                        throttleUnits [i].SetFullImage ();
                    else
                        throttleUnits [i].SetEmptyImage ();
                        
                }
                else
                    throttleUnits [i].go.SetActive (false);

            }
        }
        else if (value < 0)
            for ( int i = 0; i < throttleUnits.Length; i++ )
            {
                if ( throttleUnits [i].Value < 0 )
                {
                    throttleUnits [i].go.SetActive (true);
                    if ( throttleUnits [i].Value >= value )
                        throttleUnits [i].SetFullImage ();
                    else
                        throttleUnits [i].SetEmptyImage ();

                }
                else
                    throttleUnits [i].go.SetActive (false);

            }
        else 
        {
            ShowStopValue ();
        }
        TempInstructionsHandler = value;

    }

}
