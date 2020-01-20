using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleUnit : MonoBehaviour
{
    public int Value;
    public Sprite EmptyInd;
    public Sprite FullInd;
    public Sprite Stop;
    public Image img;
    public GameObject go;
    

    public bool IsEmpty;

    private void Awake()
    {
        go = gameObject;
        img = GetComponent<Image> ();
        EmptyInd = ResourceHolder.Instance.EmptyArrow;
        FullInd = ResourceHolder.Instance.FullArrow;
        Stop = ResourceHolder.Instance.Stop;
        if ( Value != 0 )
            SetEmptyImage();
        else
            SetStopImage();
        IsEmpty = true;
        
    }

    

    public void SetFullImage( )
    {
        if(gameObject.activeSelf)
            img.sprite = FullInd;
    }
    public void SetEmptyImage()
    {
        if ( gameObject.activeSelf )
            img.sprite = EmptyInd;
    }

    public void SetStopImage()
    {
        if ( gameObject.activeSelf )
            img.sprite = Stop;
    }
}
