using System;
using System.Xml.Serialization;

namespace Tools.Common.Config
{
	/// <summary>
	/// Summary description for ActivationParameter.
	/// </summary>
	[Serializable()]
	public class TextActivationArgument : ActivationArgument
	{
		
		private string _value = null;

		public override ActivationArgumentSource Source
		{
			get
			{
				return ActivationArgumentSource.Text;
			}
		}
		[XmlAttribute()]
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}

		}

		public TextActivationArgument()
			: base()
		{

		}
		public TextActivationArgument(string name, string description, string val)
			: base(name, description)
		{
			_value = val;
		}
		public override string ToString()
		{
			return Value;
		}
	}
}
