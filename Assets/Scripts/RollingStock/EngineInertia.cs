using System.Collections;
using UnityEngine;

public class EngineInertia : MonoBehaviour
{

    public float InertiaValue { get; set; }
    public float BreakeForce { get; set; }
    private Engine engine;
    private RSComposition composition;
    private int compCarsQuantity;
    const float engineBreakeForce = 0.35f;
    const float RSBreakeForce = 0.1f;

    private void Awake()
    {
        EventManager.onCarsCoupled += StopEngineAfterCoupling;
        engine = GetComponent<Engine> ();
        composition = GetComponent<RSComposition> ();
    }

    private void Start()
    {
        InertiaValue = 0.2f;
        BreakeForce = engineBreakeForce;
    }

    private void Update()
    {

    }

    public void AddFriction()
    {
        if ( engine.SpeedReal != 0 )
        {
            InertiaValue = 0.02f * GetNumOfCars () * Mathf.Exp (-0.14f * Mathf.Abs (engine.SpeedReal));
            //print (Value + " mult speed " + engine.SpeedReal);            
        }
        else
        {
            BreakeForce = 0.3f;
            InertiaValue = 0.2f;
        }

        if ( engine.Brakes )
        {
            BreakeForce = engineBreakeForce + GetNumOfCars () * RSBreakeForce;
        }
        else
            BreakeForce = 0;

    }

    public float GetBreakeForce()
    {
        return BreakeForce - InertiaValue * BreakeForce;
    }

    private int GetNumOfCars()
    {
        compCarsQuantity = composition.CarComposition.Quantity;
        return compCarsQuantity;
    }

    private void StopEngineAfterCoupling()
    {
        engine.InstructionsHandler = 0;
        StartCoroutine (StopAfterCouplingCoroutine ());
    }

    IEnumerator StopAfterCouplingCoroutine()
    {
        print ("coroutine on");
        while ( engine.SpeedReal != 0 )
        {
            BreakeForce = 1f * GetNumOfCars ();
            yield return null;
        }
        print ("coroutine off");
    }
}
