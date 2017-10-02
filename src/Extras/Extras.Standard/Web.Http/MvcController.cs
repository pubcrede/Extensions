//-----------------------------------------------------------------------
// <copyright file="MvcController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using System.Threading.Tasks;
using Genesys.Extensions;

namespace Genesys.Extras.Web.Http
{
    /// <summary>
    /// WebAPI controller for any app services project for native app back ends
    /// </summary>
    public abstract class MvcController : Controller
    {
        /// <summary>
        /// Standard return message for default route of services
        /// </summary>
        public const string MessageUpAndRunning = "Services up and running...";

        /// <summary>
        /// Persistent ConfigurationManager class, automatically loaded with this project .config files
        /// </summary>
        public ConfigurationManagerFull ConfigurationManager = new ConfigurationManagerFull();

        /// <summary>
        /// Sender of main Http Verbs
        /// </summary>
        public HttpVerbSender HttpSender { get; set; } = new HttpVerbSender();

        /// <summary>
        /// Send is about to begin
        /// </summary>
        public event SendBeginEventHandler SendBegin;

        /// <summary>
        /// Send is complete
        /// </summary>
        public event SendBeginEventHandler SendEnd;

        /// <summary>
        /// About to send data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SendBeginEventHandler(object sender, EventArgs e);

        /// <summary>
        /// OnSendBegin()
        /// </summary>
        protected virtual void OnSendBegin() { if (SendBegin != null) { SendBegin(this, EventArgs.Empty); } }

        /// <summary>
        /// OnSendEnd()
        /// </summary>
        protected virtual void OnSendEnd() { if (SendEnd != null) { SendEnd(this, EventArgs.Empty); } }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        protected MvcController() : base() { }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataOut> SendGetAsync<TDataOut>(Uri fullUrl) where TDataOut : new()
        {
            return await HttpSender.SendGetAsync<TDataOut>(fullUrl);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataInOut> SendPostAsync<TDataInOut>(Uri fullUrl, TDataInOut itemToSend)
        {
            return await HttpSender.SendPostAsync<TDataInOut>(fullUrl, itemToSend);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TDataInOut> SendPutAsync<TDataInOut>(Uri fullUrl, TDataInOut itemToSend)
        {
            return await HttpSender.SendPutAsync<TDataInOut>(fullUrl, itemToSend);
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<bool> SendDeleteAsync(Uri fullUrl)
        {
            return await HttpSender.SendDeleteAsync(fullUrl);
        }
    }
}
