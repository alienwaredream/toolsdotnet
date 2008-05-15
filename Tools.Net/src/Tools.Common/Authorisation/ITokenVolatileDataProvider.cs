using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Common.Authorisation
{
    public interface ITokenVolatileDataProvider
    {
        string GetVolatileData();
    }
}
