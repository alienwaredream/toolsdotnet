using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.UI.Windows.Descriptors
{
	public interface IDomainsProvider<T>
	{
		string[] GetDomainValues(T obj);
		string[] GetDomainNames();
		T GetNewDefaultInstance();
	}
}
