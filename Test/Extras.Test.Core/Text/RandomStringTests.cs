//-----------------------------------------------------------------------
// <copyright file="RandomStringTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extras.Text;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class RandomStringTests
    {
        [TestMethod()]
        public void Text_RandomString_Next()
        {
            Assert.IsTrue(RandomString.Next().Length == 10);
        }
    }
}