//-----------------------------------------------------------------------
// <copyright file="RGBByteInfoTests.cs" company="Genesys Source">
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
    /// RGBByteInfo Tests
    /// </summary>
    [TestClass()]
    public class RGBByteInfoTests
    {
        [TestMethod()]
        public void Media_RGBByteInfo()
        {
            var RGBByteObject = new RGBByteInfo();
            Assert.IsTrue(RGBByteObject.ToHex() == "#000000");
        }
    }
}