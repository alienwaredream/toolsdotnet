using Tools.Common.Asserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tools.Common.Messaging;
using System.Collections.Generic;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ErrorTrapTest and is intended
    ///to contain all ErrorTrapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ErrorTrapTest
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


        [TestMethod()]
        public void AddRaisableAssertionTestForExceptionType()
        {
            string testExceptionMessage = "Test Exception Message";

            try
            {
                ErrorTrap.AddRaisableAssertion<Exception>(false, "Test Exception Message");
            }
            catch (Exception ex)
            {
                Assert.AreEqual<string>(testExceptionMessage, ex.Message);
            }
        }
    }
}
