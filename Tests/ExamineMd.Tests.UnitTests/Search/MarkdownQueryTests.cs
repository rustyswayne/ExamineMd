namespace ExamineMd.Tests.UnitTests.Search
{
    using System.Linq;

    using ExamineMd.Search;
    using ExamineMd.Tests.UnitTests.TestHelpers;

    using NUnit.Framework;

    [TestFixture]
    public class MarkdownQueryTests : ExamineTestBase
    {
        private readonly IMarkdownQuery _markdownQuery = new MarkdownQuery();

        [Test]
        public void Can_Retrieve_A_List_Of_All_Docs()
        {
            //// Arrange
            const int FileCount = 47;
            //// Act
            var files = _markdownQuery.GetAll();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
        }
    }
}