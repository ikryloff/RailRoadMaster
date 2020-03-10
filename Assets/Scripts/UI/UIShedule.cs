using UnityEngine;

public class UIShedule : MonoBehaviour
{
    public float Position;
    public GameObject pref;
    public Transform parent;
    public UISheduleItem [] SheduleItems;
    public int counter;
    private int NUM_OF_ROWS = 24;


    private void Awake()
    {
        SheduleItems = new UISheduleItem [NUM_OF_ROWS];
        for ( int i = 0; i < NUM_OF_ROWS; i++ )
        {
            SheduleItems [i] = Instantiate (pref, parent).GetComponent<UISheduleItem> ();
            SheduleItems [i].ItemNumber = i;
            SheduleItems [i].OnAwake ();
            if(i >= 6 )
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

    public void MoveAutoPosition()
    {
        //TODO
    }



}
