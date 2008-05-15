using Tools.Common.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for NameValueSectionConfigurationProviderTest and is intended
    ///to contain all NameValueSectionConfigurationProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NameValueSectionConfigurationProviderTest
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
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            string configSectionName = "testNameValueSection";
            NameValueSectionConfigurationProvider target = 
                new NameValueSectionConfigurationProvider(configSectionName);
            string keyName = "testKey";
            string actual;
            actual = target[keyName];
            Assert.AreEqual<string>("testValue", actual);
        }

        /// <summary>
        ///A test for NameValueSectionConfigurationProvider Constructor
        ///</summary>
        [TestMethod()]
        public void NameValueSectionConfigurationProviderConstructorTest()
        {
            string configSectionName = "testNameValueSection"; // TODO: Initialize to an appropriate value
            NameValueSectionConfigurationProvider target = new NameValueSectionConfigurationProvider(configSectionName);
        }
    }
}
