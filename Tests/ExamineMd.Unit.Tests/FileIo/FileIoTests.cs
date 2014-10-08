namespace ExamineMd.Unit.Tests.FileIo
{
    using System;
    using System.Configuration;

    using NUnit.Framework;

    using Umbraco.Core.IO;

    [TestFixture]
    public class FileIoTests
    {
        [Test]
        public void Can_Find_Md_File_Directory()
        {
            //// Arrange
            var directory = IOHelper.MapPath(ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias]);
            Console.Write(directory);
        }
    }
}