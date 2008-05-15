using System;
using System.Configuration;
using System.Xml.Serialization;



namespace Tools.Common.Wcf
{
    [Serializable]
    public class ServiceTypeMappingConfigElement : ConfigurationElement
    {
        #region Properties


        [ConfigurationProperty("type",
        IsRequired = true, IsKey=true)]
        [XmlAttribute()]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }
        [ConfigurationProperty("objectName",
            IsRequired = true, IsKey = false)]
        [XmlAttribute()]
        public string ObjectName
        {
            get
            {
                return (string)this["objectName"];
            }
            set
            {
                this["objectName"] = value;
            }
        }


        #endregion Properties

        #region Constructors

        public ServiceTypeMappingConfigElement()
        {
        }
        public ServiceTypeMappingConfigElement(string type)
        {
            Type = type;
        }
        
        #endregion Constructors

        
        public override string ToString()
        {
            return base.ToString() + " type=" + Type + ", objectName=" + ObjectName;
        }
    }

}
