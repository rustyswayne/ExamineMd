namespace ExamineMd.Tests.UnitTests.TestHelpers
{
    using Examine;

    using NUnit.Framework;

    public class ExamineTestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            ExamineManager.Instance.IndexProviderCollection["ExamineMdIndexer"].RebuildIndex();
        }
    }
}