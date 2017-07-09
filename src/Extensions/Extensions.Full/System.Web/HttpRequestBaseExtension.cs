﻿//-----------------------------------------------------------------------
// <copyright file="HttpRequestBaseExtension.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Genesys.Extensions
{
    /// <summary>
    /// HttpRequestBaseExtension
    /// </summary>
    [CLSCompliant(true)]
    public static class HttpRequestBaseExtension
    {
        /// <summary>
        /// Finds the root of the url in format: http://SERVER_NAME:SERVER_PORT
        /// </summary>
        /// <param name="item">Request class of item that contains the Url</param>
        /// <returns>Url from server variables collection</returns>
        public static string TryParseUrl(this HttpRequestBase item)
        {
            return HttpRequestBaseExtension.ConstructUrl(item.ServerVariables["SERVER_PORT_SECURE"], item.ServerVariables["SERVER_NAME"], item.ServerVariables["SERVER_PORT"], item.ApplicationPath);
        }

        /// <summary>
        /// Checks for secure sockets, or returns true if localhost
        /// </summary>
        /// <param name="item">Request to check</param>
        /// <returns>True if request is secured, or is localhost</returns>
        public static bool IsSecured(this HttpRequestBase item)
        {
            return HttpRequestBaseExtension.IsSecured(item.IsSecureConnection, item.Url.ToString());
        }

        /// <summary>
        /// Checks for HTTPS, or returns true if localhost
        /// </summary>
        /// <param name="isSecureConnection">Returned from Request.IsSecured</param>
        /// <param name="url">Url to check</param>
        /// <returns>True if request is secured, or is localhost</returns>
        internal static bool IsSecured(Boolean isSecureConnection, string url)
        {
            var returnValue = TypeExtension.DefaultBoolean;

            if (isSecureConnection | url.ToString().Contains("://localhost"))
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// No trailing slash.
        /// </summary>
        /// <param name="protocol">Protocol for Url. I.e. http</param>
        /// <param name="serverName">Server name for Url. I.e. www.YourDomain.com</param>
        /// <param name="port">Port for Url. I.e. 80</param>
        /// <param name="applicationPath">Application path for Url. I.e. /Home/Index</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string protocol, string serverName, string port, string applicationPath)
        {
            var urlComplete = TypeExtension.DefaultString;

            if (protocol == null || protocol == "0")
            {
                protocol = "http://";
            } else
            {
                protocol = "https://";
            }
            if (port == null || port == "80" || port == "443")
            {
                port = "";
            } else
            {
                port = ":" + port;
            }
            urlComplete = protocol + serverName + port + applicationPath;
            urlComplete = urlComplete.RemoveLast("/");

            return urlComplete;
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH?Param1=Value1
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string urlNoQuerystring, List<KeyValuePair<string, string>> parametersAndValues)
        {
            var returnValue = new StringBuilder();

            returnValue.Append(urlNoQuerystring.RemoveLast("/"));
            if (parametersAndValues.Count > 0)
            {
                returnValue.Append("?" + Uri.EscapeDataString(parametersAndValues[0].Key) + "=" + Uri.EscapeDataString(parametersAndValues[0].Value));
                parametersAndValues.RemoveAt(0);
            }
            foreach (var Item in parametersAndValues)
            {
                returnValue.Append("&" + Uri.EscapeDataString(Item.Key) + "=" + Uri.EscapeDataString(Item.Value));
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public static string ConstructUrl(string urlNoQuerystring, List<string> parametersAndValues)
        {
            var returnValue = new StringBuilder();

            returnValue.Append(urlNoQuerystring.RemoveLast("/"));
            foreach (var item in parametersAndValues)
            {
                returnValue.Append("/" + Uri.EscapeDataString(item));
            }

            return returnValue.ToString();
        }
    }
}
