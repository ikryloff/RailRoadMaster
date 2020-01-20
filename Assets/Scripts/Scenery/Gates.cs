using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public bool IsOpened;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator> ();
    }

    private void Start()
    {
        OpenGates (true);
    }

    public void OpenGates(bool isOpen)
    {
        IsOpened = isOpen;
        animator.SetBool ("IsOpened", isOpen);
    }

}
