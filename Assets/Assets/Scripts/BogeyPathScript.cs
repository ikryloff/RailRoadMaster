using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using System.Linq;
using System.Threading;

public class BogeyPathScript : MonoBehaviour {  



    public List<BGCcMath> ownTrackPath;
    public List<BGCcMath> tempTrackPath;

    public BGCcMath mathTemp;
    public RollingStock rollingStock;
    Transform bogey;
    public float offset;
    public float distance;
    TrackPath trackPath;
    public SwitchManager switchManager;


    private void Awake()
    {
        trackPath = FindObjectOfType<TrackPath>();
        switchManager = FindObjectOfType<SwitchManager>();
    }

    private void Start()
    {
        rollingStock = GetComponentInParent<RollingStock>();
        distance = rollingStock.distance + offset;
        bogey = gameObject.transform;
        mathTemp = rollingStock.mathTemp;
        UpdatePath();
        
    }


    void Update()
    {
        distance += rollingStock.force;

                    
        if (mathTemp)
        {
            if (rollingStock.mathTemp == mathTemp && distance > 50 && mathTemp.GetDistance() - distance < 55)
            {
                distance = rollingStock.distance + offset;
            }
            Vector3 tangent;
            bogey.position = mathTemp.CalcPositionAndTangentByDistance(distance, out tangent);
            bogey.rotation = Quaternion.LookRotation(tangent);

            if (rollingStock.force > 0 && mathTemp.GetDistance() - distance < 0.1)
            {
                mathTemp = trackPath.GetNextTrack(mathTemp, ownTrackPath);
                if (mathTemp)
                    distance = 0;
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.Last();
                    distance = mathTemp.GetDistance();

                }
            }
            if (rollingStock.force < 0 && distance < 0.1)
            {
                mathTemp = trackPath.GetPrevTrack(mathTemp, ownTrackPath);
                if(mathTemp)
                    distance = mathTemp.GetDistance();
                else
                {
                    rollingStock.isMoving = 0;
                    mathTemp = ownTrackPath.First();
                    distance = 0;
                    
                }                    
            }                
        }
        else
        {
           rollingStock.isMoving = 0;
        }       
    }

    public void UpdatePath()
    {
        trackPath.GetTrackPath(this);                         
    }
       
}
