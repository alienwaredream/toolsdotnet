using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Tools.Logging.Diagnostics.Tests
{
    /// <summary>
    /// Summary description for LoggingTest
    /// </summary>
    [TestClass]
    public class LoggingTest
    {
        public LoggingTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        public void ShouldLogWithXmlRollingListenerNoConfig()
        {
            // Setup source and listener
            string initializationString =
                @"name = XmlLogger; logrootpath = c:\log; staticpattern = log_; maxSizeBytes = 20000;";

            XmlWriterRollingTraceListener traceListener =
                new XmlWriterRollingTraceListener(initializationString);

            TraceSource log = new TraceSource("Test", SourceLevels.All);

            log.Listeners.Clear();
            log.Listeners.Add(traceListener);

            // Start Activity #1
            Guid activity1Guid = Guid.NewGuid();

            Trace.CorrelationManager.ActivityId = activity1Guid;

            log.TraceEvent(TraceEventType.Start, 2, "Activity #1");
            
            // log information inside Activity #1
            log.TraceInformation("Information from Activity #1");

            // Start Activity #2
            Guid activity2Guid = Guid.NewGuid();

            log.TraceTransfer(3, "Transferring to Activity #2", activity2Guid);

            Trace.CorrelationManager.ActivityId = activity2Guid;

            log.TraceData(TraceEventType.Start, 4, "Activity #2");

            // Complete Activity #2
            log.TraceEvent(TraceEventType.Stop, 5, "Completing Activity #2");
            
            log.TraceTransfer(6, "Returning back to Activity #1", activity1Guid);

            // Get back into Activity #1
            Trace.CorrelationManager.ActivityId = activity1Guid;
            // Log something extra in Activity #1 before completing it
            log.TraceEvent(TraceEventType.Warning, 7, "Warning from Activity #1");
            // Complete Activity #1
            log.TraceEvent(TraceEventType.Stop, 8, "Completing Activity #1");
        }
    }
}
