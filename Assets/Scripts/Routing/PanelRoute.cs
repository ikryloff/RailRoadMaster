using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRoute : MonoBehaviour, IHideable {

	[SerializeField]
    private int num;

    public int Num
    {
        get
        {
            return num;
        }
     
    }
    public bool IsExist { get; set; }

    private void Start()
    {
        IsExist = false;
        Show (false);
    }

    public void Show( bool isVisible )
    {
        gameObject.SetActive (isVisible); 
        IsExist = isVisible;
    }
}
