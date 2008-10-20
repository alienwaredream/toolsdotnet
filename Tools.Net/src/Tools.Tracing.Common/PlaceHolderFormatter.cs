namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for PlaceHolderFormatter.
    /// </summary>
    public class PlaceHolderFormatter : IEventFormatter
    {
        private readonly string format;

        public PlaceHolderFormatter()
        {
        }

        public PlaceHolderFormatter(string format)
            : this()
        {
            this.format = format;
        }

        #region IEventFormatter Members

        public string Format(TraceEvent traceEvent)
        {
            //string ret = format;
            return format.
                Replace("{%ContextHolderId}", traceEvent.ContextIdentifier.ContextHolderId.ToString()).
                Replace("{%ExternalId}", traceEvent.ContextIdentifier.ExternalId.ToString()).
                Replace("{%ExternalReference}", traceEvent.ContextIdentifier.ExternalReference.ToString()).
                Replace("{%ExternalParentId}", traceEvent.ContextIdentifier.ExternalParentId.ToString()).
                Replace("{%InternalId}", traceEvent.ContextIdentifier.InternalId.ToString()).
                Replace("{%InternalParentId}", traceEvent.ContextIdentifier.InternalParentId.ToString()).
                Replace("{%ContextGuid}", traceEvent.ContextIdentifier.ContextGuid.ToString()).
                Replace("{%HostName}", traceEvent.Location.HostName).
                Replace("{%ModulePath}", traceEvent.Location.ModulePath).
                Replace("{%Principal}", traceEvent.Principal.Name).
                Replace("{%ThreadName}", traceEvent.Location.ThreadName).
                Replace("{%Message}", traceEvent.Message).
                Replace("{%Source}", traceEvent.Location.Source).
                Replace("{%Type}", traceEvent.Type.ToString()).
                Replace("{%Category}", traceEvent.Category.ToString()).
                Replace("{%EventIdText}", traceEvent.EventIdText).
                Replace("{%Time}", traceEvent.Time.ToString("dd-MMM-yyyyTHH:mm:ss (fff)")).
                Replace("{%EventId}", traceEvent.EventId.ToString()
                );
            // TODO: Provide specific format handling for the DateTime (SD)
        }

        #endregion
    }
}