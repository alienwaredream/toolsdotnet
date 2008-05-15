using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Common.Asserts;

namespace Tools.Common
{
    public class GenericValueProvider<TValue> : IGenericValueProvider<TValue>
    {
        private Func<TValue> valueMethod;

        #region IGenericValueProvider<TValue> Members

        public TValue Value
        {
            get 
            {
                return valueMethod();
            }
        }

        #endregion

        #region Ctors

        public GenericValueProvider(Func<TValue> valueMethod)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentNullException>(valueMethod != null, "valueMethod!=null");
            this.valueMethod = valueMethod;
        }


        #endregion
    }
}
