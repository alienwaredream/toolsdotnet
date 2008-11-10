using System.Collections.Generic;
using Rhino.Mocks;
using Tools.Processes.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tools.Tests.Helpers;

namespace Tools.Processes.Core.Tests
{


    /// <summary>
    ///This is a test class for ProcessCoordinatorTest and is intended
    ///to contain all ProcessCoordinatorTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProcessCoordinatorTest
    {


        private TestContext testContextInstance;
        private bool numberOfProcesesesZeroedCalled;

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
        ///A test for TotalRegularStopTimeout
        ///</summary>
        [TestMethod]
        public void TotalRegularStopTimeoutTest()
        {
            ProcessCoordinator target = new ProcessCoordinator();
            // test the default value is being set first
            Assert.AreEqual(20000, target.TotalRegularStopTimeout, "The default value for the TotalRegularStopTimeout changed, fix the test of the default value!");
            // then tests the setter
            target.TotalRegularStopTimeout = 30000;
            // and getter
            Assert.AreEqual(30000, target.TotalRegularStopTimeout);
        }

        /// <summary>
        ///A test for Processes
        ///</summary>
        [TestMethod]
        public void ProcessesTest()
        {
            var target = new ProcessCoordinator();

            var process1 = MockRepository.GenerateStub<IProcess>();
            var process2 = MockRepository.GenerateStub<IProcess>();

            Assert.IsNotNull(target.Processes);

            target.Processes.AddRange(new List<IProcess> { process1, process2 });
            // check the count
            Assert.AreEqual(2, target.Processes.Count);
            // check the order, order can be important for the sync scenarios
            // basicaly it is a bad smell if it is, but we better keep the contract here
            Assert.AreEqual(target.Processes[0], process1);
            Assert.AreEqual(target.Processes[1], process2);
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod]
        public void StopTest()
        {
            CompositePatternTestHelper.TestForCompositeOperation<ProcessCoordinator, IProcess>(
                parent => parent.Stop(), // Parent operation
                child => child.Stop(), // Should invoke following on every child
                (parent, child) => parent.Processes.Add(child) // How to add add child to the parent
                );
        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod]
        public void StartTest()
        {
            CompositePatternTestHelper.TestForCompositeOperation<ProcessCoordinator, IProcess>(parent => parent.Start(), child => child.Start(), (parent, child) => parent.Processes.Add(child));
        }

        /// <summary>
        ///A test for processJoinCallback
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void processJoinCallbackTest()
        {
            var target = new ProcessCoordinator_Accessor();
            var ar = MockRepository.GenerateStub<IAsyncResult>();
            //target.log = MockRepository.GenerateStub<TraceSource>();

            var process1 = MockRepository.GenerateStub<IProcess>();
            var process2 = MockRepository.GenerateStub<IProcess>();

            process1.Expect(p => p.Start());
            process2.Expect(p => p.Start());
            process1.Expect(p => p.Stop());
            process2.Expect(p => p.Stop());

            target.Processes.AddRange(new List<IProcess> { process1, process2 });

            target.Start();

            target.numberOfProcessesRunning.Zeroed += numberOfProcessesRunning_Zeroed;

            Assert.AreEqual(2, target.numberOfProcessesRunning.SyncValue);

            target.Stop();

            Assert.AreEqual(0, target.numberOfProcessesRunning.SyncValue);
            Assert.IsTrue(numberOfProcesesesZeroedCalled, "numberOfProceseses.Zeroed event is expected to be called!");
            //TODO: (SD) Add tests for exceptional scenarios
            // overall this test happened to be more integration test than a unit test,
            // will change it in the future
        }

        void numberOfProcessesRunning_Zeroed()
        {
            numberOfProcesesesZeroedCalled = true;
        }

        /// <summary>
        ///A test for Abort
        ///</summary>
        [TestMethod]
        public void AbortTest()
        {
            CompositePatternTestHelper.TestForCompositeOperation<ProcessCoordinator, IProcess>(parent => parent.Abort(), child => child.Abort(), (parent, child) => parent.Processes.Add(child));
        }

        /// <summary>
        ///A test for ProcessCoordinator Constructor
        ///</summary>
        [TestMethod]
        public void ProcessCoordinatorConstructorTest()
        {
            var target = new ProcessCoordinator_Accessor(new PrivateObject(new ProcessCoordinator()));
            Assert.IsNotNull(target.Processes);

            //TODO: (SD) Add test for testing Zeroed to be non null and have >0 invocation list.

        }
    }
}
