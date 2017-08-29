//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafeTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extensions;
using Genesys.Extras.Configuration;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public partial class ConfigurationManagerFullTests
    {
        [TestMethod()]
        public void Configuration_ConfigurationManagerFull_AppSettings()
        {
            var itemToTestString = TypeExtension.DefaultString;
            var itemToTest = new AppSettingSafe();
            var configuration = new ConfigurationManagerFull();
            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString);
            itemToTestString = configuration.AppSettings.GetValue("TestAppSetting");
            Assert.IsTrue(itemToTestString != TypeExtension.DefaultString);
        }

        [TestMethod()]
        public void Configuration_ConfigurationManagerFull_ConnectionStrings()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = new ConfigurationManagerFull();
            itemToTest = configuration.ConnectionString("TestEFConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString);
            Assert.IsTrue(itemToTest.IsEF);
            itemToTest = configuration.ConnectionString("TestADOConnection");
            Assert.IsTrue(itemToTest.Value != TypeExtension.DefaultString);
            Assert.IsTrue(itemToTest.IsADO);
        }        
    }
}