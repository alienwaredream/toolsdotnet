using System;
using System.Xml.Serialization;

namespace Tools.Core
{
	/// <summary>
	/// Provides the default implementation of the <see cref="IDescriptor"/>.
	/// </summary>
    /// This is a change on the branch
	[Serializable()]
	public class Descriptor : IDescriptor
	{
		#region Implementation of IDescriptor

		private string _name;
		private string _description;

		[XmlAttribute()]
		public virtual string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[XmlElement()]
		public virtual string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		public Descriptor()
		{
			
		}
		public Descriptor(string name, string description)
		{
			_name = name;
			_description = description;
		}

		#endregion Implementation of IDescriptor
	}
}
