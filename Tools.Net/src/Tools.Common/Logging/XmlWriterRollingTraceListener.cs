using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Globalization;
using System.Xml.XPath;
using System.Threading;
using System.Collections;

namespace Tools.Common.Logging
{
    //TARGET: (SD) To work as regular XmlWriterTraceListener, but if extra options are 
    // provided to roll to another file if log file size exceeds the configured max.
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public class XmlWriterRollingTraceListener : TraceListener
    {
        #region Fields
        // Fields from the XmlWriterTraceListener
        private const string fixedHeader = "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">";
        private readonly string machineName;
        private StringBuilder strBldr;
        private XmlTextWriter xmlBlobWriter;
        // Fields from the TextWriterTraceListener
        private string fileName;
        internal TextWriter writer;
        // Fields from the TraceEventCache
        private static int processId;
        private static string processName;
        // Fields from the rolling file trace listener
        // Configuration fields
        private bool isRolling = false;
        private string fileDatetimePattern = "dd-MMM-yyTHH-mm-ss";
        private string fileStaticName = "log_";
        private int maxFileSizeBytes = 2000000;
        private string logRootLocation = null;

        //runtime fields
        private Guid logGuid;
        private object syncWriteObject = new object();
        private string logFilePath = null;
        private bool isDirectoryCreated = false;

        private IXPathFormatter dataXPathFormatter = new LogDataXPathFormatter();

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

        public XmlWriterRollingTraceListener(string filename)
            : base(filename)
        {
            this.machineName = Environment.MachineName;
        }

        public XmlWriterRollingTraceListener(Stream stream, string name)
            : base(name)
        {
            this.machineName = Environment.MachineName;

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.writer = new StreamWriter(stream);
        }

        public XmlWriterRollingTraceListener(TextWriter writer, string name)
            : base(name)
        {
            this.machineName = Environment.MachineName;

            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            this.writer = writer;
        }

        public XmlWriterRollingTraceListener(string fileName, string name)
            : base(name)
        {
            this.machineName = Environment.MachineName;
            this.fileName = fileName;
        }
        public XmlWriterRollingTraceListener(int maxFileSizeBytes, string logRootLocation, string name)
            : this(maxFileSizeBytes, logRootLocation, "dd-MMM-yyTHH-mm-ss", "log_", name)
        {
        }
        public XmlWriterRollingTraceListener(int maxFileSizeBytes, string logRootLocation, string fileDatetimePattern, string fileStaticPattern, string name)
            : base(name)
        {
            this.machineName = Environment.MachineName;
            this.isRolling = true;
            this.maxFileSizeBytes = maxFileSizeBytes;
            this.fileDatetimePattern = fileDatetimePattern;
            this.logRootLocation = logRootLocation;
            this.fileStaticName = fileStaticPattern;
            this.fileDatetimePattern = fileDatetimePattern;
        }
        #endregion

        #region Methods - Disposable implementation
        
        public override void Close()
        {
            if (this.writer != null)
            {
                this.writer.Close();
            }
            this.writer = null;

            if (this.xmlBlobWriter != null)
            {
                this.xmlBlobWriter.Close();
            }
            this.xmlBlobWriter = null;
            this.strBldr = null;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Close();
            }
        } 
        #endregion

        #region Methods - TraceListener trace methods
        
        public override void Fail(string message, string detailMessage)
        {
            StringBuilder builder = new StringBuilder(message);
            if (detailMessage != null)
            {
                builder.Append(" ");
                builder.Append(detailMessage);
            }
            this.TraceEvent(null, Log.Source.Name, TraceEventType.Error, 0, builder.ToString());
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            DoWrite(() => TraceData2(eventCache, source, eventType, id, data));
        }

        private void TraceData2(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                this.WriteHeader(source, eventType, id, eventCache);
                this.InternalWrite("<TraceData>");
                if (data != null)
                {
                    this.InternalWrite("<DataItem>");
                    this.WriteData(data);
                    this.InternalWrite("</DataItem>");
                }
                this.InternalWrite("</TraceData>");
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            DoWrite(() => TraceData2(eventCache, source, eventType, id, data));
        }
        private void TraceData2(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                this.WriteHeader(source, eventType, id, eventCache);
                this.InternalWrite("<TraceData>");
                if (data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        this.InternalWrite("<DataItem>");
                        if (data[i] != null)
                        {
                            this.WriteData(data[i]);
                        }
                        this.InternalWrite("</DataItem>");
                    }
                }
                this.InternalWrite("</TraceData>");
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            DoWrite(() => TraceEvent2(eventCache, source, eventType, id, message));
        }
        private void TraceEvent2(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                this.WriteHeader(source, eventType, id, eventCache);
                this.WriteEscaped(message);
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            DoWrite(() => TraceEvent2(eventCache, source, eventType, id, format, args));
        }
        
        private void TraceEvent2(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                string str;
                this.WriteHeader(source, eventType, id, eventCache);
                if (args != null)
                {
                    str = string.Format(CultureInfo.InvariantCulture, format, args);
                }
                else
                {
                    str = format;
                }
                this.WriteEscaped(str);
                this.WriteFooter(eventCache);
            }
        }
        
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            DoWrite(()=>TraceTransfer2(eventCache, source, id, message, relatedActivityId));
        }

        private void TraceTransfer2(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            this.WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
            this.WriteEscaped(message);
            this.WriteFooter(eventCache);
        }

        public override void Write(string message)
        {
            this.WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            this.TraceEvent(null, Log.Source.Name, TraceEventType.Information, 0, message);
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

                    if (!IsFileSuitableForWriting)
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
            if (!Directory.Exists(logRootLocation))
            {
                Directory.CreateDirectory(logRootLocation);
            }
            
            isDirectoryCreated = true;
            CreateNewWriter();
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
            if (this.writer == null)
            {
                flag = false;
                if (this.fileName == null)
                {
                    return flag;
                }
                Encoding encodingWithFallback = GetEncodingWithFallback(new UTF8Encoding(false));
                string fullPath = Path.GetFullPath(this.fileName);
                string directoryName = Path.GetDirectoryName(fullPath);
                string fileName = Path.GetFileName(fullPath);
                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        this.writer = new StreamWriter(fullPath, true, encodingWithFallback, 0x1000);
                        flag = true;
                        break;
                    }
                    catch (IOException)
                    {
                        fileName = Guid.NewGuid().ToString() + fileName;
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
            XPathNavigator navigator = data as XPathNavigator;

            if (navigator == null && this.dataXPathFormatter != null)
            {
                navigator = this.dataXPathFormatter.Format(data);
            }

            if (navigator == null)
            {

                this.WriteEscaped(data.ToString());
            }
            else
            {
                if (this.strBldr == null)
                {
                    this.strBldr = new StringBuilder();
                    
                    this.xmlBlobWriter = new XmlTextWriter(new StringWriter(this.strBldr, CultureInfo.CurrentCulture));
                }
                else
                {
                    this.strBldr.Length = 0;
                }
                try
                {
                    navigator.MoveToRoot();
                    this.xmlBlobWriter.WriteNode(navigator, false);
                    //this.xmlBlobWriter.Flush(); //**
                    this.InternalWrite(this.strBldr.ToString());
                }
                catch (Exception)
                {
                    this.InternalWrite(data.ToString());
                }
            }
        }

        private void WriteEndHeader(TraceEventCache eventCache)
        {
            this.InternalWrite("\" />");
            this.InternalWrite("<Execution ProcessName=\"");
            this.InternalWrite(GetProcessName());
            this.InternalWrite("\" ProcessID=\"");
            this.InternalWrite(((uint)GetProcessId()).ToString(CultureInfo.InvariantCulture));
            this.InternalWrite("\" ThreadID=\"");
            if (eventCache != null)
            {
                this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                this.WriteEscaped(GetThreadId().ToString(CultureInfo.InvariantCulture));
            }
            this.InternalWrite("\" />");
            this.InternalWrite("<Channel/>");
            this.InternalWrite("<Computer>");
            this.InternalWrite(this.machineName);
            this.InternalWrite("</Computer>");
            this.InternalWrite("</System>");
            this.InternalWrite("<ApplicationData>");
        }

        private void WriteEscaped(string str)
        {
            if (str != null)
            {
                int startIndex = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    switch (str[i])
                    {
                        case '\n':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&#xA;");
                            startIndex = i + 1;
                            break;

                        case '\r':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&#xD;");
                            startIndex = i + 1;
                            break;

                        case '&':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&amp;");
                            startIndex = i + 1;
                            break;

                        case '\'':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&apos;");
                            startIndex = i + 1;
                            break;

                        case '"':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&quot;");
                            startIndex = i + 1;
                            break;

                        case '<':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&lt;");
                            startIndex = i + 1;
                            break;

                        case '>':
                            this.InternalWrite(str.Substring(startIndex, i - startIndex));
                            this.InternalWrite("&gt;");
                            startIndex = i + 1;
                            break;
                    }
                }
                this.InternalWrite(str.Substring(startIndex, str.Length - startIndex));
            }
        }

        private void WriteFooter(TraceEventCache eventCache)
        {
            bool flag = IsEnabled(TraceOptions.LogicalOperationStack);
            bool flag2 = IsEnabled(TraceOptions.Callstack);
            if ((eventCache != null) && (flag || flag2))
            {
                this.InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
                if (flag)
                {
                    this.InternalWrite("<LogicalOperationStack>");
                    Stack logicalOperationStack = eventCache.LogicalOperationStack;
                    if (logicalOperationStack != null)
                    {
                        foreach (object obj2 in logicalOperationStack)
                        {
                            this.InternalWrite("<LogicalOperation>");
                            this.WriteEscaped(obj2.ToString());
                            this.InternalWrite("</LogicalOperation>");
                        }
                    }
                    this.InternalWrite("</LogicalOperationStack>");
                }
                this.InternalWrite("<Timestamp>");
                this.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
                this.InternalWrite("</Timestamp>");
                if (flag2)
                {
                    this.InternalWrite("<Callstack>");
                    this.WriteEscaped(eventCache.Callstack);
                    this.InternalWrite("</Callstack>");
                }
                this.InternalWrite("</System.Diagnostics>");
            }
            this.InternalWrite("</ApplicationData></E2ETraceEvent>");
        }

        private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
        {
            this.WriteStartHeader(source, eventType, id, eventCache);
            this.WriteEndHeader(eventCache);
        }

        private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache, Guid relatedActivityId)
        {
            this.WriteStartHeader(source, eventType, id, eventCache);
            this.InternalWrite("\" RelatedActivityID=\"");
            this.InternalWrite(relatedActivityId.ToString("B"));
            this.WriteEndHeader(eventCache);
        }

        private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
        {
            this.InternalWrite("<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
            this.InternalWrite("<EventID>");
            this.InternalWrite(((uint)id).ToString(CultureInfo.InvariantCulture));
            this.InternalWrite("</EventID>");
            this.InternalWrite("<Type>3</Type>");
            this.InternalWrite("<SubType Name=\"");
            this.InternalWrite(eventType.ToString());
            this.InternalWrite("\">0</SubType>");
            this.InternalWrite("<Level>");
            int num = (int)eventType;
            if (num > 0xff)
            {
                num = 0xff;
            }
            if (num < 0)
            {
                num = 0;
            }
            this.InternalWrite(num.ToString(CultureInfo.InvariantCulture));
            this.InternalWrite("</Level>");
            this.InternalWrite("<TimeCreated SystemTime=\"");
            if (eventCache != null)
            {
                this.InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
            }
            else
            {
                this.InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
            }
            this.InternalWrite("\" />");
            this.InternalWrite("<Source Name=\"");
            this.WriteEscaped(source);
            this.InternalWrite("\" />");
            this.InternalWrite("<Correlation ActivityID=\"");
            if (eventCache != null)
            {
                this.InternalWrite(Trace.CorrelationManager.ActivityId.ToString("B"));
            }
            else
            {
                this.InternalWrite(Guid.Empty.ToString("B"));
            }
        }

        private bool IsEnabled(TraceOptions opts)
        {
            return ((opts & this.TraceOutputOptions) != TraceOptions.None);
        }

        private static Encoding GetEncodingWithFallback(Encoding encoding)
        {
            Encoding encoding2 = (Encoding)encoding.Clone();
            encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
            encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
            return encoding2;
        }

        private void CreateNewWriter()
        {
            if (this.writer != null)
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
                if (!File.Exists(logRootLocation + targetFileName + "_" + i.ToString() + ".xml"))
                {
                    pathCandidate = Path.Combine(logRootLocation,
                        targetFileName + "_" + i.ToString() + ".xml");
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
            this.writer =
                new StreamWriter(logFilePath, true, GetEncodingWithFallback(new UTF8Encoding(false)), 100);

        }
        /// <summary>
        /// Should only be called from the synchronized code
        /// </summary>
        /// <returns></returns>
        private bool IsFileSuitableForWriting
        {
            get
            {
                // checks only size for a moment
                FileInfo fi = new FileInfo(this.logFilePath);

                if (fi != null && fi.Exists)
                {
                    return (fi.Length < maxFileSizeBytes);
                }
                else
                {
                    return false;
                }
            }
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
                using (System.Diagnostics.Process process =
                    System.Diagnostics.Process.GetCurrentProcess())
                {
                    processId = process.Id;
                    processName = process.ProcessName;
                }
            }
        } 
        #endregion
    }

}
