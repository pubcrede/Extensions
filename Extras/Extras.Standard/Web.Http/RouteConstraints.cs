//-----------------------------------------------------------------------
// <copyright file="NoParametersRouteConstraint.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web;
using System.Web.Routing;
using Genesys.Extensions;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Restricts http verbs to route that has the specified parameter (like id) in the route
    /// Usage: public static void RegisterRoutes(RouteCollection routes)
    ///         {routes.MapHttpRoute("DefaultApi","api/{controller}/{id}",
    ///             new { id = RouteParameter.Optional},
    ///             new { id = new NoParameterRouteConstraint() } );}
    /// </summary>
    public class ParameterNotAllowed : IRouteConstraint
    {
        string httpMethod = TypeExtension.DefaultString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method"></param>
        public ParameterNotAllowed(string method)
        {
            httpMethod = method;
        }

        /// <summary>
        /// Match criteria
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        /// <param name="routeName">Route name</param>
        /// <param name="parameterName">ParameterName, typically ID</param>
        /// <param name="valueList">List of dictionary values</param>
        /// <param name="routeDirection">Direction of the route</param>
        /// <returns></returns>
        public bool Match(HttpContextBase httpContext, Route routeName, string parameterName, RouteValueDictionary valueList, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest && httpContext.Request.HttpMethod == httpMethod && valueList[parameterName] != null)
            {
                return false;
            }

            return true;
        }
    }
}
