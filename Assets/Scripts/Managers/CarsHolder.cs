using UnityEngine;

public class CarsHolder : MonoBehaviour, IManageable
{
    public RollingStock [] Cars { get; set; }
    public Engine [] Engines { get; set; }
    public RSConnection [] Connections { get; set; }
    private TrackCircuit tempTC;
    GameObject rsGO;
    RollingStock rs;


    public void Init()
    {
        Cars = FindObjectsOfType<RollingStock> ();
        Engines = FindObjectsOfType<Engine> ();
        Connections = FindObjectsOfType<RSConnection> ();
        CarsInit ();
    }

    private void CarsInit()
    {
        foreach ( RollingStock car in Cars )
        {
            car.Init ();
        }
    }


    //RS
    // 8701, 8888,  
    // 2140,
    // 6135, 6548
    // 7522, 7508, 7143, 7445, 7267,
    // 114, 115, 116, 117


    public void OnStart()
    {
        PutCarsOnBackTrackOnStart (new int [] { 7522, 7508, 7143, 7445, 7267, 114, 115, 116, 117, 2140, 6135, 6548, 8701, 8888 });
        SetCarsPosition ("PathTr10", 150, new int [] { 8888, 7143, 7445 }, 8888);
        SetCarsPosition ("PathTr10", 50, new int [] { 6135 });
        SetCarsPosition ("PathTr3", 150, new int [] { 8701 }, 8701);
        PutOneCarOnBackTrack (GetCar(6135));
    }

    private void PutCarsOnBackTrackOnStart( int [] carNums )
    {
        int count = 1;
        RollingStock _car;
        foreach ( int item in carNums )
        {
            _car = GetCar (item);
            //save Backside position
            _car.BackSidePosition = count;
            SetCarsPosition (Constants.TRACK_BACKSIDE, count * 100, new int [] { item });
            _car.RSComposition.CarComposition.IsOutside = true;
            count++;
        }
    }

    public void PutListOfCarsOnBackTrackByNumbers( int [] carNums )
    {
        int count;
        RollingStock _car;
        for ( int i = 0; i < carNums.Length; i++ )
        {
            int item = carNums [i];
            _car = GetCar (item);
            //load Backside position
            count = _car.BackSidePosition;
            SetCarsToBackSide (count * 100, item);
        }
    }

    public void PutOneCarOnBackTrack( RollingStock car )
    {
        int num = car.Number;
        int count = car.BackSidePosition;
        SetCarsToBackSide (count * 100, num);

    }


    public void OnUpdate()
    {
        RunEngines ();
    }

    private void RunEngines()
    {
        for ( int i = 0; i < Engines.Length; i++ )
        {
            Engines [i].RunEngineAction ();
        }
    }

    private void GetTrackPathForAllRS()
    {
        foreach ( RollingStock item in Cars )
        {
            TrackPath.Instance.GetTrackPath (item);
        }
    }

    public RollingStock GetCar( int num )
    {
        if ( num == 0 )
            return null;
        for ( int i = 0; i < Cars.Length; i++ )
        {
            if ( Cars [i].Number == num )
                return Cars [i];
        }
        return null;
    }


    public void SetCarsToBackSide( float position, int carNum )
    {
        RollingStock rs = GetCar (carNum);

        //clear engine
        if ( rs.IsEngine )
        {
            rs.Engine.IsActive = false;
            rs.Engine.IsPlayer = false;
        }
        //clear all connections
        ClearConnections (rs);
        //release prev TC 
        tempTC = rs.OwnTrackCircuit;
        // set new TPU
        rs.OwnTrack = TrackPath.Instance.GetTrackPathUnitByName (Constants.TRACK_BACKSIDE);
        // set new TC
        rs.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;
        // release prev tracks
        ReleaseStartTracks (rs, tempTC);
        //set position
        rs.OwnPosition = position;
        //set bogeys the same trackpathunit        
        rs.ResetBogeys ();

        CompositionManager.Instance.FormCompositionOnBackSide (rs);
        IndicationManager.Instance.UpdateCouplerIndication ();
    }



    public void SetCarsPosition( string trackName, float position, int [] carsNums, int activeEngine = 0 )
    {
        RollingStock [] cars = new RollingStock [carsNums.Length];
        RollingStock rs = GetCar (carsNums [0]);
        RollingStock actEng = GetCar (activeEngine);
        // fill array of cars
        cars [0] = rs;
        //clear all connections
        ClearConnections (rs);

        //release prev TC 
        tempTC = rs.OwnTrackCircuit;

        rs.OwnTrack = TrackPath.Instance.GetTrackPathUnitByName (trackName);
        rs.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;

        ReleaseStartTracks (rs, tempTC);
        //set position
        rs.OwnPosition = position;
        //set bogeys the same trackpathunit        
        rs.ResetBogeys ();

        // if one car no need
        for ( int i = 1; i < carsNums.Length; i++ )
        {
            //take previous car
            rs = GetCar (carsNums [i - 1]);
            // get next car
            RollingStock rightCar = GetCar (carsNums [i]);
            // fill array of cars
            cars [i] = rightCar;
            //clear all connections
            ClearConnections (rightCar);
            //release prev TC
            tempTC = rightCar.OwnTrackCircuit;
            rightCar.OwnTrack = rs.OwnTrack;
            rightCar.OwnTrackCircuit = rs.OwnTrack.TrackCircuit;
            ReleaseStartTracks (rightCar, tempTC);

            rs.RSConnection.InitConnection (rightCar.RSConnection);
            rightCar.ResetBogeys ();
        }
        CompositionManager.Instance.FormCompositionsAfterSettingCars (cars, actEng);
        IndicationManager.Instance.UpdateCouplerIndication ();
    }

    private void ClearConnections( RollingStock rs )
    {
        if ( rs.RSConnection.IsConnectedRight )
            rs.RSConnection.RemoveConnection ();
    }

    private void ReleaseStartTracks( RollingStock _rs, TrackCircuit _tc )
    {
        if ( _tc != null )
        {
            _tc.RemoveCars (_rs.BogeyLeft);
            _tc.RemoveCars (_rs.BogeyRight);
        }

    }


    public void UpdateConnections()
    {
        for ( int i = 0; i < Connections.Length; i++ )
        {
            Connections [i].OnUpdate ();
        }
    }


    public void SetUnactiveRS( int rsNum )
    {
        RollingStock car = GetCar (rsNum);

        car.BogeyLeft.OwnTrackCircuit = null;
        car.BogeyRight.OwnTrackCircuit = null;
        car.BogeyLeft.ProvidePresence ();
        car.BogeyRight.ProvidePresence ();
        rsGO = car.gameObject;
        rsGO.SetActive (false);

    }

    public void SetActiveRS( int rsNum )
    {
        rsGO = GetCar (rsNum).gameObject;
        rs = GetCar (rsNum);
        rsGO.SetActive (true);
        if ( rs.IsEngine )
        {
            rs.OwnEngine = rs.Engine;
            rs.Engine.IsActive = true;
        }

    }

}
