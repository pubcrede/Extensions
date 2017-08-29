//-----------------------------------------------------------------------
// <copyright file="IntegerExtensionTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class GuidExtensionTests
    {
        [TestMethod()]
        public void Guid_ToInteger()
        {
            var itemGuid = new Guid("00003039-0000-0000-0000-000000000000");
            var itemInt = 12345;

            Assert.IsTrue(itemGuid.ToInteger() == itemInt);
            Assert.IsTrue(itemInt.ToGuid() == itemGuid);
        }
    }
}