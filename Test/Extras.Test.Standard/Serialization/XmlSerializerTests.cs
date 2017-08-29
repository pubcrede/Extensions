//-----------------------------------------------------------------------
// <copyright file="XMLSerializerTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extras.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class XmlSerializerTests
    {        
        public class MyClass
        {
            public static string XmlData = "<?xml version=\"1.0\"?>\r\n<MyClass xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <MyProperty>PropertyData</MyProperty>\r\n</MyClass>";
            public string MyProperty { get; set; } = "PropertyData";
        }

        [TestMethod()]
        public void Serialization_Xml_Serialize()
        {
            var serializer = new XmlSerializer<MyClass>();
            var testClass = new MyClass();
            var deserializedData = serializer.Serialize(testClass);
            Assert.IsTrue(deserializedData == MyClass.XmlData);
        }

        [TestMethod()]
        public void Serialization_Xml_Deserialize()
        {
            var serializer = new XmlSerializer<MyClass>();
            var serializedData = serializer.Deserialize(MyClass.XmlData);
            Assert.IsTrue(serializedData.MyProperty == new MyClass().MyProperty);
        }
    }
}