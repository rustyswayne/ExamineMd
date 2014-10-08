namespace ExamineMd.Unit.Tests.MarkdownSharp
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using ExamineMd.Unit.Tests.TestHelpers;

    using global::MarkdownSharp;

    using NUnit.Framework;

    [TestFixture]
    public class MarkdownTests : ExamineTestBase
    {
        private Markdown _markdownFormatter;

        [TestFixtureSetUp]
        public void Init()
        {
            _markdownFormatter = new Markdown();
        }

        /// <summary>
        /// Test verifies that a Markdown string can be converted to an IHtmlString
        /// </summary>
        [Test]
        public void Can_Transform_A_Markdown_Headline()
        {
            //// Arrange
            var h1 = "<h1>Headline 1</h1>";
            const string md = "# Headline 1";

            //// Act
            var converted = _markdownFormatter.Transform(md).Trim();

            //// Assert
            Console.Write(converted);
            Assert.AreEqual(h1, converted);
        }
    }
}