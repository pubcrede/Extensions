//-----------------------------------------------------------------------
// <copyright file="DecimalExtensionTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extensions.Test
{
    [TestClass()]
    public class DecimalExtensionTests
    {
        [TestMethod()]
        public void Decimal_ToDouble()
        {
            decimal original = 10.00M;
            double castedValue = TypeExtension.DefaultDouble;
            castedValue = original.ToDouble();
            Assert.IsTrue(castedValue == (double)original);
        }

        [TestMethod()]
        public void Decimal_ToInt()
        {
            decimal original = 10.00M;
            var castedValue = TypeExtension.DefaultInteger;
            castedValue = original.ToInt();
            Assert.IsTrue(castedValue == (int)original);
        }

        [TestMethod()]
        public void Decimal_ToShort()
        {
            decimal original = 10.00M;
            short castedValue = TypeExtension.DefaultShort;
            castedValue = original.ToShort();
            Assert.IsTrue(castedValue == (short)original);
        }

        [TestMethod()]
        public void Decimal_ToLong()
        {
            decimal original = 10.00M;
            long castedValue = TypeExtension.DefaultLong;
            castedValue = original.ToLong();
            Assert.IsTrue(castedValue == (long)original);
        }
    }
}