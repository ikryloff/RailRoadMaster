using UnityEngine;
public abstract class SwitchParts : MonoBehaviour {
       
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
