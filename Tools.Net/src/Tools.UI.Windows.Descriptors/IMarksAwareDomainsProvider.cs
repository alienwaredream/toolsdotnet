using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.UI.Windows.Descriptors
{
    interface IMarksAwareDomainsProvider<T> : IDomainsProvider<T>
    {
        string[] GetDomainValues(T obj, MarksPresentationType presentationType);
    }
}
