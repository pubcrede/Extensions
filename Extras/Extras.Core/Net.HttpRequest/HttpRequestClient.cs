//-----------------------------------------------------------------------
// <copyright file="HttpRequestSender.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Genesys.Extensions;
using Genesys.Extras.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Communicates via GET, all transmissions String
    /// </summary>
    [CLSCompliant(true)]
    public abstract class HttpRequestClient : IDisposable
    {
        private bool disposed = false;
        private HttpClientBuilder clientValue = new HttpClientBuilder();
        private HttpResponseMessage responseValue = new HttpResponseMessage();

        /// <summary>
        /// Raw data received
        /// </summary>
        public string DataReceivedRaw { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// DataReceivedRaw value decrypted
        /// </summary>
        public string DataReceivedDecrypted { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// HttpClient for request
        /// </summary>
        public HttpClientBuilder Client { get { return clientValue; } protected set { clientValue = value; } }

        /// <summary>
        /// Response after request
        /// </summary>
        public HttpResponseMessage Response { get { return responseValue; } protected set { responseValue = value; } }

        /// <summary>
        /// Sets the HttpResponse completion option.
        ///  Default - Reading response header and content: HttpCompletionOption.ResponseContentRead
        ///  For reading response header, no content (i.e. Large files): HttpCompletionOption.ResponseHeadersRead
        /// </summary>
        public HttpCompletionOption CompletionOption { get; set; } = HttpCompletionOption.ResponseContentRead;

        /// <summary>
        /// Url of request
        /// </summary>
        public Uri Url { get; protected set; } = TypeExtension.DefaultUri;

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool SendPlainText { get; protected set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Specify if want to send plain text with no alterations
        /// </summary>
        public bool ThrowExceptionWithEmptyReponse { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// Encryptor if plain text is off
        /// </summary>
        public IEncryptor Encryptor { get; protected set; } = new CaesarEncryptor(); // Start with simple cross platform class, replace with encryption of choice

        /// <summary>
        /// Immutable
        /// </summary>
        protected internal HttpRequestClient() : base()
        {
            SendPlainText = true;
#if (DEBUG)
            ThrowExceptionWithEmptyReponse = true;
#endif
        }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(Uri url) : this() { Url = url; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(string url) : this(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestClient(Uri url, IEncryptor encrptor) : this(url) { Encryptor = Encryptor; }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH
        /// No trailing slash.
        /// </summary>
        /// <param name="protocol">Protocol for Url. I.e. http</param>
        /// <param name="serverName">Server name for Url. I.e. www.YourDomain.com</param>
        /// <param name="port">Port for Url. I.e. 80</param>
        /// <param name="applicationPath">Application path for Url. I.e. /Home/Index</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string protocol, string serverName, int port, string applicationPath)
        {
            this.Url = new UrlBuilder(protocol, serverName, port, applicationPath).Uri;
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH?Param1=Value1
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string urlNoQuerystring, List<KeyValuePair<string, string>> parametersAndValues)
        {
            var requestUri = new StringBuilder();

            requestUri.Append(urlNoQuerystring.RemoveLast("/"));
            if (parametersAndValues.Count > 0)
            {
                requestUri.Append("?" + Uri.EscapeDataString(parametersAndValues[0].Key) + "=" + Uri.EscapeDataString(parametersAndValues[0].Value));
                parametersAndValues.RemoveAt(0);
            }
            foreach (var Item in parametersAndValues)
            {
                requestUri.Append("&" + Uri.EscapeDataString(Item.Key) + "=" + Uri.EscapeDataString(Item.Value));
            }

            this.Url = new Uri(requestUri.ToString(), UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Constructor that formats the entire URL, complete with PROTOCOL://SERVER_NAME:PORT/APPLICATION_PATH/Parameter1/Parameter2/.../ParameterN/
        /// </summary>
        /// <param name="urlNoQuerystring">Url with everything but parameters and punctuation</param>
        /// <param name="parametersAndValues">Collection of parameters to add to Url</param>
        /// <returns>Constructed url</returns>
        public HttpRequestClient(string urlNoQuerystring, IEnumerable<string> parametersAndValues)
        {
            this.Url = new UrlBuilder(urlNoQuerystring, parametersAndValues).Uri;
        }

        /// <summary>
        /// Synchronously sends a GET request, Receives string response
        /// </summary>
        /// <returns>Result</returns>
        public abstract string Send();

        /// <summary>
        /// Asynchronously sends a GET request, Receives strongly typed response
        /// </summary>
        /// <returns></returns>
        public abstract Task<string> SendAsync();

        /// <summary>
        /// string format of the request Url
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Url.ToString();
        }

        /// <summary>
        /// Dispose this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Inheritance disposal
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free any other managed objects
                    clientValue.Dispose();
                    responseValue.Dispose();
                }

                // Free any unmanaged objects
                disposed = true;
            }
        }
    }
}
