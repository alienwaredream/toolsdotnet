using System;
using System.Runtime.Remoting.Messaging;


namespace Tools.Remoting.Client.Common
{
#region Class RemotingProxy
	/// <summary>
	/// Summary description for RemotingProxy.
	/// </summary>
	public class SafeRemotingProxy : RemotingProxy
	{

		#region Implementation of RealProxy

		/// <summary>
		/// TODO:
		/// </summary>
		public override IMessage Invoke( IMessage msg )
		{
			IMethodReturnMessage msgRet	= null;	
	
			msg.Properties["__Uri"] = _url;

			msgRet = _sinkChain.SyncProcessMessage( msg ) as IMethodReturnMessage;

			return msgRet;
			
		}

		#endregion Implementation of RealProxy


	#region Constructors
		/// <summary>
		/// TODO:
		/// </summary>
		public SafeRemotingProxy( Type type, string url ) : base( type, url )
		{
		}
#endregion Constructors
	}
#endregion Class RemotingProxy
}
