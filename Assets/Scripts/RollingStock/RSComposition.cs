using UnityEngine;

public class RSComposition : MonoBehaviour, IManageable
{
    public int Number { get; set; }
    public int NumInComposition { get; set; }
    public RSConnection RSConnection { get; set; }
    public RollingStock RollingStock { get; private set; }
    public bool IsMainCar { get; set; }
    public Composition CarComposition { get; set; }

    public void Init()
    {
        RSConnection = GetComponent<RSConnection> ();
        RSConnection.RSComposition = this;
        RollingStock = gameObject.GetComponent<RollingStock> ();
        Number = RollingStock.Number;
        
    }

    public void OnStart() {  }
	
    public bool GetIsMainCar()
    {
        return (!RSConnection.LeftCar);
    }

    public int GetCompositionNumber()
    {
        return (CarComposition.Number);
    }
}
