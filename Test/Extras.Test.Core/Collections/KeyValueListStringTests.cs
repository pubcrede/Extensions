//-----------------------------------------------------------------------
// <copyright file="KeyValueListStringTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Collections;
using Genesys.Extensions;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// KeyValueListString Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListStringTests
    {
        [TestMethod()]
        public void Collections_KeyValueListString_Construct()
        {
            var kvList = new KeyValueListString();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Collections_KeyValueListString_Add()
        {
            var kvString = new KeyValueListString();
            kvString.Add("TestKey", "TestValue");
            Assert.AreEqual(1, kvString.Count);
            kvString.Add("TestKey", "TestValue2");
            Assert.AreNotEqual(2, kvString.Count);
        }

        [TestMethod()]
        public void Collections_ListSafe_Remove()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            kvList.Remove("Key1");
            Assert.IsTrue(kvList.Count == 1);
        }

        [TestMethod()]
        public void Collections_ListSafe_GetValue()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            Assert.IsTrue(kvList.GetValue("Key1") != TypeExtension.DefaultString);
        }
    }
}   