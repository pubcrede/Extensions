//-----------------------------------------------------------------------
// <copyright file="DesEncryptorTests.cs" company="Genesys Source">
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
    public class DesEncryptorTests
    {
        [TestMethod()]
        public void Security_Cryptography_DesEncryptor()
        {
            var rawData = "Hello, I am raw";
            var encryptor = new DesEncryptor(rawData);
            var encrypted = encryptor.Encrypt(rawData);
            Assert.IsTrue(rawData != encrypted);
            Assert.IsTrue(encryptor.Decrypt(encrypted) == rawData);
        }        
    }
}