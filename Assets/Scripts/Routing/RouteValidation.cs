using System.Collections;
using System.Collections.Generic;

public class RouteValidation
{

    private List<int> routeValidNums = new List<int> () { 61, 62, 63, 64, 65, 60, 16, 26, 36, 46, 56, 6, 17, 27, 37, 47, 57, 89, 71, 72, 73, 74, 75, 19, 29, 39, 49, 59, 98, 91, 92, 93, 94, 95 };
    public bool approved;

    public bool InputRouteNumIsValid( int routeNum )
    {
        return routeValidNums.Contains (routeNum);       
    }

    public bool InputRouteNumIsNotDouble( int routeNum )
    {
        return !RouteDictionary.Instance.PanelRoutes [routeNum].IsExist;
    }

    public bool InputRouteIsNotDangerouse( int routeNum )
    {
        approved = false;        
        if ( InputRouteNumIsValid(routeNum) && InputRouteNumIsNotDouble (routeNum) )
        {

            RouteItem routeItem = RouteDictionary.Instance.RouteDict [routeNum];
            if ( routeItem.IsShunting && routeItem.CheckShuntingRoute () )
                approved = true;
            else if ( !routeItem.IsShunting && routeItem.CheckTrainRoute () )
                approved = true;
            else
                approved = false;
        }       
               
        return approved;
    }


}
