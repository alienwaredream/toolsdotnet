using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Spring.Context.Support;
using System.Globalization;

namespace Tools.Common.Wcf
{
    public class DependencyInjectionServiceBehavior : IServiceBehavior
    {
        private IDictionary<Type, string> serviceTypeMappings = new Dictionary<Type, string>();
        private IDictionary<string, string> serviceTypeStringMappings = new Dictionary<string, string>();

        internal IDictionary<string, string> ServiceTypeStringMappings
        {
            get { return serviceTypeStringMappings; }
        }
        public DependencyInjectionServiceBehavior(){}

        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;

                if (cd != null)
                {
                    string objectName = ResolveObjectName(serviceDescription);

                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        
                        
                        ed.DispatchRuntime.InstanceProvider =
                            new DependencyInjectionInstanceProvider(objectName);
                    }
                }
            }
        }

        private string ResolveObjectName(ServiceDescription serviceDescription)
        {
            string objectName = null;
            // Find the object name to create when the end point is called
            // This is either the service type class name when used without
            // the service type mapping or service type mapping object name.
            if (!serviceTypeMappings.TryGetValue(serviceDescription.ServiceType,
                out objectName))
            {
                // Apply the convention over configuration approach
                // There should be exactly one object name as validated in the Validate method
                return ContextRegistry.GetContext().GetObjectNamesForType(
                    serviceDescription.ServiceType)[0];
            }
            return objectName;
        }
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters) { }

        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase) 
        {
            // Validate that all mapping types are available, if there is an 
            // issue with the type, at least this is shown during activation
            foreach (string key in serviceTypeStringMappings.Keys)
            {
                serviceTypeMappings.Add(Type.GetType(key, true), serviceTypeStringMappings[key]);
            }

            string objectName;

            if (!serviceTypeMappings.TryGetValue(serviceDescription.ServiceType,
                out objectName))
            {
                //Validate for the convention over configuration approach
                string[] objectNames = ContextRegistry.GetContext().GetObjectNamesForType(serviceDescription.ServiceType);

                if (objectNames.Length != 1)
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture,
                        "Exactly one <object> definition for the {0} service with type {1} is required unless you define" +
                       " the service type mapping explicitely via the DependencyInjectionElement. Found <object>s: {2}",
                       serviceDescription.ServiceType.Name, serviceDescription.ServiceType.FullName,
                       objectNames.Aggregate(String.Empty, (acc, next) => (acc += next + ",")).TrimEnd(',')));

                }
            }
            else
            {
                if (!ContextRegistry.GetContext().ContainsObject(objectName))
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture,
                        "No <object> definition found in the spring.net configuration for the object with name {0}" +
                        " to which the service {1} of type {2} is mapped! Create the <object> definition or change the mapping.",
                        objectName, serviceDescription.ServiceType.Name, serviceDescription.ServiceType.FullName));

                }
            }
        }
    }
}
