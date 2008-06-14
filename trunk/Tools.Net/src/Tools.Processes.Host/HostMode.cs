using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Processes.Host
{
    /// <summary>
    /// Mode the host is supposed to run in.
    /// </summary>
    public enum HostMode
    {
        WindowsService = 0,
        WindowsApplication = 1,
        WindowsConsole = 2
    }
}
