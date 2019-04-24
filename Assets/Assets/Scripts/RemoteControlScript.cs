using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControlScript : Singleton<RemoteControlScript> {

    public Vector3 axisZ;
    [SerializeField]
    private GameObject remoteControll;
    [SerializeField]
    private GameObject gamePanels;   
    [SerializeField]
    private GameObject remoteControlPanel;
    [SerializeField]
    private CameraController cc;
    private bool isRemoteControllerOn;    
    private GameObject[] switches;
    private TrackCircuit tc;
    private SpriteRenderer remoteSprite;
    public float cameraHeight;
    public Vector2 spriteSize;
    public Vector2 scale;
    private Vector2 cameraSize;
    public float halfCameraHeight;

    public bool IsRemoteControllerOn
    {
        get
        {
            return isRemoteControllerOn;
        }

        set
        {
            isRemoteControllerOn = value;
        }
    }

    void Awake () {        
        switches = GameObject.FindGameObjectsWithTag("Switch");
        IsRemoteControllerOn = true;
        RunRemoteControl();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RunRemoteControl();
        }
        remoteControll.transform.position = Camera.main.transform.position + axisZ;
    }
   

    public void RunRemoteControl()
    {
        if (IsRemoteControllerOn)
        {
            remoteControll.gameObject.SetActive(false);
            gamePanels.gameObject.SetActive(true);
            remoteControlPanel.gameObject.SetActive(false);
            IsRemoteControllerOn = false;
            cc.CanMoveCamera = true;
        }
        else
        {
            remoteControll.gameObject.SetActive(true);
            gamePanels.gameObject.SetActive(false);
            remoteControlPanel.gameObject.SetActive(true);
            IsRemoteControllerOn = true;
            cc.CanMoveCamera = false;
        }
    }

    public void ShowSwitches()
    {
        foreach (GameObject item in switches)
        {
            tc = item.GetComponent<TrackCircuit>();
        }              
    }
    public void DefaultSwitches()
    {
        foreach (GameObject item in switches)
        {
            tc = item.GetComponent<TrackCircuit>();
        }
    }

}
