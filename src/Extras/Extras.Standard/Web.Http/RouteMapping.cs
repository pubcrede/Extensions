//-----------------------------------------------------------------------
// <copyright file="RouteMapping.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Http;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// Purpose is to support Mvc and Web API route mapping
    /// Routes.MapRoute and Routes.MapHttpRoute
    /// </summary>
    public struct RouteMapping
    {
        /// <summary>
        /// asdf
        /// </summary>
        public struct Mappings
        {
            /// <summary>
            /// DefaulApi: api/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiDefault = new RouteMapping("DefaulApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            /// <summary>
            /// DefaulApiV1: v1/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiV1 = new RouteMapping("DefaulV1", "v1/{controller}/{id}", new { id = RouteParameter.Optional });

            /// <summary>
            /// DefaultV1Naked: v1/{controller}/{id}
            /// </summary>
            public static RouteMapping WebApiV1Naked = new RouteMapping("DefaultV1Naked", "v1", new { controller = "HomeApi", action = "Index" });

            /// <summary>
            /// DefaulApi: api/{controller}/{id}
            /// </summary>
            public static RouteMapping MvcDefault = new RouteMapping("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = RouteParameter.Optional });
        }

        /// <summary>
        /// Name of route
        /// Example: "DefaulApi"
        /// </summary>
        public string Name;

        /// <summary>
        /// Route template/mask
        /// Example: "api/{controller}/{id}"
        /// </summary>
        public string RouteTemplate;

        /// <summary>
        /// Default properties
        /// Example: new { id = RouteParameter.Optional })
        /// </summary>
        public object Defaults;

        /// <summary>
        /// Constructor that fully hydrates
        /// </summary>
        /// <param name="name">Name of route mapping: "DefaulApi"</param>
        /// <param name="routeTemplate">Route template/mask</param>
        /// <param name="defaults">Default properties: new { id = RouteParameter.Optional })</param>
        public RouteMapping(string name, string routeTemplate, object defaults)
        {
            this.Name = name;
            this.RouteTemplate = routeTemplate;
            this.Defaults = defaults;
        }
    }
}
