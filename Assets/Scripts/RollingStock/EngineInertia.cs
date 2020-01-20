using System.Collections;
using UnityEngine;

public class EngineInertia : MonoBehaviour
{

    public float InertiaValue { get; set; }
    public float BreakeForce { get; set; }
    private Engine engine;
    private RSComposition composition;
    private int compCarsQuantity;
    const float engineBreakeForce = 0.06f;
    const float RSBreakeForce = 0.012f;
    private int tempCompQuantity;

    private void Awake()
    {
        engine = GetComponent<Engine> ();
        composition = GetComponent<RSComposition> ();
    }

    private void Start()
    {
        InertiaValue = 0.1f;
        BreakeForce = engineBreakeForce;
        tempCompQuantity = composition.CarComposition.Quantity;
    }

    private void Update()
    {
        StopEngineAfterCoupling ();
    }

    public float GetBreakeForce()
    {
        return engineBreakeForce + GetNumOfCars () * RSBreakeForce;
    }

    private int GetNumOfCars()
    {
        compCarsQuantity = composition.CarComposition.Quantity;
        return compCarsQuantity;
    }

    private void StopEngineAfterCoupling()
    {
        if(composition.CarComposition.Quantity != tempCompQuantity )
        {
            engine.InstructionsHandler = 0;
            tempCompQuantity = composition.CarComposition.Quantity;
        }
       
    }

}
