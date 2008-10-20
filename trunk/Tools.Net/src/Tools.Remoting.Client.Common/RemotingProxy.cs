using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Tools.Remoting.Client.Common
{

    #region Class RemotingProxy

    /// <summary>
    /// Summary description for RemotingProxy.
    /// </summary>
    public class RemotingProxy : RealProxy
    {
        #region Implementation of RealProxy

        /// <summary>
        /// TODO:
        /// </summary>
        public override IMessage Invoke(IMessage msg)
        {
            IMethodReturnMessage msgRet = null;

            msg.Properties["__Uri"] = _url;

            msgRet = _sinkChain.SyncProcessMessage(msg) as IMethodReturnMessage;

            if (msgRet != null && msgRet.Exception != null)
            {
                Log.Source.TraceData(TraceEventType.Error, RemotingProxyMessage.ExceptionDuringMethodInvocation,
                                     "There was an exception during remoted method invocation. " + Environment.NewLine +
                                     "Invocation Url is " + _url + Environment.NewLine +
                                     " Reflected type is " + msgRet.MethodBase.DeclaringType.FullName +
                                     Environment.NewLine +
                                     " Called method is " + msgRet.MethodName + Environment.NewLine + "Exception: " +
                                     msgRet.Exception
                    );
            }

            return msgRet;
        }

        #endregion Implementation of RealProxy

        #region Declarations

        protected IMessageSink _sinkChain;
        protected string _uri = String.Empty;
        protected string _url = String.Empty;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// TODO:
        /// </summary>
        public RemotingProxy(Type type, string url) : base(type)
        {
            _url = url;


            // Check each registered channel to see if it accepts the given url

            IChannel[] registeredChannels = ChannelServices.RegisteredChannels;

            foreach (IChannel channel in registeredChannels)
            {
                if (channel is IChannelSender)
                {
                    var channelSender = (IChannelSender) channel;


                    // try to create the sink

                    _sinkChain = channelSender.CreateMessageSink(_url, null, out _uri);


                    // if the channel returned a sink chain, exit the loop

                    if (_sinkChain != null)
                    {
                        break;
                    }
                }
            }


            // no registered channel accepted the url

            if (_sinkChain == null)
            {
                throw new ApplicationException("No remoting channel found for " + _url);
            }
        }

        #endregion Constructors
    }

    #endregion Class RemotingProxy
}