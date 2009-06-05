using Tools.Coordination.ProducerConsumer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Coordination.Core;
using System;
using Tools.Processes.Core;
using System.Collections.Generic;
using Tools.Core.Context;
using Tools.Coordination.WorkItems;
using Tools.Core.Threading;
using Tools.Tests.Helpers;
using System.Diagnostics;
using System.Threading;

namespace Tools.Coordination.Tests
{


    /// <summary>
    ///This is a test class for ConsumerManagerTest and is intended
    ///to contain all ConsumerManagerTest Unit Tests
    ///</summary>
    [TestClass]
    public class ConsumerManagerTest
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


        /// <summary>
        ///A test for WaitForLowPrioritySubmissionDelayResetEvent
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void WaitForLowPrioritySubmissionDelayResetEventTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.IsNotNull(target.waitForLowPrioritySubmissionDelayResetEvent);
            Assert.AreEqual(target.waitForLowPrioritySubmissionDelayResetEvent, target.WaitForLowPrioritySubmissionDelayResetEvent);
        }

        /// <summary>
        ///A test for TotalConsumerManagerStopTimeout
        ///</summary>
        [TestMethod]
        public void TotalConsumerManagerStopTimeoutTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.AreEqual(10000, target.TotalConsumerManagerStopTimeout, "The default value for TotalConsumerManagerStopTimeout should be 10000 ms, if you changed the value intentionaly, correct the test!");
        }

        /// <summary>
        ///A test for TimeOutSubmissionsCollector
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void TimeOutSubmissionsCollectorTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();
            // Put the check for not null in place when timeout submission is reintroduced
            // But only if it required (SD)
            Assert.IsNull(target.TimeOutSubmissionsCollector, "If default TimeOutSubmissionsCollector has been implemented, correct this test!");
        }

        /// <summary>
        ///A test for ThresholdMinimumSampleCount
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ThresholdMinimumSampleCountTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.AreEqual(100, target.ThresholdMinimumSampleCount, "The default value for ThresholdMinimumSampleCount should be 100 samples, if you changed the value intentionaly, correct the test!");
        }

        /// <summary>
        ///A test for SubmittedItemsCounter
        ///</summary>
        [TestMethod]
        public void SubmittedItemsCounterTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.IsNotNull(ConsumerManager.SubmittedItemsCounter);
            Assert.AreEqual(0, ConsumerManager.SubmittedItemsCounter.SyncValue);
        }

        /// <summary>
        ///A test for StateData
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void StateDataTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            var stateData = new ProcessingStateData(new WorkItemSlotsConfiguration());

            target.StateData = stateData;

            Assert.AreEqual(stateData, target.StateData);

        }

        /// <summary>
        ///A test for RegularToPendingThreshold
        ///</summary>
        [TestMethod]
        public void RegularToPendingThresholdTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.AreEqual(90, target.RegularToPendingThreshold, "The default value for RegularToPendingThreshold should be 90 percent, if you changed the value intentionaly, correct the test!");
        }

        /// <summary>
        ///A test for RegularResponseObtainedCount
        ///</summary>
        [TestMethod]
        public void RegularResponseObtainedCountTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.AreEqual(0, target.RegularResponseObtainedCount, "The initialized value of should be equal to zero!");

            target.regularResponseObtainedCount = 10;

            Assert.AreEqual(10, target.RegularResponseObtainedCount);

        }

        /// <summary>
        ///A test for PendingResponseObtainedCount
        ///</summary>
        [TestMethod]
        public void PendingResponseObtainedCountTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.AreEqual(0, target.PendingResponseObtainedCount, "The initialized value of should be equal to zero!");

            target.pendingResponseObtainedCount = 10;

            Assert.AreEqual(10, target.PendingResponseObtainedCount);
        }

        /// <summary>
        ///A test for CountersSyncLock
        ///</summary>
        [TestMethod]
        public void CountersSyncLockTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.IsNotNull(target.CountersSyncLock);
        }

        /// <summary>
        ///A test for ContextIdentifier
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ContextIdentifierTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            Assert.IsNotNull(target.ContextIdentifier);
        }

        /// <summary>
        ///A test for ConsumerThreadsCount
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ConsumerThreadsCountTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();
            int expected = 20;
            int actual;
            target.ConsumerThreadsCount = expected;
            actual = target.ConsumerThreadsCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Consumers
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ConsumersTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();
            List<IProcess> expected = new List<IProcess> { new MockJobConsumer(), new MockJobConsumer() };
            target.Consumers = expected;
            List<IProcess> actual = target.Consumers;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2, actual.Count);
        }

        /// <summary>
        ///A test for ConsumerConfiguration
        ///</summary>
        [TestMethod]
        public void ConsumerConfigurationTest()
        {
            ConsumerManager target = new ConsumerManager();
            // TODO: (SD) Use this in the ConsumerConfiguration tests
            ConsumerConfiguration expected = new ConsumerConfiguration { Description = "test description", MaxTotalSubmittedItemsCount = 2, Name = "testname", SubmissionInterval = 10000, SubmissionQueuingProcessTimeout = 15000, SubmissionType = SubmissionType.RegularSubmission };

            target.ConsumerConfiguration = expected;
            ConsumerConfiguration actual = target.ConsumerConfiguration;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CollectorConfiguration
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void CollectorConfigurationTest()
        {
            ConsumerManager_Accessor target = CreateConsumerManagerAccessor();

            TimeOutSubmissionsCollectorConfiguration expected = new TimeOutSubmissionsCollectorConfiguration { ClearItems = true };

            target.CollectorConfiguration = expected;
            TimeOutSubmissionsCollectorConfiguration actual = target.CollectorConfiguration;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod]
        public void StopTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));

            new TestRunner(() =>
            CompositePatternTestHelper.TestForCompositeOperation<ConsumerManager_Accessor, IProcess, TrivialAsyncResultMock>
                (
                target, parent => parent.Stop(), child => { child.BeginStop(null, new AsyncCallback(target.ConsumerStoppedCallback)); return new TrivialAsyncResultMock(); }, (parent, child) =>
                    parent.Consumers.Add(child)
                    ),
                    ApartmentState.MTA).Execute();

            Assert.AreEqual(ProcessExecutionState.Stopped, consumerManager.ExecutionState);
        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod]
        public void StartTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));

            CompositePatternTestHelper.TestForCompositeOperation<ConsumerManager_Accessor, IProcess>
                (
                target, parent => parent.Start(), child => child.Start(), (parent, child) =>
                    parent.Consumers.Add(child)
                    );
            Assert.AreEqual(ProcessExecutionState.Running, consumerManager.ExecutionState);

            new TestRunner(() =>
            CompositePatternTestHelper.TestForCompositeOperation<ConsumerManager_Accessor, IProcess, TrivialAsyncResultMock>
                (
                target, parent => parent.Stop(), child => { child.BeginStop(null, new AsyncCallback(target.ConsumerStoppedCallback)); return new TrivialAsyncResultMock(); },
                () => { return target.Consumers; }),
                    ApartmentState.MTA).Execute();

            Assert.AreEqual(ProcessExecutionState.Stopped, consumerManager.ExecutionState);

        }

        /// <summary>
        ///A test for ResetRegularResponseObtainedCount
        ///</summary>
        [TestMethod]
        public void ResetRegularResponseObtainedCountTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));
            target.regularResponseObtainedCount = 10;
            
            target.ResetRegularResponseObtainedCount();
            Assert.AreEqual(0, target.RegularResponseObtainedCount);
        }

        /// <summary>
        ///A test for ResetPendingResponseObtainedCount
        ///</summary>
        [TestMethod]
        public void ResetPendingResponseObtainedCountTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));
            target.pendingResponseObtainedCount = 10;

            target.ResetPendingResponseObtainedCount();
            Assert.AreEqual(0, target.PendingResponseObtainedCount);
        }

        /// <summary>
        ///A test for CreateConsumers
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void CreateConsumersTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));
            target.ConsumerThreadsCount = 1;
            target.ConsumerConfiguration = new ConsumerConfiguration { Description = "test description", MaxTotalSubmittedItemsCount = 2, Name = "testname", SubmissionInterval = 10000, SubmissionQueuingProcessTimeout = 15000, SubmissionType = SubmissionType.RegularSubmission };


            MockJobConsumer consumer = new MockJobConsumer {Name = "testconsumer" };

            target.createConsumerFunction = (name) => { return consumer; };

            target.CreateConsumers();

            Assert.AreEqual(1, target.Consumers.Count);
            Assert.AreEqual(consumer, target.Consumers[0]);
            Assert.AreEqual("testconsumer_0", consumer.Name);
        }

        /// <summary>
        ///A test for Consumer_RegularResponseObtained
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void Consumer_RegularResponseObtainedTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));
            target.regularResponseObtainedCount = 2;

            target.Consumer_RegularResponseObtained(null, new JobProcessedEventArgs { Success = true });

            Assert.AreEqual(3, target.RegularResponseObtainedCount);
            Assert.AreEqual(0, target.PendingResponseObtainedCount);
        }

        /// <summary>
        ///A test for Consumer_PendingResponseObtained
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void Consumer_PendingResponseObtainedTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));
            target.pendingResponseObtainedCount = 2;

            target.Consumer_PendingResponseObtained(null, new JobProcessedEventArgs { Success = true });

            Assert.AreEqual(3, target.PendingResponseObtainedCount);
            Assert.AreEqual(0, target.RegularResponseObtainedCount);
        }

        /// <summary>
        ///A test for Abort
        ///</summary>
        [TestMethod]
        public void AbortTest()
        {
            var consumerManager = new ConsumerManager();

            var target = new ConsumerManager_Accessor(new PrivateObject(consumerManager));

            CompositePatternTestHelper.TestForCompositeOperation<ConsumerManager_Accessor, IProcess>
                (
                target, parent => parent.Abort(), child => child.Abort(), (parent, child) =>
                    parent.Consumers.Add(child)
                    );

            Assert.AreEqual(ProcessExecutionState.AbortRequested, consumerManager.ExecutionState);
        }

        /// <summary>
        ///A test for ConsumerManager Constructor
        ///</summary>
        [TestMethod]
        public void ConsumerManagerConstructorTest()
        {
            ConsumerManager target = new ConsumerManager();
        }

        private ConsumerManager_Accessor CreateConsumerManagerAccessor()
        {
            return new ConsumerManager_Accessor(new PrivateObject(new ConsumerManager()));
        }
    }
    internal class MockJobConsumer : JobConsumer<string>
    {

    }
}
