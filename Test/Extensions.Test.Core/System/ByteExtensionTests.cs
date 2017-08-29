//-----------------------------------------------------------------------
// <copyright file="ByteExtensionTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extensions.Test
{
    /// <summary>
    /// ByteExtension Tests
    /// </summary>
    [TestClass()]
    public class ByteExtensionTests
    {
        [TestMethod()]
        public void Byte_ToString()
        {
            byte[] bytes = { 0, 0, 0, 25 };
            Assert.IsTrue(bytes.ToString().Length > 0);
        }
    }
}