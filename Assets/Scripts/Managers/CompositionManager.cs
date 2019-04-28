using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public static int CompositionID { get; set; }
    public static Dictionary<int, Composition> CompositionsDict;
    public RollingStock[] RollingStocks { get; set; }
    public RSConnection [] RSConnections { get; set; }

    public void Init()
    {
        RollingStocks = FindObjectsOfType<RollingStock>();
        RSConnections = FindObjectsOfType<RSConnection>();
        CompositionsDict = new Dictionary<int, Composition>();  // new Dict of compositions 
        RollingStockInitialisation();
    }

    public void OnStart()
    {
        UpdateCompositionDictionary();
        RollingStockStarting();
    }

    private void RollingStockInitialisation()
    {
        foreach (RollingStock rs in RollingStocks)
        {
            rs.Init();
        }
        foreach ( RSConnection rs in RSConnections )
        {
            rs.Init ();
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


    private void UpdateCompositionDictionary()
    {
        if (CompositionsDict.Count > 0)
            CompositionsDict.Clear();
        CompositionID = 0;
        EventManager.OnCompositionChanged();       
    }

   
}
