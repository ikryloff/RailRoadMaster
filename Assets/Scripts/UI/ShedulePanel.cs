using UnityEngine;

public class ShedulePanel : MonoBehaviour
{
    UIShedulePointer [] pointers;

    private void Awake()
    {
        pointers = FindObjectsOfType<UIShedulePointer> ();
        MakePontersArray ();
    }

    private void MakePontersArray()
    {
        UIShedulePointer temp;
        for ( int i = 0; i < pointers.Length; i++ )
        {
            for ( int j = 0; j < pointers.Length - 1; j++ )
            {
                if ( pointers [j + 1].PointerNumber < pointers [j].PointerNumber )
                {
                    temp = pointers [j];
                    pointers [j] = pointers [j + 1];
                    pointers [j + 1] = temp;
                }

            }
        }
    }


}
