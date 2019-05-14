﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public static int CompositionID { get; set; }
    public static Dictionary<int, Composition> CompositionsDict;
    public RollingStock[] RollingStocks { get; set; }
    public RSConnection [] RSConnections { get; set; }
    public RSComposition [] RSCompositions { get; set; }

    public void Init()
    {
        RollingStocks = FindObjectsOfType<RollingStock>();
        RSConnections = FindObjectsOfType<RSConnection>();
        RSCompositions = FindObjectsOfType<RSComposition> ();
        CompositionsDict = new Dictionary<int, Composition>();  // new Dict of compositions 
        RollingStockInitialisation();
    }

    public void OnStart()
    {
        UpdateCompositions();
        RollingStockStarting();
    }

    private void RollingStockInitialisation()
    {
        foreach (RollingStock rs in RollingStocks)
        {
            rs.Init();
        }
        
    }

    private void RollingStockStarting()
    {
        foreach (RollingStock rs in RollingStocks)
        {
            rs.OnStart();
        }
        foreach ( RSConnection rs in RSConnections )
        {
            rs.OnStart ();
        }
    }


    public void UpdateCompositions()
    {
        if (CompositionsDict.Count > 0)
            CompositionsDict.Clear();
        CompositionID = 0;
        EventManager.OnCompositionChanged();       
    }

    public static void UpdateCarComposition(RollingStock rollingStock)
    {
        // make new composition
        Composition composition = new Composition (CompositionManager.CompositionID);
        //find Engine in this Car
        if ( rollingStock.IsEngine )
            composition.CompEngine = rollingStock.OwnEngine;
        else
            composition.CompEngine = null;
        // add composition in Dict
        CompositionsDict.Add (CompositionID, composition);
        // add rs in composition 
        AddRSInComposition (rollingStock.RSComposition, composition, CompositionID);
        // temp car from left
        RSConnection conLeft = rollingStock.RSConnection.LeftCar;
        // if there any connected to left cars rs in composition
        while ( conLeft != null )
        {
            AddRSInComposition (conLeft.RSComposition, composition, CompositionID);
            if ( conLeft.RollingStock.IsEngine )
                composition.CompEngine = conLeft.RollingStock.OwnEngine;
            else
                composition.CompEngine = null;
            conLeft = conLeft.LeftCar;
        }
        //set quantity of cars in composition
        composition.Quantity = composition.Cars.Count;
        // set engine to all cars of composition
        composition.SetEngineToAllCars ();
        print ("ID:" + CompositionID + "  Cars: " + composition.Quantity);
        //increase composition ID
        CompositionID++;
    }

    public static void AddRSInComposition( RSComposition rs, Composition composition, int compositionID )
    {
        composition.Cars.Insert (0, rs);
        rs.CompositionNumber = compositionID;
    }

}
