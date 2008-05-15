using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Common.Config
{
    public interface IConfigurationValueProvider
    {
        string this[string keyName] { get; }
    }
}
