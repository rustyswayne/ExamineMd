namespace ExamineMd.Tests.UnitTests.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Examine;

    using ExamineMd.Models;

    using NUnit.Framework;

    public class ExamineTestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            ExamineManager.Instance.IndexProviderCollection["ExamineMdIndexer"].RebuildIndex();
        }

        /// <summary>
        /// This is not a test, but used to show information about files in file store in the Console window
        /// </summary>
        protected void ShowFileStoreInfo(IEnumerable<IMdFile> files)
        {
            var mdFiles = files as IMdFile[] ?? files.ToArray();
            if (mdFiles.Any())
            {
                var first = mdFiles.First();
                Console.WriteLine(first.BodyHtml());    
            }

            foreach (var f in mdFiles)
                Console.WriteLine("{0} -> {1} = {2}", f.Path, f.Path.FileName, f.Title);
        }
    }
}