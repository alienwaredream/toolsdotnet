using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Configuration;

namespace Tools.Common.Wcf
{
    public class ExceptionHandlingBehavior : IServiceBehavior
    {
        #region Private fields

        private string handlingPolicyName;
        private Type errorHandlerType;

        #endregion

        #region Properties
        /// <summary>
        /// Exception handling policy name
        /// </summary>
        public string HandlingPolicyName
        {
            get { return handlingPolicyName; }
        } 
        #endregion

        #region Constructor

        public ExceptionHandlingBehavior()
        {
            this.errorHandlerType = typeof(ServiceEnterpriseLibraryErrorHandler);
            this.handlingPolicyName = "Default";
        }
        public ExceptionHandlingBehavior(string handlingPolicyName) : this()
        {
            this.handlingPolicyName = handlingPolicyName;
        }
        #endregion

        #region IServiceBehavior

        //TODO: (SD) Validate to be used to check if policy is setup in the configuration
        void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase) { }
        void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters) { }
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler;

            try
            {
                errorHandler = new ServiceEnterpriseLibraryErrorHandler(this.handlingPolicyName);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehaviorAttribute constructor must have a public empty constructor.", e);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehaviorAttribute constructor must implement System.ServiceModel.Dispatcher.IErrorHandler.", e);
            }

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        #endregion

    }
}
