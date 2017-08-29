//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensionTests.cs" company="Genesys Source">
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
    public class DateTimeExtensionTests
    {
        [TestMethod()]
        public void DateTime_ISO8601_FormatStrings()
        {
            DateTime defaultDateValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            DateTime defaultShort = TypeExtension.DefaultDate;
            Assert.IsTrue(defaultDateValue.Ticks == defaultShort.Ticks);
            Assert.IsTrue(defaultDateValue.ToString() == defaultShort.ToString());

            string ISO8601 = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601);
            string ISO8601F = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601F);
            Assert.IsTrue(ISO8601.TryParseDateTime().Ticks == ISO8601F.TryParseDateTime().Ticks);
            Assert.IsTrue(ISO8601.TryParseDateTime().ToString() == ISO8601F.TryParseDateTime().ToString());
        }

        [TestMethod()]
        public void DateTime_Tomorrow()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Tomorrow().Day == DateTime.Now.AddDays(1).Day);
        }

        [TestMethod()]
        public void DateTime_Yesterday()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Yesterday().Day == DateTime.Now.AddDays(-1).Day);
        }

        [TestMethod()]
        public void DateTime_FirstDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.FirstDayOfMonth().Day == 1);
        }

        [TestMethod()]
        public void DateTime_LastDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.LastDayOfMonth().Day == 31);
        }

        [TestMethod()]
        public void DateTime_IsSavable()
        {
            var date = new DateTime(1700, 1, 1);
            Assert.IsTrue(date.IsSavable() == false);
        }
    }
}