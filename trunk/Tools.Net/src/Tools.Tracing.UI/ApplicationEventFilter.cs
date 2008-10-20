using System;
using Tools.Core;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for TraceEventFilter.
    /// </summary>
    [Serializable]
    public class ApplicationEventFilter : IEnabled, IChangeEventRaiser
    {
        private FilterEntryCollection _filterEntries;

        private string[] pathsDictionary =
            new[]
                {
                    "TraceEvent::Location.AppDomainName",
                    "TraceEvent::Location.HostName",
                    "TraceEvent::Location.ModulePath",
                    "TraceEvent::Location.ClassName",
                    "TraceEvent::Location.ThreadName",
                    "TraceEvent::Location.MethodName",
                    "TraceEvent::Location.ProcessId",
                    "TraceEvent::Principal.Name",
                    "TraceEvent::LifeCycleType",
                    "TraceEvent::Message",
                    "TraceEvent::Time",
                    "TraceEvent::Type",
                    "TraceEvent::EventId",
                    "TraceEvent::EventIdText",
                    "TraceEvent::ContextIdentifier.ContextHolderId",
                    "TraceEvent::ContextIdentifier.ExternalId",
                    "TraceEvent::ContextIdentifier.ExternalParentId",
                    "TraceEvent::ContextIdentifier.ExternalReference",
                    "TraceEvent::ContextIdentifier.InternalId",
                    "TraceEvent::ContextIdentifier.InternalParentId",
                    "TraceEvent::ContextIdentifier.AuthenticationTokenId"
                };

        public ApplicationEventFilter()
        {
            _filterEntries = new FilterEntryCollection();
            _filterEntries.Changed += filterEntryCollectionChanged;
        }

        public FilterEntryCollection FilterEntries
        {
            get { return _filterEntries; }
            set
            {
                _filterEntries = value;
                OnChanged();
            }
        }

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        #endregion

        // TODO: What about closures in 2.0? For combined queres (SD)

        #region IEnabled Members

        public event EventHandler EnabledChanged;

        public bool Enabled
        {
            get
            {
                // TODO:  Add TraceEventFilter.Enabled getter implementation
                return false;
            }
            set
            {
                // TODO:  Add TraceEventFilter.Enabled setter implementation
            }
        }

        #endregion

        public bool Test(TraceEvent ae)
        {
            // TODO: Very temporary and non-optimized algorithm in place, subject to change (SD)
            for (int i = 0; i < FilterEntries.Count; i ++)
            {
                FilterEntry feCandidate = FilterEntries[i];
                if (!feCandidate.Enabled) continue;
                //if (feCandidate == null) continue;
                // TODO: change from the implicitely AND.
                if (!feCandidate.Test(getMappedEventValue(feCandidate.Path, ae))) return false;
            }
            return true;
        }

        /// <summary>
        /// Just proof of concept implementation. Iteration 0.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ae"></param>
        /// <returns></returns>
        private string getMappedEventValue(string path, TraceEvent ae)
        {
            if (path == "TraceEvent::Location.AppDomainName") return ae.Location.AppDomainName;
            if (path == "TraceEvent::Location.HostName") return ae.Location.HostName;
            if (path == "TraceEvent::Location.ModulePath") return ae.Location.ModulePath;
            if (path == "TraceEvent::Location.ClassName") return ae.Location.ClassName;
            if (path == "TraceEvent::Location.ThreadName") return ae.Location.ThreadName;
            if (path == "TraceEvent::Location.MethodName") return ae.Location.MethodName;
            if (path == "TraceEvent::Location.ProcessId") return ae.Location.ProcessId;
            //
            if (path == "TraceEvent::Principal.Name") return ae.Principal.Name;
            //
            if (path == "TraceEvent::LifeCycleType") return ae.LifeCycleType.ToString();
            //
            if (path == "TraceEvent::Message") return ae.Message;
            //
            if (path == "TraceEvent::Time") return ae.Time.ToString();
            //
            if (path == "TraceEvent::Type") return ae.Type.ToString();
            //
            if (path == "TraceEvent::EventId") return ae.EventId.ToString();
            //
            if (path == "TraceEvent::EventIdText") return ae.EventIdText;
            //
            if (path == "TraceEvent::ContextIdentifier.ContextHolderId")
                return ae.ContextIdentifier.ContextHolderId.ToString();
            if (path == "TraceEvent::ContextIdentifier.ExternalId") return ae.ContextIdentifier.ExternalId.ToString();
            if (path == "TraceEvent::ContextIdentifier.ExternalParentId")
                return ae.ContextIdentifier.ExternalParentId.ToString();
            if (path == "TraceEvent::ContextIdentifier.ExternalReference")
                return ae.ContextIdentifier.ExternalReference.ToString();
            if (path == "TraceEvent::ContextIdentifier.InternalId") return ae.ContextIdentifier.InternalId.ToString();
            if (path == "TraceEvent::ContextIdentifier.InternalParentId")
                return ae.ContextIdentifier.InternalParentId.ToString();
            if (path == "TraceEvent::ContextIdentifier.AuthenticationTokenId")
                return ae.ContextIdentifier.AuthenticationTokenId.ToString();

            return null;
        }

        private void filterEntryCollectionChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }
    }
}