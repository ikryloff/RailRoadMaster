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
    public RollingStock TempCar { get; set; }
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
        if ( CompEngine )
            step = CompEngine.EngineStep;
        else
            step = 0;

        for ( int i = 0; i < Cars.Count; i++ )
        {
            if ( i == 0 )
            {
                TempCar = Cars [i].RollingStock;
                TempCar.MoveByPath (step);

                TempCar.BogeyLeft.CalcCompositionPosition (TempCar, TempCar.BogeyLeft.Offset);
                TempCar.BogeyRight.CalcCompositionPosition (TempCar, TempCar.BogeyRight.Offset);
                //Cars [i].RollingStock.BogeyLeft.MoveByPath (step);
                //Cars [i].RollingStock.BogeyRight.MoveByPath (step);
            }
            else
            {
                TempCar = Cars [i].RollingStock;
                TempCar.CalcCompositionPosition (Cars [i - 1].RollingStock, Constants.RS_OFFSET);
                TempCar.BogeyLeft.CalcCompositionPosition (TempCar, TempCar.BogeyLeft.Offset);
                TempCar.BogeyRight.CalcCompositionPosition (TempCar, TempCar.BogeyRight.Offset);
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
