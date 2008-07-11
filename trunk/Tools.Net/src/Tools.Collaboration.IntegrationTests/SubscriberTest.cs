using Tools.Collaboration.Contracts.Tools.Common.DataTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Collaboration.Contracts;
using System;
using System.ServiceModel;
using Tools.Collaboration.Subscriptions;
using System.ServiceModel.Description;

namespace Tools.Collaboration.Publishing.IntegrationTests
{

    /// <summary>
    ///This is a test class for PublisherClientTest and is intended
    ///to contain all PublisherClientTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SubscriberTest
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
        ///A test for Publish
        ///</summary>
        [TestMethod()]
        public void ShouldPublishToPublisherAndGetNotificationBack()
        {
            ServiceHost hostDefault = new
   ServiceHost(typeof(Subscriber));

            TimeSpan closeTimeout = hostDefault.CloseTimeout;

            TimeSpan openTimeout = hostDefault.OpenTimeout;


            ServiceAuthorizationBehavior authorization =
                hostDefault.Authorization;

            ServiceCredentials credentials =
                            hostDefault.Credentials;

            ServiceDescription description =
                    hostDefault.Description;


            int manualFlowControlLimit =
                    hostDefault.ManualFlowControlLimit;


            NetTcpBinding portsharingBinding = new NetTcpBinding();
            hostDefault.AddServiceEndpoint(
          typeof(ISubscriber),
          portsharingBinding,
          "net.tcp://localhost/MyService");


            int newLimit = hostDefault.IncrementManualFlowControlLimit(100);

            using (ServiceHost serviceHost = new ServiceHost(typeof(Subscriber)))
            {
                try
                {
                    // Open the ServiceHost to start listening for messages.
                    serviceHost.Open();
                    // The service can now be accessed.
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
                    serviceHost.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                    throw ex;
                }
            }

            using (PublisherClient target = new PublisherClient())
            {
                string message = "Test message";
                string activityId = "Test activity";
                target.Publish(message, activityId);
            }
        }

        /// <summary>
        ///A test for Publish
        ///</summary>
        [TestMethod()]
        public void PublishTest()
        {
            using (PublisherClient target = new PublisherClient())
            {
                string message = "Test message";
                target.Publish(message);
            }
        }

        /// <summary>
        ///A test for GetName
        ///</summary>
        [TestMethod()]
        public void GetNameTest()
        {
            using (PublisherClient target = new PublisherClient())
            {
                Assert.IsTrue(!String.IsNullOrEmpty(target.GetName()), "Not null name is expected as a return from the publisher's GetName call");
            }
        }

        /// <summary>
        ///A test for AddSubscriber
        ///</summary>
        [TestMethod()]
        public void AddSubscriberTest()
        {
            PublisherClient target = new PublisherClient();
            SubscriberData subscriber = new SubscriberData { Name = "Test subscriber", Url = "http://localhost:8002/Subscriber" };
            target.AddSubscriber(subscriber);
        }

        /// <summary>
        ///A test for PublisherClient Constructor
        ///</summary>
        [TestMethod()]
        public void PublisherClientConstructorTest2()
        {
            PublisherClient target = new PublisherClient();
        }
    }
}
