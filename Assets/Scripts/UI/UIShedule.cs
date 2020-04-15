using UnityEngine;

public class UIShedule : MonoBehaviour
{
    public float Position;
    public GameObject pref;
    public Transform parent;
    public TrainList trainList;
    public UISheduleItem [] SheduleItems;
    public int counter;
    private int NUM_OF_ROWS = 24;
    private int NUM_OF_CAR_PANELS = 9;
    public int diff;


    private void Awake()
    {
        trainList = FindObjectOfType<TrainList> ();
        SheduleItems = new UISheduleItem [NUM_OF_ROWS];
        for ( int i = 0; i < NUM_OF_ROWS; i++ )
        {
            SheduleItems [i] = Instantiate (pref, parent).GetComponent<UISheduleItem> ();
            SheduleItems [i].ItemNumber = i;
            SheduleItems [i].OnAwake ();
            SheduleItemHandle (SheduleItems [i]);
            if ( i >= 6 )
            {
                SheduleItems [i].gameObject.SetActive (false);
            }
        }
    }


    public void MoveUpPosition()
    {
        if ( counter > 17 )
            return;
        SheduleItems [counter].gameObject.SetActive (false);
        SheduleItems [counter + 6].gameObject.SetActive (true);
        for ( int i = 0; i < NUM_OF_ROWS; i++ )
        {
            SheduleItems [i].IncreaseUpPosition ();
        }
        counter++;
    }

    public void MoveDownPosition()
    {
        if ( counter < 1 )
            return;
        SheduleItems [counter - 1].gameObject.SetActive (true);
        SheduleItems [counter + 5].gameObject.SetActive (false);
        for ( int i = 0; i < NUM_OF_ROWS; i++ )
        {
            SheduleItems [i].IncreaseDownPosition ();
        }
        counter--;
    }


    public void FocusPosition()
    {
        diff = TimeManager.Instance.TimeValue [0] - counter;
        if ( diff < 0 && counter >= 3 )
        {
            for ( int i = 0; i < -diff; i++ )
            {
                MoveDownPosition ();
            }
        }
        else if ( diff > 0 && counter < 17 )
            for ( int i = 0; i < diff; i++ )
            {
                MoveUpPosition ();
            }

    }

    private void SheduleItemHandle( UISheduleItem item )
    {
        TrainData [] trains = trainList.trains;
        TrainData train;
        UIHalfHourPanel halfHourPanel = null;
        for ( int i = 0; i < trains.Length; i++ )
        {
            train = trains [i];
            if ( item.ItemNumber == train.PrevStationTime || item.ItemNumber == train.StopTime || item.ItemNumber == train.DepartureTime || item.ItemNumber == train.NextStationTime )
            {
                if ( train.HalfHour == 1 )
                {
                    halfHourPanel = item.HalfHourFirst;
                    item.firstTrainNumber.SetNumber (train.TrainNumber.ToString ());
                    item.HalfHourFirst.IsFull = true;
                }
                else if ( train.HalfHour == 2 )
                {
                    halfHourPanel = item.HalfHourSecond;
                    item.secondTrainNumber.SetNumber (train.TrainNumber.ToString ());
                    item.HalfHourSecond.IsFull = true;
                }

                if ( item.ItemNumber == train.PrevStationTime )
                {
                    if ( train.TrainDirection == 1 ) // =>
                    {
                        //Alfa panel                    
                        PanelHandleForward (halfHourPanel.AlfaPanel, train, train.OperationBefore);

                    }
                    else if ( train.TrainDirection == -1 ) // <=
                    {

                        //Charlie panel
                        PanelHandleBack (halfHourPanel.CharliePanel, train, train.OperationBefore);
                    }
                }

                if ( item.ItemNumber == train.StopTime )
                {
                    if ( train.TrainDirection == 1 ) // =>
                    {
                        //Bravo panel
                        PanelHandleForward (halfHourPanel.BravoPanel, train, train.OperationArrive);
                    }
                    else if ( train.TrainDirection == -1 ) // <=
                    {
                        //Bravo panel
                        PanelHandleBack (halfHourPanel.BravoPanel, train, train.OperationArrive);
                    }
                }
               

                if ( item.ItemNumber == train.NextStationTime )
                {
                    if ( train.NextStationTime == -1 )
                        return;
                    if ( train.TrainDirection == 1 ) // =>
                    {
                        //Charlie panel
                        PanelHandleForward (halfHourPanel.CharliePanel, train);
                    }
                    else if ( train.TrainDirection == -1 ) // <=
                    {
                        //Alfa panel                    
                        PanelHandleBack (halfHourPanel.AlfaPanel, train);
                    }
                }
            }
            else
            {
                if ( !item.HalfHourFirst.IsFull )
                    item.firstTrainNumber.SetNumber ("");
                if ( !item.HalfHourSecond.IsFull )
                    item.secondTrainNumber.SetNumber ("");

            }


        }
    }


    private void PanelHandleForward( UIStationPanel panel, TrainData trainData, int operationBefore = 2 )
    {
        int count = 0;
        for ( int j = NUM_OF_CAR_PANELS - 2; j >= NUM_OF_CAR_PANELS - trainData.Cars.Length - 1; j-- )
        {
            panel.CarPanels [j].CalcAndSetCarIcon (trainData.Cars [count]);
            count++;
        }
        panel.CarPanels [NUM_OF_CAR_PANELS - 1].CalcAndSetOperationIcon (operationBefore, trainData.TrainDirection);

    }

    private void PanelHandleBack( UIStationPanel panel, TrainData trainData, int operationOut = 2 )
    {
        int count = 0;
        for ( int j = 1; j < trainData.Cars.Length + 1; j++ )
        {
            panel.CarPanels [j].CalcAndSetCarIcon (trainData.Cars [count]);
            count++;
        }
        panel.CarPanels [0].CalcAndSetOperationIcon (operationOut, trainData.TrainDirection);
    }

}
