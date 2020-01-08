using UnityEngine;

public class CouplerLever : MonoBehaviour
{

    public RSConnection RSConnection { get; private set; }

    private void Awake()
    {
        RSConnection = GetComponentInParent<RSConnection> ();
    }

    public void Uncouple()
    {
        RSConnection.DestroyConnection ();        
    }

    
}
