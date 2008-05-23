using Tools.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Rhino.Mocks;
using System.Collections;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for DatabaseTraceListener2Test and is intended
    ///to contain all DatabaseTraceListener2Test Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseTraceListener2TraceSourceTest
    {
        private string storedProcedureName = "[Common].[uspInsertLogMessage]";
        private string logConnectionStringName = "LogDatabase";

        private TraceListener databaseListener;
        private TraceSource logSource;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

        }

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
            Trace.WriteLine("Inside MyTestInitialize");

            databaseListener = new DatabaseTraceListener(storedProcedureName, logConnectionStringName,
                null, null);
            logSource = new TraceSource("TestSource");
            logSource.Listeners.Add(databaseListener);

        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteTraceTest()
        {
            Assert.IsTrue(logSource.Listeners.Contains(databaseListener));
            logSource.Switch.Level = SourceLevels.All;

            logSource.TraceData(TraceEventType.Information, 20, new { Message = "TraceData message with anon type" });
            logSource.TraceData(TraceEventType.Error, 21, "TraceData plain message");
            logSource.TraceEvent(TraceEventType.Critical, 22, "TraceEvent plain message");
            logSource.TraceEvent(TraceEventType.Resume, 23, "TraceEvent with format [{0}]", "format object");

            logSource.TraceInformation("Trace information with plain message");
            logSource.TraceInformation("Trace information with format [{0}]", "format object");

            logSource.TraceTransfer(24, "Trace transfer", Guid.NewGuid());
        }

        
        /// <summary>
        ///A test for AddTransformerParameters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Common.dll")]
        public void AddTransformerParametersTest()
        {
            IExtraDataTransformer extraFieldsProvider = new SampleLogDataProvider();

            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(
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
            DatabaseTraceListener_Accessor target = new DatabaseTraceListener_Accessor(
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


        private class CustomLogEntry
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
