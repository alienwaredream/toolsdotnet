using Tools.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tools.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for InitializationStringParserTest and is intended
    ///to contain all InitializationStringParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InitializationStringParserTest
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
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            InitializationStringParser target = new InitializationStringParser();
            string initializationString = "key 1 = value 1 ;  key  2 = value2; key3 = value3;; ;  ;";

            IDictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("key 1", "value 1");
            expected.Add("key 2", "value2");
            expected.Add("key3", "value3");
 
            IDictionary<string, string> actual = target.Parse(initializationString);
            
            Assert.IsTrue(expected.Count == actual.Count, "Expected and Actual dictionaries are expected to be the same size");
            
            foreach (string key in expected.Keys)
            {
                Assert.IsTrue(actual.ContainsKey(key), "Key of " + key + " is expected to be present in the actual dictionary");

                Assert.AreEqual<string>(expected[key], actual[key]);
            }
        }

        /// <summary>
        ///A test for InitializationStringParser Constructor
        ///</summary>
        [TestMethod()]
        public void InitializationStringParserConstructorTest()
        {
            InitializationStringParser target = new InitializationStringParser();
        }
    }
}
