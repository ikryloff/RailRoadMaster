using System;
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
    RouteUnit unit;
    List<RouteButton> routeButtons;
    public RouteCancelButton [] routeCancelButtons;


    private void Awake()
    {
        Input = new int [2];
        buttons = routePanel.GetComponentsInChildren<RouteButton> ();
        SetRouteButtonsInArray ();
        routeList = new List<int>();
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
                    unit = RouteDictionary.Instance.PanelRoutes [routeList [i]];
                    routeCancelButtons [i].gameObject.SetActive (true);
                    routeCancelButtons [i].RouteExistNumber = routeList [i];
                    routeCancelButtons [i].SetTextRouteNumber (unit.RouteStringName);
                    CancelButtonInteraction (unit, i);
                }
                else
                    routeCancelButtons [i].gameObject.SetActive (false);
            }
        }
    }

    private void CancelButtonInteraction( RouteUnit _unit, int count )
    {
        if ( _unit.IsInUse )
            routeCancelButtons [count].CancelRouteButton.interactable = false;
        else
            routeCancelButtons [count].CancelRouteButton.interactable = true;
    }

   

    public void UpdateButtonsStates()
    {
        foreach ( RouteButton but in buttons )
        {
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
        if (Count == 2)
        {
            routeNumber = Input [0] * 100 + Input [1];
            SetRouteByNumber (routeNumber);
        }
            

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



