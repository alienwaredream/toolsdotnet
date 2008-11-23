using System;

namespace Tools.Core
{
    /// <summary>
    /// Summary description for IChanged.
    /// </summary>
    public interface IChangeEventRaiser
    {
        event EventHandler Changed;
    }
}