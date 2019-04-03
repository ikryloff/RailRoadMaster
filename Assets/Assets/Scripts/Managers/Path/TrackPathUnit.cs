using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPathUnit : MonoBehaviour {

    public int numOfBogeys = 0;
    public BGCcMath math;
    public List <BogeyPathScript> bogeys;
    public bool hasObjects;
    public TrackCircuit trackCircuit;
    public BogeyPathScript leftBogey;
    public BogeyPathScript rightBogey;

    private void Awake()
    {
        math = GetComponent<BGCcMath>();
        bogeys = new List<BogeyPathScript>();
    }

    private void Update()
    {
        CheckObjectAtPath();
        
    }

    public void CheckObjectAtPath()
    {
        if (bogeys.Count > 0)
        {
            hasObjects = true;            
        }
        else if (bogeys.Count == 0)
        {
            hasObjects = false;
        }
        // if num of bogeys changed, update order of bogeys
        if (bogeys.Count != numOfBogeys)
        {
            MakeOrderedBogeysList();
            numOfBogeys = bogeys.Count;
        }
            
    }

    //order list of bogeys in one math
    public void MakeOrderedBogeysList()
    {
        BogeyPathScript temp = null;
        if(bogeys.Count > 2)
        for (int i = 0; i < bogeys.Count - 1; i++)
        {
            for (int j = i + 1; j < bogeys.Count; j++)
            {
                if (bogeys[i].distance > bogeys[j].distance)
                {
                    temp = bogeys[i];
                    bogeys[i] = bogeys[j];
                    bogeys[j] = temp;
                }
            }
        }
        else if(bogeys.Count > 0)
        {
            leftBogey = bogeys.First();
            rightBogey = bogeys.Last();
        }
        
    }
}
