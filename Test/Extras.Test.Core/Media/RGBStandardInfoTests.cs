//-----------------------------------------------------------------------
// <copyright file="RGBStandardInfoTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Media;

namespace Genesys.Extras.Test
{
    /// <summary>
    /// RGBStandardInfo Tests
    /// </summary>
    [TestClass()]
    public class RGBStandardInfoTests
    {
        [TestMethod()]
        public void Media_RGBStandardInfo()
        {
            var RGBStandardObject = new RGBStandardInfo();
            Assert.IsTrue(RGBStandardObject.Inverse().Red == 1);
        }
    }
}