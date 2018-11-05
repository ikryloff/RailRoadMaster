using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControlScript : Singleton<RemoteControlScript> {

    private Vector3 axisZ;
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
        remoteSprite = remoteControll.GetComponent<SpriteRenderer>();
        halfCameraHeight = Camera.main.orthographicSize;
        cameraHeight = halfCameraHeight * 2;
        cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        spriteSize = remoteSprite.sprite.bounds.size;
        axisZ = new Vector3(0, 0, 190f);
        scale = remoteSprite.transform.localScale;
        scale *= cameraSize.x / spriteSize.x;  
        remoteSprite.transform.localScale = scale;
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
        float tempRatio = Camera.main.orthographicSize / halfCameraHeight;
        scale = new Vector3(tempRatio, tempRatio);
        remoteSprite.transform.localScale *= scale;
        halfCameraHeight = Camera.main.orthographicSize;
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
            tc.SetCellsLight(tc.ReturnCells(), Constants.TC_WAIT);
        }              
    }
    public void DefaultSwitches()
    {
        foreach (GameObject item in switches)
        {
            tc = item.GetComponent<TrackCircuit>();
            tc.SetCellsLight(tc.ReturnCells(), tc.UseMode);
        }
    }

}
