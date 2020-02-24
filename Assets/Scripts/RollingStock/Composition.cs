using System.Collections.Generic;

public class Composition
{

    public int Number { get; set; }

    public int Quantity { get; set; }

    public Engine CompEngine { get; set; }

    public List<RSComposition> Cars { get; set; }
    public RollingStock LeftCar { get; set; }
    public RollingStock RightCar { get; set; }

    public RollingStock MainCar { get; set; }
    private float step;

    public Composition( int number )
    {
        Cars = new List<RSComposition> ();
        Number = number;
    }
    public void SetEngineToAllCars()
    {
        for ( int i = 0; i < Cars.Count; i++ )
        {
            Cars [i].RollingStock.SetEngineToRS (CompEngine);
        }
    }

    public void Move()
    {
        for ( int i = 0; i < Cars.Count; i++ )
        {
            if ( CompEngine )
                step = CompEngine.EngineStep;
            else
                step = 0;

            Cars [i].RollingStock.MoveByPath (step);
            Cars [i].RollingStock.BogeyLeft.MoveByPath (step);
            Cars [i].RollingStock.BogeyRight.MoveByPath (step);
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
