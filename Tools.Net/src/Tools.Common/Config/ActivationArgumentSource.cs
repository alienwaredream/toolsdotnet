using System;

namespace Tools.Common.Config
{
	/// <summary>
	/// Summary description for ActivationParameterType.
	/// </summary>
	[Serializable()]
	public enum ActivationArgumentSource
	{
		/// <summary>
		/// The parameter value is taken from the text value supplied.
		/// </summary>
		Text	= 1,
		/// <summary>
		/// The parameter is taken from the hash dictionary.
		/// </summary>
		Hash	= 2,
	}
}
