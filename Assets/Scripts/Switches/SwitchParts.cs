using UnityEngine;
public abstract class SwitchParts : MonoBehaviour, IManageable
{
    IndicatorPath indicator;

    public void Delete()
    {
        gameObject.SetActive (false);
    }

    public void Install()
    {
        gameObject.SetActive (true);
        indicator.Show (IndicationManager.Instance.IsIndicate);
    }

   

    public void Init()
    {
        indicator = GetComponentInChildren<IndicatorPath> ();
    }

    public void OnStart()
    {
        indicator.Show (IndicationManager.Instance.IsIndicate);
    }
}
