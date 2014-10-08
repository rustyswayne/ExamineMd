namespace ExamineMd.Tests.UnitTests.Repository
{
    using System;
    using System.Configuration;
    using System.Linq;

    using ExamineMd.Persistence.Repositories;
    using ExamineMd.Tests.UnitTests.TestHelpers;

    using NUnit.Framework;

    using Umbraco.Core.IO;

    [TestFixture]
    public class RepositoryTests : ExamineTestBase
    {
        
        [Test]
        public void Can_Retrieve_A_List_Of_All_Markdown_files()
        {
            //// Arrange
            const int fileCount = 47;

            var repo = new MarkdownFileRepository();

            var files = repo.GetAllMarkdownFiles();

            Assert.IsTrue(files.Any(), "No files were found in the repository");
            Assert.AreEqual(fileCount, files.Count());

            Console.Write(files.First().Markdown());
        }
    }
}