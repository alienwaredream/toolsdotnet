using System;

namespace Tools.Core
{

    #region Interface IEnabled

    public interface IEnabled
    {
        bool Enabled { get; set; }
        event EventHandler EnabledChanged;
    }

    #endregion
}