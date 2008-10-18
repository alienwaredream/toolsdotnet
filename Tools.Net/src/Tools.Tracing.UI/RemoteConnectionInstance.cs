using System;

using Tools.Core;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for RemoteConnectionInstance.
	/// </summary>
	public class RemoteConnectionInstance : ServiceConnectionInstance
	{
		private RemoteConnectionConfiguration _configuration = null;

		public RemoteConnectionConfiguration Configuration
		{
			get
			{
				return _configuration;
			}
		}

		public RemoteConnectionInstance(RemoteConnectionConfiguration configuration)
		{
			_configuration = configuration;
			_configuration.Changed += new EventHandler(_configuration_Changed);
		}

		private void _configuration_Changed(object sender, EventArgs e)
		{
			base.OnChanged();
		}
	}
}
