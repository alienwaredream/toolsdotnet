using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace Tools.Common.Wcf
{
    public class DependencyInjectionElement : BehaviorExtensionElement
    {
        #region BehaviorExtensionElement

        public override Type BehaviorType
        {
            get { return typeof(DependencyInjectionServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            DependencyInjectionServiceBehavior behavior = new DependencyInjectionServiceBehavior();

            foreach (ServiceTypeMappingConfigElement el in Mappings)
            {
                behavior.ServiceTypeStringMappings.Add(el.Type, el.ObjectName);
            }
            return behavior;
        }

        #endregion

        #region Properties

        [ConfigurationProperty("mappings",
            IsDefaultCollection = true, IsRequired = true)]
        public ServiceTypeMappingConfigElementCollection Mappings
        {

            get
            {
                return base["mappings"] as ServiceTypeMappingConfigElementCollection;
            }
        }


        #endregion
    }
}
