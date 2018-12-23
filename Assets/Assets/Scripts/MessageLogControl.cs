using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLogControl : MonoBehaviour {

    [SerializeField]
    private GameObject textTemplate;

    private List<GameObject> messageItems;

    private void Awake()
    {
        messageItems = new List<GameObject>();
    }

    public void MessageText(string newMessage)
    {
        GameObject newText = Instantiate(textTemplate) as GameObject;
        newText.SetActive(true);

        newText.GetComponent<MessageItem>().SetText(newMessage);
        newText.transform.SetParent(textTemplate.transform.parent, false);

        messageItems.Add(newText);
    }

}
