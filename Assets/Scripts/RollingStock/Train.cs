using UnityEngine;

public class Train : MonoBehaviour
{
    private int TrainNumber;

    private int TrainDirection;

    private TrackPathUnit startTrack;

    private TrackPathUnit outTrack;

    private int intEngine;

    private bool isComplete;

    private int faults = 4;

    private int check = 19;

    private bool IsActive;

    private Engine engine;

    private Engine trainEngine;

    private RollingStock mainCar;


    private int [] Cars; // left to right

    private int StartTime;

    private int OverTime;

    public CarsHolder CarsHolder;

    public void MakeTrainFromData( TrainData trainData )
    {
        TrainNumber = trainData.TrainNumber;
        TrainDirection = trainData.TrainDirection;
        intEngine = trainData.Engine;
        Cars = trainData.Cars;
        StartTime = trainData.PrevStationTime;
        OverTime = trainData.NextStationTime;
        isComplete = false;
        IsActive = false;
        CarsHolder = FindObjectOfType<CarsHolder> ();
        //cache each train engine, but will use only trainEngine
        engine = CarsHolder.GetCar (intEngine).GetComponent<Engine> ();
        SetTrainProperties (this);
        EventManager.onHourPassed += TrainBuild;
        EventManager.onMinutePassed += CheckTrain;
        EventManager.onTrainSignalChanged += TryToGo;
    }


    public void TrainBuild()
    {
        int time = TimeManager.Instance.TimeValue [0];
        if ( time == StartTime && time == 1 )
        {
            if ( startTrack.TrackCircuit.HasCarPresence )
            {
                print ("Game Over. Track is not free");
            }

            CarsHolder.SetCarsPosition (startTrack.name, 1000, Cars, intEngine);
            trainEngine = engine;
            // cache maincar
            mainCar = trainEngine.EngineRS.RSComposition.CarComposition.MainCar;
            IsActive = true;
            EventManager.SignalChanged ();
            trainEngine.EngineAI.MoveWithDirection (TrainDirection * Constants.MAX_THROTTLE);
        }

    }

    public void CheckTrain()
    {
        if ( IsActive )
        {
            // trigger to make Unactive
            if ( mainCar.OwnTrack == outTrack && mainCar.OwnPosition <= 3000 )
            {
                print ("Train passed");
                IsActive = false;
                CarsHolder.PutListOfCarsOnBackTrackByNumbers (Cars);
                return;
            }
            //check if player passed train
            if ( OverTime >= 0 )
            {
                if ( OverTime == TimeManager.Instance.TimeValue [0] + 1 )
                {
                    print ("Game Over");
                    return;
                }
            }
            else
            {
                // if train that must be go to Bravo left station
                if( mainCar.OwnTrack == outTrack )
                {
                    print ("Game Over");
                    return;
                }                
            }

            if ( faults == 0 )
            {
                print ("Game Over");
                return;
            }

            //check if player did nt take train

            if ( TimeManager.Instance.TimeValue [0] == StartTime )
            {
                if ( TimeManager.Instance.TimeValue [1] % check == 0 )
                {
                    if ( startTrack.TrackCircuit.HasCarPresence )
                    {
                        faults--;
                        print (faults + " left");
                    }
                    TryToGo ();
                }

            }
        }

    }

    public void SetTrainProperties( Train train )
    {
        //Start TPU
        if ( train.TrainDirection == -1 )
            startTrack = TrackPath.Instance.GetTrackPathUnitByName (Constants.TRACK_I_RIGHT);
        else
            startTrack = TrackPath.Instance.GetTrackPathUnitByName (Constants.TRACK_I_LEFT);
        //out TPU
        outTrack = TrackPath.Instance.GetTrackPathUnitByName (startTrack.name.Equals (Constants.TRACK_I_RIGHT) ? Constants.TRACK_I_LEFT : Constants.TRACK_I_RIGHT);

    }

    public void TryToGo()
    {
        //if it transit
        if ( trainEngine && StartTime == OverTime )
            trainEngine.EngineAI.MoveWithDirection (TrainDirection * Constants.MAX_THROTTLE);
    }



}
