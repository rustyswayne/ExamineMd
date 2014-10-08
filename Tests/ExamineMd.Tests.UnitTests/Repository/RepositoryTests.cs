﻿namespace ExamineMd.Tests.UnitTests.Repository
{
    using System;
    using System.Linq;

    using ExamineMd.Persistence.Repositories;
    using ExamineMd.Services;
    using ExamineMd.Tests.UnitTests.TestHelpers;

    using NUnit.Framework;

    [TestFixture]
    public class RepositoryTests : ExamineTestBase
    {
        private readonly IMarkdownFileService _service = new MarkdownFileService();


        /// <summary>
        /// Test asserts that the file count of .md files in the test data store equals the file count returned from the repository
        /// </summary>
        [Test]
        public void Can_Retrieve_A_List_Of_All_Markdown_files()
        {
            //// Arrange
            const int FileCount = 47;

            //// Act
            var files = this._service.GetAllMarkdownFiles().ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert
            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(FileCount, files.Count());

        }

        /// <summary>
        /// Test shows that files can be found by name
        /// </summary>
        [Test]
        public void Can_Find_Files_By_Name()
        {
            //// Arrange
            const string NameFilter = "merchello";

            //// Act
            var files = this._service.Find(NameFilter).ToArray();
            this.ShowFileStoreInfo(files);

           //// Assert
           Assert.IsTrue(files.Any(), "No files found");
           Assert.IsFalse(files.Any(x => !x.FileName.StartsWith(NameFilter, StringComparison.InvariantCultureIgnoreCase)));
        }

        [Test]
        public void Can_Find_Files_By_Name_And_Path()
        {
            //// Arrange
            const string Path = "/developers";
            const string NameFilter = "merchello";

            //// Act
            var files = this._service.Find(Path, NameFilter).ToArray();
            this.ShowFileStoreInfo(files);

            //// Assert

            //// Assert
            Assert.IsTrue(files.Any(), "No files found");
            Assert.IsFalse(files.Any(x => !x.FileName.StartsWith(NameFilter, StringComparison.InvariantCultureIgnoreCase)), "File name failure");
            Assert.IsFalse(files.Any(x => !x.Path.StartsWith("developers", StringComparison.InvariantCultureIgnoreCase)), "Path failure");
        }
    }
}