using System.Collections.Generic;
using System.Linq;

public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public static int CompositionID { get; set; }
    public static Composition [] Compositions;    
    public RollingStock [] RollingStocks { get; set; }
    public RSConnection [] RSConnections { get; set; }
    public RSComposition [] RSCompositions { get; set; }
    private Composition [] tmp;

    public void Init()
    {
        RollingStocks = FindObjectsOfType<RollingStock> ();
        RSConnections = FindObjectsOfType<RSConnection> ();
        RSCompositions = FindObjectsOfType<RSComposition> ();
        Compositions = new Composition [50];
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
        for ( int i = 0; i < CompositionID; i++ )
        {
            Compositions [i] = null;
        }
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
        if ( rollingStock.IsEngine && rollingStock.Engine.IsActive )
        {
            composition.CompEngine = rollingStock.Engine;
        }
        else
            composition.CompEngine = null;
        // add composition in Array
        Compositions [composition.Number] = composition;
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
                if ( conLeft.RollingStock.IsEngine && conLeft.RollingStock.Engine.IsActive )
                    composition.CompEngine = conLeft.RollingStock.Engine;
                else
                    composition.CompEngine = null;
            }

            conLeft = conLeft.LeftCar;
        }
        //set quantity of cars in composition
        composition.Quantity = composition.Cars.Count;
        //define Left and Right Cars
        composition.LeftCar = composition.Cars.First ().RollingStock;
        composition.RightCar = composition.Cars.Last ().RollingStock;
        // set engine to all cars of composition
        composition.SetEngineToAllCars ();
        print ("ID:" + CompositionID + "  Cars: " + composition.Quantity + " Left Car " + composition.LeftCar.Number);
        //increase composition ID
        CompositionID++;
    }

    public static void AddRSInComposition( RSComposition rs, Composition composition, int compositionID )
    {
        composition.Cars.Insert (0, rs);
        rs.CompositionNumber = compositionID;
        rs.CarComposition = composition;
    }

    public void OnUpdate()
    {
        CompositionMoving ();
    }

    private void CompositionMoving()
    {
                      
        for ( int i = 0; i < CompositionID; i++ )
        {
            if(Compositions[i] != null )
            {
                Compositions [i].Move ();
            }
        }
    }

    private void CompositionInstantiate()
    {
        for ( int i = 0; i < CompositionID; i++ )
        {
            if ( Compositions [i] != null )
            {
                Compositions [i].Instantiate ();
            }
        }
    }
}
