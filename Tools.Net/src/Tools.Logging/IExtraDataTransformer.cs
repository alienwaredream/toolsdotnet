using System.Collections.Generic;

namespace Tools.Logging
{
    public interface IExtraDataTransformer
    {
        Dictionary<string, object> TransformToDictionary(object extraDataContainer);
    }
}