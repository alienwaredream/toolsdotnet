using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Tools.Common.Cache
{
    public class CacheWindowsProvider:ICacheProvider
    {
        private Hashtable cache;
        public CacheWindowsProvider() {
            cache = new Hashtable();
        }
        #region ICacheProvider Members
        public object GetItem(string name)
        {
            lock (cache.SyncRoot)
            {
                return cache[name];
            }
            
        }

        public void SetItem(string name, object value)
        {
            lock (cache.SyncRoot)
            {
                cache[name] = value;
            }
            
        }

        #endregion
    }
}
