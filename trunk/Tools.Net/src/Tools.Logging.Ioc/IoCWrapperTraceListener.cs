using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Spring.Context;
using Spring.Context.Support;

namespace Tools.Logging.Ioc
{
    //TODO:(SD) Introduce configuration for max message size, mapping to params, etc.
    /// <summary>
    /// Provides logging to the database
    /// </summary>
    public class IocWrapperTraceListener : TraceListener
    {
        private readonly TraceListener traceListener;


        /// <summary>
        /// 
        /// </summary>
        public IocWrapperTraceListener()
        {
            Debug.WriteLine("IoCWrapperTraceListener default ctor called", "TraceListener IoC");
        }

        public IocWrapperTraceListener(string initializationData)
            : this()
        {
            Debug.WriteLine("IoCWrapperTraceListener initializationData ctor called",
                            "TraceListener IoC");

            using (IApplicationContext ctx = ContextRegistry.GetContext())
            {
                // Get a new instance of the message object
                traceListener =
                    (TraceListener) ctx.GetObject(initializationData);
            }
        }


        /// <summary>
        /// Writes trace information, a data object and event information to the listener specific output.
        /// This method is used by Enterprise Library.
        /// </summary>
        /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"></see> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"></see> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification =
                "This method is supposed to be highly fault tolerant and report any exception that happened during its execution to the fallback logger."
            )]
        public override void TraceData(TraceEventCache eventCache, string source,
                                       TraceEventType eventType, int id, object data)
        {
            traceListener.TraceData(eventCache, source, eventType, id, data);
        }

        #region Trace listener methods

        #region calling overloads

        public override void Fail(string message, string detailMessage)
        {
            traceListener.Fail(message, detailMessage);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                        string format, params object[] args)
        {
            traceListener.TraceEvent(eventCache, source, eventType, id, format, args);
        }

        #endregion

        //public override void TraceData(
        //    TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        //{
        //    traceListener.TraceData(eventCache, source, eventType, id, data);
        //}

        public override void TraceData(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            traceListener.TraceData(eventCache, source, eventType, id, data);
        }

        public override void TraceEvent(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            traceListener.TraceEvent(eventCache, source, eventType, id, message);
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message,
                                           Guid relatedActivityId)
        {
            traceListener.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        }

        public override void Write(string message)
        {
            traceListener.Write(message);
        }

        public override void WriteLine(string message)
        {
            traceListener.WriteLine(message);
        }

        #endregion Trace listener methods
    }
}