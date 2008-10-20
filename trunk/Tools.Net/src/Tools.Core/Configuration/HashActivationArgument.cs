using System;
using System.Xml.Serialization;

namespace Tools.Core.Configuration
{
    /// <summary>
    /// Summary description for ActivationParameter.
    /// </summary>
    [Serializable]
    public class HashActivationArgument : ActivationArgument
    {
        public HashActivationArgument()
        {
        }

        public HashActivationArgument(string name, string description, string hashEntryName)
            : base(name, description)
        {
            HashEntryName = hashEntryName;
        }

        public override ActivationArgumentSource Source
        {
            get { return ActivationArgumentSource.Hash; }
        }

        [XmlAttribute]
        public string HashEntryName { get; set; }
    }
}