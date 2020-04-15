using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public Train [] Trains;
    public TrainData [] TrainDatas;
    public CarsHolder CarsHolder;

    public void OnAwake()
    {
        CarsHolder = FindObjectOfType<CarsHolder> ();
        TrainDatas = FindObjectOfType<TrainList> ().trains;
        Trains = new Train [50];
        ConvertDataToTrain ();
    }

    public void OnStart()
    {
        for ( int i = 0; i < Trains.Length; i++ )
        {
            if ( Trains [i] != null )
            {
                Trains [i].TrainBuild ();
            }
        }

    }

    private void ConvertDataToTrain()
    {
        for ( int i = 0; i < TrainDatas.Length; i++ )
        {
            Train train = gameObject.AddComponent<Train> ();
            train.MakeTrainFromData (TrainDatas [i]);
            Trains [i] = train;
        }
    }


}
