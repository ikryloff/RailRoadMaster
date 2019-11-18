using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : Singleton<GameTime>
{


    public IEnumerator WaitSomeSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

    }

}
