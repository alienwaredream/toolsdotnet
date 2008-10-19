using System;
using System.Xml.Serialization;

namespace Tools.Core
{
    /// <summary>
    /// Summary description for DescriptiveNameValue.
    /// </summary>
    [Serializable]
    public class DescriptiveNameValue : Descriptor, ICloneable
    {
        #region Fields

        private string _value;

        #endregion

        #region Properties

        /// <summary>
        /// Value.
        /// </summary>
        [XmlAttribute]
        public virtual string Value { get; set; }

        #endregion

        #region Constructors

        public DescriptiveNameValue()
        {
        }

        public DescriptiveNameValue(string name, string val, string description)
            : base(name, description)
        {
            _value = val;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new DescriptiveNameValue
                (
                Name,
                Value,
                Description
                );
        }

        #endregion
    }
}