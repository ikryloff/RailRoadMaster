using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTubeBehaviour : MonoBehaviour
{
    bool IsReady;
    bool IsInWork;
    OilTubeLamp lamp;
    public Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator> ();
    }

    void Start()
    {
        lamp = transform.parent.GetComponentInChildren<OilTubeLamp> ();
        lamp.TurnRedColor ();
        
    }


    private void OnTriggerEnter( Collider other )
    {
        lamp.TurnGreenColor ();
        IsReady = true;
        
    }

    private void OnTriggerExit( Collider other )
    {
        IsReady = false;
        if ( IsInWork && Animator )
        {
            Animator.SetBool ("SwitchOn", false);
            IsInWork = false;
            StopOilDelivering ();
        }
        lamp.TurnRedColor ();
    }

    public void MakeOilDelivering()
    {
        StartCoroutine (DeliveringProcess());
    }

    public void StopOilDelivering()
    {
        StopCoroutine (DeliveringProcess ());
    }

    public IEnumerator DeliveringProcess ()
    {
        yield return new WaitForSecondsRealtime (1);
        while ( IsInWork )
        {
            lamp.TurnYellowColor ();
            yield return new WaitForSecondsRealtime (0.3f);
            lamp.TurnNoColor ();
            yield return new WaitForSecondsRealtime (0.3f);
        }
        lamp.TurnRedColor ();
    }

    public void SwitchTube(bool isOn)
    {
        if ( Animator && IsReady )
        {
            Animator.SetBool ("SwitchOn", isOn);
            IsInWork = isOn;
            if(isOn)
                MakeOilDelivering ();
            else
            {
                StopOilDelivering ();
            }
        }
    }
}
