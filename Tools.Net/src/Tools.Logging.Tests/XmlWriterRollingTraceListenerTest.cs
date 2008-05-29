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
        XmlWriterRollingTraceListener_Accessor target;
        string log;

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
        ///A test for WriteLine
        ///</summary>
        [TestMethod()]
        public void WriteLineTest()
        {
            Setup(200);

            string message = "Test of WriteLine message";
            target.WriteLine(message);

            target.logFileHelper.Stub((h) => h.IsFileSuitableForWriting).Return(true);

            target.WriteLine(message);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Assert.IsTrue(log.Contains(message), "Log message is expected to have logged text!");

            Trace.WriteLine(log);
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            Setup(200);

            string message = "Test of Write message";
            target.Write(message);

            target.logFileHelper.Stub((h) => h.IsFileSuitableForWriting).Return(true);

            target.Write(message);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Assert.IsTrue(log.Contains(message), "Log message is expected to have logged text!");

            Trace.WriteLine(log);
        }

        /// <summary>
        ///A test for TraceTransfer
        ///</summary>
        [TestMethod()]
        public void TraceTransferTest()
        {
            Setup(200);

            string message = "Test of TraceTransfer message";

            Guid relatedActivityId = Guid.NewGuid();

            target.TraceTransfer(null, null, 100, message, relatedActivityId);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(message), "Log message is expected to have logged text!");
            Assert.IsTrue(log.Contains(relatedActivityId.ToString()), "Related activity id is expected to be present in the message");
        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventWithMessageTest()
        {
            Setup(200);

            string message = "Test of TraceEvent message";

            target.TraceEvent(null, null, TraceEventType.Information, 100, message);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(message), "Log message is expected to have logged text!");

        }

        /// <summary>
        ///A test for TraceEvent
        ///</summary>
        [TestMethod()]
        public void TraceEventWithFormatMessageTest()
        {
            Setup(200);


            string format = "Test of formatted message with argument [{0}]";
            object[] args = new object[] { "testargument"};

            target.TraceEvent(null, null, TraceEventType.Information, 100, format, args);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(String.Format(format, args)), "Log message is expected to have logged text!");
            
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceParamsDataTest()
        {
            Setup(200);

            string message = "Test exception";
            Exception ex = new Exception(message);
            string extraInfo = "This is extra info for the exception thrown!";

            target.TraceData(null, null, TraceEventType.Error, 100, ex, 
                extraInfo);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(message), "Log message is expected to have exception text!");
            Assert.IsTrue(log.Contains(ex.ToString()), "Log message is expected to have exception ToString() data recorded!");
            Assert.IsTrue(log.Contains(extraInfo), "When params data are used, every params argument is expected to be present in the log data recorded!");
        }

        /// <summary>
        ///A test for TraceData
        ///</summary>
        [TestMethod()]
        public void TraceExceptionDataTest()
        {
            Setup(200);
   
            string message = "Test exception";
            Exception ex = new Exception(message);

            target.TraceData(null, null, TraceEventType.Error, 100, ex);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(message), "Log message is expected to have exception text!");
            Assert.IsTrue(log.Contains(ex.ToString()), "Log message is expected to have exception ToString() data recorded!");

        }

        /// <summary>
        ///A test for GetEncodingWithFallback
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void GetEncodingWithFallbackTest()
        {
            Assert.AreEqual(Encoding.UTF8,
                XmlWriterRollingTraceListener_Accessor.GetEncodingWithFallback(Encoding.UTF8));
        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        public void FailTest()
        {
            Setup(200);

            string message = "Test of Fail message";
            string detail = "Test of Fail detail";

            target.Fail(message, detail);

            target.directoryHelper.AssertWasCalled((h) => h.CreateDirectory());

            target.Close();

            Trace.WriteLine(log);

            Assert.IsTrue(log.Contains(message + " " + detail), "Fail message and detail expected to be present in the log message");

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

        }
        /// <summary>
        /// Verifies if listener closes underlying streams
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.Logging.dll")]
        public void CloseInternalWritersTest()
        {
            XmlWriterRollingTraceListener_Accessor target =
                new XmlWriterRollingTraceListener_Accessor(20000, "TestXmlRollingWriter");

            target.fileName = "testfilename.xml";

            target.textWriterProvider =
                MockRepository.GenerateStub<Tools.Logging.XmlWriterRollingTraceListener.ITextWriterProvider>();


            TextWriter writer = MockRepository.GenerateStub<TextWriter>();
            target.textWriterProvider.Stub((p) => p.CreateWriter(target.fileName)).Return(writer);

            writer.Stub((w) => w.Close());

            target.EnsureWriter();

            target.Close();

            writer.AssertWasCalled((w) => w.Close());

            Assert.IsNull(target.writer);

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

        #region Helper methods

        private void Setup(int maxFileSize)
        {
            target = new XmlWriterRollingTraceListener_Accessor(
                maxFileSize, "TestListener");


            target.fileName = Guid.NewGuid().ToString() + ".xml";

            target.textWriterProvider =
                MockRepository.GenerateStub<Tools.Logging.XmlWriterRollingTraceListener.ITextWriterProvider>();
            target.directoryHelper =
                MockRepository.GenerateStub<Tools.Logging.XmlWriterRollingTraceListener.IDirectoryHelper>();
            target.logFileHelper =
MockRepository.GenerateStub<Tools.Logging.XmlWriterRollingTraceListener.ILogFileHelper>();

            TextWriter writer = MockRepository.GenerateStub<TextWriter>();
            target.textWriterProvider.Stub((p) => p.CreateWriter(null)).IgnoreArguments().Return(writer);
            target.directoryHelper.Stub((h) => h.CreateDirectory());

            target.logFileHelper.Stub((h) => h.IsFileSuitableForWriting).Return(false);

            log = null;

            writer.Stub((w) => w.Write(String.Empty)).IgnoreArguments().Repeat.Any().Do((Action<string>)delegate(string s) { log += s; });

        }

        #endregion
    }
}
