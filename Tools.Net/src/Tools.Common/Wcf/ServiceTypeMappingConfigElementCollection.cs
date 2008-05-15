using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Tools.Common.Wcf
{
    public class ServiceTypeMappingConfigElementCollection : ConfigurationElementCollection
    {
        public ServiceTypeMappingConfigElementCollection()
        {
        }

        public override
            ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return
                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override
            ConfigurationElement CreateNewElement()
        {
            return new ServiceTypeMappingConfigElement();
        }


        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new ServiceTypeMappingConfigElement(elementName);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((ServiceTypeMappingConfigElement)element).Type;
        }


        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }


        }

        public new int Count
        {

            get { return base.Count; }

        }


        public ServiceTypeMappingConfigElement this[int index]
        {
            get
            {
                return (ServiceTypeMappingConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ServiceTypeMappingConfigElement this[string Name]
        {
            get
            {
                return (ServiceTypeMappingConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(ServiceTypeMappingConfigElement objectEndPoint)
        {
            return BaseIndexOf(objectEndPoint);
        }

        public void Add(ServiceTypeMappingConfigElement objectEndPoint)
        {
            BaseAdd(objectEndPoint);

            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(ServiceTypeMappingConfigElement objectEndPoint)
        {
            if (BaseIndexOf(objectEndPoint) >= 0)
                BaseRemove(objectEndPoint.Type);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }
}
