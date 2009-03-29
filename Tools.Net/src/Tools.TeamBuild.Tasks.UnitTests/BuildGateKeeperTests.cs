using System;
using NUnit.Framework;
using System.IO;
using System.Text;
using Rhino.Mocks;

namespace Tools.TeamBuild.Tasks.UnitTests
{
    [TestFixture]
    public class BuildGateKeeperTests
    {
        [Test()]
        public void Execute_Should_ReturnBreakerData()
        {
            // Set values.
            string keeperFileName = @"c:\build\tools.buildkeeper.state.xml";
            BuildStatus status = BuildStatus.Failure;
            string requestorEmail = "stan@stan.com";
            string requestorDisplayName = "Stanislav Dvoychenko";
            DateTime expectedTimeStamp = DateTime.Now;
            string dateFormat = "dd-MMM-yyyTHH:mm:ss";
            string expectedRecord = String.Format("{0};{1};{2};{3}",
                expectedTimeStamp.ToString(dateFormat), requestorDisplayName, requestorEmail, status);
            // Create date provider and state provider stubs/mocks
            MockRepository mocks = new MockRepository();

            IDateProvider dateProvider = mocks.Stub<IDateProvider>();
            IStatePersistor statePersistor = mocks.DynamicMock<IStatePersistor>();
            // Set up stubbed/mocked methods
            dateProvider.Stub<IDateProvider>((p) => p.GetTimeStamp()).Return(expectedTimeStamp);

            using (mocks.Record())
            {
                SetupResult.For<bool>(statePersistor.ContainsBreak).Return(false);
                statePersistor.Expect<IStatePersistor>((p) => p.WriteState(expectedRecord));
            }

            // Create task instance
            BuildGateKeeper keeper = new BuildGateKeeper();
            // Setup properties.
            keeper.BuildStatus = status;
            keeper.StateFilePath = keeperFileName;
            keeper.RequestorMailAddress = requestorEmail;
            keeper.RequestorDisplayName = requestorDisplayName;
            // Assert input properties are set alright.
            Assert.AreEqual(keeperFileName, keeper.StateFilePath);
            Assert.AreEqual(status, keeper.BuildStatus);
            // Prepare a string builder to hold the keeper output
            StringBuilder sb = new StringBuilder();

            
            using (StringWriter writer = new StringWriter(sb))
            {
                using (mocks.Playback())
                {

                    keeper.StatePersistor = statePersistor;

                    keeper.Execute();
                }
            }
        }

        private void TestHelper(BuildStatus status, Action expectationSetup)
        {

        }
    }
}