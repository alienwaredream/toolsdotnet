using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Tools.Remoting.Client.Common
{

    #region Class RemotingClient

    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public abstract class RemotingClient
    {
        #region Declarations

        /// <summary>
        /// TODO:
        /// </summary>
        protected static bool _tcpClientChannelRegistered;

        //
        private readonly string _serviceHost;
        private readonly int _servicePort;
        private string _objectUriPath;

        #endregion Declarations

        #region Properties

        // TODO: That to be configurable. (SD)
        protected virtual string ProtocolSchema
        {
            get { return "TCP"; }
        }

        protected string ServiceHost
        {
            get { return _serviceHost; }
        }

        protected int ServicePort
        {
            get { return _servicePort; }
        }

        protected string ObjectUriPath
        {
            get { return _objectUriPath; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Left for backward compatibility only (SD)
        /// </summary>
        /// <param name="serviceHost"></param>
        /// <param name="servicePort"></param>
        protected RemotingClient(string serviceHost, int servicePort)
            : this
                (
                serviceHost,
                servicePort,
                null
                )
        {
        }

        protected RemotingClient
            (
            string serviceHost,
            int servicePort,
            string objectUriPath
            )
        {
            _serviceHost = serviceHost;
            _servicePort = servicePort;
            _objectUriPath = objectUriPath;
        }

        #endregion

        #region Functions

        /// <summary>
        /// TODO:
        /// </summary>
        protected void registerTcpClientChannel()
        {
            var properties = new Hashtable();

            properties["includeVersions"] = "false";

            var sinkProvider =
                new BinaryClientFormatterSinkProvider(properties, null);

            var tcpChannel = new TcpClientChannel(String.Empty, sinkProvider);

            ChannelServices.RegisterChannel(tcpChannel);

            _tcpClientChannelRegistered = true;
        }

        /// <summary>
        /// For backward compatibility only (SD). Implementation of the method
        /// is also dictated only by bckw compatibility.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="objectUriPath"></param>
        /// <returns></returns>
        [Obsolete("Use ctor with objectUriPath instead and call to the objectUriPath-less method!")]
        protected object getTransparentProxy
            (
            Type type,
            string objectUriPath
            )
        {
            _objectUriPath = objectUriPath;
            return getTransparentProxy(type);
        }

        protected object getTransparentProxy
            (
            Type type
            )
        {
            return getTransparentProxy
                (
                type,
                false
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        protected object getTransparentProxy
            (
            Type type,
            bool safe
            )
        {
            object objectProxy;

            Uri u =
                new UriBuilder
                    (
                    ProtocolSchema,
                    ServiceHost,
                    ServicePort,
                    ObjectUriPath
                    ).Uri;

            try
            {
                if (!_tcpClientChannelRegistered)
                {
                    registerTcpClientChannel();
                }
                RemotingProxy proxy = null;
                if (!safe)
                {
                    proxy =
                        new RemotingProxy
                            (
                            type,
                            u.AbsoluteUri
                            );
                }
                else
                {
                    proxy =
                        new SafeRemotingProxy
                            (
                            type,
                            u.AbsoluteUri
                            );
                }
                objectProxy = proxy.GetTransparentProxy();
            }
            catch (Exception e)
            {
                // Handle exception better (SD)
                throw new ApplicationException(
                    "Service not responding " +
                    u.AbsoluteUri, e);
            }

            return objectProxy;
        }

        #endregion Functions
    }

    #endregion Class RemotingClient
}