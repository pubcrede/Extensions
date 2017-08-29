//-----------------------------------------------------------------------
// <copyright file="DateRelativeTests.cs" company="Genesys Source">
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
    public class DateRelativeTests
    {
        [TestMethod()]
        public void Text_DateRelative_ToString()
        {
            var objDateRelative = new DateRelative(new System.DateTime(2000, 3, 6));
            Assert.IsTrue(objDateRelative.ToString() == "less than a minute ago");
        }
    }
}