using Tools.Common.Authorisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TokenDateTimeProviderTest and is intended
    ///to contain all TokenDateTimeProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TokenDateTimeProviderTest
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
        ///A test for GetVolatileData
        ///</summary>
        [TestMethod()]
        public void GetVolatileDataTest()
        {
            TokenDateTimeProvider target = new TokenDateTimeProvider();
            string actual;
            DateTime now = DateTime.UtcNow;
            actual = target.GetVolatileData();
            Console.WriteLine("Volatile data generated: " + actual);
            Assert.IsTrue(!String.IsNullOrEmpty(actual));
            DateTime parsedDateTime = Convert.ToDateTime(actual, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///A test for TokenDateTimeProvider Constructor
        ///</summary>
        [TestMethod()]
        public void TokenDateTimeProviderConstructorTest()
        {
            TokenDateTimeProvider target = new TokenDateTimeProvider();
        }
    }
}
