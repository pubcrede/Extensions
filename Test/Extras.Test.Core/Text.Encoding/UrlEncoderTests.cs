//-----------------------------------------------------------------------
// <copyright file="UrlEncoderTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extras.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class UrlEncoderTests
    {
        [TestMethod()]
        public void Text_Encoding_UrlEncoder()
        {
            var testObject = new UrlEncoder("& and < and /");
            var result = testObject.Encode();
            Assert.IsTrue(result.Length > 0, "Item did not work.");
            Assert.IsTrue(result.Contains("&") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("<") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("/") == false, "Item did not work.");
        }
    }
}