//-----------------------------------------------------------------------
// <copyright file="HttpRequestDeleteTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Extras.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class HttpRequestDeleteTests
    {
        [TestMethod()]
        public async Task Net_HttpRequestDeleteString_SendAsync()
        {
            var dataOut = TypeExtension.DefaultString;
            var configuration = ConfigurationManagerSafeTests.ConfigurationManagerSafeConstruct();
            var request = new HttpRequestDeleteString(configuration.AppSettingValue(AppSettingList.MyWebServiceKeyName) + "/HomeApi");
            try
            {
                dataOut = await request.SendAsync();
                Assert.IsTrue(request.Response.IsSuccessStatusCode);
                throw new WebException();
            }
            catch (WebException)
            {
                Assert.IsTrue(dataOut != null);
            }
            finally
            {
                request.Dispose();
            }
        }

        [TestMethod()]
        public async Task Net_HttpRequestDelete_SendAsync()
        {
            object dataOut = null;
            var configuration = ConfigurationManagerSafeTests.ConfigurationManagerSafeConstruct();
            var request = new HttpRequestDelete<object>(configuration.AppSettingValue(AppSettingList.MyWebServiceKeyName) + "/HomeApi");
            try
            {
                dataOut = await request.SendAsync();
                Assert.IsTrue(request.Response.IsSuccessStatusCode);
            }
            catch (WebException)
            {
                Assert.IsTrue(dataOut != null);
            }
            finally
            {
                request.Dispose();
            }
        }
    }
}