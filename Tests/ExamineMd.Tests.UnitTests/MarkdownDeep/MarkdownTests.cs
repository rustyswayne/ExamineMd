namespace ExamineMd.Tests.UnitTests.MarkdownSharp
{
    using System;
    using System.Linq;

    using ExamineMd.Services;
    using ExamineMd.Tests.UnitTests.TestHelpers;

    using MarkdownDeep;

    using NUnit.Framework;

    [TestFixture]
    public class MarkdownTests : ExamineTestBase
    {
        private Markdown _markdownFormatter;

        [TestFixtureSetUp]
        public void Init()
        {
            this._markdownFormatter = new Markdown();
        }

        /// <summary>
        /// Test verifies that a MarkdownAsHtmlString string can be converted to an IHtmlString
        /// </summary>
        [Test]
        public void Can_Transform_A_Markdown_Headline()
        {
            //// Arrange
            var h1 = "<h1>Headline 1</h1>";
            const string md = "# Headline 1";

            //// Act
            var converted = this._markdownFormatter.Transform(md).Trim();

            //// Assert
            Console.Write(converted);
            Assert.AreEqual(h1, converted);
        }

        //// used to test event trigger to override link definitions
        //[Test]
        public void Can_Get_Link_Definitions()
        {
            var service = new MarkdownFileService();

            var MarkdownFormatter   = new Markdown()
                                        {
                                            ExtraMode = true,
                                            SafeMode = false
                                        };

            var files = service.Find("index").ToArray();

            Assert.IsTrue(files.Any());

            this.ShowFileStoreInfo(files);

            
        }
    }
}