using System.Collections.Generic;

public class Composition {

	public int Number { get; set; }

    public List<RSComposition> compositions { get; set; }

    public Composition(int number)
    {
        compositions = new List<RSComposition> ();
        Number = number;
    }

}
