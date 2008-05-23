using Tools.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Rhino.Mocks;
using Tools.Core.Configuration;

namespace Tools.Logging.Tests
{
    
    
    /// <summary>
    ///This is a test class for DatabaseTraceListenerTest and is intended
    ///to contain all DatabaseTraceListenerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseTraceListenerTest
    {
        private string storedProcedureName = "[Common].[uspInsertLogMessage]";
        private string logConnectionStringName = "LogDatabase";
        private int paramsCount = 0;
        List<DbParameter> parametersList;

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            parametersList = new List<DbParameter>();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            parametersList = null;
        }
        //
        #endregion


        private int HandleParameter(object parameter)
        {
            parametersList.Add(parameter as DbParameter);
            return 0;
        }
        private delegate int HandleParameterDelegate(object parameter);
        /// <summary>
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteLineTest()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            target.connectionStringName = "TestConnectionString";

            target.factory = MockRepository.GenerateStub<DbProviderFactory>();
            target.connectionStringProvider = MockRepository.GenerateStub<IConfigurationValueProvider>();
            target.connectionStringProvider.Stub((p) => p[target.connectionStringName]).
                Return(target.connectionStringName);
            

            DbConnection connection = MockRepository.GenerateStub<DbConnection>();
            DbCommand command = MockRepository.GenerateStub<DbCommand>();
            DbParameterCollection parameters = MockRepository.GenerateStub<DbParameterCollection>();

            // Stubs for the connection and command
            target.factory.Stub(
                (f) => f.CreateConnection()).Return(connection);
            target.factory.Stub((f) => f.CreateCommand()).Return(command);

            // Definitely want to verify that Open is called
            connection.Stub((c) => connection.Open());
            // Unfortunately can't verify Dispose got called
            connection.Stub((c) => connection.Dispose());
            // Will verify later if ExecuteNonQuery is called
            command.Stub((c) => command.ExecuteNonQuery()).Return(1);
            command.Stub((c) => c.Dispose());
            // Setup repetitive stub for the command parameters
            //DbParameter parameter = null;
            target.factory.Stub((f) => f.CreateParameter()).Repeat.Any().
                Return(MockRepository.GenerateStub<DbParameter>());
            command.Stub((c) => c.Parameters).Repeat.Any().Return(parameters);
            parameters.Stub((p) => p.Add(null)).IgnoreArguments().Repeat.Any().Do(
                new HandleParameterDelegate(HandleParameter));


            string message = "Test of listener message for WriteLine";
            target.WriteLine(message);

            Assert.IsTrue(!target.initializedInFailedMode, "Per current setup, listener is not expected to be initialized in the failed mode!");
           
            // verify connection string got assigned
            Assert.AreEqual<string>(target.connectionString, connection.ConnectionString);

            Trace.WriteLine(String.Format("Parameters count: {0}", parametersList.Count));

            connection.AssertWasCalled((c) => c.Open());
            command.AssertWasCalled((c) => c.ExecuteNonQuery());
            //target.connectionStringProvider.AssertWasCalled((p) => p[target.connectionStringName]);
            //Assert.IsTrue(parametersList.Find((p) => { return p.ParameterName == "Date"; }).Value != null);
        }


        /// <summary>
        ///A test for WriteInternal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteInternalTest1()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            object data = null; // TODO: Initialize to an appropriate value
            target.WriteInternal(eventCache, source, eventType, id, data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteInternal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteInternalTest()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            Guid correlationId = new Guid(); // TODO: Initialize to an appropriate value
            target.WriteInternal(eventCache, source, eventType, id, message, correlationId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.Write(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceTransfer
        ///</summary>
        [TestMethod()]
        public void TraceTransferTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            Guid relatedActivityId = new Guid(); // TODO: Initialize to an appropriate value
            target.TraceTransfer(eventCache, source, id, message, relatedActivityId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest1()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            string format = string.Empty; // TODO: Initialize to an appropriate value
            object[] args = null; // TODO: Initialize to an appropriate value
            target.TraceEvent(eventCache, source, eventType, id, format, args);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceEvent(eventCache, source, eventType, id, message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest1()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            object[] data = null; // TODO: Initialize to an appropriate value
            target.TraceData(eventCache, source, eventType, id, data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            object data = null; // TODO: Initialize to an appropriate value
            target.TraceData(eventCache, source, eventType, id, data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void InitializeTest()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(); // TODO: Initialize to an appropriate value
            target.Initialize();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        public void FailTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string detailMessage = string.Empty; // TODO: Initialize to an appropriate value
            target.Fail(message, detailMessage);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddContextParameters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void AddTransformerParametersTest()
        {
            IExtraDataTransformer extraFieldsProvider = new SampleLogDataProvider();

            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(
                storedProcedureName, logConnectionStringName, null, extraFieldsProvider);

            //object data = new CustomLogEntry { JobId = 12345, RepId = 54321 };
            //IDbCommand command = new SqlCommand();

            //target.AddTransformerParameters(data, command);

            //IDataParameter jobParam = command.Parameters["JobId"] as IDataParameter;
            //Assert.IsNotNull(jobParam);
            //Assert.IsNotNull(jobParam.Value);
            //Assert.AreEqual<string>(jobParam.Value.ToString(), "12345");

            //IDataParameter repParam = command.Parameters["RepId"] as IDataParameter;
            //Assert.IsNotNull(repParam);
            //Assert.IsNotNull(repParam.Value);
            //Assert.AreEqual<string>(repParam.Value.ToString(), "54321");

        }

        /// <summary>
        ///A test for AddContextParameters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void AddContextParametersTest()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            //TraceEventCache eventCache = null;
            //TraceEventType eventType = TraceEventType.Information;
            //int id = 101;
            //IDbCommand command = new SqlCommand();
            //target.AddContextParameters(eventCache, eventType, id, command);

            //IDataParameter midParam = command.Parameters["MessageId"] as IDataParameter;
            //Assert.IsNotNull(midParam);
            //Assert.IsNotNull(midParam.Value);
            //Assert.AreEqual<string>(midParam.Value.ToString(), "101");


        }

        /// <summary>
        ///A test for DatabaseTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void DatabaseTraceListenerConstructorTest()
        {
            DatabaseTraceListener target = new DatabaseTraceListener();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
        internal class CustomLogEntry
        {
            internal long JobId { get; set; }
            internal long RepId { get; set; }
            internal string IP { get; set; }
        }
        private class SampleLogDataProvider : IExtraDataTransformer
        {
            #region IExtraDataTransformer Members

            public Dictionary<string, object> TransformToDictionary(object extraDataContainer)
            {
                CustomLogEntry logEntry = extraDataContainer as CustomLogEntry;

                if (logEntry == null) return null;

                Dictionary<string, object> retValue = new Dictionary<string, object>();

                retValue.Add("JobId", logEntry.JobId);
                retValue.Add("RepId", logEntry.RepId);

                return retValue;
            }

            #endregion
        }
    }
}
