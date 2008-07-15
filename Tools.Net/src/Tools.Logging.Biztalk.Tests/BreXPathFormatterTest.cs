using Tools.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System;
using System.Xml.XPath;

namespace Tools.Logging.Biztalk.Tests
{
    
    
    /// <summary>
    ///This is a test class for BreXPathFormatterTest and is intended
    ///to contain all BreXPathFormatterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BreXPathFormatterTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for XmlEncode
        ///</summary>
        [TestMethod()]
        public void XmlEncodeTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = BreXPathFormatter.XmlEncode(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StackTraceString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.Biztalk.dll")]
        public void StackTraceStringTest()
        {
            BreXPathFormatter_Accessor target = new BreXPathFormatter_Accessor(); // TODO: Initialize to an appropriate value
            Exception exception = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.StackTraceString(exception);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Format
        ///</summary>
        [TestMethod()]
        public void FormatTest()
        {
            BreXPathFormatter target = new BreXPathFormatter(); // TODO: Initialize to an appropriate value
            object data = null; // TODO: Initialize to an appropriate value
            XPathNavigator expected = null; // TODO: Initialize to an appropriate value
            XPathNavigator actual;
            actual = target.Format(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CombineTraceStringForMessageOnly
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.Biztalk.dll")]
        public void CombineTraceStringForMessageOnlyTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = BreXPathFormatter_Accessor.CombineTraceStringForMessageOnly(message);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddExceptionToTraceString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.Biztalk.dll")]
        public void AddExceptionToTraceStringTest()
        {
            BreXPathFormatter_Accessor target = new BreXPathFormatter_Accessor(); // TODO: Initialize to an appropriate value
            XmlWriter xml = null; // TODO: Initialize to an appropriate value
            Exception exception = null; // TODO: Initialize to an appropriate value
            target.AddExceptionToTraceString(xml, exception);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BreXPathFormatter Constructor
        ///</summary>
        [TestMethod()]
        public void BreXPathFormatterConstructorTest()
        {
            BreXPathFormatter target = new BreXPathFormatter();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
