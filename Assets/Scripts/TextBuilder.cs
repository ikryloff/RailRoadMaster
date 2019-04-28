using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBuilder : Singleton<TextBuilder> {

    [SerializeField]
    private MessageLogControl messLogControl;

    private string communicationMessage;
    private static int counter = 0;

    // Use this for initialization
    void Start () {        
        PrintMessage("Hello! Game is started");
        PrintMessage("It's a test");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrintMessage(string message, string from = "")
    {   
        string mess = string.Format("# {0} : {1} {2}", counter, from, message);
        messLogControl.MessageText(mess);
       // print(mess);
        counter++;
    }

    public string CommunicationMessage
    {
        get
        {
            return communicationMessage;
        }

        set
        {
            communicationMessage = value;
        }
    }
}
