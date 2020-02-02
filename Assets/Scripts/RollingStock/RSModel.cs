using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSModel : MonoBehaviour
{
    public MeshRenderer meshRend;
    public Material DefaultMaterial;
    public Material LightedMaterial;

    public void Awake()
    {
        meshRend = GetComponent<MeshRenderer> ();
        DefaultMaterial = ResourceHolder.Instance.Default_LP_Mat;
        LightedMaterial = ResourceHolder.Instance.Lighted_LP_Mat;
}

    public virtual void LightModelMaterial()
    {
        meshRend.material = LightedMaterial;
    }

    public virtual void DefaultModelMaterial()
    {
        meshRend.material = DefaultMaterial;
    }

    IEnumerator Blink_Process()
    {
        LightModelMaterial ();
        yield return new WaitForSecondsRealtime (0.1f);
        DefaultModelMaterial ();
        yield return new WaitForSecondsRealtime (0.1f);
        LightModelMaterial ();
        yield return new WaitForSecondsRealtime (0.1f);
        DefaultModelMaterial ();
        yield return new WaitForSecondsRealtime (0.1f);
        LightModelMaterial ();
        yield return new WaitForSecondsRealtime (0.1f);
        DefaultModelMaterial ();
    }

    public void Blink()
    {
        StartCoroutine (Blink_Process ());
    }
}
