using System.Collections.Generic;

namespace Tools.Core.Configuration
{
    public interface IInitializationStringParser
    {
        IDictionary<string, string> Parse(string initializationString);
    }
}