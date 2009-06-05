using System.Diagnostics;
using Tools.Coordination.ProducerConsumer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Coordination.WorkItems;
using Tools.Coordination.Core;
using System.Threading;

namespace Tools.Coordination.Tests
{


    /// <summary>
    ///This is a test class for ProducerTest and is intended
    ///to contain all ProducerTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProducerTest
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
        ///A test for StateData
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void StateDataTest()
        {
            Producer_Accessor target = CreateProducer_Accessor();
            // State data will be set by the container
            var stateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            target.StateData = stateData;

            Assert.AreEqual(stateData, target.StateData);
        }

        /// <summary>
        ///A test for RetrievedItems
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void RetrievedItemsTest()
        {
            Producer_Accessor target = CreateProducer_Accessor();
            // State data is expected to be set by the container
            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            // When state data is set, retrieved items should be equal to the state date retrieved items
            Assert.AreEqual(target.StateData.RetrievedItems, target.RetrievedItems);
        }

        /// <summary>
        ///A test for PriorityScope
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void PriorityScopeTest()
        {
            Producer_Accessor target = CreateProducer_Accessor();
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(new ProcessorConfiguration { Count = 1, Description = "Test producer", Enabled = true, Name = "TestProducer", Priority = SubmissionPriority.High });

            Assert.AreEqual(SubmissionPriority.High, target.PriorityScope);
        }

        /// <summary>
        ///A test for Configuration
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ConfigurationTest()
        {
            Producer_Accessor target = CreateProducer_Accessor();
            var configuration = new ProcessorConfiguration
                                    {
                                        Count = 1,
                                        Description = "Test producer",
                                        Enabled = true,
                                        Name = "TestProducer",
                                        Priority = SubmissionPriority.High
                                    };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            Assert.AreEqual(configuration, target.Configuration);
        }

        /// <summary>
        ///A test for StartInternal
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void StartInternalTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            var configuration = new ProcessorConfiguration
            {
                Count = 1,
                Description = "Test producer",
                Enabled = true,
                Name = "TestProducer",
                Priority = SubmissionPriority.High
            };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration());
            //TODO: (SD) Return to the scenario when producer will be throwing errors in the
            // GetNextWorkItem method.
            producer.Start();
            Thread.Sleep(2000);
            producer.Stop();
        }

        /// <summary>
        ///A test for SetParameters
        ///</summary>
        [TestMethod]
        public void SetParametersTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            const string name = "TestProducer";
            const string description = "TestProducer description";

            var config = new ProcessorConfiguration
                                    {
                                        Count = 1,
                                        Description = description,
                                        Enabled = true,
                                        Name = name,
                                        Priority = SubmissionPriority.High
                                    };
            producer.SetParameters(config);

            Assert.AreEqual(name, producer.Name);
            Assert.AreEqual(description, producer.Description);
            Assert.AreEqual(SubmissionPriority.High, target.PriorityScope);
        }

        /// <summary>
        ///A test for ReservePrioritySlot
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ReservePrioritySlotTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            var configuration = new ProcessorConfiguration
            {
                Count = 1,
                Description = "Test producer",
                Enabled = true,
                Name = "TestProducer",
                Priority = SubmissionPriority.High
            };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            var slots = new PrioritySlotsCountCollection();
            slots.Add(new PrioritySlotsConfiguration{ Count = 2, SubmissionPriority = SubmissionPriority.Normal, Timeout = -1});

            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration{Name = "Test",
                                                                                      Description = "Test description",
                                                                                      PrioritySlotCounts = slots
            });

            target.ReservePrioritySlot(SubmissionPriority.Normal);

            Assert.AreEqual(1, target.RetrievedItems.Counters[SubmissionPriority.Normal].SyncValue);

            target.ReservePrioritySlot(SubmissionPriority.Normal);
            Assert.AreEqual(2, target.RetrievedItems.Counters[SubmissionPriority.Normal].SyncValue);


        }

        /// <summary>
        ///A test for ProcessRetrievedWorkItem
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void ProcessRetrievedWorkItemTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            var configuration = new ProcessorConfiguration
            {
                Count = 1,
                Description = "Test producer",
                Enabled = true,
                Name = "TestProducer",
                Priority = SubmissionPriority.High
            };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            var slots = new PrioritySlotsCountCollection();
            slots.Add(new PrioritySlotsConfiguration { Count = 2, SubmissionPriority = SubmissionPriority.Normal, Timeout = -1 });

            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration
            {
                Name = "Test",
                Description = "Test description",
                PrioritySlotCounts = slots
            });

            var workItem = new RequestWorkItem();

            target.ReservePrioritySlot(SubmissionPriority.Normal);
            target.ReservePrioritySlot(SubmissionPriority.Normal);

            target.ProcessRetrievedWorkItem(workItem);

            Assert.AreEqual(1, target.RetrievedItems.Counters[SubmissionPriority.Normal].SyncValue);

        }
        //[TestMethod]
        //[DeploymentItem("Tools.Coordination.dll")]
        public void ProcessRetrievedWorkItemWithBlockAndStopTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            var configuration = new ProcessorConfiguration
            {
                Count = 1,
                Description = "Test producer",
                Enabled = true,
                Name = "TestProducer",
                Priority = SubmissionPriority.High
            };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            var slots = new PrioritySlotsCountCollection();
            slots.Add(new PrioritySlotsConfiguration { Count = 2, SubmissionPriority = SubmissionPriority.Normal, Timeout = -1 });

            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration
            {
                Name = "Test",
                Description = "Test description",
                PrioritySlotCounts = slots
            });

            target.ReservePrioritySlot(SubmissionPriority.Normal);
            target.ReservePrioritySlot(SubmissionPriority.Normal);
            target.ReservePrioritySlot(SubmissionPriority.Normal);

            producer.Stop();
        }

        /// <summary>
        ///A test for GetNextWorkItem
        ///</summary>
        [TestMethod]
        public void GetNextWorkItemTest()
        {
            // method is abstract
        }

        /// <summary>
        ///A test for CancelPrioritySlotReservation
        ///</summary>
        [TestMethod]
        [DeploymentItem("Tools.Coordination.dll")]
        public void CancelPrioritySlotReservationTest()
        {
            var producer = new MockProducer();
            var target = new Producer_Accessor(new PrivateObject(producer));

            var configuration = new ProcessorConfiguration
            {
                Count = 1,
                Description = "Test producer",
                Enabled = true,
                Name = "TestProducer",
                Priority = SubmissionPriority.High
            };
            //TODO: (SD) set parameters method is rudimentary, subject ti refactor
            target.SetParameters(configuration);

            var slots = new PrioritySlotsCountCollection();
            slots.Add(new PrioritySlotsConfiguration { Count = 2, SubmissionPriority = SubmissionPriority.Normal, Timeout = -1 });

            target.StateData = new ProcessingStateData(new WorkItemSlotsConfiguration
            {
                Name = "Test",
                Description = "Test description",
                PrioritySlotCounts = slots
            });

            target.ReservePrioritySlot(SubmissionPriority.Normal);
            Assert.AreEqual(1, target.RetrievedItems.Counters[SubmissionPriority.Normal].SyncValue);
   
            target.CancelPrioritySlotReservation(SubmissionPriority.Normal);
            Assert.AreEqual(0, target.RetrievedItems.Counters[SubmissionPriority.Normal].SyncValue);

        }

        internal virtual Producer_Accessor CreateProducer_Accessor()
        {
            return new Producer_Accessor(new PrivateObject(new MockProducer()));
        }
    }

    internal class MockProducer : Producer
    {

        public override WorkItem GetNextWorkItem(WorkItemSlotCollection slots)
        {
            return null;
        }
    }
}
