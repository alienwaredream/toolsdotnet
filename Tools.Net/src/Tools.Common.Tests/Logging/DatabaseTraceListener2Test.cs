using Tools.Common.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Rhino.Mocks;
using Tools.Common.DataAccess;
using System.Collections;
using System.Data.Common;
using Tools.Common.Utils;

namespace Tools.Common.UnitTests
{


    /// <summary>
    ///This is a test class for DatabaseTraceListener2Test and is intended
    ///to contain all DatabaseTraceListener2Test Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseTraceListener2Test
    {
        private string storedProcedureName = "[Common].[uspInsertLogMessage]";
        private string logConnectionStringName = "LogDatabase";
        private int paramsCount = 0;

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

        private int HandleParameter(object parameter)
        {
            Trace.WriteLine(ReflectionUtility.DumpObjectFieldsAndProperties(parameter));
            return ++paramsCount;
        }
        private delegate int HandleParameterDelegate(object parameter);

        /// <summary>
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteLineTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            target.connectionString = "Test of connection string";

            target.factory = MockRepository.GenerateStub<DbProviderFactory>();

            DbConnection connection = MockRepository.GenerateStub<DbConnection>();
            DbCommand command = MockRepository.GenerateStub<DbCommand>();
            DbParameterCollection parameters = MockRepository.GenerateStub<DbParameterCollection>();


            target.factory.Stub(
                (f) => f.CreateConnection()).Return(connection);
            target.factory.Stub((f) => f.CreateCommand()).Return(command);

            connection.Stub((c) => connection.Open());
            connection.Stub((c) => connection.Dispose());
            command.Stub((c) => command.ExecuteNonQuery()).Return(1);
            command.Stub((c) => c.Dispose());

            target.factory.Stub((f) => f.CreateParameter()).Repeat.Any().
                Return(MockRepository.GenerateStub<DbParameter>());
            command.Stub((c) => c.Parameters).Repeat.Any().Return(parameters);
            parameters.Stub((p) => p.Add(null)).IgnoreArguments().Repeat.Any().Do(
                new HandleParameterDelegate(HandleParameter));


            string message = "Test of listener message for WriteLine";
            target.WriteLine(message);

            Assert.AreEqual<string>(target.connectionString, connection.ConnectionString);
            Trace.WriteLine(String.Format("Parameters count: {0}", paramsCount));
            connection.AssertWasCalled((c) => c.Open());
            command.AssertWasCalled((c) => c.ExecuteNonQuery());

        }

        /// <summary>
        ///A test for WriteInternal with null data object
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void WriteInternalTestWithNullData()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Information;
            int id = 99;
            object data = null;
            target.WriteInternal(eventCache, source, eventType, id, data);
        }
        /// <summary>
        ///A test for WriteInternal with null data object
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void WriteInternalTestWithNonNullData()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Information;
            int id = 99;
            object data = new { Message = "Test log entry", Id = 13 };

            target.WriteInternal(eventCache, source, eventType, id, data);
        }

        /// <summary>
        ///A test for WriteInternal for activity transfer overload
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void WriteInternalTestForActivityTransfer()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Transfer;
            int id = 99;
            string message = "Test of activity transfer.";

            target.WriteInternal(eventCache, source, eventType, id, message, Guid.NewGuid());
        }
        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            string message = "Test of message for listener.Write";
            target.Write(message);
        }

        /// <summary>
        ///A test for TraceTransfer
        ///</summary>
        [TestMethod()]
        public void TraceTransferTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;

            string source = Log.Source.Name;
            int id = 13;
            string message = "Test of TraceTransfer";
            Guid relatedActivityId = Guid.NewGuid();
            target.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventWithFormatAndArgsTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            Trace.CorrelationManager.StartLogicalOperation(Guid.NewGuid());

            TraceEventCache eventCache = new TraceEventCache();

            string source = string.Empty;
            TraceEventType eventType = TraceEventType.Information;
            int id = 0;
            string format = "format string with placeholder [{0}] and [{1}]";
            object[] args = new object[] { "filler 1", new { filler2f1 = "filler 2 field 1", filler2f2 = "filler 2 field 1" } };

            target.TraceEvent(eventCache, source, eventType, id, format, args);

            Trace.CorrelationManager.StopLogicalOperation();
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Verbose;
            int id = 12;
            string message = "Test of TraceEventTest";
            target.TraceEvent(eventCache, source, eventType, id, message);
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest1()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Critical;
            int id = 11;
            object[] data = new object[] { "test", "of", "object", "array", "data" };

            target.TraceData(eventCache, source, eventType, id, data);
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            string source = Log.Source.Name;
            TraceEventType eventType = TraceEventType.Error;
            int id = 10; // TODO: Initialize to an appropriate value
            object data = new { Message = "test of message", f = "test of extra field" }; // TODO: Initialize to an appropriate value
            target.TraceData(eventCache, source, eventType, id, data);
        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        public void FailTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            string message = "Test of Fail message.";
            string detailMessage = "Failure detail";
            target.Fail(message, detailMessage);
        }

        /// <summary>
        ///A test for AddTransformerParameters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void AddTransformerParametersTest()
        {
            IExtraDataTransformer extraFieldsProvider = new SampleLogDataProvider();

            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, extraFieldsProvider);

            object data = new CustomLogEntry { JobId = 12345, RepId = 54321 };
            IDbCommand command = new SqlCommand();
            //MockRepository mocks = new MockRepository();

            //IDbCommand command = mocks.StrictMock<IDbCommand>();
            //IDataParameterCollection parameters = mocks.StrictMock<IDataParameterCollection>();

            //Expect.Call(command.Parameters).Return(parameters);

            //Expect.Call(command.Parameters.Add(target.factory.CreateParameter(
            //                    (p) => { p.DbType = DbType.String; p.Value = 12345; p.ParameterName = "JobId"; })));
            //Expect.Call(command.Parameters).Return(parameters);
            //Expect.Call(command.Parameters.Add(target.factory.CreateParameter(
            //        (p) => { p.DbType = DbType.String; p.Value = 54321; p.ParameterName = "RepId"; })));
            //mocks.ReplayAll();

            target.AddTransformerParameters(data, command);

            //mocks.VerifyAll();
            IDataParameter jobParam = command.Parameters["JobId"] as IDataParameter;
            Assert.IsNotNull(jobParam);
            Assert.IsNotNull(jobParam.Value);
            Assert.AreEqual<string>(jobParam.Value.ToString(), "12345");

            IDataParameter repParam = command.Parameters["RepId"] as IDataParameter;
            Assert.IsNotNull(repParam);
            Assert.IsNotNull(repParam.Value);
            Assert.AreEqual<string>(repParam.Value.ToString(), "54321");

        }

        /// <summary>
        ///A test for AddContextParameters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void AddContextParametersTest()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor(
                storedProcedureName, logConnectionStringName, null, null);

            TraceEventCache eventCache = null;
            TraceEventType eventType = TraceEventType.Information;
            int id = 101;
            IDbCommand command = new SqlCommand();
            target.AddContextParameters(eventCache, eventType, id, command);

            IDataParameter midParam = command.Parameters["MessageId"] as IDataParameter;
            Assert.IsNotNull(midParam);
            Assert.IsNotNull(midParam.Value);
            Assert.AreEqual<string>(midParam.Value.ToString(), "101");


        }

        /// <summary>
        ///A test for DatabaseTraceListener2 Constructor
        ///</summary>
        [TestMethod()]
        public void DatabaseTraceListener2ConstructorTest1()
        {
            DatabaseTraceListener2_Accessor target = new DatabaseTraceListener2_Accessor();
            Assert.IsNotNull(target.factory);
            Assert.IsNotNull(target.fallbackTraceListener);
        }

        /// <summary>
        ///A test for DatabaseTraceListener2 Constructor
        ///</summary>
        [TestMethod()]
        public void DatabaseTraceListener2ConstructorTest()
        {
            string storedProcedureName = this.storedProcedureName;
            string connectionStringName = this.logConnectionStringName;
            TraceListener fallbackListener = null;
            IExtraDataTransformer extraLogDataTransformer = null;
            DatabaseTraceListener2 target = new DatabaseTraceListener2(storedProcedureName, connectionStringName, fallbackListener, extraLogDataTransformer);

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
