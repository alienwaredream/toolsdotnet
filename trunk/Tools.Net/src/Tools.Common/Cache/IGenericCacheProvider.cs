using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    /// <summary>
    /// Cache provider for the classes which needs a cache collection same types
    /// </summary>
    /// <typeparam name="Key">The type of the cache key</typeparam>
    /// <typeparam name="Value">The value of the objects to be put in cache</typeparam>
    /// <created by="Sidar Ok" date="15-Jan-2007" />
    public interface IGenericCacheProvider<Key, Value>
    {
        Value GetItem(Key name);
        void SetItem(Key name, Value value);
        Value[] GetAll();
    }
}
