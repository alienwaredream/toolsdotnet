using Tools.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Rhino.Mocks;
using Tools.Core.Configuration;
using System.Linq;
using System.Globalization;

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
        List<DbParameter> parametersList;

        DatabaseTraceListener_Accessor target;
        DbConnection connection;
        DbCommand command;
        DbParameterCollection parameters;
        string paramsAggregateList = "Date,MessageId,TypeId,TypeName,MachineName,ModulePath,ModuleName,ThreadName,ThreadIdentity,OSIdentity,ActivityId,Message,";

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

            Setup();
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
            return parametersList.Count;
        }
        private delegate int HandleParameterDelegate(object parameter);
        /// <summary>
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteLineTest()
        {
            string message = "Test of listener message for WriteLine";
            target.WriteLine(message);

            Verify(message, TraceEventType.Information, 0);
        }


        /// <summary>
        ///A test for WriteInternal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteInternalTest1()
        {
            TraceDataTest();
        }

        /// <summary>
        ///A test for WriteInternal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteInternalTest()
        {
            TraceTransferTest();
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            string message = "Test of listener message for Write";
            target.Write(message);

            Verify(message, TraceEventType.Information, 0);
        }

        /// <summary>
        ///A test for TraceTransfer
        ///</summary>
        [TestMethod()]
        public void TraceTransferTest()
        {
            string source = string.Empty; // TODO: Initialize to an appropriate value
            int id = 300; // TODO: Initialize to an appropriate value
            string message = "Test of transfer message";
            Guid relatedActivityId = Guid.NewGuid();
            // Setup extra one for the Guid
            DbParameter parameter = MockRepository.GenerateStub<DbParameter>();
            target.factory.Stub((f) => f.CreateParameter()).Return(parameter);
            parameters.Stub((p) => p.Add(parameter)).Do(new HandleParameterDelegate(HandleParameter));
            paramsAggregateList += "CorrelationId,";
            // Call the test method
            target.TraceTransfer(null, source, id, message, relatedActivityId);
            // Verify standard expectations
            Verify(message, TraceEventType.Transfer, 300);
            // Verify specific to Transfer expectations
            Assert.AreEqual<string>(parametersList.Find(
                (p) => { return p.ParameterName == "CorrelationId"; }).Value.ToString(),
                relatedActivityId.ToString());
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest1()
        {
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            int id = 400; // TODO: Initialize to an appropriate value
            string format = "Test of TraceEvent with arg1 [{0}] and arg2 [{1}]";
            object[] args = new object[] {"argument1 value", 10};

            target.TraceEvent(eventCache, source, TraceEventType.Verbose, id, format, args);

            Verify(String.Format(CultureInfo.InvariantCulture, format, args), TraceEventType.Verbose, 400);
 
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest()
        {
            TraceEventCache eventCache = null;
            string source = string.Empty;
            int id = 500;
            string format = "Test of TraceEvent with arg1 [{0}] and arg2 [{1}]";
            object[] args = new object[] { "argument1 value", 10 };
            string message = String.Format(CultureInfo.InvariantCulture, format, args);

            target.TraceEvent(eventCache, source, TraceEventType.Start, id, format, args);

            Verify(message, TraceEventType.Start, 500);

        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest1()
        {
            TraceEventCache eventCache = null;
            string source = string.Empty;

            int id = 600;
            object[] data = new object[] {"data1", 100};
            target.TraceData(eventCache, source, TraceEventType.Stop, id, data);

            Verify(data.ToString(), TraceEventType.Stop, id);
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest()
        {
            TraceEventCache eventCache = null;
            string source = string.Empty;

            int id = 100;
            var data = new { Message = "Test message", Detail = "Test details" };
            target.TraceData(eventCache, source, TraceEventType.Information, id, data);

            Verify(data.ToString(), TraceEventType.Information, id);
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void InitializeTest()
        {
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor();

            target.Initialize();
            // No stubbing for the connection string provider so
            Assert.IsTrue(target.initializedInFailedMode);




        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        public void FailTest()
        {
            string message = "Fail message";
            string detailMessage = "Fail detail message";

            target.Fail(message, detailMessage);

            Verify(message+ " " + detailMessage, TraceEventType.Error, 0);

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
            Assert.IsNotNull(target.factory);
            Assert.IsNull(target.fallbackTraceListener);
            Assert.IsTrue(!String.IsNullOrEmpty(target.storedProcedureName));
            Assert.IsTrue(!String.IsNullOrEmpty(target.machineName));
            Assert.IsTrue(!String.IsNullOrEmpty(target.modulePath));
            Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, "storedProcedureName [{0}], machineName [{1}], modulePath [{2}]",
                target.storedProcedureName, target.machineName, target.modulePath));
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

        private void Setup()
        {
            parametersList = new List<DbParameter>();

            target = new DatabaseTraceListener_Accessor(
    storedProcedureName, logConnectionStringName, null, null);

            //target.connectionStringName = "TestConnectionString";

            target.factory = MockRepository.GenerateStub<DbProviderFactory>();
            target.connectionStringProvider = MockRepository.GenerateStub<IConfigurationValueProvider>();
            target.connectionStringProvider.Stub((p) => p[target.connectionStringName]).
                Return(target.connectionStringName);


            connection = MockRepository.GenerateStub<DbConnection>();
            command = MockRepository.GenerateStub<DbCommand>();
            parameters = MockRepository.GenerateStub<DbParameterCollection>();

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

            command.Stub((c) => c.Parameters).Repeat.Any().Return(parameters);

            for (int i = 0; i < 12; i++)
            {
                DbParameter parameter = MockRepository.GenerateStub<DbParameter>();
                target.factory.Stub((f) => f.CreateParameter()).Return(parameter);
                parameters.Stub((p) => p.Add(parameter)).Do(new HandleParameterDelegate(HandleParameter));
            }
        }
        private void Verify(string message, TraceEventType eventType, int id)
        {
            Assert.IsTrue(!target.initializedInFailedMode, "Per current setup, listener is not expected to be initialized in the failed mode!");

            // verify connection string got assigned
            Assert.AreEqual<string>(target.connectionString, connection.ConnectionString);

            Trace.WriteLine(String.Format("Parameters count: {0}", parametersList.Count));
            Trace.WriteLine("Parameters:" + parametersList.Aggregate(String.Empty, (s, p) => s += p.ParameterName + ","));

            connection.AssertWasCalled((c) => c.Open());
            command.AssertWasCalled((c) => c.ExecuteNonQuery());

            Assert.AreEqual<string>(paramsAggregateList,
                parametersList.Aggregate(String.Empty, (s, p) => s += p.ParameterName + ","));

            //target.connectionStringProvider.AssertWasCalled((p) => p[target.connectionStringName]);
            Assert.IsTrue(parametersList.Find((p) => { return p.ParameterName == "Date"; }).Value != null);

            Assert.AreEqual<int>(Convert.ToInt32(parametersList.Find((p) => { return p.ParameterName == "MessageId"; }).Value), id);
            Assert.AreEqual<int>(Convert.ToInt32(parametersList.Find((p) => { return p.ParameterName == "TypeId"; }).Value), (int)eventType);
            Assert.AreEqual<string>(parametersList.Find((p) => { return p.ParameterName == "TypeName"; }).Value.ToString(), eventType.ToString());
            Assert.AreEqual<string>(parametersList.Find((p) => { return p.ParameterName == "MachineName"; }).Value.ToString(), Environment.MachineName);


            Assert.AreEqual<string>(parametersList.Find((p) => { return p.ParameterName == "Message"; }).Value.ToString(), message);

        }
    }
}
