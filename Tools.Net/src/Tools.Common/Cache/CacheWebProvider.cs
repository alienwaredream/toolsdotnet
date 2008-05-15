using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Tools.Common.Asserts;

namespace Tools.Common.Cache
{
    public class CacheWebProvider: ICacheProvider
    {

        public object GetItem(string name)
        {
            ErrorTrap.AddRaisableAssertion<InvalidOperationException>(HttpContext.Current != null, CommonCacheResource.ItSNotPossibleToUseACacheWebProviderInANonWebEnvironment);
            //if (HttpContext.Current == null)
            //    throw new Exception("It's not possible to use a CacheWebProvider in a non web environment");


            return HttpContext.Current.Session[name];
        }

        public void SetItem(string name, object value)
        {
            ErrorTrap.AddRaisableAssertion<InvalidOperationException>(HttpContext.Current != null, CommonCacheResource.ItSNotPossibleToUseACacheWebProviderInANonWebEnvironment);
            //if (HttpContext.Current == null)
            //    throw new Exception("It's not possible to use a CacheWebProvider in a non web environment");
            HttpContext.Current.Session[name] = value;
        }

    }
}
