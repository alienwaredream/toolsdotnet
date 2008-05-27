using Tools.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Diagnostics;
using System;
using Rhino.Mocks;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for XmlWriterRollingTraceListenerTest and is intended
    ///to contain all XmlWriterRollingTraceListenerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlWriterRollingTraceListenerTest
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
        ///A test for WriteStartHeader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteStartHeaderTest()
        {
            PrivateObject param0 = null;
            XmlWriterRollingTraceListener_Accessor target = 
                new XmlWriterRollingTraceListener_Accessor(param0); 
            string source = string.Empty;

            //target

            //target.WriteStartHeader(source, eventType, id, eventCache);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteLineTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.WriteLine(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteHeader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteHeaderTest1()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            Guid relatedActivityId = new Guid(); // TODO: Initialize to an appropriate value
            target.WriteHeader(source, eventType, id, eventCache, relatedActivityId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteHeader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteHeaderTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            target.WriteHeader(source, eventType, id, eventCache);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteFooter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteFooterTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            target.WriteFooter(eventCache);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteEscaped
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteEscapedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            string str = string.Empty; // TODO: Initialize to an appropriate value
            target.WriteEscaped(str);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteEndHeader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteEndHeaderTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            target.WriteEndHeader(eventCache);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void WriteDataTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            object data = null; // TODO: Initialize to an appropriate value
            target.WriteData(data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
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
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
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
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceEvent(eventCache, source, eventType, id, message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
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
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest1()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            object data = null; // TODO: Initialize to an appropriate value
            target.TraceData(eventCache, source, eventType, id, data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceDataTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            TraceEventCache eventCache = null; // TODO: Initialize to an appropriate value
            string source = string.Empty; // TODO: Initialize to an appropriate value
            TraceEventType eventType = new TraceEventType(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            object[] data = null; // TODO: Initialize to an appropriate value
            target.TraceData(eventCache, source, eventType, id, data);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsEnabled
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void IsEnabledTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            TraceOptions opts = new TraceOptions(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsEnabled(opts);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InternalWrite
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void InternalWriteTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.InternalWrite(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitProcessInfo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void InitProcessInfoTest()
        {
            XmlWriterRollingTraceListener_Accessor.InitProcessInfo();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetThreadId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void GetThreadIdTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetThreadId();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetProcessName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void GetProcessNameTest()
        {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = XmlWriterRollingTraceListener_Accessor.GetProcessName();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetProcessId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void GetProcessIdTest()
        {
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = XmlWriterRollingTraceListener_Accessor.GetProcessId();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEncodingWithFallback
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void GetEncodingWithFallbackTest()
        {
            Encoding encoding = null; // TODO: Initialize to an appropriate value
            Encoding expected = null; // TODO: Initialize to an appropriate value
            Encoding actual;
            actual = XmlWriterRollingTraceListener_Accessor.GetEncodingWithFallback(encoding);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        public void FailTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string detailMessage = string.Empty; // TODO: Initialize to an appropriate value
            target.Fail(message, detailMessage);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EnsureWriter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void EnsureWriterTest()
        {
            XmlWriterRollingTraceListener_Accessor target = 
                new XmlWriterRollingTraceListener_Accessor(20000, "TestXmlRollingWriter"); 
            
            bool expected = false;

            bool actual = target.EnsureWriter();

            Assert.AreEqual(expected, actual);
            Assert.IsNull(target.writer);

            target.fileName = "testfilename.xml";

            Assert.IsTrue(target.EnsureWriter());
            Assert.IsNotNull(target.writer);

            target.Close();

            Assert.IsNull(target.writer);

            target.writer = MockRepository.GenerateStub<StreamWriter>(target.fileName, true,
                XmlWriterRollingTraceListener_Accessor.GetEncodingWithFallback(new UTF8Encoding(false)), 0x1000);

            target.writer.Stub((w) => w.Close());

            StreamWriter writerRef = target.writer as StreamWriter;

            target.EnsureWriter();

            target.Close();

            writerRef.AssertWasCalled((w) => w.Close());

            writerRef.Close();

            
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void DisposeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener_Accessor target = new XmlWriterRollingTraceListener_Accessor(param0); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Close
        ///</summary>
        [TestMethod()]
        public void CloseTest()
        {
            MockRepository mocks = new MockRepository();

            Stream stream = mocks.DynamicMock<Stream>();

            Expect.Call(stream.CanWrite).Return(true);

            Expect.Call(stream.Close);

            mocks.ReplayAll();

            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream); // TODO: Initialize to an appropriate value
            target.Close();

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TraceCompareTest()
        {
            TraceEventCache eventCache = new TraceEventCache();
            string output1 = null;
            string output2 = null;

            using (MemoryStream stream1 = new MemoryStream())
            {

                using (TraceListener tracer = new XmlWriterTraceListener(stream1))
                {

                    tracer.TraceData(eventCache, "TestSource", TraceEventType.Information, 1,
                        new { Message = "Test of Message", Id = 123 });
                    tracer.Flush();
                    //stream.Position = 0;
                    output1 = Encoding.UTF8.GetString(stream1.ToArray());
                    Trace.WriteLine(output1);
                }
                
            }
            using (MemoryStream stream2 = new MemoryStream())
            {

                using (TraceListener tracer = new XmlWriterTraceListener(stream2))
                {

                    tracer.TraceData(eventCache, "TestSource", TraceEventType.Information, 1,
                        new { Message = "Test of Message", Id = 123 });
                    tracer.Flush();
                    //stream.Position = 0;
                    output2 = Encoding.UTF8.GetString(stream2.ToArray());
                    Trace.WriteLine(output1);
                }
                
            }

            Assert.AreEqual<string>(output1, output2);            
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest5()
        {
            string filename = string.Empty; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(filename);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest4()
        {
            TextWriter writer = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(writer);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest3()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest2()
        {
            string fileName = string.Empty; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(fileName, name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest1()
        {
            TextWriter writer = null; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(writer, name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for XmlWriterRollingTraceListener Constructor
        ///</summary>
        [TestMethod()]
        public void XmlWriterRollingTraceListenerConstructorTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            XmlWriterRollingTraceListener target = new XmlWriterRollingTraceListener(stream, name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
