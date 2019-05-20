using System.Collections.Generic;

public class Composition {

	public int Number { get; set; }

    public int Quantity { get; set; }

    public Engine CompEngine { get; set; }

    public List<RSComposition> Cars { get; set; }

    public RollingStock MainCar { get; set; }
        
    public Composition(int number)
    {
        EventManager.onPathUpdated += SetPathToAllCars;
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

    public void SetPathToAllCars()
    {
        foreach ( RSComposition car in Cars )
        {
            car.RollingStock.OwnPath = MainCar.OwnPath;
        }

    }
}
