using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tools.Coordination.Tests
{
    /// <summary>
    /// Summary description for IntegrationTest
    /// </summary>
    [TestClass]
    public class AsyncBenchTest
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BeginMethodTest()
        {
            bool methodCalled = false;
            // setup the method
            Func<int, int> method = n =>
                                        {
                                            methodCalled = true;
                                            return -1;
                                        };
            // setup the test instance
            var asyncSample = new AsyncBench(
                20, method);
            IAsyncResult ar = asyncSample.BeginMethod();
            // wait until method call completes
            ar.AsyncWaitHandle.WaitOne();
            // verify it is completed
            Assert.IsTrue(ar.IsCompleted, "Operation should have completed before reaching this point!");
            // verify our delegate was called
            Assert.IsTrue(methodCalled, "Test method should have been called, but it was not!");
            // The bellow assert would require more synchronization and exceeds the testing contract
            //Assert.AreEqual(-1, asyncSample.Param);
        }
        [TestMethod]
        public void AsyncMethodCallbackTest()
        {
            bool methodCalled = false;
            // setup method
            Func<int, int> method = n =>
            {
                methodCalled = true;
                return -1;
            };
            // setup test instance
            var asyncSample = new AsyncBench(
                20, method);
            IAsyncResult ar = method.BeginInvoke(20, null, new State {Field = 10});
            // wait for the method to complete
            ar.AsyncWaitHandle.WaitOne();
            // verify it has completed
            Assert.IsTrue(ar.IsCompleted, "Operation should have completed before reaching this point!");
            // and was really called
            Assert.IsTrue(methodCalled, "Test method should have been called, but it was not!");
            // use IAsyncResult from our own BeginInvoke for the callback on the test instance
            asyncSample.AsyncMethodCallback(ar);
            // check that EndInvoke worked as expected
            Assert.AreEqual(-1, asyncSample.Param);
        }

    }
}
