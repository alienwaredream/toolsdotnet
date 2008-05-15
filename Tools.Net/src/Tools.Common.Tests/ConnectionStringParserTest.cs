using Tools.Common.Asserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tools.Common.Messaging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Tools.Common.UnitTests
{
    /// <summary>
    ///This is a test class for ErrorTrapTest and is intended
    ///to contain all ErrorTrapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConnectionStringParserTest
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
        public void ShouldReturnCorrectUserName()
        {
            string userName = "testUser";
            string password = "testPassword";

            string connectionString = @"Data Source=testDBServer\testInstance;Initial Catalog=testDatabase;User ID=testUser; Password=testPassword";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            Trace.WriteLine(String.Format("builder.UserID: {0}, builder.Password: {1}", builder.UserID, builder.Password));

            Assert.IsTrue(builder.UserID == userName, "builder.UserID == userName");
            Assert.IsTrue(builder.Password == password, "builder.Password == password");
        }
    }
}
