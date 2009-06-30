using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace Tools.Zip.Tests
{

    using SharpZip = ICSharpCode.SharpZipLib;

    /// <summary>
    ///This is a test class for ZipFileTest and is intended
    ///to contain all ZipFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SharpZipFileTest
    {


        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ZipData\\test.txt")]
        public void AddTest()
        {
            string name = "ZipData\\test.txt";
            string outName = "ZipData\\test.zip";
            if (File.Exists(outName)) File.Delete(outName);

            using (SharpZip.Zip.ZipFile target = SharpZip.Zip.ZipFile.Create(outName))
            {

                string fileName = "ZipData\\test.txt";

                target.BeginUpdate();

                //ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);

                target.Add(name);

                target.CommitUpdate();
            }
        }


    }
}
