using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Common
{
    public interface IGenericValueProvider<TValue> 
    {
        TValue Value { get; }
    }
}
