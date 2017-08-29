//-----------------------------------------------------------------------
// <copyright file="DoubleExtensionTests.cs" company="Genesys Source">
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
    public class DoubleExtensionTests
    {
        [TestMethod()]
        public void Double_ToDecimal()
        {
            double original = 10.00;
            decimal castedValue = TypeExtension.DefaultDecimal;
            castedValue = original.ToDecimal();
            Assert.IsTrue(castedValue == (decimal)original);
        }
    }
}