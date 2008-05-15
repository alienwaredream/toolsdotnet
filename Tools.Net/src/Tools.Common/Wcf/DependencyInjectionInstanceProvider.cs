using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Globalization;

namespace Tools.Common.Wcf
{
    public class DependencyInjectionInstanceProvider : IInstanceProvider
    {
        private string objectName;

        public DependencyInjectionInstanceProvider(){}

        public DependencyInjectionInstanceProvider(string objectName)
        {
            this.objectName = objectName;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return ContextRegistry.GetContext().GetObject(objectName);
        }
        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, 
            object instance) 
        { 
            //TODO: (SD) see what Bruno and Mark got in here
        }
    }
}
