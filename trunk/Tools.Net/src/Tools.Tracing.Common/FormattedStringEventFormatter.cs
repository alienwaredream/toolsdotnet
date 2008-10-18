using System;
using System.Text;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for FormattedStringEventFormatter.
	/// </summary>
	public class FormattedStringEventFormatter : IEventFormatter
	{
		public FormattedStringEventFormatter()
		{

		}

		#region IEventFormatter Members

		public string Format(TraceEvent traceEvent)
		{
			StringBuilder sb = new StringBuilder();
				sb.Append("ContextHolderId:" + traceEvent.ContextIdentifier.ContextHolderId + System.Environment.NewLine);
				sb.Append("ExternalId:" + traceEvent.ContextIdentifier.ExternalId + System.Environment.NewLine);
				sb.Append("ExternalReference:" + traceEvent.ContextIdentifier.ExternalReference + System.Environment.NewLine);
				sb.Append("ExternalParentId:" + traceEvent.ContextIdentifier.ExternalParentId + System.Environment.NewLine);
				sb.Append("InternalId:" + traceEvent.ContextIdentifier.InternalId + System.Environment.NewLine);
				sb.Append("InternalParentId:" + traceEvent.ContextIdentifier.InternalParentId + System.Environment.NewLine);
				sb.Append("ContextGuid:" + traceEvent.ContextIdentifier.ContextGuid + System.Environment.NewLine);
				sb.Append("Host:" + traceEvent.Location.HostName + System.Environment.NewLine);
				sb.Append("Module:" + traceEvent.Location.ModulePath + System.Environment.NewLine);
				sb.Append("Principal:" + traceEvent.Principal.Name + System.Environment.NewLine);
				sb.Append("ThreadName:" + traceEvent.Location.ThreadName + System.Environment.NewLine);
				sb.Append("**Message:" + System.Environment.NewLine + traceEvent.Message);
			return sb.ToString();
		}

		#endregion
	}
}
