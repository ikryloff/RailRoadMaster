
using System.Collections.Generic;

public class Composition {

	public int Number { get; set; }

    public List<RollingStock> RollingStocks { get; set; }

    

    public Composition(int number)
    {
        RollingStocks = new List<RollingStock>();
        Number = number;
    }

}
