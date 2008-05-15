using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Common
{
    public interface IGenericKeyValueProvider<TKey, TValue> 
    {
        TValue GetValue(TKey key);
    }
}
