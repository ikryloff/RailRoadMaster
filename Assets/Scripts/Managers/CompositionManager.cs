using System.Collections.Generic;

public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public static int CompositionID { get; set; }
    public static Dictionary<int, Composition> CompositionsDict;
    public RollingStock [] RollingStocks { get; set; }
    public RSConnection [] RSConnections { get; set; }
    public RSComposition [] RSCompositions { get; set; }

    public void Init()
    {
        RollingStocks = FindObjectsOfType<RollingStock> ();
        RSConnections = FindObjectsOfType<RSConnection> ();
        RSCompositions = FindObjectsOfType<RSComposition> ();
        CompositionsDict = new Dictionary<int, Composition> ();  // new Dict of compositions 
        RollingStockInitialisation ();
        UpdateCompositions ();
    }

    public void OnStart()
    {
        RollingStockStarting ();
        CompositionInstantiate ();
    }

    private void RollingStockInitialisation()
    {
        foreach ( RollingStock rs in RollingStocks )
        {
            rs.Init ();
        }

    }

    private void RollingStockStarting()
    {
        foreach ( RollingStock rs in RollingStocks )
        {
            rs.OnStart ();
        }
        foreach ( RSConnection rs in RSConnections )
        {
            rs.OnStart ();
        }
    }

    // after coupling and uncoupling
    public void UpdateCompositions()
    {
        if ( CompositionsDict.Count > 0 )
            CompositionsDict.Clear ();
        CompositionID = 0;
        EventManager.CompositionChanged (); // send to RSComposition
    }

    public static void UpdateCarComposition( RollingStock rollingStock )
    {
        // make new composition
        Composition composition = new Composition (CompositionID);
        //set main car for paths
        composition.MainCar = rollingStock;
        //find Engine in this Car
        if ( rollingStock.IsEngine )
        {
            composition.CompEngine = rollingStock.OwnEngine;
        }

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
            if ( !composition.CompEngine )
            {
                if ( conLeft.RollingStock.IsEngine )
                    composition.CompEngine = conLeft.RollingStock.OwnEngine;
                else
                    composition.CompEngine = null;
            }

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
        rs.CarComposition = composition;
    }

    private void Update()
    {
        CompositionMoving ();
    }

    private void CompositionMoving()
    {
        foreach ( Composition comp in CompositionsDict.Values )
        {
            comp.Move ();
        }
    }

    private void CompositionInstantiate()
    {
        foreach ( Composition comp in CompositionsDict.Values )
        {
            comp.Instantiate ();
        }
    }
}
