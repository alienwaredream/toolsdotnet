using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Tools.Core.Tests
{
    public partial class XmlUtilityTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_212944_000()
        {
            this.EncodeTest('\0');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_213857_002()
        {
            this.EncodeTest('\r');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_214008_001()
        {
            this.EncodeTest('\n');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_225019_002()
        {
            this.EncodeTest('\"');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_225020_005()
        {
            this.EncodeTest('&');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_225020_007()
        {
            this.EncodeTest('\'');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_230110_006()
        {
            this.EncodeTest('<');
        }

        [TestMethod]
        [PexGeneratedBy(typeof(XmlUtilityTest))]
        public void EncodeTestChar_20080531_235913_007()
        {
            this.EncodeTest('>');
        }

    }
}
