using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour {

    public bool IsOn { get; private set; }

    public void Off()
    {
        gameObject.SetActive(false);
        IsOn = false;
    }

    public void On()
    {
        gameObject.SetActive(true);
        IsOn = true;
    }
}
