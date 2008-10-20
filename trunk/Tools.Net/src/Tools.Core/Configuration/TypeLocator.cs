using System;
using System.Xml.Serialization;

namespace Tools.Core.Configuration
{
    [Serializable]
    public class TypeLocator : Descriptor
    {
        private string _path;

        public TypeLocator()
        {
        }

//		public TypeActivationSource(string type, string path)
//			: this(null, null, type, path)
//		{
//
//		}
        public TypeLocator(string name, string description, string type, string path)
            : base(name, description)
        {
            Type = type;
            _path = _path;
        }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
    }
}