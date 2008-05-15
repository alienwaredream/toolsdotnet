using System;

namespace Tools.Common
{
	/// <summary>
	/// Summary description for IIdentifierHolder.
	/// </summary>
	/// <typeparam name="IDType">The type of the D type.</typeparam>
	public interface IIdentifierHolder<IDType>
	{

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
        IDType Id { get;set;}
	}
}
