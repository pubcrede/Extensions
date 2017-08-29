//-----------------------------------------------------------------------
// <copyright file="CaesarEncryptorTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Security.Cryptography;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class CaesarEncryptorTests
    {
        [TestMethod()]
        public void Security_Cryptography_CaesarEncryptor()
        {
            var TestItem = new CaesarEncryptor();
            var encryptedString = TestItem.Encrypt("Test");
            Assert.IsTrue(encryptedString == "bQB%2BAIwAjQA%3D");
            var decryptedString = TestItem.Decrypt(encryptedString);
            Assert.IsTrue(decryptedString == "Test");
        }                
    }
}