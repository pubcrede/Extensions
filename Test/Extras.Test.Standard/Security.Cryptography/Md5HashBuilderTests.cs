//-----------------------------------------------------------------------
// <copyright file="Md5HashBuilderTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extras.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class MD5HashBuilderTests
    {
        [TestMethod()]
        public void Security_Cryptography_MD5HashBuilder_Compare()
        {
            var rawData = "Hello, I am raw";
            var hasher = new Md5HashBuilder(rawData);
            var hashed = hasher.HashedString;
            Assert.IsTrue(rawData != hashed);
            Assert.IsTrue(hasher.Compare(rawData) == true);
        }
    }
}