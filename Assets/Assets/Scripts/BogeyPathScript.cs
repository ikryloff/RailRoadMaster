using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;
using System.Threading;

public class BogeyPathScript : MonoBehaviour {  



    public List<TrackPathUnit> ownTrackPath;

    public TrackPathUnit mathTemp;
    public RollingStock rollingStock;
    Transform bogey;
    public float offset;
    public float distance;
    TrackPath trackPath;
    public SwitchManager switchManager;
    public TrackCircuit trackCircuit;
    public bool isRightBogey;
    // wihich is of 2 bogeys (-1 = left; +1 = right)
    public int bogeyPos;
    public BogeyPathScript otherBogey; 

    private void Awake()
    {
        trackPath = FindObjectOfType<TrackPath>();
        switchManager = FindObjectOfType<SwitchManager>();
        rollingStock = GetComponentInParent<RollingStock>();
        bogeyPos = offset > 0 ? 1 : -1;
        isRightBogey = offset > 0 ? true : false;
        //find other bogey

    }

    private void Start()
    {

        foreach (BogeyPathScript item in rollingStock.bogeys)
        {
            if (item != this)
                otherBogey = item;
        }
        distance = rollingStock.distance + offset;
        bogey = gameObject.transform;
        mathTemp = rollingStock.mathTemp;
        mathTemp.bogeys.Add(this);
        trackCircuit = mathTemp.trackCircuit;
        UpdatePath();
        
    }


    void Update()
    {
        distance += rollingStock.force;       

        if (mathTemp)
        {
            if (rollingStock.mathTemp == mathTemp && distance > 50 && mathTemp.math.GetDistance() - distance < 55)
            {
                distance = rollingStock.distance + offset;
            }
            Vector3 tangent;
            bogey.position = mathTemp.math.CalcPositionAndTangentByDistance(distance, out tangent);
            bogey.rotation = Quaternion.LookRotation(tangent);             
            
            if (rollingStock.force > 0 && mathTemp.math.GetDistance() - distance < 0.1)
            {
                mathTemp.bogeys.Remove(this);
                mathTemp = trackPath.GetNextTrack(mathTemp, ownTrackPath);                                           
                
                if (mathTemp)
                {                   
                    distance = 0;                    
                }                    
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.Last();
                    distance = mathTemp.math.GetDistance();

                }
            }
            if (rollingStock.force < 0 && distance < 0.1)
            {
                mathTemp.bogeys.Remove(this);
                mathTemp = trackPath.GetPrevTrack(mathTemp, ownTrackPath);               
                
                if (mathTemp)
                {                    
                    distance = mathTemp.math.GetDistance();                    
                }
                    
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.First();
                    distance = 0;
                    
                }                    
            }            
            trackCircuit = mathTemp.trackCircuit;
            if(!mathTemp.bogeys.Contains(this))
                mathTemp.bogeys.Add(this);
        }
        else
        {
           rollingStock.isMoving = 0;
        }       
    }

    public void UpdatePath()
    {
        // getPath for only one bogey
        if (rollingStock.force >= 0 && bogeyPos == 1 || rollingStock.force < 0 && bogeyPos == -1)
            trackPath.GetTrackPath(this);
        else
            trackPath.GetTrackPath(otherBogey);
    }  
   
       
}
