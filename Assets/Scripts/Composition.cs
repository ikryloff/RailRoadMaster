using System.Collections.Generic;

public class Composition {

	public int Number { get; set; }

    public List<RSConnection> RSConnections { get; set; }

    

    public Composition(int number)
    {
        RSConnections = new List<RSConnection>();
        Number = number;
    }

}
