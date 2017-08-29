//-----------------------------------------------------------------------
// <copyright file="AgeTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Genesys.Extras.Mathematics;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// Age Tests
    /// </summary>
    [TestClass()]
    public class AgeTests
    {
        [TestMethod()]
        public void Mathematics_Age()
        {
            var now = DateTime.UtcNow;
            var birthDate = new DateTime(1988, 05, 18);
            var age = new Age(birthDate);
            var diff = now.Subtract(birthDate);
            Assert.IsTrue(age.Days == diff.Days);
        }
    }
}