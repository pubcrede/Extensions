//-----------------------------------------------------------------------
// <copyright file="StringMutableTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Text;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class StringMutableTests
    {
        [TestMethod()]
        public void Text_StringMutable()
        {
            // Common
            var data = "test string";
            // Creation
            StringMutable item1 = data;
            Assert.IsTrue(item1 == data, "Failed. Should have same data.");
            // Identity
            StringMutable item2 = item1;
            StringMutable item3 = new StringMutable() { Value = data };
            Assert.IsTrue(item1 == item2, "Failed. Should be equivalent");
            Assert.IsTrue(item1 == item3, "Failed. Should be equivalent");
            // Equality
            item1 = data;
            item2 = data;
            item3 = new StringMutable() { Value = data };
            Assert.IsTrue(item1.Equals(item2), "Failed. Should be equivalent");
            Assert.IsTrue(item1.Equals(item3), "Failed. Should be equivalent");
        }
    }
}