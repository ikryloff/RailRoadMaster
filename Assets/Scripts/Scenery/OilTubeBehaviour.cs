using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTubeBehaviour : MonoBehaviour
{
    bool IsReady;
    bool IsInWork;
    OilTubeLamp lamp;
    public Animator Animator;
    // Start is called before the first frame update

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
        lamp.TurnRedColor ();
        IsReady = false;
        if ( IsInWork && Animator )
        {
            Animator.SetBool ("SwitchOn", false);
            IsInWork = false;
        }
    }

    public void MakeOilDelivering()
    {
        StartCoroutine (DeliveringProcess());
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
    }

    public void SwitchTube()
    {
        if ( Animator && IsReady )
        {
            Animator.SetBool ("SwitchOn", IsReady);
            IsInWork = true;
            MakeOilDelivering ();
        }
    }
}
