//-----------------------------------------------------------------------
// <copyright file="ContentTypesTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Net;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class ContentTypesTests
    {
        [TestMethod()]
        public void Net_Mime_ContentTypes()
        {
            // Structure of Http content types
            Assert.IsTrue(ContentTypes.Types.ApplicationUnknown == "application/unknown");

            var x = new ContentTypes();
            Assert.IsTrue(x.Count > 0);
        }
    }
}