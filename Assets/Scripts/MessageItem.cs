using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour {
    
    public void SetText( string messageText)
    {
        GetComponent<Text>().text = messageText;
    }
}
