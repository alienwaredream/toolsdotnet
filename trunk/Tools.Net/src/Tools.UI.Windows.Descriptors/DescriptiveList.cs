using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable]
    public class DescriptiveList<T> : List<T>, IDescriptor
    {
        #region Implementation of IDescriptor

        public DescriptiveList()
            : this
                (
                "DescriptiveListGenericName",
                "DescriptiveListGenericDescription"
                )
        {
        }

        public DescriptiveList
            (
            string name,
            string description
            )
        {
            Name = name;
            Description = description;
        }

        public DescriptiveList
            (
            string name,
            string description,
            IEnumerable<T> collection
            )
            : base
                (
                collection
                )
        {
            Name = name;
            Description = description;
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement]
        public string Description { get; set; }

        #endregion Implementation of IDescriptor
    }
}