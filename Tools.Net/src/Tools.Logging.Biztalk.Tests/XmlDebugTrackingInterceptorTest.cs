using Tools.Logging.Biztalk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Microsoft.RuleEngine;
using System;



namespace Tools.Logging.Biztalk.Tests
{

    using Tests = Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text;
    using System.Xml.XPath;
    using System.IO;
    using System.Xml.Linq;
    using System.Diagnostics;
    /// <summary>
    ///This is a test class for XmlDebugTrackingInterceptorTest and is intended
    ///to contain all XmlDebugTrackingInterceptorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlDebugTrackingInterceptorTest
    {


        private TestContext testContextInstance;
        XmlDebugTrackingInterceptor_Accessor target;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {

        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new XmlDebugTrackingInterceptor_Accessor((s) => Console.WriteLine(s));

            //target.XmlOutputTracker = new Action<string>();

            target.source.Listeners.Add(new XmlWriterRollingTraceListener(Console.Out));


            target.source.Switch.Level = SourceLevels.All;
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            TraceSource source = new TraceSource(typeof(XmlDebugTrackingInterceptor).Assembly.GetName().Name);
            source.Listeners.Clear();
        }
        //
        #endregion


        /// <summary>
        ///A test for TrackRuleSetEngineAssociation
        ///</summary>
        [TestMethod()]
        public void TrackRuleSetEngineAssociationTest()
        {
            XmlDebugTrackingInterceptor target = new XmlDebugTrackingInterceptor(); // TODO: Initialize to an appropriate value
            RuleSetInfo ruleSetInfo = null; // TODO: Initialize to an appropriate value
            Guid ruleEngineGuid = new Guid(); // TODO: Initialize to an appropriate value
            target.TrackRuleSetEngineAssociation(ruleSetInfo, ruleEngineGuid);
            Tests.Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TrackRuleFiring
        ///</summary>
        [TestMethod()]
        public void TrackRuleFiringTest()
        {
            XmlDebugTrackingInterceptor target = new XmlDebugTrackingInterceptor(); // TODO: Initialize to an appropriate value
            string ruleName = string.Empty; // TODO: Initialize to an appropriate value
            object conflictResolutionCriteria = null; // TODO: Initialize to an appropriate value
            target.TrackRuleFiring(ruleName, conflictResolutionCriteria);
            Tests.Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TrackFactActivity
        ///</summary>
        [TestMethod()]
        public void TrackFactActivityTest()
        {
            XmlDebugTrackingInterceptor target = new XmlDebugTrackingInterceptor(); // TODO: Initialize to an appropriate value
            FactActivityType activityType = new FactActivityType(); // TODO: Initialize to an appropriate value
            string classType = string.Empty; // TODO: Initialize to an appropriate value
            int classInstanceId = 0; // TODO: Initialize to an appropriate value
            target.TrackFactActivity(activityType, classType, classInstanceId);
            Tests.Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TrackConditionEvaluation
        ///</summary>
        [TestMethod()]
        public void TrackConditionEvaluationTest()
        {
            XmlDebugTrackingInterceptor target = new XmlDebugTrackingInterceptor(); // TODO: Initialize to an appropriate value
            string testExpression = string.Empty; // TODO: Initialize to an appropriate value
            string leftClassType = string.Empty; // TODO: Initialize to an appropriate value
            int leftClassInstanceId = 0; // TODO: Initialize to an appropriate value
            object leftValue = null; // TODO: Initialize to an appropriate value
            string rightClassType = string.Empty; // TODO: Initialize to an appropriate value
            int rightClassInstanceId = 0; // TODO: Initialize to an appropriate value
            object rightValue = null; // TODO: Initialize to an appropriate value
            bool result = false; // TODO: Initialize to an appropriate value
            target.TrackConditionEvaluation(testExpression, leftClassType, leftClassInstanceId, leftValue, rightClassType, rightClassInstanceId, rightValue, result);
            Tests.Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TrackAgendaUpdate
        ///</summary>
        [TestMethod()]
        public void TrackAgendaUpdateTest()
        {
            bool isAddition = false;
            string ruleName = "Test rule name";
            object conflictResolutionCriteria = null;

            target.source.Listeners.Add(new XmlWriterRollingTraceListener(
                @"Name = XmlRollingLogger;LogRootPath = c:\logs\bre; DatetimePattern = DDMMMYY-HHmm; StaticPattern = log.bre."));

            target.TrackAgendaUpdate(isAddition, ruleName, conflictResolutionCriteria);
        }

        /// <summary>
        ///A test for SetTrackingConfig
        ///</summary>
        [TestMethod()]
        public void SetTrackingConfigTest()
        {
            XmlDebugTrackingInterceptor_Accessor target = new XmlDebugTrackingInterceptor_Accessor();
            RuleSetTrackingConfiguration trackingConfig =
                new RuleSetTrackingConfiguration(Guid.NewGuid(), RuleSetTrackingConfiguration.TrackingOption.All);

            target.SetTrackingConfig(trackingConfig);

            Tests.Assert.AreEqual(trackingConfig, target.trackingConfig);

        }

        /// <summary>
        ///A test for PrintHeader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.Biztalk.dll")]
        public void PrintHeaderTest()
        {
            XmlDebugTrackingInterceptor_Accessor target = new XmlDebugTrackingInterceptor_Accessor();
            string hdr = "Test header";

            StringBuilder builder = new StringBuilder(300);

            using (XmlWriter xml = XmlWriter.Create(builder, 
                new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                target.PrintHeader(hdr, xml);
            }

            Console.Write(builder.ToString());
            Tests.Assert.AreEqual<string>("<Action>" + hdr + "</Action><EngineInstance /><Ruleset />",
                builder.ToString());
        }

        /// <summary>
        ///A test for XmlDebugTrackingInterceptor Constructor
        ///</summary>
        [TestMethod()]
        public void XmlDebugTrackingInterceptorConstructorTest()
        {
            XmlDebugTrackingInterceptor target = new XmlDebugTrackingInterceptor();
        }
    }
}
