using System;
using System.Xml.Serialization;

namespace Tools.Common.Config
{
	/// <summary>
	/// Summary description for ActivationParameter.
	/// </summary>
	[Serializable()]
	[XmlInclude(typeof(HashActivationArgument)),
	XmlInclude(typeof(TextActivationArgument))]
	public abstract class ActivationArgument : Descriptor
	{
		
		[XmlAttribute()]
		public abstract ActivationArgumentSource Source
		{
			get;
		}
	

		protected ActivationArgument()
		{

		}
		protected ActivationArgument(string name, string description)
			: base(name, description)
		{
			
		}
	}
}
