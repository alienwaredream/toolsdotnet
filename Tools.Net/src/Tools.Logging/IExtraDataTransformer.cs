using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Logging
{
    public interface IExtraDataTransformer
    {
        Dictionary<string, object> TransformToDictionary(object extraDataContainer);
    }
}
