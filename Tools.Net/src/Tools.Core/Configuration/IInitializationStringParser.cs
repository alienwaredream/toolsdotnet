using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Core.Configuration
{
    public interface IInitializationStringParser
    {
        IDictionary<string, string> Parse(string initializationString);
    }
}
