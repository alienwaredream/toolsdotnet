using System.Diagnostics;
using System.Threading;
using Tools.Processes.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ThreadState=System.Threading.ThreadState;
using System.Runtime.CompilerServices;

namespace Tools.Processes.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for ThreadedProcessTest and is intended
    ///to contain all ThreadedProcessTest Unit Tests
    ///</summary>
    [TestClass]
    public class ThreadedProcessTest
    {

        private bool beginStopCallbackCalled;
        private TestContext testContextInstance;
        private EventWaitHandle beginStopCallbackHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

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
        ///A test for WorkingThread
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void WorkingThreadTest()
        {
            ThreadedProcess_Accessor target = CreateThreadedProcess_Accessor();
            target.Start();
            Assert.IsNotNull(target.WorkingThread);
        }

        /// <summary>
        ///A test for OperationReset
        ///</summary>
        [TestMethod]
        public void OperationResetTest()
        {
            Assert.IsNotNull(CreateThreadedProcess_Accessor().OperationReset);
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod]
        public void EmptyStopTest()
        {
            ThreadedProcess_Accessor target = CreateThreadedProcess_Accessor();

            bool terminatedEventCalled = false;

            target.Start();

            target.add_Terminated((s, e) =>
                                      {
                                          terminatedEventCalled = true;
                                          Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Terminated, target.ExecutionState);
                                      });
            target.Stop();

            Assert.IsTrue(terminatedEventCalled);
            Assert.IsTrue(target.WorkingThread.ThreadState == ThreadState.Stopped);
            Assert.AreEqual<ProcessExecutionState>(ProcessExecutionState.Terminated, target.ExecutionState);
        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod]
        public void StartTest()
        {
            var process = new MockThreadedBeginStopWithSleepingThreadProcess();
            var target = new ThreadedProcess_Accessor(new PrivateObject(process));

            target.Start();

            Assert.AreEqual(ProcessExecutionState.Running, process.ExecutionState);

            target.Stop();

            Assert.AreEqual(ProcessExecutionState.Terminated, process.ExecutionState);
            Assert.IsTrue(target.WorkingThread.ThreadState == ThreadState.Stopped);
        }

        /// <summary>
        ///A test for SelfSuspend
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Processes.Core.dll")]
        public void SelfSuspendTest()
        {
            // time to suspend, ms.
            const int suspendTimeout = 5000;
            MockThreadedSuspendProcess process = new MockThreadedSuspendProcess(suspendTimeout);
            ThreadedProcess_Accessor target = new ThreadedProcess_Accessor(new PrivateObject(process ));

            target.Start();
            // give it some time before calling stop, or if it completes it will set its event (not now :))
            target.CompletedEvent.WaitOne((int)(suspendTimeout*2));
            
            target.Stop();

            Assert.IsTrue(process.StopWatch.ElapsedMilliseconds >= suspendTimeout);
            Assert.IsTrue(!process.ResumedBeforeTimeout);
        }

        internal virtual ThreadedProcess_Accessor CreateThreadedProcess_Accessor()
        {
            return new ThreadedProcess_Accessor(new PrivateObject(CreateThreadedProcess()));
        }

        /// <summary>
        ///A test for BeginStop
        ///</summary>
        [TestMethod]
        public void BeginStopTest()
        {
            var process = new MockThreadedBeginStopWithSleepingThreadProcess();
            var target = new ThreadedProcess_Accessor(new PrivateObject(process));

            var state = new MockAsyncState {TestField = "TestFieldValue"};

            target.Start();

            IAsyncResult ar = target.BeginStop(state, StopCallback);

            if (!ar.AsyncWaitHandle.WaitOne(20000))
            {
                Assert.IsTrue(false, "The time given to the process to stop has been exceeded!");
            }

            Assert.IsTrue(ar.IsCompleted);
            Assert.IsTrue(process.ThreadWasInterrupted, "Thread should have been interrupted from its WaitSleepJoin state!");
            beginStopCallbackHandle.WaitOne();
            Assert.IsTrue(beginStopCallbackCalled, "The BeginStopCallback should have been called!");
        }

        private void StopCallback(IAsyncResult ar)
        {
            beginStopCallbackCalled = true;
            Assert.IsNotNull(ar.AsyncState);
            Assert.IsNotNull(ar.AsyncState as MockAsyncState);
            Assert.AreEqual((ar.AsyncState as MockAsyncState).TestField, "TestFieldValue");
            beginStopCallbackHandle.Set();
        }

        internal virtual ThreadedProcess CreateThreadedProcess()
        {
            return new MockThreadedProcess();
        }

        /// <summary>
        ///A test for Abort
        ///</summary>
        [TestMethod]
        public void AbortTest()
        {
            var process = new MockThreadedBeginStopWithSleepingThreadProcess();
            var target = new ThreadedProcess_Accessor(new PrivateObject(process));

            target.Start();

            Assert.AreEqual(ProcessExecutionState.Running, process.ExecutionState);

            target.Abort();

            Assert.AreEqual(ProcessExecutionState.Terminated, process.ExecutionState);
            Assert.AreEqual(ThreadState.Stopped, target.WorkingThread.ThreadState);
        }
    }
    class MockThreadedProcess : ThreadedProcess
    {
        protected override void StartInternal()
        {
            // Asserts can't be called from here
            //Assert.AreEqual(ThreadState.Running, WorkingThread.ThreadState);
        }
    }
    class MockThreadedSuspendProcess : ThreadedProcess
    {
        private readonly int timeoutMs;
        public Stopwatch StopWatch { get; set; }


        public bool ResumedBeforeTimeout { get; set; }

        internal MockThreadedSuspendProcess(int timeoutMs)
        {
            this.timeoutMs = timeoutMs;
        }

        protected override void StartInternal()
        {
            StopWatch = new Stopwatch();
            StopWatch.Start();
            ResumedBeforeTimeout = SelfSuspend(TimeSpan.FromMilliseconds(timeoutMs));
            StopWatch.Stop();
        }
    }
    class MockThreadedBeginStopWithSleepingThreadProcess : ThreadedProcess
    {
        public bool ThreadWasInterrupted { get; set; }

        protected override void StartInternal()
        {
            // Without the bellow try/catch the test host will go down.
            //RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                Thread.Sleep(-1);
            }
            catch (ThreadInterruptedException ex)
            {
                ThreadWasInterrupted = true;
                Console.WriteLine("[" + Name + "] Thread interrupted" + ex);
            }
        }
    }
    class MockAsyncState { public string TestField { get; set; } }

}