
using UnityEngine;

[CreateAssetMenu (fileName = "New TrainData", menuName = "TrainData", order = 52)]
public class TrainData : ScriptableObject
{
    [SerializeField]
    private int trainNumber;
   
    [SerializeField]
    private int trainDirection;

    [SerializeField]
    private int engine;

    [SerializeField]
    private int [] cars; // left to right

    [SerializeField]
    private int prevStationTime;


    [SerializeField]
    private int stopTime;

    [SerializeField]
    private int departureTime;

    [SerializeField]
    private int nextStationTime;

    //operations 
    // 0 - stop
    // 1 - passanger stop
    // 2 - fast
    // 3 - normal
    // 4 - make
    // 5 - break   

    [SerializeField]
    private int operationBefore;

    [SerializeField]
    private int operationArrive; 

    // 1 - first
    // 2 - second
    [SerializeField]
    private int halfHour;

    public int TrainNumber
    {
        get
        {
            return trainNumber;
        }

        set
        {
            trainNumber = value;
        }
    }

    public int TrainDirection
    {
        get
        {
            return trainDirection;
        }

        set
        {
            trainDirection = value;
        }
    }

    public int Engine
    {
        get
        {
            return engine;
        }

        set
        {
            engine = value;
        }
    }

    public int [] Cars
    {
        get
        {
            return cars;
        }

        set
        {
            cars = value;
        }
    }

    public int DepartureTime
    {
        get
        {
            return departureTime;
        }

        set
        {
            departureTime = value;
        }
    }

    public int StopTime
    {
        get
        {
            return stopTime;
        }

        set
        {
            stopTime = value;
        }
    }

    public int PrevStationTime
    {
        get
        {
            return prevStationTime;
        }

        set
        {
            prevStationTime = value;
        }
    }

   
    public int HalfHour
    {
        get
        {
            return halfHour;
        }

        set
        {
            halfHour = value;
        }
    }

    
    public int OperationBefore
    {
        get
        {
            return operationBefore;
        }

        set
        {
            operationBefore = value;
        }
    }

    public int NextStationTime
    {
        get
        {
            return nextStationTime;
        }

        set
        {
            nextStationTime = value;
        }
    }
   

    public int OperationArrive
    {
        get
        {
            return operationArrive;
        }

        set
        {
            operationArrive = value;
        }
    }
}
