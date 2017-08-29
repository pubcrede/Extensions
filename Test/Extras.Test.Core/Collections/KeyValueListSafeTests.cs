//-----------------------------------------------------------------------
// <copyright file="KeyValueListTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Collections;
using Genesys.Extras.Text;
using Genesys.Extensions;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// KeyValueList Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListTests
    {
        [TestMethod()]
        public void Collections_KeyValueList_Construct()
        {
            var kvList = new KeyValueList<int, int>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Collections_KeyValueList_Add()
        {
            var kvList = new KeyValueList<int, int>();
            kvList.Add(new KeyValuePairSafe<int, int>(0,0));
            kvList.Add(new KeyValuePairSafe<int, int>(1,1));
            kvList.Add(new KeyValuePairSafe<int, int>(2,2));

            Assert.AreNotEqual(2, kvList.Count);
            Assert.AreEqual(3, kvList.Count);
        }

        [TestMethod()]
        public void Collections_KeyValueList_Remove()
        {
            var kvList = new KeyValueList<int, double>();
            kvList.Add(1, 100.00);
            kvList.Add(2, 200.00);
            kvList.Remove(1);
            Assert.IsTrue(kvList.Count == 1);
        }        

        [TestMethod()]
        public void Collections_KeyValueList_GetValue()
        {
            var kvList = new KeyValueList<int, double>();
            kvList.Add(1, 100.00);
            kvList.Add(2, 200.00);
            Assert.IsTrue(kvList.GetValue(2) == 200.00);
        }

        [TestMethod()]
        public void Collections_KeyValueList_ToString()
        {
            var kvList = new KeyValueList<int, StringMutable>();
            var delimiterLength = 0;
            var lengthExpected = 0;
            kvList.Add(1, "Value1");
            kvList.Add(2, "Value2");
            kvList.Add(3, "Value3");
            foreach(var item in kvList)
            {
                lengthExpected += item.ToString().Length + delimiterLength;
                delimiterLength = 2;
            }
            Assert.IsTrue(kvList.ToString("G").Length == lengthExpected);
        }
    }
}