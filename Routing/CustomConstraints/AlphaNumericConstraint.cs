using System.Text.RegularExpressions;

namespace Routing.CustomConstraints
{
    //step1 for custom route constraints
    public class AlphaNumericConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection
            )
        {
            if(!values.ContainsKey(routeKey))
            {
                return false;
            }
            Regex regex = new Regex("^[a-zA-Z0-9]$");
            string? usernmae = Convert.ToString(values[routeKey]);
            if(regex.IsMatch(usernmae))
            {
                return true;
            }
            return false;
        }
    }
    
}
