using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Cache
{
    public class CacheOffProvider:ICacheProvider
    {

        #region ICacheProvider Members

        public object GetItem(string name)
        {
            return null;
        }

        public void SetItem(string name, object value)
        {
           
        }

        #endregion
    }
}
