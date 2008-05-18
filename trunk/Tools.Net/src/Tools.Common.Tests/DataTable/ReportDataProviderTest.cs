using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Diagnostics;
using System;
using Tools.Common.DataTables;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for DataTableProviderTest and is intended
    ///to contain all DataTableProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataTableProviderTest
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
        ///A test for GetNamedDataTable
        ///</summary>
        [TestMethod()]
        public void GetNamedDataTableTest()
        {
            DataTableProvider target = new DataTableProvider();
            string dataTableName = "getAreas";
            string[] parameters = new string[] { "517" };
            DataTable actual;
            actual = target.GetNamedDataTable(dataTableName, parameters);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Rows.Count > 0);
            actual.WriteXml(Console.Out);

        }
        [TestMethod()]
        public void GetDivisionsDataTableTest()
        {
            DataTableProvider target = new DataTableProvider();
            string dataTableName = "getDivisions";
            string[] parameters = new string[] { "517", "003" };
            DataTable actual;
            actual = target.GetNamedDataTable(dataTableName, parameters);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Rows.Count > 0);
            actual.WriteXml(Console.Out);

        }
    }
}
