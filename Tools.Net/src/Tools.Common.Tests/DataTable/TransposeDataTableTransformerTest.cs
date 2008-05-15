
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
using Tools.Common.DataTables;



namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TransposeDataTableTransformerTest and is intended
    ///to contain all TransposeDataTableTransformerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TransposeDataTableTransformerTest
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
        ///A test for Transform
        ///</summary>
        [TestMethod()]
        public void TransformTest()
        {
            SampleTransposeDataProvider dataProvider = new SampleTransposeDataProvider();
            string[] parameters = new string[] { "10", "7" };
            DataTable dt = dataProvider.GetNamedDataTable("test", parameters);

            TransposeDataTableTransformer target = 
                new TransposeDataTableTransformer(new TransposeDefinition 
                {KeyColumnName = "F1", SourceNameColumnName = "F2", SourceValueColumnName = "F3"});

            DataTable actual = target.Transform(dt);

            Assert.IsNotNull(actual);
            //Assert.IsTrue(actual.Rows.Count > 0);
            actual.WriteXml(Console.Out);
        }

        /// <summary>
        ///A test for TransposeDataTableTransformer Constructor
        ///</summary>
        [TestMethod()]
        public void TransposeDataTableTransformerConstructorTest()
        {
            TransposeDataTableTransformer target = new TransposeDataTableTransformer(new TransposeDefinition { SourceNameColumnName = "F2", SourceValueColumnName = "F3" });
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
