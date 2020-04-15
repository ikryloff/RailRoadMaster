using System.Collections.Generic;
using UnityEngine;

public class Composition : MonoBehaviour
{

    public int Number { get; set; }

    public int Quantity { get; set; }

    public Engine CompEngine { get; set; }
    public bool IsActive { get; set; }
    public bool IsOutside { get; set; }


    public RollingStock [] Cars { get; set; }
    public RollingStock LeftCar { get; set; }
    public RollingStock RightCar { get; set; }

    public RollingStock MainCar { get; set; }
    public RollingStock CompCar { get; set; }
    private float step;

   
    public void Move()
    {
        if ( IsOutside )
            return;
        if ( CompEngine )
            step = CompEngine.EngineStep;
        else
            step = 0;

        for ( int i = 0; i < Quantity; i++ )
        {
            CompCar = Cars [i];
            if ( i == 0 )
            {
                CompCar.MoveByPath (step);
                CompCar.BogeyLeft.CalcCompositionPosition (CompCar, CompCar.BogeyLeft.Offset);
                CompCar.BogeyRight.CalcCompositionPosition (CompCar, CompCar.BogeyRight.Offset);

            }
            else
            {
                CompCar.CalcCompositionPosition (Cars [i - 1], Constants.RS_OFFSET);
                CompCar.BogeyLeft.CalcCompositionPosition (CompCar, CompCar.BogeyLeft.Offset);
                CompCar.BogeyRight.CalcCompositionPosition (CompCar, CompCar.BogeyRight.Offset);
            }
        }
    }

    public void Instantiate()
    {

        for ( int i = 0; i < Cars.Length; i++ )
        {
            if ( Cars [i] != null )
            {
                step = 0;
                Cars [i].MoveByPath (step);
                Cars [i].BogeyLeft.MoveByPath (step);
                Cars [i].BogeyRight.MoveByPath (step);
            }
                
        }

    }

}
