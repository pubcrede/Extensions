//-----------------------------------------------------------------------
// <copyright file="KeyValuePairSafeTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Collections;
using Genesys.Extras.Text;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class KeyValuePairSafeTests
    {
        [TestMethod()]
        public void Collections_KeyValuePairSafe()
        {
            var kvp = new KeyValuePairSafe<int, int>(1,1);
            kvp.Key = 1;
            kvp.Value = 1;           
            Assert.AreEqual(1, kvp.Key);
            var kvp1 = new KeyValuePairSafe<int, StringMutable>(1, "1");
            Assert.AreEqual(1, kvp1.Key);
        }
    }
}