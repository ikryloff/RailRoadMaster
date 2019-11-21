using System.Collections.Generic;
using UnityEngine;

public class Composition
{

    public int Number { get; set; }

    public int Quantity { get; set; }

    public Engine CompEngine { get; set; }

    public List<RSComposition> Cars { get; set; }

    public RollingStock MainCar { get; set; }
    private float step;

    public Composition( int number )
    {       
        Cars = new List<RSComposition> ();
        Number = number;
    }
    public void SetEngineToAllCars()
    {
        foreach ( RSComposition car in Cars )
        {
            car.RollingStock.SetEngineToRS (CompEngine);
        }
    }

    public void Move()
    {
        foreach(RSComposition car in Cars )
        {
            if ( CompEngine )
            {
                step = CompEngine.EngineStep;
                car.RollingStock.MoveByPath (step);
                car.RollingStock.BogeyLeft.MoveByPath (step);
                car.RollingStock.BogeyRight.MoveByPath (step);
            }
            
        }
    }

    public void Instantiate()
    {
        foreach ( RSComposition car in Cars )
        {
            step = 0;
            car.RollingStock.MoveByPath (step);
            car.RollingStock.BogeyLeft.MoveByPath (step);
            car.RollingStock.BogeyRight.MoveByPath (step);

        }
    }
}
