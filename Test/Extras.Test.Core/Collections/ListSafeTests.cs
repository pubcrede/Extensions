//-----------------------------------------------------------------------
// <copyright file="ListSafeTests.cs" company="Genesys Source">
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
    /// ListSafe Tests
    /// </summary>
    [TestClass()]
    public class ListSafeTests
    {
        [TestMethod()]
        public void Collections_ListSafe_Construct()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Collections_ListSafe_Add()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            Assert.AreEqual(1, kvList.Count);
        }

        [TestMethod()]
        public void Collections_ListSafe_Remove()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            kvList.Remove("TestKey1");
            Assert.IsTrue(kvList.Count == 1);
        }

        [TestMethod()]
        public void Collections_ListSafe_FindIndex()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.FindIndex("TestKey2") == 1);
        }

        [TestMethod()]
        public void Collections_ListSafe_GetValue()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.GetValue("TestKey2") != TypeExtension.DefaultString);
        }
    }
}