﻿using UnityEngine;

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
    

    public float GetBreakeForce()
    {
        return engineBreakeForce + GetNumOfCars () * RSBreakeForce;
    }

    private int GetNumOfCars()
    {
        compCarsQuantity = composition.CarComposition.Quantity;
        return compCarsQuantity;
    }

    

}
