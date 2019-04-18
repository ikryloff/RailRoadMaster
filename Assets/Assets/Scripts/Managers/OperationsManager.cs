using UnityEngine;
public class OperationsManager : Singleton<OperationsManager>
{
    [SerializeField]
    private TrackPath trackPath;
    [SerializeField]
    private SwitchManager switchManager;

    void Awake()
    {
        // 1) Caching TrackPath go 
        trackPath = FindObjectOfType<TrackPath>();
        // 2) Initialize TrackPath
        trackPath.Init();
        // 3) Caching SwitchManager go 
        switchManager = FindObjectOfType<SwitchManager>();
        // 4) Initialize SwitchManager
        switchManager.Init();


    }

    private void Start()
    {
        trackPath.SetEachPathClosePaths();

    }


    void Update()
    {


    }


}
