using System;
using System.Xml.Serialization;

namespace Tools.Core.Configuration
{
    /// <summary>
    /// Summary description for ActivationParameter.
    /// </summary>
    [Serializable]
    public class TextActivationArgument : ActivationArgument
    {
        public TextActivationArgument()
        {
        }

        public TextActivationArgument(string name, string description, string val)
            : base(name, description)
        {
            Value = val;
        }

        public override ActivationArgumentSource Source
        {
            get { return ActivationArgumentSource.Text; }
        }

        [XmlAttribute]
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}