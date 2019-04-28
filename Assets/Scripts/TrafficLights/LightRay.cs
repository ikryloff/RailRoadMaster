using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour {

    
    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void On()
    {
        gameObject.SetActive(true);
    }
}
