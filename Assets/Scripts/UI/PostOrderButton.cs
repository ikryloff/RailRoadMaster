using UnityEngine;
using UnityEngine.UI;

public class PostOrderButton : MonoBehaviour
{
    public GameObject postWorkButtObject;
    private PostOrderButton [] postOrderButts;
    public ConductorCameraController CommonCamera;
    public Toggle CameraFreeButton;
    public bool IsActivated;
    private Button [] postButts;


    private void Awake()
    {
       
        CommonCamera = FindObjectOfType<ConductorCameraController> ();
        GetComponent<Button> ().onClick.AddListener (ButtonAction);
        EventManager.onFollowProcessFinished += SetAllPostButtonsActive;
    }

    public virtual void ButtonAction()
    {
        if ( !IsActivated )
        {
            SetOtherPostButtonsUnactive ();
        }
        CameraFreeButton.isOn = true;
    }

   
    public void SetOtherPostButtonsUnactive()
    {
        for ( int i = 0; i < postButts.Length; i++ )
        {
            if ( postButts [i].IsInteractable() && postOrderButts [i] != this )
                postButts [i].interactable = false;
        }
    }

    public void SetAllPostButtonsActive()
    {
        if ( IsActivated )
        {
            for ( int i = 0; i < postButts.Length; i++ )
            {
                postButts [i].interactable = true;
            }
        }
    }

   
  
}
