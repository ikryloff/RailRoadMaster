using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : Singleton<Route> {

    [SerializeField]
    private Switch sw2_4;
    [SerializeField]
    private Switch sw6_8;
    [SerializeField]
    private Switch sw10;
    [SerializeField]
    private Switch sw12;
    [SerializeField]
    private Switch sw14;
    [SerializeField]
    private Switch sw16;
    [SerializeField]
    private Switch sw1_3;
    [SerializeField]
    private Switch sw5_17;
    [SerializeField]
    private Switch sw7_9;
    [SerializeField]    
    private Switch sw11;
    [SerializeField]
    private Switch sw13;
    [SerializeField]
    private Switch sw15;

    [SerializeField]
    private TrafficLights n5;
    [SerializeField]
    private TrafficLights m2;  
    ArrayList routes = new ArrayList();


    private void Make12to5()
    {
        sw12.directionTurn();
        sw12.SwitchLock = true;
        sw10.directionTurn();
        sw10.SwitchLock = true;
        sw2_4.directionStraight();
        sw2_4.SwitchLock = true;
        sw6_8.directionStraight();
        sw6_8.SwitchLock = true;
    }
    private void Destroy12to5()
    {
        sw12.SwitchLock = false;
        sw10.SwitchLock = false;
        sw2_4.SwitchLock = false;
        sw6_8.SwitchLock = false;
    }

    public void MakeRoute(string startRoute, string endRoute)
    {
        if (startRoute == n5.Name && endRoute == m2.Name)
        {
            m2.SetLightColor(0);
            n5.SetLightColor(3);
            Make12to5();            
        }

        if (startRoute == m2.Name && endRoute == n5.Name)
        {
            m2.SetLightColor(3);
            n5.SetLightColor(0);
            Make12to5();            
        }

    }

    public void DestroyRoute(string startRoute, string endRoute)
    {
        if (startRoute == n5.Name && endRoute == m2.Name)
        {
            m2.SetLightColor(0);
            n5.SetLightColor(0);
            Destroy12to5();
        }

        if (startRoute == m2.Name && endRoute == n5.Name)
        {
            m2.SetLightColor(0);
            n5.SetLightColor(0);
            Destroy12to5();
        }

    }

}
