using System;
using System.Collections.Generic;
using Tools.Core.Context;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for WorkspaceConfiguration.
    /// </summary>
    [Serializable]
    public class WorkspaceConfiguration //: IChangeEventRaiser
    {
        private ApplicationEventFilter _filter =
            new ApplicationEventFilter();

        private List<ContextHolderIdDescriptorPointer> _keyPointers = new List<ContextHolderIdDescriptorPointer>();

        private RemoteConnectionConfigurationCollection _managementConnections =
            new RemoteConnectionConfigurationCollection();

        private TraceEventHandlerManagerConfiguration _managerConfiguration =
            new TraceEventHandlerManagerConfiguration();

        private RemoteConnectionConfigurationCollection _observerConnections =
            new RemoteConnectionConfigurationCollection();

        private TracingOptions _tracingOptions =
            new TracingOptions();

        public List<ContextHolderIdDescriptorPointer> KeyPointers
        {
            get { return _keyPointers; }
            set { _keyPointers = value; }
        }

        public string XPathLibraryPath { get; set; }

        public string XQueryLibraryPath { get; set; }

        public ApplicationEventFilter Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public TraceEventHandlerManagerConfiguration ManagerConfiguration
        {
            get { return _managerConfiguration; }
            set { _managerConfiguration = value; }
        }

        public RemoteConnectionConfigurationCollection ManagementConnections
        {
            get { return _managementConnections; }
            set { _managementConnections = value; }
        }

        public RemoteConnectionConfigurationCollection ObserverConnections
        {
            get { return _observerConnections; }
            set { _observerConnections = value; }
        }

        public TracingOptions TracingOptions
        {
            get { return _tracingOptions; }
            set { _tracingOptions = value; }
        }
    }
}