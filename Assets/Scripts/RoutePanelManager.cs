using System.Collections.Generic;
using UnityEngine;

public class RoutePanelManager : MonoBehaviour, IHideable
{

    [SerializeField]
    private GameObject routePanel;
    public static int Count;
    public int [] Input { get; set; }
    private int routeNumber;
    RouteButton [] buttons;
    List<RouteButton> routeButtons;


    private void Awake()
    {
        Input = new int [2];
        buttons = routePanel.GetComponentsInChildren<RouteButton> ();
        SetRouteButtonsInArray ();
    }

    public void UpdateButtonsStates()
    {
        foreach ( RouteButton but in buttons )
        {
            but.UpdateButtonState ();
        }
        foreach ( int r in Route.Instance.Routes )
        {
            RouteDictionary.Instance.PanelRoutes [r].Show (true);
        }

    }

    public void Show( bool isVisible )
    {
        routePanel.SetActive (isVisible);
        if ( isVisible )
        {
            GameManager.Instance.PauseOn ();
            Invoke ("UpdateButtonsStates", 0.1f);
            ResetInput ();

        }
        else
            GameManager.Instance.PauseOff ();

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
        ResetButtonInput (Input [0]);
        ResetButtonInput (Input [1]);
        Input [0] = -1;
        Input [1] = -1;
        UpdateButtonsStates ();
    }

    public void SetRouteByNumber( int number, int but = -1 )
    {
        if ( Route.Instance.CheckRoute (number) )
        {
            Route.Instance.MakeRoute (number, but);
            if ( but != -1 )
                GetRouteButtonByNumber (but).SetInRouteShuntingFirst ();
        }
        else
            print ("Wrong shit!");
        ResetInput ();
        
    }
    public void CallOffRouteByNumber( int number, int but = -1 )
    {
        if ( RouteDictionary.Instance.PanelRoutes [number].IsExist )
        {
            Route.Instance.DestroyRoute (number);
            if ( but != -1 )
                GetRouteButtonByNumber (but).SetRouteOff ();
            ResetInput ();
        }           
    }

    private void SetRoute()
    {
        if ( ValidateInput () )
        {
            routeNumber = Input [0] * 10 + Input [1];
            if ( Route.Instance.CheckRoute (routeNumber) )
            {
                Route.Instance.MakeRoute (routeNumber, Input [0]);
                GetRouteButtonByNumber (Input [0]).SetInRouteShuntingFirst ();
            }
            else
                print ("Wrong shit!");
            ResetInput ();
        }
        else
            print ("Fuck your Input, Asshole");
    }

    public void CallOffRoute()
    {
        if ( ValidateInput () )
        {
            routeNumber = Input [0] * 10 + Input [1];
            if ( Route.Instance.Validate (routeNumber) )
            {
                if ( RouteDictionary.Instance.PanelRoutes [routeNumber].IsExist )
                {
                    DestroyRouteWithInputs (routeNumber);
                } 
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

    private void ResetButtonInput( int input )
    {
        if ( input >= 0 && input < 10 )
        {
            if ( !GetRouteButtonByNumber (input).IsStartsRoute )
            {
                GetRouteButtonByNumber (input).SetRouteOff ();
            }
            else
                GetRouteButtonByNumber (input).SetInRouteImage ();
        }
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
            routeButtons.Add (GetRouteButtonByNumber (i));
        }
    }
}
