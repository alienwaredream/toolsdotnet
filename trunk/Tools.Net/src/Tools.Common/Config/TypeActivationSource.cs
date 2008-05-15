using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Tools.Common.Config
{
	/// <summary>
	/// Summary description for TypeActivationSource.
	/// </summary>
	[Serializable()]
	public class TypeActivationSource : Descriptor
	{
		private TypeLocator						_typeLocator	= null;
		private List<ActivationArgument>	    _arguments		= null;	

		public TypeLocator TypeLocator
		{
			get
			{
				return _typeLocator;
			}
			set
			{
				_typeLocator = value;
			}
		}
		[XmlArray()]
		public List<ActivationArgument>	Arguments
		{
			get
			{
				return _arguments;
			}
			set
			{
				_arguments = value;
			}
		}

		public TypeActivationSource()
		{
			_arguments = new List<ActivationArgument>();
			_typeLocator = new TypeLocator();
		}
	}
}
