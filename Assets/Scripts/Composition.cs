using System.Collections.Generic;

public class Composition {

	public int Number { get; set; }

    public int Quantity { get; set; }

    public Engine CompEngine { get; set; }

    public List<RSComposition> Cars { get; set; }

    public Composition(int number)
    {
        Cars = new List<RSComposition> ();
        Number = number;
    }
    public void SetEngineToAllCars()
    {
        foreach ( RSComposition car in Cars )
        {
            if ( CompEngine )
            {
                car.RollingStock.OwnEngine = CompEngine;
                car.RollingStock.bogeyLeft.OwnEngine = CompEngine;
                car.RollingStock.bogeyRight.OwnEngine = CompEngine;
            }
            else
            {
                car.RollingStock.OwnEngine = null;
                car.RollingStock.bogeyLeft.OwnEngine = null;
                car.RollingStock.bogeyRight.OwnEngine = null;
            }
        }

    }
}
