using System.ServiceModel;

namespace Tools.Wcf.Host
{
    /// <summary>
    /// A contract for obtaining a status.
    /// </summary>
    [ServiceContract(Namespace = "http://Dsi.Tools.Common.servicehost.wcf/test")]
    public interface IStatusQuerable
    {
        /// <summary>
        /// Queries for status. Represented by a method instead of a property so it is
        /// easier callable.
        /// </summary>
        /// <returns>A string with a status information.</returns>
        [OperationContract]
        string QueryForStatus();
    }
}