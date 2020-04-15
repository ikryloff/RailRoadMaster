public class CompositionManager : Singleton<CompositionManager>, IManageable
{
    public Composition [] Compositions;
    public RollingStock [] Cars { get; set; }
    private CarsHolder carsHolder;
    public RSConnection [] RSConnections { get; set; }
    public RSComposition [] RSCompositions { get; set; }
    RSConnection rightConnection;
    private int CompLength;


    public void Init()
    {
        carsHolder = FindObjectOfType<CarsHolder> ();
        Cars = carsHolder.Cars;
        RSConnections = FindObjectsOfType<RSConnection> ();
        RSCompositions = FindObjectsOfType<RSComposition> ();
        InitCompositions ();
        EventManager.onPathChanged += UpdatePath;
    }

    public void OnStart()
    {
        CompositionInstantiate ();
        UpdatePath ();
    }

    public void UpdatePath()
    {
        for ( int i = 0; i < CompLength; i++ )
        {
            if ( Compositions [i].IsActive && Compositions [i].CompEngine != null )
                TrackPath.Instance.GetTrackPath (Compositions [i].MainCar);
        }

    }

    public void FormCompositionOnBackSide( RollingStock _car )
    {
        // cache old comp
        Composition oldComposition = _car.RSComposition.CarComposition;
        // if it is last car
        if ( _car.RSComposition.NumInComposition == oldComposition.Quantity - 1 )
        {
            oldComposition.IsActive = false;
        }
        //delete car from comp
        oldComposition.Cars [_car.RSComposition.NumInComposition] = null;
        oldComposition.Quantity -= 1;  
        //delete comp engine
        oldComposition.CompEngine = null;
        // make new comp
        Composition carComposition = Compositions[GetFreeCompositionSlotNum (1)];
        //clear engine
        carComposition.CompEngine = null;
        _car.RSComposition.CarComposition = carComposition;
        _car.RSComposition.NumInComposition = 0;
        carComposition.Quantity = 1;
        carComposition.Cars [0] = _car;
        carComposition.LeftCar = _car;
        carComposition.RightCar = _car;
        // Make it mainCar
        _car.RSComposition.IsMainCar = true;
        carComposition.MainCar = _car;
        carComposition.IsOutside = true;
        TrackPath.Instance.GetTrackPath (carComposition.MainCar);
        carComposition.Instantiate ();
    }



    public void FormCompositionsAfterSettingCars( RollingStock [] _cars, RollingStock activeEngine )
    {
        for ( int i = 0; i < _cars.Length; i++ )
        {
            // we take only cars from left 
            if ( _cars [i].RSConnection.LeftCar == null ) // it means that it is still Maincar
            {
                RollingStock car = _cars [i];
                Composition thisCarComposition = car.RSComposition.CarComposition;

                // set engine
                if ( activeEngine != null )
                {
                    thisCarComposition.CompEngine = activeEngine.Engine;
                    thisCarComposition.CompEngine.IsActive = true;
                }
                else
                {
                    thisCarComposition.CompEngine = null;
                }

                //it is in first position now
                car.RSComposition.NumInComposition = 0;
                int count = 1;
                //it is next car now
                rightConnection = car.RSConnection.RightCar;
                while ( rightConnection != null )
                {
                    // define num in comp
                    rightConnection.RSComposition.NumInComposition = count;
                    // comp of connected cars is not active
                    rightConnection.RSComposition.CarComposition.IsActive = false;
                    // set cars comp to each next car
                    rightConnection.RSComposition.CarComposition = thisCarComposition;
                    // place of each next car in comp
                    thisCarComposition.Cars [count] = rightConnection.RollingStock;
                    // each next car is not main now
                    rightConnection.RSComposition.IsMainCar = false;
                    // take next car
                    rightConnection = rightConnection.RightCar;
                    count++;
                }
                thisCarComposition.Quantity = count;
                thisCarComposition.LeftCar = car;
                thisCarComposition.RightCar = thisCarComposition.Cars [count - 1];
                thisCarComposition.IsOutside = false;
                TrackPath.Instance.GetTrackPath (thisCarComposition.MainCar);
            }
        }
    }

    private void CheckEngineToComp( Composition composition, Engine fEngine, Engine sEngine = null )
    {
        if ( composition.CompEngine && composition.CompEngine.IsPlayer )
            return;
        if ( !fEngine && !sEngine && composition.CompEngine == null )
        {
            return;
        }
        if ( fEngine && fEngine.IsPlayer )
        {
            composition.CompEngine = fEngine;
            return;
        }
        else if ( sEngine && sEngine.IsPlayer )
        {
            composition.CompEngine = sEngine;
            return;
        }

        if ( fEngine && fEngine.IsActive )
        {
            composition.CompEngine = fEngine;
        }
        else if ( sEngine && sEngine.IsActive )
        {
            composition.CompEngine = sEngine;
        }

    }

    public void UpdateCompositionsAfterCoupling( RollingStock leftCar, RollingStock rightCar )
    {
        // cach new composition
        Composition leftComposition = leftCar.RSComposition.CarComposition;
        // cach old composition 
        Composition rightComposition = rightCar.RSComposition.CarComposition;

        // clear maincar from right comp
        rightCar.RSComposition.IsMainCar = false;
        // get left comp last car number
        int leftCompQuantity = leftComposition.Quantity;
        // get right comp Quantity
        int rightCompQuantity = rightComposition.Quantity;
        // add cars from right to left comp
        int count = 0;
        for ( int i = 0; i < rightCompQuantity + leftCompQuantity; i++ )
        {
            if ( i >= leftCompQuantity )
            {
                leftComposition.Cars [i] = rightComposition.Cars [count];
                // set new comp to cars after coupler
                leftComposition.Cars [i].RSComposition.CarComposition = leftComposition;
                // set num in composition
                leftComposition.Cars [i].RSComposition.NumInComposition = i;
                // delete cars from right comp
                rightComposition.Cars [count] = null;
                count++;
            }

        }
        // set right car to new comp
        leftComposition.RightCar = leftComposition.Cars [count];
        // set Quantity to new comp
        leftComposition.Quantity = rightCompQuantity + leftCompQuantity;
        // define engines
        CheckEngineToComp (leftComposition, leftComposition.CompEngine, rightComposition.CompEngine);
        //clear right comp
        ClearComposition (rightComposition);
        // stop engine
        if ( leftComposition.CompEngine )
            leftComposition.CompEngine.HandlerZero ();
        UpdatePath ();
    }

    private void ClearComposition( Composition comp )
    {
        //clear engine
        comp.CompEngine = null;
        // set Quantity of right comp
        comp.Quantity = 0;
        //make right comp unactive
        comp.IsActive = false;
        // delete main, right and left cars
        comp.LeftCar = null;
        comp.RightCar = null;
        comp.MainCar = null;
    }

    public void UpdateCompositionsAfterUncoupling( RollingStock rightCar )
    {
        // cache old comp num
        Composition startComposition = rightCar.RSComposition.CarComposition;
        Composition rightComposition;
        // Get newComposition
        int newCompQuantity = startComposition.Quantity - rightCar.RSComposition.NumInComposition;
        //find comp slot
        rightComposition = Compositions [GetFreeCompositionSlotNum (newCompQuantity)];
        // clear engines
        rightComposition.CompEngine = null;
        startComposition.CompEngine = null;
        //set Quantity to new comp
        rightComposition.Quantity = newCompQuantity;
        // take all cars after uncoupled car        
        int count = 0;
        int num = rightCar.RSComposition.NumInComposition;
        for ( int i = 0; i < startComposition.Quantity; i++ )
        {
            if ( i >= num )
            {
                // put all cars after coupler to new comp
                rightComposition.Cars [count] = startComposition.Cars [i];
                // set Num in comp
                rightComposition.Cars [count].RSComposition.NumInComposition = count;
                // set new comp to cars after coupler
                rightComposition.Cars [count].RSComposition.CarComposition = rightComposition;
                // set engine to right comp
                CheckEngineToComp (rightComposition, startComposition.Cars [i].Engine);
                //delete old cars
                startComposition.Cars [i] = null;
                count++;
            }
            else
            {
                // set engine to startComposition comp
                CheckEngineToComp (startComposition, startComposition.Cars [i].Engine);
            }
        }
        // set Quantity to old comp
        startComposition.Quantity = num;
        // set pos of car after coupler
        rightCar.RSComposition.NumInComposition = 0;
        //Set main Car to new comp        
        rightCar.RSComposition.IsMainCar = true;
        rightComposition.MainCar = rightCar;
        // set left and right cars
        rightComposition.LeftCar = rightCar;
        rightComposition.RightCar = rightComposition.Cars [count - 1];
        startComposition.RightCar = startComposition.Cars [startComposition.Quantity - 1];
        // stop engine
        StopEngine (rightComposition.CompEngine);
        StopEngine (startComposition.CompEngine);
        //make comp not outside
        rightComposition.IsOutside = false;
        UpdatePath ();
    }

    private void StopEngine( Engine engine )
    {
        if ( engine )
        {
            if ( engine.IsPlayer )
                engine.HandlerZero ();
            else
                engine.AbsoluteStop ();
        }
    }


    public void InitCompositions()
    {
        Compositions = new Composition [Constants.MAX_COMPS];
        int count = 0;
        foreach ( RollingStock car in Cars )
        {
            // make new composition
            Composition composition = gameObject.AddComponent<Composition> ();
            // make array of cars
            composition.Cars = new RollingStock [Constants.MAX_CARS];
            composition.Number = count;
            composition.Quantity = 1;
            composition.IsActive = true;
            // set comp to each cars
            car.RSComposition.CarComposition = composition;
            //add comp in Compositions
            Compositions [count] = composition;
            composition.Cars [0] = car;
            //set main car for composition
            composition.MainCar = car;
            car.RSComposition.IsMainCar = true;
            //find Engine in this Car
            if ( car.IsEngine )
            {
                composition.CompEngine = car.Engine;
                composition.CompEngine.IsActive = true;
            }
            else
                composition.CompEngine = null;
            count++;

        }
        CompLength = count;
    }

    private int GetFreeCompositionSlotNum( int quantity )
    {
        for ( int i = 0; i < Compositions.Length; i++ )
        {
            if ( !Compositions [i].IsActive && Compositions [i].Quantity <= quantity )
            {
                Compositions [i].IsActive = true;
                return i;
            }
        }
        return -1;
    }



    public void OnUpdate()
    {
        CompositionMoving ();
    }

    private void CompositionMoving()
    {

        for ( int i = 0; i < Compositions.Length; i++ )
        {
            if ( Compositions [i] != null && Compositions [i].IsActive )
            {
                Compositions [i].Move ();
            }
        }
    }

    private void CompositionInstantiate()
    {
        for ( int i = 0; i < Compositions.Length; i++ )
        {
            if ( Compositions [i] != null && Compositions [i].IsActive )
            {
                Compositions [i].Instantiate ();
            }
        }
    }
}
