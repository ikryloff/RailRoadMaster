﻿using UnityEngine;

public class RSComposition : MonoBehaviour, IManageable
{
    public int CompositionNumber { get; set; }
    public RSConnection RSConnection { get; set; }
    public Engine Engine { get; set; }
    public RollingStock RollingStock { get; private set; }
    public bool IsMainCar { get; private set; }
    public Composition CarComposition { get; set; }

    public void Init()
    {
        EventManager.onCompositionChanged += UpdateCarComposition;
        RSConnection = GetComponent<RSConnection> ();
        RSConnection.RSComposition = this;
        RollingStock = gameObject.GetComponent<RollingStock> ();
        
    }

    public void OnStart() {  }
	
    // Get this car position in path   
    public void UpdateCarComposition()
    {
        // if car not connected from right
        if ( !RSConnection.IsConnectedRight )
        {
            IsMainCar = true;
            CompositionManager.UpdateCarComposition (RollingStock);
        }
        else IsMainCar = false;
    }

    
}
