using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>
{
    private List<string> compositions = new List<string>();

    public List<string> Compositions
    {
        get
        {
            return compositions;
        }

        set
        {
            compositions = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (string item in Compositions)
            {
                Debug.Log("comp " + item);
            }            
        }

    }
}
