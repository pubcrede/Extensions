//-----------------------------------------------------------------------
// <copyright file="EnumExtensionTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extensions;
using System;

namespace Genesys.Extensions.Test
{
    [TestClass()] 
    public class EnumExtensionTests
    {
        [TestMethod()]
        public void Enum_Contains()
        {
            var consumer = new EnumConsumer();
            Assert.IsTrue(consumer.enumFlag.Contains(0x01) == true);
        }

        [TestMethod()]
        public void Enum_ToDictionary()
        {
            var dict = EnumConsumer.MyEnumInts.one.ToDictionary();
            Assert.IsTrue(dict.Count > 0 == true);
        }
        public class EnumConsumer
        {
            public enum MyEnumInts
            {
                one = 1,
                two = 2,
                three = 3
            }

            [Flags]
            public enum MyEnumFlags
            {
                one = 0x01,
                two = 0x02,
                four = 0x04,
                eight = 0x08
            }

            public MyEnumInts enumInt = MyEnumInts.one;
            public MyEnumFlags enumFlag = MyEnumFlags.one;
        }

    }
}