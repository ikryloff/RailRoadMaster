using System.Collections;
using System.Collections.Generic;

public class RouteValidation
{

    private List<int> routeValidNums = new List<int> ()
    {
        1611, 1612, 1613, 1614, 1615, 1610, 1116, 1216,
        1316, 1416, 1516, 1016, 1117, 1217, 1317, 1417,
        1517, 1819, 1711, 1712, 1713, 1714, 1715, 1119,
        1219, 1319, 1419, 1519, 1918, 1911, 1912, 1913,
        1914, 1915, 1351, 1152, 1151, 1250, 1251, 1252,
        1352, 1452, 1451, 1552, 1551, 5211, 5212, 1351,
        5213, 5214, 5215, 5111, 5112, 5113, 5114, 5115
    };

    private bool approved;

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
