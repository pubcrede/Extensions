//-----------------------------------------------------------------------
// <copyright file="TextCleaningTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extensions;
using Genesys.Extras.Text.Cleansing;

namespace Genesys.Extras.Test
{
    [TestClass()]
    public class TextCleaningTests
    {
        private string safeTag1 = "Hello World";
        private string unsafeTag1 = "<script>function () { var unsafeX = 1;}</script>";
        private string unsafeTag2 = "<script type=\"text\\javascript/\">function () { var unsafeX = 2;}</script>";
        private string unsafeHtml { get { return string.Format("{0}{1}{2}", unsafeTag1, safeTag1, unsafeTag2); } }

        [TestMethod()]
        public void Text_Cleanser_HtmlUnsafe()
        {
            var safeHtml = TypeExtension.DefaultString;
            var cleanser = new HtmlUnsafeCleanser(unsafeHtml);
            safeHtml = cleanser.Cleanse();
            Assert.IsTrue(safeHtml.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(safeHtml.Contains(safeTag1) == true, "Did not work.");
        }

        [TestMethod()]
        public void Text_Cleanser_Attribute()
        {
            var testItem = new CleanserAttributeTester() { CleanseMe = unsafeHtml };

            Cleanser.CleanseAll(testItem);

            Assert.IsTrue(testItem.CleanseMe.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(testItem.CleanseMe.Contains(safeTag1) == true, "Did not work.");
        }

        private class CleanserAttributeTester
        {
            [CleanseFor(CleanserIDs.UnsafeHtml)]
            public string CleanseMe { get; set; }
        }
    }
}