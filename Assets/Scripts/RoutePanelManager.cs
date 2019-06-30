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
    }

    public void Show( bool isVisible )
    {
        if ( isVisible )
            GameManager.Instance.PauseOn ();
        else
            GameManager.Instance.PauseOff ();
        routePanel.SetActive (isVisible);
        ResetInput ();
        UpdateButtonsStates ();
    }

    public void GetInput( int input )
    {
        if ( Count < 2 )
        {
            Input [Count] = input;
            Count++;
        }
    }

    public void ResetInput()
    {
        Count = 0;
        ResetButtonInput (Input [0]);
        ResetButtonInput (Input [1]);
        Input [0] = -1;
        Input [1] = -1;
    }

    public void SetRoute()
    {
        if ( ValidateInput () )
        {
            routeNumber = Input [0] * 10 + Input [1];
            if ( Route.Instance.CheckRoute (routeNumber) )
            {
                Route.Instance.MakeRoute (routeNumber, Input[0]);
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
                //may be player has changed order 
                if ( RouteDictionary.Instance.PanelRoutes [routeNumber].IsExist )
                {
                    DestroyRouteWithInputs (routeNumber);
                }
                else
                {
                    //may be player has changed order 
                    routeNumber = Input [1] * 10 + Input [0];
                    if ( RouteDictionary.Instance.PanelRoutes [routeNumber].IsExist )
                    {
                        DestroyRouteWithInputs (routeNumber);
                    }
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
        GetRouteButtonByNumber (Input [1]).SetRouteOff ();
    }

    private void ResetButtonInput( int input )
    {
        if ( input >= 0 )
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
