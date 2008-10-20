using System;
using System.Text;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for FormattedStringEventFormatter.
    /// </summary>
    public class FormattedStringEventFormatter : IEventFormatter
    {
        #region IEventFormatter Members

        public string Format(TraceEvent traceEvent)
        {
            var sb = new StringBuilder();
            sb.Append("ContextHolderId:" + traceEvent.ContextIdentifier.ContextHolderId + Environment.NewLine);
            sb.Append("ExternalId:" + traceEvent.ContextIdentifier.ExternalId + Environment.NewLine);
            sb.Append("ExternalReference:" + traceEvent.ContextIdentifier.ExternalReference + Environment.NewLine);
            sb.Append("ExternalParentId:" + traceEvent.ContextIdentifier.ExternalParentId + Environment.NewLine);
            sb.Append("InternalId:" + traceEvent.ContextIdentifier.InternalId + Environment.NewLine);
            sb.Append("InternalParentId:" + traceEvent.ContextIdentifier.InternalParentId + Environment.NewLine);
            sb.Append("ContextGuid:" + traceEvent.ContextIdentifier.ContextGuid + Environment.NewLine);
            sb.Append("Host:" + traceEvent.Location.HostName + Environment.NewLine);
            sb.Append("Module:" + traceEvent.Location.ModulePath + Environment.NewLine);
            sb.Append("Principal:" + traceEvent.Principal.Name + Environment.NewLine);
            sb.Append("ThreadName:" + traceEvent.Location.ThreadName + Environment.NewLine);
            sb.Append("**Message:" + Environment.NewLine + traceEvent.Message);
            return sb.ToString();
        }

        #endregion
    }
}