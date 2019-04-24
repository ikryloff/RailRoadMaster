using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public static int CompositionID = 0;
    public Dictionary<int, Composition> CompositionsDict { get; set; }
    public RollingStock[] RollingStocks { get; set; }
   

    

    public void Init()
    {
        RollingStocks = FindObjectsOfType<RollingStock>();
    }

    public void OnStart()
    {
        InitializeCompositionDict();
    }


    private void InitializeCompositionDict()
    {
        CompositionsDict = new Dictionary<int, Composition>();        
        foreach (RollingStock rs in RollingStocks)
        {
            if (!rs.IsConnectedRight)
            {
                Composition c = new Composition(CompositionID);                
                CompositionsDict.Add(CompositionID, c);
                c.RollingStocks.Add(rs);
                rs.CompositionNumber = CompositionID;
                //print("CompositionID " + CompositionID);
                CompositionID++;

            }
        }
    }
}
