namespace ExamineMd.Tests.UnitTests.Search
{
    using System;
    using System.Linq;
    using System.Text;

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
            var files = _markdownQuery.Files.GetAll();
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
            var files = _markdownQuery.Files.List(Path).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.Path.Value.EnsureForwardSlashes().StartsWith("/developers", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
        }

        /// <summary>
        /// Tests shows that files can be listed in the developers directory
        /// </summary>
        [Test]
        public void Can_List_All_Md_Files_On_At_Root()
        {
            //// Arrange
            const string Path = "~/";

            //// Act
            var files = _markdownQuery.Files.List(Path).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.Path.Value.EnsureForwardSlashes().StartsWith("/", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
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
            var files = _markdownQuery.Files.List(Path).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.Path.Value.EnsureForwardSlashes().StartsWith("/api", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
        }

        [Test]
        public void Can_Get_A_Md_File_By_Its_Url()
        {
            //// Arrange
            var searchUrl = "/developers/jquery-standards";

            //// Act
            var file = _markdownQuery.Files.GetByUrl(searchUrl);

            //// Assert
            Assert.NotNull(file, "File was null");
        }

        [Test]
        public void Can_Search_For_Md_Files()
        {

            var files = _markdownQuery.Files.Search("AngularJS");
            this.ShowFileStoreInfo(files);
        }

        [Test]
        public void Can_Search_For_Md_Files_With_Path()
        {
            var files = _markdownQuery.Files.Search("/provider", "api");
            this.ShowFileStoreInfo(files);
        }

        [Test]
        public void Can_Get_An_Md_File()
        {
            //// Arrange
            const string Path = "/developers";
            const string FileName = "merchello-history.md";

            //// Act
            var file = _markdownQuery.Files.Get(Path, FileName);

            //// Assert
            Assert.NotNull(file);
        }

        [Test]
        public void Can_List_Root_Files()
        {
            //// Arrange
            const int FileCount = 1;
            //// Act
            var files = _markdownQuery.Files.List("/");
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
        }

        [Test]
        public void Can_List_All_Files_Starting_At_Root()
        {
            //// Arrange
            const int FileCount = 48;
            //// Act
            var files = _markdownQuery.Files.List("/", true);
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
        }

        [Test]
        public void Can_List_Only_Api_Files()
        {
            //// Arrange
            const int FileCount = 2;
            //// Act
            var files = _markdownQuery.Files.List("/api");
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
            
        }

        [Test]
        public void Can_List_All_Files_Under_Api()
        {
            //// Arrange
            const int FileCount = 23;
            //// Act
            var files = _markdownQuery.Files.List("/api", true);
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());
        }

        [Test]
        public void Can_List_All_Paths()
        {
            //// Arrange
            const int FileCount = 10;

            //// Act
            var paths = _markdownQuery.Paths.GetAll();

            //// Assert
            Assert.IsTrue(paths.Any());
            Assert.AreEqual(FileCount, paths.Count());
        }

        [Test]
        public void Can_Find_File_By_Searchable_Path()
        {
            var file = _markdownQuery.Paths.GetByUrl("/api/merchellocontext/merchellocontext-current-cache");
            Assert.NotNull(file);

            Assert.IsTrue(file.IsDocument);
        }
    }
}