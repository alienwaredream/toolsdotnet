using Tools.Coordination.ProducerConsumer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Core.Context;
using Tools.Coordination.WorkItems;
using System;
using Tools.Coordination.Core;
using System.Diagnostics;
using Tools.Processes.Core;

namespace Tools.Coordination.Tests
{


    /// <summary>
    ///This is a test class for JobConsumerTest and is intended
    ///to contain all JobConsumerTest Unit Tests
    ///</summary>
    [TestClass]
    public class JobConsumerTest
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
        [TestInitialize]
        public void MyTestInitialize()
        {
            Log.Source.Listeners.Add(new ConsoleTraceListener());
            Log.Source.Switch.Level = SourceLevels.All;
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            Log.Source.Listeners.Clear();
        }
        //
        #endregion


        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void SubmittedItemsTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();
            // State data is expected to be set by the container
            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            // When state data is set, submitted items should be equal to the state date submitted items
            Assert.AreEqual(target.StateData.SubmittedItems, target.SubmittedItems);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void StateDataTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();
            // State data will be set by the container
            var stateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            target.StateData = stateData;

            Assert.AreEqual(stateData, target.StateData);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void RetrievedItemsTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();
            // State data is expected to be set by the container
            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            // When state data is set, submitted items should be equal to the state data submitted items
            Assert.AreEqual(target.StateData.RetrievedItems, target.RetrievedItems);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void JobProcessorTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();

            var jobProcessor = new MockJobProcessor();

            // To be set by the container
            target.JobProcessor = jobProcessor;

            Assert.AreEqual(jobProcessor, target.JobProcessor);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void VerifyItemAfterRetrievalTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();

            WorkItem item = new RequestWorkItem();
            MockJob job = new MockJob();

            var actual = target.VerifyItemAfterRetrieval(item, job);
            Assert.IsNotNull(actual, "Verification result should not be null!");
            Assert.AreEqual(true, actual.PassedSuccessfuly);
            Assert.AreEqual(false, actual.GenerateNotification);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void UpdateJobWithResponseTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();
            // Default implementation is empty, only checking it will not throw.
            target.UpdateJobWithResponse(new JobProcessedEventArgs());
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void SubmitTJobCallbackTest()
        {
            JobConsumer_Accessor<MockJob> target = CreateMockAccessor();

            bool endinvokeCalled = false;
            var wi = new RequestWorkItem();
            SubmitJobCallbackDelegate<MockJob> action = (job, workItem, jobProcessedDelegate, submittingJobDelegate) =>
            {
                endinvokeCalled = true;
                Assert.AreEqual(wi, workItem);
            };


            IAsyncResult ar = action.BeginInvoke(new MockJob(), wi, null, null, null, wi);
            target.SubmitTJobCallback(ar);

            Assert.IsTrue(endinvokeCalled, "EndInvoke should be called by the method!");
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        //[ExpectedExceptionAttribute(typeof(ConfigurationErrorsException), "StateData != null. Configure StateData!")]
        public void StartAndStopWithNoStateDataTest()
        {
            var consumer = new MockConsumer();
            consumer.Start();
            consumer.Stop();
        }
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        //[ExpectedExceptionAttribute(typeof(ConfigurationErrorsException), "StateData != null. Configure StateData!")]
        public void StartAndStopWithStateDataTest()
        {
            var target = new MockConsumer(new ProcessingStateData(new WorkItemSlotsConfiguration()));
            target.Start();
            target.Stop();
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void Sender_SubmittingRequestTest()
        {
            IProcess process = new MockConsumer();
            JobConsumer_Accessor<MockJob> target = new JobConsumer_Accessor<MockJob>(new PrivateObject(process));
            var workItem = new RequestWorkItem();
            var submittingArgs = new SubmittingJobEventArgs { WorkItem = workItem };
            process.Start();
            process.Stop();
            target.Sender_SubmittingRequest(null, submittingArgs);
            Assert.IsTrue(submittingArgs.Cancel);
            Assert.IsFalse(submittingArgs.Retry);
            Assert.AreEqual(workItem, submittingArgs.WorkItem);

        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void Sender_ResponseReceivedTest()
        {
            IProcess process = new MockConsumer();
            var target = new JobConsumer_Accessor<MockJob>(new PrivateObject(process))
                             {
                                 StateData = new ProcessingStateData(new WorkItemSlotsConfiguration())
                             };
            var workItem = new RequestWorkItem();
            var joProcessedArgs = new JobProcessedEventArgs { WorkItem = workItem, OperationContextShortcut = workItem.ContextIdentifier, Retry = false, Success = true };

            target.Sender_ResponseReceived(joProcessedArgs);

            // Review the log for this one, should correspond to a delayed item
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ProcessJobTest()
        {
            var process = new MockConsumer();
            var target = new JobConsumer_Accessor<MockJob>(new PrivateObject(process))
            {
                StateData = new ProcessingStateData(new WorkItemSlotsConfiguration()),
                Configuration = new ConsumerConfiguration { MaxTotalSubmittedItemsCount = 1, SubmissionType = SubmissionType.RegularSubmission }
            };
            var workItem = new RequestWorkItem();
            var job = new MockJob();
            var jobProcessor = new MockJobProcessor();
            target.JobProcessor = jobProcessor;

            SubmissionStatus submissionStatus = SubmissionStatus.None;

            process.Start();

            target.ProcessJob(job, workItem, ref submissionStatus);
            Assert.AreEqual(SubmissionStatus.SubmissionCompleted, submissionStatus);

            Assert.IsTrue(jobProcessor.ProcessJobWithEventCallbackCalled, "ProcessJobWithEventCallback should be called!");
            Assert.IsTrue(process.LogJobCompletionCalled);
            process.Stop();
        }


        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void LogJobCompletionTest()
        {

            var target = CreateMockAccessor();
            JobProcessedEventArgs e = null;
            target.LogJobCompletion(e);
            // should not throw, nothing else
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void HandleLoopExceptionTest()
        {
            var target = CreateMockAccessor();
            WorkItem workItem = new RequestWorkItem();
            var job = new MockJob();
            bool jobPreCheckFlag = true;
            // Nothing is expected from this method, though asap 
            // transaction rollback should be implemented by it (SD)
            target.HandleLoopException(SubmissionStatus.SubmissionCompleted, workItem, job, jobPreCheckFlag);
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void HandleGracefulStopTest()
        {
            var target = CreateMockAccessor();
            WorkItem workItem = new RequestWorkItem();
            var job = new MockJob();
            bool jobPreCheckFlag = true;
            // Nothing is expected from this method, though asap 
            // transaction rollback should be implemented by it (SD)
            target.HandleGracefulStop(SubmissionStatus.SubmissionCompleted, workItem, job, jobPreCheckFlag);
        }
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void FireRegularResponseObtainedTest()
        {
            var target = CreateMockAccessor();
            // Nothing is expected from this method right now, though
            // coming to the lazy mode should be implemented soon
            target.FireRegularResponseObtained(new ContextIdentifier());
        }

        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void FirePendingResponseObtainedTest()
        {
            var target = CreateMockAccessor();
            // Nothing is expected from this method right now, though
            // coming to the lazy mode should be implemented soon
            target.FirePendingResponseObtained(new ContextIdentifier());
        }

        #region Helper methods

        private static JobConsumer_Accessor<MockJob> CreateMockAccessor()
        {
            return new JobConsumer_Accessor<MockJob>(new PrivateObject(new MockConsumer()));
        }

        #endregion
    }

    #region Helper classes
    class MockJob { public int JobId { get; set; } }
    class MockConsumer : JobConsumer<MockJob>
    {
        public bool LogJobCompletionCalled { get; set; }

        internal MockConsumer() { }


        internal MockConsumer(ProcessingStateData stateData)
            : base(stateData)
        {
        }
        protected override void LogJobCompletion(JobProcessedEventArgs e)
        {
            base.LogJobCompletion(e);
            LogJobCompletionCalled = true;
        }
    }
    class MockJobProcessor : IJobProcessor<MockJob>
    {

        internal SubmittingJobEventArgs SubmittingArgs { get; set; }
        internal bool ProcessJobWithEventCallbackCalled { get; set; }

        #region IJobProcessor<MockJob> Members

        public void ProcessJobWithEventCallback(MockJob job, WorkItem workItem, JobProcessedDelegate jobProcessedDelegate, SubmittingJobDelegate submittingJobDelegate)
        {
            ProcessJobWithEventCallbackCalled = true;
            SubmittingArgs = new SubmittingJobEventArgs { WorkItem = workItem };
            submittingJobDelegate(this, SubmittingArgs);
            jobProcessedDelegate(new JobProcessedEventArgs
                                     {
                                         WorkItem = workItem,
                                         OperationContextShortcut = workItem.ContextIdentifier,
                                         Retry = false,
                                         Success = true
                                     });
        }

        #endregion

        #region IDescriptor Members

        public string Name
        {
            get
            {
                return "MockJobProcessor";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }

    #endregion

}
