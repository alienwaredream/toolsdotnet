using System;
using Tools.Core.Context;

namespace Tools.Coordination.Sample.Implementation
{
    [Serializable]
    public class Job : IContextIdentifierHolder
    {
        #region Ctors

        #endregion

        #region IContextIdentifierHolder Members

        public ContextIdentifier ContextIdentifier { get; set; }

        #endregion
    }
}