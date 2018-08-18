using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

   
    private ShuntingScript script;
   
    void Start ()
    {
        script = gameObject.AddComponent<ShuntingScript>();
    }
   


}
