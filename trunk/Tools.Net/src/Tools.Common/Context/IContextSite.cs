using System;

namespace Tools.Common.Context
{
	/// <summary>
	/// Summary description for IContextSite.
	/// </summary>
	public interface IContextSite : IContextIdentifierHolder, IDescriptor
	{
		// will have collection of holders (or ContextHolder will have it), for a 
		// moment just need decoupling (SD)
	}
}
