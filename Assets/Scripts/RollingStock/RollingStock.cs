using System.Collections.Generic;
using UnityEngine;

public class RollingStock : MovableObject
{
    public float PositionInPath { get; set; }


    [SerializeField]
    private TrackPathUnit thisRSTrack;
    public int FirstTrackIndex;
    public int LastTrackIndex;
    public int Number;
    public int BackSidePosition;
    public Engine Engine { get; set; }

    //Components
    public RSConnection RSConnection { get; set; }
    public RSComposition RSComposition { get; set; }
    public CarProperties CarProperties { get; set; }

    [SerializeField]
    public bool IsEngine { get; set; }
    public bool isDirectionChanged;
    public float movingSpeed;
    public float pathLength;
    private Bogey [] bogeys;
    public Bogey BogeyLeft { get; set; }
    public Bogey BogeyRight { get; set; }
    private Transform bogeyLeftTransform;
    private Transform bogeyRightTransform;

    public RSModel Model;

    Vector3 dir;
    float angle;

    public void Init()
    {
        OwnTransform = GetComponent<Transform>();        
        Engine = GetComponent<Engine> ();       
        Model = GetComponentInChildren<RSModel> ();
        RSConnection = GetComponent<RSConnection> ();
        RSComposition = GetComponent<RSComposition> ();
        CarProperties = GetComponent<CarProperties> ();
        
        if ( Engine )
        {
            OwnEngine = Engine;
            IsEngine = true;
        }
        
        RSConnection.Init ();
        RSComposition.Init ();
        // set bogeys to RS
        SetBogeys ();
    }



    public float GetPositionInPath()
    {
        if ( OwnPath != null )
        {
            float tempPosition = 0;
            foreach ( TrackPathUnit item in OwnPath )
            {
                if ( item == OwnTrack )
                {
                    tempPosition += OwnPosition;
                    return tempPosition;
                }
                else
                    tempPosition += item.PathLenght;
            }
        }
        return -1;
    }

    private void SetBogeys()
    {
        bogeys = GetComponentsInChildren<Bogey> ();
        BogeyLeft = bogeys [0].transform.position.x < bogeys [1].transform.position.x ? bogeys [0] : bogeys [1];
        BogeyRight = BogeyLeft == bogeys [0] ? bogeys [1] : bogeys [0];
        bogeyLeftTransform = BogeyLeft.GetComponent<Transform> ();
        bogeyRightTransform = BogeyRight.GetComponent<Transform> ();        
    }

    public override void MoveAndRotate()
    {
        OwnTransform.position = OwnTrack.trackMath.CalcPositionByDistance (OwnPosition);
        dir = bogeyRightTransform.position - bogeyLeftTransform.position;
        angle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
        OwnTransform.rotation = Quaternion.Euler (0, angle, 0);
        OwnTransform.rotation *= Quaternion.Euler (0, -90, 0);
    }

    public void SetPathToRS( TrackPathUnit[] path , int fTrack, int lTrack)
    {
        OwnPath = path;
        BogeyLeft.OwnPath = path;
        BogeyRight.OwnPath = path;
        FirstTrackIndex = fTrack;
        LastTrackIndex = lTrack;
    }

    public void ResetBogeys()
    {
        BogeyLeft.ResetBogey ();
        BogeyRight.ResetBogey ();
    }

    public void SetEngineToRS( Engine engine )
    {
        OwnEngine = engine;
        BogeyLeft.OwnEngine = engine;
        BogeyRight.OwnEngine = engine;
    }

    public bool GetCoupledLeft()
    {
        return RSConnection.LeftCar;
    }

    public bool GetCoupledRight()
    {
        return RSConnection.RightCar;
    }

    public float GetPositionX()
    {
        return OwnTransform.position.x;
    }


}