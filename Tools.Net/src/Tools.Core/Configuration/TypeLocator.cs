using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Core.Configuration
{
	[Serializable()]
	public class TypeLocator : Descriptor
	{
		private string _type	= null;
		private string _path	= null;

		[XmlAttribute()]
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		[XmlAttribute()]
		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
			}
		}
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
			_type = type;
			_path = _path;
		}
	}
}
