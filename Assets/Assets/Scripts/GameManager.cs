using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    [SerializeField]
    private TrackPath trackPath;
    [SerializeField]
    private SwitchManager switchManager;

    void Awake()
    {
        trackPath = FindObjectOfType<TrackPath>();
        trackPath.Init();

        switchManager = FindObjectOfType<SwitchManager>();
        switchManager.Init();
    }

    private void Start()
    {
        

    }
    

    void Update()
    {
        
       
    }

   


}
