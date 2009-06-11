using System.Collections.Generic;
using System.Globalization;
using Tools.Coordination.ProducerConsumer;
using Tools.Logging;
using System.Diagnostics;
using Tools.Coordination.Core;
using System;
using System.Text;

namespace Tools.Commands.Implementation.IF1.Processors
{
    public class ResponseConsumer
        : JobConsumer<string>
    {

        public ResponseConsumer
            (
            )
        {
        }
        protected override string GetWorkItemBody(Tools.Coordination.WorkItems.WorkItem workItem)
        {
            return Encoding.UTF8.GetString(workItem.MessageBody);
        }
        protected override void LogJobCompletion(JobProcessedEventArgs e)
        {
            Log.TraceData(Log.Source, TraceEventType.Verbose,
                                 999,
                                 String.Format(CultureInfo.InvariantCulture,
                                               "Item: {0}. Consumer turnover time (ms) is {1},  ticks {2}.\r\n{3}", e.WorkItem.ContextIdentifier.ExternalReference,
                                               (e.WorkItem.CompletedAt - e.WorkItem.RetrievedAt).
                                                   TotalMilliseconds,
                                               (e.WorkItem.CompletedAt - e.WorkItem.RetrievedAt).Ticks,
                                               GetWorkItemBody(e.WorkItem))
                                               );
        }
    }
}