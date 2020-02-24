using UnityEngine;

public class RSModel : MonoBehaviour
{
    public MeshRenderer [] RSMeshRenderers;
    public Material DefaultMaterial;
    public Material LightedCarMaterial;
    public Material LightedEngineMaterial;
    public bool IsEngine;

    public void Awake()
    {
        DefaultMaterial = ResourceHolder.Instance.Default_LP_Mat;
        LightedCarMaterial = ResourceHolder.Instance.Lighted_Car_LP_Mat;
        LightedEngineMaterial = ResourceHolder.Instance.Lighted_Engine_LP_Mat;
        IsEngine = transform.parent.GetComponent<Engine> ();
        GetAllMeshRenderers ();
        DefaultModelMaterial ();
    }

    private void GetAllMeshRenderers()
    {
        RSMeshRenderers = new MeshRenderer [7];
        RSMeshRenderers [0] = GetComponent<MeshRenderer> ();
        int count = 1;

        Bogey [] bogeys = transform.parent.GetComponentsInChildren<Bogey> ();
        for ( int i = 0; i < bogeys.Length; i++ )
        {
            bogeys [i].GetMeshRenderers ();
            for ( int j = 0; j < bogeys [i].BogeysMeshRenderers.Length; j++ )
            {
                RSMeshRenderers [count] = bogeys [i].BogeysMeshRenderers [j];
                count++;
            }
        }      

    }

    public virtual void LightModelMaterial()
    {
        if ( IsEngine )
        {
            for ( int i = 0; i < RSMeshRenderers.Length; i++ )
            {
                RSMeshRenderers [i].material = LightedEngineMaterial;            
            }
            return;
        }
        else
        {
            for ( int i = 0; i < RSMeshRenderers.Length; i++ )
            {
                RSMeshRenderers [i].material = LightedCarMaterial;
            }
        }


    }

    public virtual void DefaultModelMaterial()
    {
        for ( int i = 0; i < RSMeshRenderers.Length; i++ )
        {
            RSMeshRenderers [i].material = DefaultMaterial;
        }
    }

    public void HighLightCarOutline()
    {
        LightModelMaterial ();
    }

    public void HighLightEngineOutline()
    {
        LightModelMaterial ();
    }

    public void DefaultOutline()
    {
        DefaultModelMaterial ();
    }
}
