//-----------------------------------------------------------------------
// <copyright file="ObjectExtensionTests.cs" company="Genesys Source">
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
    /// Tests the extensions
    /// </summary>
    [TestClass()]
    public class ObjectExtensionTests
    {
        public interface IMyClass
        {
            string MyProperty { get; set; }
        }
        public class MyClass1 : IMyClass
        {
            public string MyProperty { get; set; } = "PropertyData1";
        }
        public class MyClass2 : IMyClass
        {
            public string MyProperty { get; set; } = "PropertyData2";
        }
        public class MyClass3
        {
            public string MyProperty { get; set; } = "PropertyData3";
        }

        /// <summary>
        /// Extensions_Object_CastSafe
        /// </summary>
        [TestMethod()]
        public void Object_CastSafe()
        {
            var testItem = new MyClass1();
            var compareItem = new MyClass3();
            Assert.IsTrue(testItem.CastSafe<MyClass3>().GetType() == compareItem.GetType());
        }

        /// <summary>
        /// Extensions_Object_CastSafe
        /// </summary>
        [TestMethod()]
        public void Object_CastOrFill()
        {
            var testItem = new MyClass1();
            var compareItem = new MyClass3();
            Assert.IsTrue(testItem.CastOrFill<MyClass3>().GetType() == compareItem.GetType());
        }

        /// <summary>
        /// Extensions_Object_Fill
        /// </summary>
        [TestMethod()]
        public void Object_Fill()
        {
            var testItem = new MyClass1();
            var fillItem1 = new MyClass2();
            var fillItem2 = new MyClass3();
            fillItem1.Fill(testItem);
            fillItem2.Fill(testItem);
            Assert.IsTrue(testItem.MyProperty == fillItem1.MyProperty);
            Assert.IsTrue(testItem.MyProperty == fillItem2.MyProperty);
            Assert.IsTrue(testItem.MyProperty != new MyClass2().MyProperty);
        }
    }
}