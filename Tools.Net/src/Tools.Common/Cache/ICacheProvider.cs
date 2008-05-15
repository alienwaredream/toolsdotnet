using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    public interface ICacheProvider
    {
        object GetItem(String name);
        void SetItem(String name, object value);
    }
}
