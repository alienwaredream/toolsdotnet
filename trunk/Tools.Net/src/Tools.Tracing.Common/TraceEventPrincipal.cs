using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for TraceEventPrincipal.
    /// </summary>
    [Serializable]
    public class TraceEventPrincipal
    {
        public TraceEventPrincipal()
        {
        }

        public TraceEventPrincipal(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Principal name.
        /// </summary>
        public string Name { get; set; }
    }
}