using UnityEngine;

public class RSModelBoxCar : RSModel
{
    public MeshRenderer [] meshRenderers;

    private new void Awake()
    {
        base.Awake ();
        meshRenderers = GetComponentsInChildren<MeshRenderer> ();
    }

    public override void LightModelMaterial()
    {
        for ( int i = 0; i < meshRenderers.Length; i++ )
        {
            meshRenderers [i].material = LightedCarMaterial;
        }
    }

    public override void DefaultModelMaterial()
    {
        for ( int i = 0; i < meshRenderers.Length; i++ )
        {
            meshRenderers [i].material = DefaultMaterial;
        }
    }
}
