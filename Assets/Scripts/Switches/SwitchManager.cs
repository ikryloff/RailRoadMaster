using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] indicators;
    public Switch [] Switches { get; private set; }
    private Renderer rend;
    MovableObject [] movables;

    public Dictionary<string, Switch> SwitchDict { get; set; }

    public void Init()
    {
        Switches = FindObjectsOfType<Switch> ();
        indicators = GameObject.FindGameObjectsWithTag ("Indication");
        SwitchesInitilisation ();
    }

    private void SwitchesInitilisation()
    {
        SwitchDict = new Dictionary<string, Switch> ();
        foreach ( Switch sw in Switches )
        {
            // save Switch in dictionary
            SwitchDict.Add (sw.name, sw);
            //initialize
            sw.Init ();
        }
        SwitchDict.Add ("", null);

    }

    public void TurnHandSwitchListener( Collider collider )
    {
        Switch sw = collider.transform.parent.gameObject.GetComponent<Switch> ();
        sw.SetSwitchDirection (Switch.SwitchDir.Change);
    }

}
