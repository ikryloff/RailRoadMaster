using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public bool IsOpened;
    public Animator animator;
    [SerializeField]
    private TrackCircuit [] trackCircuits;

    void Awake()
    {
        animator = GetComponent<Animator> ();
        
    }

    private void Start()
    {
        EventManager.onTrackCircuitsStateChanged += CheckAndUseGate;
    }

    private void CheckAndUseGate()
    {
        for ( int i = 0; i < trackCircuits.Length; i++ )
        {
            if ( trackCircuits [i].HasCarPresence && !IsOpened )
            {
                OpenGates (true);
                return;
            }
            else if( !trackCircuits [i].HasCarPresence && IsOpened )
            {
                OpenGates (false);
            }
        }
    }
   
    public void OpenGates(bool isOpen)
    {
        IsOpened = isOpen;
        animator.SetBool ("IsOpened", isOpen);
    }

}
