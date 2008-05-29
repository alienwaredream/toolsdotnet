using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tools.Common.UnitTests
{
    /// <summary>
    ///This is a test class for ErrorTrapTest and is intended
    ///to contain all ErrorTrapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IoCWrapperTraceListenerTest
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
        public void ShouldBePresentInTheSysDiagListenersList()
        {
            TraceSource trace = new TraceSource("Tools.Logging");

            Assert.IsNotNull(trace.Listeners);
            TraceListener listener = trace.Listeners["IoCLogger"];

            Assert.IsNotNull(listener);

            listener.WriteLine("Test of IoCLogger");

        }
        [TestMethod()]
        public void ShouldLogForEnterpriseLib()
        {
            //Logger.Write("Test message from " + this.GetType().FullName, "General");
        }
        [TestMethod()]
        public void ShouldLogForEnterpriseLibWithExtraData()
        {
            Dictionary<string, object> extendedProperties = new Dictionary<string, object>();
            extendedProperties.Add("ExtraField1", "Test value");

            //Logger.Write("Test message from " + this.GetType().FullName, "General",
            //    extendedProperties);
        }
    }
}
