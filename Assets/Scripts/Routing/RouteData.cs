using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RouteData", menuName = "RouteData", order = 51)]
public class RouteData : ScriptableObject
{
    [SerializeField]
    private string routeName;
    [SerializeField]
    private string[] switchesToStraight;
    [SerializeField]
    private string[] switchesToTurn;
    [SerializeField]
    private string[] trackCircuits;
    [SerializeField]
    private string[] routeLights;
    [SerializeField]
    private bool isShunting;
    [SerializeField]
    private bool isStraight;
    [SerializeField]
    private string dependsOnSignal;

    public string RouteName
    {
        get
        {
            return routeName;
        }       
    }

    public string[] SwitchesToStraight
    {
        get
        {
            return switchesToStraight;
        }
      
    }

    public string[] SwitchesToTurn
    {
        get
        {
            return switchesToTurn;
        }       
    }

    public string[] TrackCircuits
    {
        get
        {
            return trackCircuits;
        }

    }

    public string[] RouteLights
    {
        get
        {
            return routeLights;
        }
     
    }

    public bool IsShunting
    {
        get
        {
            return isShunting;
        }

        
    }

    public string DependsOnSignal
    {
        get
        {
            return dependsOnSignal;
        }

    }

    public bool IsStraight
    {
        get
        {
            return isStraight;
        }
        
    }
}
