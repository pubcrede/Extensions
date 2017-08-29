//-----------------------------------------------------------------------
// <copyright file="FileSearcherTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Genesys.Extras.IO;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class FileSearcherTests
    {
        [TestMethod()]
        public void IO_FileSearcher()
        {
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = @"app.config";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 2);
            searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
        }
    }
}