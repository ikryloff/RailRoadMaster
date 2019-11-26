﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class RoutePanelManager : MonoBehaviour, IHideable
{

    [SerializeField]
    private GameObject routePanel;
    public static int Count;
    public int [] Input { get; set; }
    private int routeNumber;
    private int routesCount;
    private int routeStringName;
    private List<int> routeList;
    RouteButton [] buttons;
    List<RouteButton> routeButtons;
    public RouteCancelButton [] routeCancelButtons;


    private void Awake()
    {
        Input = new int [2];
        buttons = routePanel.GetComponentsInChildren<RouteButton> ();
        SetRouteButtonsInArray ();
        routeList = new List<int>();
        EventManager.onTrackCircuitsStateChanged += UpdateButtonsStatesWithCarsCount;
        EventManager.onPathChanged += UpdateRouteCancelButtons;
    }

    private void UpdateRouteCancelButtons()
    {
        if ( routePanel.activeSelf )
        {
            routesCount = Route.Instance.Routes.Count;
            routeList = Route.Instance.Routes;            
            for ( int i = 0; i < routeCancelButtons.Length; i++ )
            {
                if ( routesCount > i )
                {
                    routeCancelButtons [i].gameObject.SetActive (true);
                    routeCancelButtons [i].RouteExistNumber = routeList [i];
                    routeCancelButtons [i].SetTextRouteNumber(RouteDictionary.Instance.PanelRoutes[routeList[i]].RouteStringName);
                }                    
                else
                    routeCancelButtons [i].gameObject.SetActive (false);
            }
        }
    }
    private void UpdateButtonsStatesWithCarsCount()
    {
        foreach ( RouteButton but in buttons )
        {
            but.UpdateButtonState ();            
        }
    }

    public void UpdateButtonsStates()
    {
        foreach ( RouteButton but in buttons )
        {
            but.UpdateButtonState ();
            but.RButton.interactable = true;
        }        
    }

    public void Show( bool isVisible )
    {
        routePanel.SetActive (isVisible);
        if ( isVisible )
        {           
            ResetInput ();
            UpdateRouteCancelButtons ();
        }   
       
    }

    public void GetInput( int input )
    {
        if ( Count < 2 )
        {
            Input [Count] = input;
            Count++;
        }
        else
            GetRouteButtonByNumber (input).SetRouteOff ();

    }

    public void ResetInput()
    {
        Count = 0;
        Input [0] = -1;
        Input [1] = -1;
        UpdateButtonsStates ();
    }

    public void SetRouteByNumber( int number )
    {
        if ( Route.Instance.CheckRoute (number) )
        {
            Route.Instance.MakeRoute (number);
        }
        else
            print ("Wrong shit!");
        ResetInput ();
        
    }
    public void CallOffRouteByNumber( int number )
    {
        if ( RouteDictionary.Instance.PanelRoutes [number].IsExist )
        {
            Route.Instance.DestroyRoute (number);
        }           
    }

    private void SetRoute()
    {
        if ( ValidateInput () )
        {
            routeNumber = Input [0] * 100 + Input [1];
            if ( Route.Instance.CheckRoute (routeNumber) )
            {
                Route.Instance.MakeRoute (routeNumber);               
            }
            else
                print ("Wrong shit!");
            ResetInput ();
        }
        else
            print ("Fuck your Input, Asshole");
    }

    
    private void DestroyRouteWithInputs( int route )
    {
        Route.Instance.DestroyRoute (route);
        GetRouteButtonByNumber (Input [0]).SetRouteOff ();       
    }

   
    private bool ValidateInput()
    {
        return Input [0] != -1 && Input [1] != -1;
    }

    public RouteButton GetRouteButtonByNumber( int num )
    {
        foreach ( RouteButton item in buttons )
        {
            if ( item.Number == num )
                return item;
        }
        return null;
    }

    private void SetRouteButtonsInArray()
    {
        routeButtons = new List<RouteButton> ();

        for ( int i = 0; i < buttons.Length; i++ )
        {
            routeButtons.Add (GetRouteButtonByNumber (buttons[i].Number));
        }
    }
}
