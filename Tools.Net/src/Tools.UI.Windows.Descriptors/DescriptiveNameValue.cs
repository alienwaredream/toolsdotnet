using System;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
	/// <summary>
	/// Summary description for DescriptiveNameValue.
	/// </summary>
	[Serializable]
	public class DescriptiveNameValue<T> : Descriptor, ICloneable
	{

		#region Global declarations

		private T _value;

		#endregion

		#region Properties
		/// <summary>
		/// Value.
		/// </summary>
		public virtual T Value
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

		#endregion

		#region Constructors

		public DescriptiveNameValue() : base() { }

		public DescriptiveNameValue
			(
			string name,
			T val,
			string description
			)
			: base(name, description)
		{
			_value = val;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return Value.ToString();
		}


		#endregion


		#region ICloneable Members

		public object Clone()
		{
			return new DescriptiveNameValue <T>
			(
			Name,
			Value,
			Description
			);
		}

		#endregion
	}
}
