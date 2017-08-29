//-----------------------------------------------------------------------
// <copyright file="UrlInfoTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Genesys.Extras.Net;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class UrlInfoTests
    {
        [TestMethod()]
        public void Net_UrlInfo()
        {
            var TestItem = new UrlInfo("http://test");
            Assert.IsTrue(TestItem.ToString() == "http://test:80");
        }
        
        [TestMethod()]
        public void Net_UrlInfo_ToString()
        {
            var MyRoot = "http://testURL";
            var MyController = "MyController";
            var MyAction = "MyAction";
            var TestItem = new UrlInfo(MyRoot, MyController, MyAction);

            // Check formatting
            Assert.IsTrue(TestItem.ToString().ToLowerInvariant() == String.Format("{0}:80/{1}/{2}", MyRoot, MyController, MyAction).ToLowerInvariant());
        }
    }
}