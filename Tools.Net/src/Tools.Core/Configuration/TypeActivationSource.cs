using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tools.Core.Configuration
{
    /// <summary>
    /// Summary description for TypeActivationSource.
    /// </summary>
    [Serializable]
    public class TypeActivationSource : Descriptor
    {
        public TypeActivationSource()
        {
            Arguments = new List<ActivationArgument>();
            TypeLocator = new TypeLocator();
        }

        public TypeLocator TypeLocator { get; set; }

        [XmlArray]
        public List<ActivationArgument> Arguments { get; set; }
    }
}