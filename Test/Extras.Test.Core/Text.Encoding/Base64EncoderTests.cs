//-----------------------------------------------------------------------
// <copyright file="TemplateBuilderTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Extras.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class Base64EncoderTests
    {
        [TestMethod()]
        public void Text_Encoding_Base64Encoder()
        {
            var rawValue = "Raw data value";
            var encodedValue = TypeExtension.DefaultString;

            var encoder = new Base64Encoder(rawValue);
            encodedValue = encoder.Encode();
            encoder = new Base64Encoder(encodedValue);
            Assert.IsTrue(encoder.Decode() == rawValue, "Did not work.");
        }
    }
}