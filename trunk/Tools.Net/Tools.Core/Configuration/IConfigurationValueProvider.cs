using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Core.Configuration
{
    public interface IConfigurationValueProvider
    {
        string this[string keyName] { get; }
    }
}
