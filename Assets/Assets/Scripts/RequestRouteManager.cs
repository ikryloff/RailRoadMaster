using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestRouteManager : Singleton<RequestRouteManager> {
    public GameObject btnPrefab;
    public GameObject canvasParent;
    public Button startBtn;


	public void AddButton()
    {
        GameObject newBtn = Instantiate(btnPrefab);
        newBtn.transform.SetParent(canvasParent.transform, false);
        Destroy(startBtn.image);
        Destroy(startBtn);
    }
}
