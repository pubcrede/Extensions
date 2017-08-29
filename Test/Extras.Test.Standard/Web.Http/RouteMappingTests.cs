//-----------------------------------------------------------------------
// <copyright file="RouteMappingTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Web.Http;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class RouteMappingTests
    {
        [TestMethod()]
        public void Web_Http_RouteMapping()
        {
            var custom = new RouteMapping("CustomRoute" ,"/CustomPath", new { controller = "Custom", action = "ActionMethod" });
            Assert.IsTrue(custom.ToString() != string.Empty);
        }
    }
}