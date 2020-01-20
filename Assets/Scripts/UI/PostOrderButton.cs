using UnityEngine;
using UnityEngine.UI;

public class PostOrderButton : MonoBehaviour
{
    public GameObject postWorkButtObject;
    private PostOrderButton [] postButts;
    public ConductorCameraController CommonCamera;
    public Toggle CameraFreeButton;
    public bool IsActivated;


    private void Awake()
    {
        postButts = FindObjectsOfType<PostOrderButton> ();
        Button [] actors = GetComponentsInChildren<Button> ();        
        foreach ( Button item in actors )
        {
            if ( item.CompareTag ("PostWorkButton") )
            {
                postWorkButtObject = item.gameObject;
            }
        }
        CommonCamera = FindObjectOfType<ConductorCameraController> ();
        GetComponent<Button> ().onClick.AddListener (ButtonAction);
    }

    public virtual void ButtonAction()
    {
        if ( !IsActivated )
            CloseOtherPostButtons ();
        CameraFreeButton.isOn = true;
    }

    private void CloseOtherPostButtons()
    {
        for ( int i = 0; i < postButts.Length; i++ )
        {
            if ( postButts [i].IsActivated && postButts [i] != this )
                postButts [i].SetButtonsActive (false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetButtonsActive (false);
    }

    public void SetButtonsActive( bool _isActive )
    {
        postWorkButtObject.SetActive (_isActive);
        IsActivated = false;
    }
}
