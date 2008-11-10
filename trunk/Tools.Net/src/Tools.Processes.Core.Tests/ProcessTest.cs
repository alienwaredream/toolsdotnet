using Rhino.Mocks;
using Tools.Processes.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Tools.Processes.Core.Tests
{


    /// <summary>
    ///This is a test class for ProcessTest and is intended
    ///to contain all ProcessTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProcessTest
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
        ///A test for Name
        ///</summary>
        [TestMethod]
        public void NameTest()
        {
            Process target = CreateProcess();
            string expected = "TestOfName";
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ExecutionStateSyncObj
        ///</summary>
        [TestMethod]
        public void ExecutionStateSyncObjTest()
        {
            Assert.IsNotNull(CreateProcess().ExecutionStateSyncObj);
        }

        /// <summary>
        ///A test for ExecutionState
        ///</summary>
        [TestMethod]
        public void ExecutionStateTest()
        {
            Assert.AreEqual<ProcessExecutionState>(CreateProcess().ExecutionState, ProcessExecutionState.Unstarted);
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod]
        public void DescriptionTest()
        {
            Assert.AreEqual<string>("TestOfDescription", new MockProcess { Description = "TestOfDescription" }.Description);
        }

        /// <summary>
        ///A test for CompletedHandle
        ///</summary>
        [TestMethod]
        public void CompletedHandleTest()
        {
            Assert.IsNotNull(CreateProcess().CompletedHandle);
        }

        /// <summary>
        ///A test for CompletedEvent
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void CompletedEventTest()
        {
            var param0 = new PrivateObject(new MockProcess());
            var target = new Process_Accessor(param0);
            Assert.IsNotNull(target.CompletedEvent);
        }

        /// <summary>
        ///A test for Suspend
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void SuspendTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            target.Suspend();
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod]
        public void StopTest()
        {
            var param0 = new PrivateObject(new MockProcess());
            var process = new Process_Accessor(param0);

            //var resetEvent = MockRepository.GenerateStub<EventWaitHandle>(false, EventResetMode.ManualReset);

            //process.completedEvent = resetEvent;
            //resetEvent.Stub(r => r.Set());



            bool stoppingEventCalled = false;
            bool stoppedEventCalled = false;

            process.add_Stopping((s, a) =>
                                    {
                                        stoppingEventCalled = true;
                                        Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.StopRequested, process.ExecutionState);
                                    });
            process.add_Stopped((s, a) =>
                                    {
                                        stoppedEventCalled = true;
                                        Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Stopped,
                                                                               process.ExecutionState);
                                    });

            process.Stop();

            //resetEvent.AssertWasCalled(r => r.Set());

            Assert.IsTrue(stoppingEventCalled);
            Assert.IsTrue(stoppedEventCalled);

            Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Stopped, process.ExecutionState);

        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod]
        public void StartTest()
        {
            Process target = CreateProcess();
            target.Start();
        }

        /// <summary>
        ///A test for SetExecutionState
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void SetExecutionStateTest()
        {
            var param0 = new PrivateObject(CreateProcess());
            var target = new Process_Accessor(param0);
            ProcessExecutionState state = ProcessExecutionState.Finished;
            target.SetExecutionState(state);
            Assert.AreEqual<ProcessExecutionState>(target.ExecutionState, state);
            // changing in case the initial value was equal to the previous state.
            state = ProcessExecutionState.Stopped;
            target.SetExecutionState(state);
            Assert.AreEqual<ProcessExecutionState>(target.ExecutionState, state);
        }

        /// <summary>
        ///A test for Resume
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ResumeTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            target.Resume();
        }

        /// <summary>
        ///A test for OnTerminated
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void OnTerminatedTest()
        {
            PrivateObject param0 = new PrivateObject(CreateProcess());
            Process_Accessor target = new Process_Accessor(param0);
            ProcessExitEventArgs eventArgs = new ProcessExitEventArgs {CompletionState = new {Field = 123}};

            bool terminatedEventCalled = false;

            target.add_Terminated((s, e)=>
                                      {
                                          terminatedEventCalled = true;
                                          Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Terminated, target.ExecutionState);
                                      });

            target.OnTerminated(eventArgs);

            Assert.IsTrue(terminatedEventCalled);
        }

        internal virtual Process_Accessor CreateProcess_Accessor()
        {
            return new Process_Accessor(new PrivateObject(CreateProcess()));
        }

        /// <summary>
        ///A test for OnCompleted
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void OnCompletedTest()
        {

            var target = CreateProcess_Accessor();
            var eventArgs = new ProcessExitEventArgs {CompletionState = new {Field = 123}};

            bool completedEventCalled = false;

            target.add_Completed((s, e) =>
            {
                completedEventCalled = true;
                Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Completed, target.ExecutionState);
            });

            target.OnCompleted(eventArgs);

            Assert.IsTrue(completedEventCalled);
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod]
        public void InitializeTest()
        {
            Process target = CreateProcess();
            target.Initialize();
        }

        /// <summary>
        ///A test for EndStop
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void EndStopTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            IAsyncResult ar = null; // TODO: Initialize to an appropriate value
            target.EndStop(ar);
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod]
        public void DisposeTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            target.Dispose();
            target.Dispose();
        }

        /// <summary>
        ///A test for BeginStop
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void BeginStopTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            object state = null; // TODO: Initialize to an appropriate value
            AsyncCallback callback = null; // TODO: Initialize to an appropriate value
            IAsyncResult expected = null; // TODO: Initialize to an appropriate value
            IAsyncResult actual;
            actual = target.BeginStop(state, callback);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual Process CreateProcess()
        {
            Process target = new MockProcess();
            return target;
        }

        /// <summary>
        ///A test for Abort
        ///</summary>
        [TestMethod]

        public void AbortTest()
        {
            Process target = CreateProcess(); // TODO: Initialize to an appropriate value
            target.Abort();
            Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.AbortRequested, target.ExecutionState);
        }
    }

    class MockProcess : Process
    {
        public override void Start()
        {
        }
    }

}
