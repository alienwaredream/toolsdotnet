using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context;
using Spring.Context.Support;
using Tools.Coordination.Core;
using Tools.Processes.Core;
using Tools.Core.Asserts;
using System.Configuration;
using System.Threading;
using System;

namespace Tools.Coordination.Tests
{
    /// <summary>
    /// Summary description for IntegrationTest
    /// </summary>
    [TestClass]
    public class IntegrationTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
         //Use ClassInitialize to run code before running the first test in the class
         [ClassInitialize()]
         public static void MyClassInitialize(TestContext testContext)
         {
             AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
         }

         static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
         {
             //e.IsTerminating;
             Console.WriteLine(e.ExceptionObject.ToString());
         }
        //
        // Use ClassCleanup to run code after all tests in a class have run
         [ClassCleanup()]
         public static void MyClassCleanup()
         {
             AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
         }
         
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
        [DeploymentItem("IntegrationTest.config.xml")]
        public void RunModelTest()
        {

            IApplicationContext context = new XmlApplicationContext(
                 "IntegrationTest.config.xml");
            ProcessorFactory.ContainerContext = context;

            IProcess coordinator = context.GetObject("Coordinator") as IProcess;

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(coordinator != null,
                                                                         "Configure coordinator object!");

// ReSharper disable PossibleNullReferenceException - Checked by ErrorTrap
            coordinator.Start();
// ReSharper restore PossibleNullReferenceException

            Thread.Sleep(10000);

            coordinator.Stop();
        }
    }
}
