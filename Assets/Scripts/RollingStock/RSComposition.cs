using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSComposition : MonoBehaviour, IManageable
{
    public int CompositionNumber { get; set; }
    public RSConnection RSConnection { get; set; }


    public void Init()
    {
        EventManager.onCompositionChanged += UpdateCarComposition;
        RSConnection = GetComponent<RSConnection> ();
        RSConnection.RSComposition = this;
    }

    public void OnStart() {  }
	

    // Get this car position in path   
    public void UpdateCarComposition()
    {        
        // if car not connected from right
        if ( !RSConnection.IsConnectedRight )
        {
            // make new composition
            Composition composition = new Composition (CompositionManager.CompositionID);

            // add composition in Dict
            CompositionManager.CompositionsDict.Add (CompositionManager.CompositionID, composition);
            // add rs in composition 
            AddRSInComposition (this, composition, CompositionManager.CompositionID);
            // if there any connected to left cars
            if ( RSConnection.LeftCar )
            {
                // temp car
                RSConnection conLeft = RSConnection.LeftCar;
                // add rs in composition
                AddRSInComposition (conLeft.RSComposition, composition, CompositionManager.CompositionID);

                while ( conLeft.LeftCar != null )
                {
                    conLeft = conLeft.LeftCar;
                    AddRSInComposition (conLeft.RSComposition, composition, CompositionManager.CompositionID);

                }
            }
            //increase composition ID
            CompositionManager.CompositionID++;
            print (CompositionManager.CompositionID);
        }
    }

    private void AddRSInComposition( RSComposition rs, Composition composition, int compositionID )
    {
        composition.compositions.Add (rs);
        rs.CompositionNumber = compositionID;
    }
}
