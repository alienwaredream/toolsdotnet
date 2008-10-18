using System;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for IServiceConnector.
	/// </summary>
	public interface IServiceConnector
	{
		bool IsConnected {get;}
		void Connect();
		void Disconnect();
		//
		event ServiceConnectionDelegate Connected;
		event ServiceConnectionDelegate Disconnected;

	}
}
