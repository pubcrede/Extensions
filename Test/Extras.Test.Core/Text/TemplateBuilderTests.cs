//-----------------------------------------------------------------------
// <copyright file="TemplateBuilderTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Extras.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class TemplateBuilderTests
    {
        [TestMethod()]
        public void Text_TemplateBuilder_ToString()
        {
            var template = "1: {0}, 2: {1}, 3: {2}";
            var result = TypeExtension.DefaultString;
            var data = new List<string>() { "FirstItem", "SecondItem", "ThirdItem" };
            var builder = new TemplateBuilder(template, data);
            result = builder.ToString();
            foreach(var item in data)
            {
                Assert.IsTrue(result.Contains(item) == true);
            }            
        }        
    }
}