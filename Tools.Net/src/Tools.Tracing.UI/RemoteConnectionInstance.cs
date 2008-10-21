using System;
using Tools.Remoting.Client.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for RemoteConnectionInstance.
    /// </summary>
    public class RemoteConnectionInstance : ServiceConnectionInstance
    {
        private readonly RemoteConnectionConfiguration _configuration;

        public RemoteConnectionInstance(RemoteConnectionConfiguration configuration)
        {
            _configuration = configuration;
            _configuration.Changed += _configuration_Changed;
        }

        public RemoteConnectionConfiguration Configuration
        {
            get { return _configuration; }
        }

        private void _configuration_Changed(object sender, EventArgs e)
        {
            base.OnChanged();
        }
    }
}