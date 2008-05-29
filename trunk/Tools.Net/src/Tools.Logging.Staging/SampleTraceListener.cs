using System;
using System.Text;
using System.Security.Permissions;
using System.Diagnostics;

namespace Tools.Logging
{
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public class SampleTraceListener : TraceListener
    {
        #region Fields
        
        private readonly string machineName;

        #endregion

        #region Ctors

        public SampleTraceListener()
            : base()
        {
            this.machineName = Environment.MachineName;
        }

        #endregion

        public override void Close()
        {
            // Close/Dispose any resources if required
            base.Close();
        }

        #region methods to copy

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
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                // Internal write
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                // Internal write
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                // Write
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                // Write
            }
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            //this.WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
            //this.WriteEscaped(message);
            //this.WriteFooter(eventCache);
        }

        public override void Write(string message)
        {
            this.WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            this.TraceEvent(null, Log.Source.Name, TraceEventType.Information, 0, message);
        }

        #endregion methods to copy

        private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
        {
            #region Sample immplementation
        //    this.InternalWrite("<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
        //    this.InternalWrite("<EventID>");
        //    this.InternalWrite(((uint)id).ToString(CultureInfo.InvariantCulture));
        //    this.InternalWrite("</EventID>");
        //    this.InternalWrite("<Type>3</Type>");
        //    this.InternalWrite("<SubType Name=\"");
        //    this.InternalWrite(eventType.ToString());
        //    this.InternalWrite("\">0</SubType>");
        //    this.InternalWrite("<Level>");
        //    int num = (int)eventType;
        //    if (num > 0xff)
        //    {
        //        num = 0xff;
        //    }
        //    if (num < 0)
        //    {
        //        num = 0;
        //    }
        //    this.InternalWrite(num.ToString(CultureInfo.InvariantCulture));
        //    this.InternalWrite("</Level>");
        //    this.InternalWrite("<TimeCreated SystemTime=\"");
        //    if (eventCache != null)
        //    {
        //        this.InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
        //    }
        //    else
        //    {
        //        this.InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
        //    }
        //    this.InternalWrite("\" />");
        //    this.InternalWrite("<Source Name=\"");
        //    this.WriteEscaped(source);
        //    this.InternalWrite("\" />");
        //    this.InternalWrite("<Correlation ActivityID=\"");
        //    if (eventCache != null)
        //    {
        //        this.InternalWrite(eventCache.ActivityId.ToString("B"));
        //    }
        //    else
        //    {
        //        this.InternalWrite(Guid.Empty.ToString("B"));
            //    }
#endregion
        }
        private void WriteFooter(TraceEventCache eventCache)
        {
            //bool flag = base.IsEnabled(TraceOptions.LogicalOperationStack);
            //bool flag2 = base.IsEnabled(TraceOptions.Callstack);
            //if ((eventCache != null) && (flag || flag2))
            //{
            //    this.InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
            //    if (flag)
            //    {
            //        this.InternalWrite("<LogicalOperationStack>");
            //        Stack logicalOperationStack = eventCache.LogicalOperationStack;
            //        if (logicalOperationStack != null)
            //        {
            //            foreach (object obj2 in logicalOperationStack)
            //            {
            //                this.InternalWrite("<LogicalOperation>");
            //                this.WriteEscaped(obj2.ToString());
            //                this.InternalWrite("</LogicalOperation>");
            //            }
            //        }
            //        this.InternalWrite("</LogicalOperationStack>");
            //    }
            //    this.InternalWrite("<Timestamp>");
            //    this.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
            //    this.InternalWrite("</Timestamp>");
            //    if (flag2)
            //    {
            //        this.InternalWrite("<Callstack>");
            //        this.WriteEscaped(eventCache.Callstack);
            //        this.InternalWrite("</Callstack>");
            //    }
            //    this.InternalWrite("</System.Diagnostics>");
            //}
            //this.InternalWrite("</ApplicationData></E2ETraceEvent>");
        }

        private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache, Guid relatedActivityId)
        {
            //this.WriteStartHeader(source, eventType, id, eventCache);
            //this.InternalWrite("\" RelatedActivityID=\"");
            //this.InternalWrite(relatedActivityId.ToString("B"));
            //this.WriteEndHeader(eventCache);
        }

    }
}
