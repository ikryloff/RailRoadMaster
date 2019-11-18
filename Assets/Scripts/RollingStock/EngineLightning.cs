using UnityEngine;

public class EngineLightning : MonoBehaviour
{

    [SerializeField]
    private Lamp lightForward;
    [SerializeField]
    private Lamp lightBack;
    private MeshRenderer whiteForward;
    private MeshRenderer whiteBack;
    private Engine engine;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        whiteForward = lightForward.GetComponent<MeshRenderer> ();
        whiteBack = lightBack.GetComponent<MeshRenderer> ();
    }

    private void Start()
    {
        NoLight ();
    }
    public void NoLight()
    {
        LampSwitchOff (whiteBack, lightBack);
        LampSwitchOff (whiteForward, lightForward);
    }

    public void TurnAnyLight(int handler)
    {
        if ( handler > 0 && !lightForward.IsOn )
        {
            LampSwitchOn (whiteForward, lightForward);
            LampSwitchOff (whiteBack, lightBack);
        }
        else if ( handler < 0 && !lightBack.IsOn )
        {
            LampSwitchOn (whiteBack, lightBack);
            LampSwitchOff (whiteForward, lightForward);
        }
        else if ( handler == 0 )
        {
            NoLight ();
        }
    }

    protected void LampSwitchOff( MeshRenderer meshRenderer, Lamp lamp )
    {
        meshRenderer.material = lamp.NoLightMaterial;
        lamp.IsOn = false;

    }

    protected void LampSwitchOn( MeshRenderer meshRenderer, Lamp lamp )
    {
        meshRenderer.material = lamp.LightOnMaterial;
        lamp.IsOn = true;
    }
}
