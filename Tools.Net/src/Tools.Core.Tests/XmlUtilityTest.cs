using Tools.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Microsoft.Pex.Framework;
namespace Tools.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for XmlUtilityTest and is intended
    ///to contain all XmlUtilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public partial class XmlUtilityTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            //Debugger.Launch();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Encode
        ///</summary>
        [TestMethod()]
        public void EncodeTest()
        {
            Assert.AreEqual<string>("&#xA;", XmlUtility.Encode('\n'));
            Assert.AreEqual<string>("&#xD;", XmlUtility.Encode('\r'));
            Assert.AreEqual<string>("&amp;", XmlUtility.Encode('&'));
            Assert.AreEqual<string>("&apos;", XmlUtility.Encode('\''));
            Assert.AreEqual<string>("&quot;", XmlUtility.Encode('"'));
            Assert.AreEqual<string>("&lt;", XmlUtility.Encode('<'));
            //Assert.AreEqual<string>("&gt;", XmlUtility.Encode('>'));
            Assert.AreEqual<string>("a", XmlUtility.Encode('a'));
        }
        [PexMethod()]
        public void EncodeTest(char input)
        {
            if (input == '\n') { Assert.AreEqual<string>("&#xA;", XmlUtility.Encode(input)); return; }
            if (input == '\r') { Assert.AreEqual<string>("&#xD;", XmlUtility.Encode(input)); return; }
            if (input == '&') { Assert.AreEqual<string>("&amp;", XmlUtility.Encode(input)); return; }
            if (input == '\'') { Assert.AreEqual<string>("&apos;", XmlUtility.Encode(input)); return; }
            if (input == '"') { Assert.AreEqual<string>("&quot;", XmlUtility.Encode(input)); return; }
            if (input == '<') { Assert.AreEqual<string>("&lt;", XmlUtility.Encode(input)); return; }
            if (input == '>') { Assert.AreEqual<string>("&gt;", XmlUtility.Encode(input)); return; }
            // Everything else should not be encoded
            Assert.AreEqual<string>(new string(input, 1), XmlUtility.Encode(input));
        }
    }
}
