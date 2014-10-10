namespace ExamineMd.Tests.UnitTests.Search
{
    using System;
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
            const int FileCount = 48;
            //// Act
            var files = _markdownQuery.GetAll();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
        }

        /// <summary>
        /// Tests shows that files can be listed in the developers directory
        /// </summary>
        [Test]
        public void Can_List_All_Md_Files_On_A_Specific_Path()
        {
            //// Arrange
            const string Path = "~/developers";

            //// Act
            var files = _markdownQuery.List(Path).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.Path.StartsWith("developers", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
        }

        /// <summary>
        /// Tests shows that a files can be listed in the api directory and from all child directores
        /// </summary>
        [Test]
        public void Can_List_All_Md_Files_On_A_Specific_Path_And_All_Child_Directories()
        {
            //// Arrange
            const string Path = "~/api/basket";

            //// Act
            var files = _markdownQuery.List(Path).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.Path.StartsWith("api", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
        }

        [Test]
        public void Can_Search_For_Md_Files()
        {

            var files = _markdownQuery.Search("AngularJS");
            this.ShowFileStoreInfo(files);
        }

        [Test]
        public void Can_Search_For_Md_Files_With_Path()
        {
            var files = _markdownQuery.Search("provider", "api");
            this.ShowFileStoreInfo(files);
        }

        [Test]
        public void Can_Get_An_Md_File()
        {
            //// Arrange
            const string Path = "developers";
            const string FileName = "merchello-history.md";

            //// Act
            var file = _markdownQuery.Get(Path, FileName);

            //// Assert
            Assert.NotNull(file);
        }
    }
}