using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControlScript : Singleton<RemoteControlScript> {

    private Vector3 axisZ;
    [SerializeField]
    private GameObject remoteControll;
    [SerializeField]
    private GameObject enginePanel;
    [SerializeField]
    private GameObject remoteControlPanel;
    [SerializeField]
    private CameraController cc;
    private bool isRemoteControllerOn = false;    
    private GameObject[] switches;
    private TrackCircuit tc;

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
        SpriteRenderer remoteSprite = remoteControll.GetComponent<SpriteRenderer>();
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = remoteSprite.sprite.bounds.size;
        axisZ = new Vector3(0, 0, 190f);

        Vector2 scale = remoteSprite.transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }
        remoteSprite.transform.localScale = scale;
        remoteControll.gameObject.SetActive(false);
        remoteControlPanel.gameObject.SetActive(false);
        switches = GameObject.FindGameObjectsWithTag("Switch");
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (IsRemoteControllerOn)
            {
                remoteControll.gameObject.SetActive(false);
                enginePanel.gameObject.SetActive(true);
                remoteControlPanel.gameObject.SetActive(false);
                IsRemoteControllerOn = false;
                cc.CanMoveCamera = true;
            }
            else
            {                
                remoteControll.gameObject.SetActive(true);
                enginePanel.gameObject.SetActive(false);
                remoteControlPanel.gameObject.SetActive(true);
                IsRemoteControllerOn = true;
                cc.CanMoveCamera = false;
            }
        }
        remoteControll.transform.position = Camera.main.transform.position + axisZ;
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
