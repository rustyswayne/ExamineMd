﻿namespace ExamineMd.Tests.UnitTests.Helpers
{
    using ExamineMd.Search;
    using ExamineMd.Tests.UnitTests.TestHelpers;

    using NUnit.Framework;

    [TestFixture]
    public class PathHelperTests : ExamineTestBase
    {
        [Test]
        public void Can_Get_The_Virtual_Level()
        {
            //// Arrage
            var query = new MarkdownQuery();
            const int Expected = 3;

            //// Act

            // the url nees to be /api/merchellocontext/merchellocontext-current-cache - but the query object
            // needs to remove it
            var file = query.Files.GetByUrl("/api/merchellocontext/merchellocontext-current-cache");
            Assert.NotNull(file, "File was null");
            var calc = file.GetContentLevel(1);

            //// Assert
            Assert.AreEqual(Expected, calc);

        }
    }
}