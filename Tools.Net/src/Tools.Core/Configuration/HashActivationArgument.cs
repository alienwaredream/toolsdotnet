using System;
using System.Xml.Serialization;

namespace Tools.Core.Configuration
{
	/// <summary>
	/// Summary description for ActivationParameter.
	/// </summary>
	[Serializable()]
	public class HashActivationArgument : ActivationArgument
	{
		
		private string _hashEntryName = null;

		public override ActivationArgumentSource Source
		{
			get
			{
				return ActivationArgumentSource.Hash;
			}
		}
		[XmlAttribute()]
		public string HashEntryName
		{
			get
			{
				return _hashEntryName;
			}
			set
			{
				_hashEntryName = value;
			}

		}
		public HashActivationArgument()
			: base()
		{

		}
		public HashActivationArgument(string name, string description, string hashEntryName)
			: base(name, description)
		{
			_hashEntryName = hashEntryName;
		}
	}
}
