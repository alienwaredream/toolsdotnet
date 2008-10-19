using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using Tools.Core;
using Tools.Core.Configuration;
using Tools.Core.Utils;

namespace Tools.Logging
{
    //TARGET: (SD) To work as regular XmlWriterTraceListener, but if extra options are 
    // provided to roll to another file if log file size exceeds the configured max.
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public class XmlWriterRollingTraceListener : TraceListener
    {
        #region Fields

        // Fields from the XmlWriterTraceListener
        private const string fixedHeader =
            "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">";

        // Fields from the TraceEventCache
        private static int processId;
        private static string processName;
        private readonly IInitializationStringParser initStringParser = new InitializationStringParser();
        // Fields from the rolling file trace listener
        // Configuration fields
        private readonly bool isRolling;
        private readonly string machineName;
        private readonly object syncWriteObject = new object();
        private IXPathFormatter dataXPathFormatter = new LogDataXPathFormatter();
        private IDirectoryHelper directoryHelper;
        private string fileDatetimePattern = "dd-MMM-yyTHH-mm-ss";
        private string fileName;
        private string fileStaticName = "log_";
        private bool isDirectoryCreated;

        private ILogFileHelper logFileHelper;
        private string logFilePath;
        private Guid logGuid;
        private string logRootLocation;
        private int maxFileSizeBytes = 2000000;
        private StringBuilder strBldr;
        private ITextWriterProvider textWriterProvider;
        internal TextWriter writer;
        private XmlTextWriter xmlBlobWriter;

        #endregion

        #region Properties

        public IXPathFormatter DataXPathFormatter
        {
            get { return dataXPathFormatter; }
            set { dataXPathFormatter = value; }
        }

        #endregion

        #region Ctors

        public XmlWriterRollingTraceListener(Stream stream)
            : this(stream, String.Empty)
        {
        }

        public XmlWriterRollingTraceListener(TextWriter writer)
            : this(writer, string.Empty)
        {
        }

        public XmlWriterRollingTraceListener(string initializationString)
        {
            IDictionary<string, string> initParameters = initStringParser.Parse(initializationString);

            if (initParameters == null)
            {
                fileName = initializationString;
                return;
            }

            isRolling = true;

            Name = Guid.NewGuid().ToString();

            SetValueIfPresent((string s) => Name = s, "name", initParameters);

            SetValueIfPresent((string s) => logRootLocation = s, "logrootpath", initParameters);

            SetValueIfPresent((string s) => fileDatetimePattern = s, "datetimepattern", initParameters);

            SetValueIfPresent((string s) => fileStaticName = s, "staticpattern", initParameters);

            SetValueIfPresent((string s) => maxFileSizeBytes = Convert.ToInt32(s),
                              "maxsizebytes", initParameters);

            machineName = Environment.MachineName;
        }

        public XmlWriterRollingTraceListener(Stream stream, string name)
            : base(name)
        {
            machineName = Environment.MachineName;

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            writer = new StreamWriter(stream);
        }

        public XmlWriterRollingTraceListener(TextWriter writer, string name)
            : base(name)
        {
            machineName = Environment.MachineName;

            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            this.writer = writer;
        }

        public XmlWriterRollingTraceListener(string fileName, string name)
            : base(name)
        {
            machineName = Environment.MachineName;
            this.fileName = fileName;
        }

        public XmlWriterRollingTraceListener(int maxFileSizeBytes, string name)
            : this(maxFileSizeBytes, null, "dd-MMM-yyTHH-mm-ss", name + "_", name)
        {
        }

        public XmlWriterRollingTraceListener(int maxFileSizeBytes, string logRootLocation, string name)
            : this(maxFileSizeBytes, logRootLocation, "dd-MMM-yyTHH-mm-ss", "log_", name)
        {
        }

        public XmlWriterRollingTraceListener(int maxFileSizeBytes, string logRootLocation, string fileDatetimePattern,
                                             string fileStaticPattern, string name)
            : base(name)
        {
            machineName = Environment.MachineName;
            isRolling = true;
            this.maxFileSizeBytes = maxFileSizeBytes;
            this.fileDatetimePattern = fileDatetimePattern;
            this.logRootLocation = logRootLocation;
            fileStaticName = fileStaticPattern;
            this.fileDatetimePattern = fileDatetimePattern;
        }

        private static void SetValueIfPresent(Action<string> setAction, string keyName, IDictionary<string, string> dictionary)
        {
            string val = null;
            if (dictionary.TryGetValue(keyName, out val) && !String.IsNullOrEmpty(val))
            {
                setAction(val);
            }
        }

        #endregion

        #region Methods - Disposable implementation

        public override void Close()
        {
            if (writer != null)
            {
                writer.Close();
            }
            writer = null;

            if (xmlBlobWriter != null)
            {
                xmlBlobWriter.Close();
            }
            xmlBlobWriter = null;
            strBldr = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        #endregion

        #region Methods - TraceListener trace methods

        public override void Fail(string message, string detailMessage)
        {
            var builder = new StringBuilder(message);
            if (detailMessage != null)
            {
                builder.Append(" ");
                builder.Append(detailMessage);
            }
            TraceEvent(null, Log.Source.Name, TraceEventType.Error, 0, builder.ToString());
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                       object data)
        {
            DoWrite(() => TraceData2(eventCache, source, eventType, id, data));
        }

        private void TraceData2(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                WriteHeader(source, eventType, id, eventCache);
                InternalWrite("<TraceData>");
                if (data != null)
                {
                    InternalWrite("<DataItem>");
                    WriteData(data);
                    InternalWrite("</DataItem>");
                }
                InternalWrite("</TraceData>");
                WriteFooter(eventCache);
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                       params object[] data)
        {
            DoWrite(() => TraceData2(eventCache, source, eventType, id, data));
        }

        private void TraceData2(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                params object[] data)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                WriteHeader(source, eventType, id, eventCache);
                InternalWrite("<TraceData>");
                if (data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        InternalWrite("<DataItem>");
                        if (data[i] != null)
                        {
                            WriteData(data[i]);
                        }
                        InternalWrite("</DataItem>");
                    }
                }
                InternalWrite("</TraceData>");
                WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                        string message)
        {
            DoWrite(() => TraceEvent2(eventCache, source, eventType, id, message));
        }

        private void TraceEvent2(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                 string message)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                WriteHeader(source, eventType, id, eventCache);
                WriteEscaped(message);
                WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                        string format, params object[] args)
        {
            DoWrite(() => TraceEvent2(eventCache, source, eventType, id, format, args));
        }

        private void TraceEvent2(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                 string format, params object[] args)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                WriteHeader(source, eventType, id, eventCache);
                string str = args != null ? string.Format(CultureInfo.InvariantCulture, format, args) : format;
                WriteEscaped(str);
                WriteFooter(eventCache);
            }
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message,
                                           Guid relatedActivityId)
        {
            DoWrite(() => TraceTransfer2(eventCache, source, id, message, relatedActivityId));
        }

        private void TraceTransfer2(TraceEventCache eventCache, string source, int id, string message,
                                    Guid relatedActivityId)
        {
            WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
            WriteEscaped(message);
            WriteFooter(eventCache);
        }

        public override void Write(string message)
        {
            WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            TraceEvent(null, Log.Source.Name, TraceEventType.Information, 0, message);
        }

        #endregion

        #region Methods - Helper write methods

        private void DoWrite(Action writeAction)
        {
            if (isRolling)
            {
                lock (syncWriteObject)
                {
                    if (!isDirectoryCreated) CreateLogDirectory();

                    if (logFileHelper == null || !logFileHelper.IsFileSuitableForWriting)
                    {
                        CreateNewWriter();
                    }

                    writeAction();
                    return;
                }
            }
            writeAction();
        }

        private void CreateLogDirectory()
        {
            if (String.IsNullOrEmpty(logRootLocation))
            {
                logRootLocation = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                                  + @"\logs";
            }
            // logFileHelper is either injected or default instance is created
            if (directoryHelper == null)
            {
                directoryHelper = new FileDirectoryHelper(logRootLocation);
            }

            directoryHelper.CreateDirectory();

            isDirectoryCreated = true;
        }

        private void InternalWrite(string message)
        {
            // only call EnsureWriter if file is not rolling which is a default.
            if (!isRolling)
            {
                EnsureWriter();

                writer.Write(message);
                writer.Flush();
                return;
            }
            writer.Write(message);
            writer.Flush();
        }

        /// <summary>
        /// Ensures the writer in a way how standard .net 2.0 XmlWriterTraceListener does it.
        /// </summary>
        /// <returns></returns>
        private bool EnsureWriter()
        {
            bool flag = true;
            if (writer == null)
            {
                flag = false;
                if (fileName == null)
                {
                    return flag;
                }

                string fullPath = Path.GetFullPath(fileName);
                string directoryName = Path.GetDirectoryName(fullPath);
                fileName = Path.GetFileName(fullPath);

                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        if (textWriterProvider == null)
                        {
                            textWriterProvider = new FileTextWriterProvider(
                                true, GetEncodingWithFallback(new UTF8Encoding(false)), 0x1000);
                        }
                        writer = textWriterProvider.CreateWriter(fullPath);
                        flag = true;
                        break;
                    }
                    catch (IOException)
                    {
                        fileName = Guid.NewGuid() + fileName;
                        fullPath = Path.Combine(directoryName, fileName);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        break;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                if (!flag)
                {
                    this.fileName = null;
                }
            }
            return flag;
        }

        private void WriteData(object data)
        {
            var navigator = data as XPathNavigator;

            if (navigator == null && dataXPathFormatter != null)
            {
                navigator = dataXPathFormatter.Format(data);
            }

            if (navigator == null)
            {
                WriteEscaped(data.ToString());
            }
            else
            {
                if (strBldr == null)
                {
                    strBldr = new StringBuilder();

                    xmlBlobWriter = new XmlTextWriter(new StringWriter(strBldr, CultureInfo.CurrentCulture));
                }
                else
                {
                    strBldr.Length = 0;
                }
                try
                {
                    navigator.MoveToRoot();
                    xmlBlobWriter.WriteNode(navigator, false);
                    //this.xmlBlobWriter.Flush(); //**
                    InternalWrite(strBldr.ToString());
                }
                catch (Exception)
                {
                    InternalWrite(data.ToString());
                }
            }
        }

        private void WriteEndHeader(TraceEventCache eventCache)
        {
            InternalWrite("\" />");
            InternalWrite("<Execution ProcessName=\"");
            InternalWrite(GetProcessName());
            InternalWrite("\" ProcessID=\"");
            InternalWrite(((uint) GetProcessId()).ToString(CultureInfo.InvariantCulture));
            InternalWrite("\" ThreadID=\"");
            if (eventCache != null)
            {
                WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                WriteEscaped(GetThreadId().ToString(CultureInfo.InvariantCulture));
            }
            InternalWrite("\" />");
            InternalWrite("<Channel/>");
            InternalWrite("<Computer>");
            InternalWrite(machineName);
            InternalWrite("</Computer>");
            InternalWrite("</System>");
            InternalWrite("<ApplicationData>");
        }

        private void WriteEscaped(string str)
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    InternalWrite(XmlUtility.Encode(str[i]));
                }
            }
        }


        private void WriteFooter(TraceEventCache eventCache)
        {
            bool flag = IsEnabled(TraceOptions.LogicalOperationStack);
            bool flag2 = IsEnabled(TraceOptions.Callstack);
            if ((eventCache != null) && (flag || flag2))
            {
                InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
                if (flag)
                {
                    InternalWrite("<LogicalOperationStack>");
                    Stack logicalOperationStack = eventCache.LogicalOperationStack;
                    if (logicalOperationStack != null)
                    {
                        foreach (object obj2 in logicalOperationStack)
                        {
                            InternalWrite("<LogicalOperation>");
                            WriteEscaped(obj2.ToString());
                            InternalWrite("</LogicalOperation>");
                        }
                    }
                    InternalWrite("</LogicalOperationStack>");
                }
                InternalWrite("<Timestamp>");
                InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
                InternalWrite("</Timestamp>");
                if (flag2)
                {
                    InternalWrite("<Callstack>");
                    WriteEscaped(eventCache.Callstack);
                    InternalWrite("</Callstack>");
                }
                InternalWrite("</System.Diagnostics>");
            }
            InternalWrite("</ApplicationData></E2ETraceEvent>");
        }

        private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
        {
            WriteStartHeader(source, eventType, id, eventCache);
            WriteEndHeader(eventCache);
        }

        private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache,
                                 Guid relatedActivityId)
        {
            WriteStartHeader(source, eventType, id, eventCache);
            InternalWrite("\" RelatedActivityID=\"");
            InternalWrite(relatedActivityId.ToString("B"));
            WriteEndHeader(eventCache);
        }

        private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
        {
            InternalWrite(
                "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
            InternalWrite("<EventID>");
            InternalWrite(((uint) id).ToString(CultureInfo.InvariantCulture));
            InternalWrite("</EventID>");
            InternalWrite("<Type>3</Type>");
            InternalWrite("<SubType Name=\"");
            InternalWrite(eventType.ToString());
            InternalWrite("\">0</SubType>");
            InternalWrite("<Level>");
            var num = (int) eventType;
            if (num > 0xff)
            {
                num = 0xff;
            }
            if (num < 0)
            {
                num = 0;
            }
            InternalWrite(num.ToString(CultureInfo.InvariantCulture));
            InternalWrite("</Level>");
            InternalWrite("<TimeCreated SystemTime=\"");
            if (eventCache != null)
            {
                InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
            }
            else
            {
                InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
            }
            InternalWrite("\" />");
            InternalWrite("<Source Name=\"");
            WriteEscaped(source);
            InternalWrite("\" />");
            InternalWrite("<Correlation ActivityID=\"");
            if (eventCache != null)
            {
                InternalWrite(Trace.CorrelationManager.ActivityId.ToString("B"));
            }
            else
            {
                InternalWrite(Guid.Empty.ToString("B"));
            }
        }

        private bool IsEnabled(TraceOptions opts)
        {
            return ((opts & TraceOutputOptions) != TraceOptions.None);
        }

        private static Encoding GetEncodingWithFallback(ICloneable encoding)
        {
            var encoding2 = (Encoding) encoding.Clone();
            encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
            encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
            return encoding2;
        }

        private void CreateNewWriter()
        {
            if (writer != null)
            {
                if (xmlBlobWriter != null)
                {
                    xmlBlobWriter.Flush();
                }
                writer.Flush();
                writer.Close();
                //writer = null;
            }

            string targetFileName = fileStaticName + DateTime.UtcNow.ToString(fileDatetimePattern);
            string pathCandidate = null;

            int maxIter = 10;

            for (int i = 1; i < maxIter; i++)
            {
                if (!File.Exists(logRootLocation + targetFileName + "_" + i + ".xml"))
                {
                    pathCandidate = Path.Combine(logRootLocation,
                                                 targetFileName + "_" + i + ".xml");
                    break;
                }
            }
            if (pathCandidate == null)
            {
                // fallback name, uses guid
                pathCandidate = Path.Combine(logRootLocation,
                                             fileStaticName + Guid.NewGuid() + ".xml");
            }

            logFilePath = pathCandidate;

            if (textWriterProvider == null)
            {
                textWriterProvider = new FileTextWriterProvider(
                    true, GetEncodingWithFallback(new UTF8Encoding(false)), 0x1000);
            }

            writer = textWriterProvider.CreateWriter(logFilePath);

            if (logFileHelper == null)
            {
                logFileHelper = new LogFileHelper();
            }
            logFileHelper.MaxFileSizeBytes = maxFileSizeBytes;
            logFileHelper.FilePath = logFilePath;
        }

        #endregion

        #region Methods - Helper information methods

        private int GetThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }

        private static int GetProcessId()
        {
            InitProcessInfo();
            return processId;
        }

        private static string GetProcessName()
        {
            InitProcessInfo();
            return processName;
        }

        private static void InitProcessInfo()
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();

            if (processName == null)
            {
                using (Process process =
                    Process.GetCurrentProcess())
                {
                    processId = process.Id;
                    processName = process.ProcessName;
                }
            }
        }

        #endregion

        #region Helper classes

        #region Nested type: FileDirectoryHelper

        public class FileDirectoryHelper : IDirectoryHelper
        {
            public FileDirectoryHelper(string directoryPath)
            {
                DirectoryPath = directoryPath;
            }

            public string DirectoryPath { get; set; }

            #region IDirectoryHelper Members

            public void CreateDirectory()
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
            }

            #endregion
        }

        #endregion

        #region Nested type: FileTextWriterProvider

        public class FileTextWriterProvider : ITextWriterProvider
        {
            public FileTextWriterProvider(bool append, Encoding encoding, int bufferLength)
            {
                Append = append;
                Encoding = encoding;
                BufferLength = bufferLength;
            }

            private bool Append { get; set; }
            private Encoding Encoding { get; set; }
            private int BufferLength { get; set; }

            #region ITextWriterProvider Members

            public TextWriter CreateWriter(string fullPath)
            {
                return new StreamWriter(fullPath, Append, Encoding, BufferLength);
            }

            #endregion
        }

        #endregion

        #region Nested type: IDirectoryHelper

        public interface IDirectoryHelper
        {
            void CreateDirectory();
        }

        #endregion

        #region Nested type: ILogFileHelper

        public interface ILogFileHelper
        {
            bool IsFileSuitableForWriting { get; }
            string FilePath { get; set; }
            int MaxFileSizeBytes { get; set; }
        }

        #endregion

        #region Nested type: ITextWriterProvider

        public interface ITextWriterProvider
        {
            TextWriter CreateWriter(string fullPath);
        }

        #endregion

        //public abstract class BaseLogFileHelper
        //{
        //    public static ILogFileHelper Create(string filePath, int maxFileSizeBytes)
        //    {
        //        return new LogFileHelper(filePath, maxFileSizeBytes);
        //    }
        //}

        #region Nested type: LogFileHelper

        public class LogFileHelper : ILogFileHelper
        {
            public LogFileHelper()
            {
            }

            public LogFileHelper(string filePath, int maxFileSizeBytes)
            {
                FilePath = filePath;
                MaxFileSizeBytes = maxFileSizeBytes;
            }

            #region ILogFileHelper Members

            public string FilePath { get; set; }
            public int MaxFileSizeBytes { get; set; }

            public bool IsFileSuitableForWriting
            {
                get
                {
                    // checks only size for a moment
                    var fi = new FileInfo(FilePath);

                    if (fi.Exists)
                    {
                        return (fi.Length < MaxFileSizeBytes);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            #endregion
        }

        #endregion

        #endregion
    }
}